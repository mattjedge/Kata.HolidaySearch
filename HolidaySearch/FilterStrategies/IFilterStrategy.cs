using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
    public interface IFilterStrategy
    {
        bool IsMatch(FlightData flight);
    }
}
