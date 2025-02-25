using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
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
}
