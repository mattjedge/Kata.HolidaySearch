using HolidaySearch.FilterStrategies;
using HolidaySearch.Models;

namespace HolidaySearch
{
    public record FlightSearchRequest(IEnumerable<IFilterStrategy<FlightData>> SearchFilters);
}
