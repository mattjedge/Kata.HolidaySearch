using HolidaySearch.Models;

namespace HolidaySearch
{
    public interface IFilterStrategy
    {
        bool IsMatch(FlightData flight);
    }

    public class DepartureFilterStrategy : IFilterStrategy
    {
        private readonly IEnumerable<string> _departureLocations;

        public DepartureFilterStrategy(IEnumerable<string> departureLocations)
        {
            _departureLocations = departureLocations;
        }

        public bool IsMatch(FlightData flight)
        {
            return _departureLocations.Contains(flight.From);
        }
    }

    public class DestinationFilterStrategy : IFilterStrategy
    {
        private readonly IEnumerable<string> _destinations;

        public DestinationFilterStrategy(IEnumerable<string> destinations)
        {
            _destinations = destinations;
        }

        public bool IsMatch(FlightData flight)
        {
            return _destinations.Contains(flight.To);
        }
    }

    public record FlightSearchRequest(IEnumerable<IFilterStrategy> SearchFilters);

    public class FlightSearch(IEnumerable<FlightData> flights)
    {
        public IEnumerable<FlightData> SearchFlights(FlightSearchRequest request)
        {
            var query = flights;
            foreach (var filter in request.SearchFilters)
            {
                query = query.Where(filter.IsMatch);
            }

            return query.OrderBy(x => x.Price);
        }
    }
}
