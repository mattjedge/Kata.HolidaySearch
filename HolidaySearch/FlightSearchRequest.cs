using HolidaySearch.FilterStrategies;

namespace HolidaySearch
{
    public record FlightSearchRequest(IEnumerable<IFilterStrategy> SearchFilters);
}
