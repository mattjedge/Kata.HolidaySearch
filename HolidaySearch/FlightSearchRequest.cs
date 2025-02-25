using HolidaySearch.FilterStrategies;

namespace HolidaySearch
{
    public record FlightSearchRequest(IEnumerable<IFlightFilterStrategy> SearchFilters);
}
