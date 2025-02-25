using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
    public class DepartureDateFilterStrategy : IFlightFilterStrategy
    {
        private readonly DateOnly _departureDate;

        public DepartureDateFilterStrategy(DateOnly departureDate)
        {
            _departureDate = departureDate;
            // option to include +/- 3 day filtering here
        }

        public bool IsMatch(FlightData flight)
        {
            return flight.DepartureDate == _departureDate;
        }
    }
}
