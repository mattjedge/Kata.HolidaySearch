using HolidaySearch.Models;

namespace HolidaySearch
{
    public class FlightSearch(IEnumerable<FlightData> flights)
    {
        public IEnumerable<FlightData> SearchFlights(FlightSearchRequest request)
        {
            var query = flights;
            foreach (var filter in request.SearchFilters)
            {
                query = query.Where(filter.IsMatch);
            }

            return query.OrderBy(x => x.Price);
        }
    }
}
