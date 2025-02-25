using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
    public class DestinationFilterStrategy : IFlightFilterStrategy
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

    public class HotelDestinationFilterStrategy : IHotelFilterStrategy
    {
        private readonly IEnumerable<string> _destinations;

        public HotelDestinationFilterStrategy(IEnumerable<string> destinations)
        {
            _destinations = destinations;
        }

        public bool IsMatch(HotelData hotel)
        {
            return hotel.LocalAirports.Any(airport => _destinations.Contains(airport));
        }
    }
}
