using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
    public class DepartureLocationFilterStrategy : IFilterStrategy
    {
        private readonly IEnumerable<string> _departureLocations;

        public DepartureLocationFilterStrategy(IEnumerable<string> departureLocations)
        {
            _departureLocations = departureLocations;
        }

        public bool IsMatch(FlightData flight)
        {
            return _departureLocations.Contains(flight.From);
        }
    }
}
