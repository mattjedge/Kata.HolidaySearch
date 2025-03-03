using HolidaySearch.FilterStrategies;
using HolidaySearch.Models;
using HolidaySearch.Search;

namespace HolidaySearch
{
    public record HolidaySearchQuery(string[] DepartingFrom, string[] TravelingTo, DateOnly DepartureDate, int Duration);
    public record HolidaySearchResult(FlightData Flight, HotelData Hotel);

    public class HolidaySearch
    {
        private ISearch<HotelData> _hotelSearch;
        private ISearch<FlightData> _flightSearch;

        public HolidaySearch(ISearch<HotelData> hotelSearch, ISearch<FlightData> flightSearch)
        {
            _hotelSearch = hotelSearch;
            _flightSearch = flightSearch;
        }

        public IEnumerable<HolidaySearchResult> SearchHolidays(HolidaySearchQuery query)
        {
            var hotelFilters = GenerateHotelSearchFilters(query);
            var flightFilters = GenerateFlightSearchFilters(query);

            var hotelResults = _hotelSearch.Search(hotelFilters);
            var flightResults = _flightSearch.Search(flightFilters);

            return CreatePackageHolidaySearchResults(hotelResults, flightResults);
        }

        public IEnumerable<HolidaySearchResult> CreatePackageHolidaySearchResults(IEnumerable<HotelData> hotels, IEnumerable<FlightData> flights)
        {
            return hotels.SelectMany(hotel => flights.Where(flight => hotel.LocalAirports.Contains(flight.To)),
                (hotel, flight) => new HolidaySearchResult(flight, hotel))
                .OrderBy(package => package.Flight.Price + package.Hotel.PricePerNight)
                .ToList();
        }
        
        private IEnumerable<IFilterStrategy<HotelData>> GenerateHotelSearchFilters(HolidaySearchQuery query)
        {
            var filters = new List<IFilterStrategy<HotelData>>
            {
                new DurationFilterStrategy(query.Duration),
                new DatesFilterStrategy<HotelData>(query.DepartureDate, x => x.ArrivalDate)
            };

            if (query.TravelingTo.Any())
            {
                filters.Add(new DestinationFilterStrategy<HotelData>(query.TravelingTo, x => x.LocalAirports));
            }

            return filters;
        }

        private IEnumerable<IFilterStrategy<FlightData>> GenerateFlightSearchFilters(HolidaySearchQuery query)
        {
            var filters = new List<IFilterStrategy<FlightData>>
            {
                new DestinationFilterStrategy<FlightData>(query.TravelingTo, x => [x.To]),
                new DatesFilterStrategy<FlightData>(query.DepartureDate, x => x.DepartureDate)
            };

            if (query.DepartingFrom.Any())
            {
                filters.Add(new DepartureLocationFilterStrategy(query.DepartingFrom));
            }

            return filters;
        }
    }
}
