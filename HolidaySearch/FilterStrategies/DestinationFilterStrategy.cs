using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
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
}
