using HolidaySearch.FilterStrategies;
using HolidaySearch.Models;

namespace HolidaySearch
{
    public record HotelSearchRequest(List<IHotelFilterStrategy> SearchFilters);

    public class HotelSearch(IEnumerable<HotelData> hotels)
    {
        public IEnumerable<HotelData> SearchHotels(HotelSearchRequest request)
        {
            var query = hotels;
            foreach (var filter in request.SearchFilters)
            {
                query = query.Where(filter.IsMatch);
            }

            return query;
        }
    }
}
