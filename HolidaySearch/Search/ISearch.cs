using HolidaySearch.FilterStrategies;

namespace HolidaySearch.Search
{
    public interface ISearch<T>
    {
        public IEnumerable<T> Search(IEnumerable<IFilterStrategy<T>> searchFilters);
    }
}
