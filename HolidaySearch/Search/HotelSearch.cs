using HolidaySearch.FilterStrategies;
using HolidaySearch.Models;

namespace HolidaySearch.Search
{
    public class HotelSearch(IEnumerable<HotelData> hotels) : ISearch<HotelData>
    {
        public IEnumerable<HotelData> Search(IEnumerable<IFilterStrategy<HotelData>> searchFilters)
        {
            var query = hotels;
            foreach (var filter in searchFilters)
            {
                query = query.Where(filter.IsMatch);
            }

            return query.OrderBy(x => x.PricePerNight);
        }
    }
}
