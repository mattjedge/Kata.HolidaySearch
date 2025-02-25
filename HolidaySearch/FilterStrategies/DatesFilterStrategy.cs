using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
    public class DatesFilterStrategy<T> : IFilterStrategy<T>
    {
        private readonly DateOnly _targetDate;
        private readonly Func<T, DateOnly> _itemDate;

        public DatesFilterStrategy(DateOnly targetDate, Func<T, DateOnly> itemDate)
        {
            _targetDate = targetDate;
            _itemDate = itemDate;
            // option to include +/- 3 day filtering here
        }

        public bool IsMatch(T item)
        {
            return _itemDate(item) == _targetDate;
        }
    }
}
