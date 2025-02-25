using HolidaySearch.FilterStrategies;
using HolidaySearch.Models;

namespace HolidaySearch.Search
{
    public class FlightSearch(IEnumerable<FlightData> flights) : ISearch<FlightData>
    {
        public IEnumerable<FlightData> Search(IEnumerable<IFilterStrategy<FlightData>> searchFilters)
        {
            var query = flights;
            foreach (var filter in searchFilters)
            {
                query = query.Where(filter.IsMatch);
            }

            return query.OrderBy(x => x.Price);
        }
    }
}
