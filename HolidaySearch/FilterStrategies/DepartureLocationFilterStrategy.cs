using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
    public class DepartureLocationFilterStrategy : IFlightFilterStrategy
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
