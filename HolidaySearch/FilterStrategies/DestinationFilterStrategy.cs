namespace HolidaySearch.FilterStrategies
{
    public class DestinationFilterStrategy<T> : IFilterStrategy<T>
    {
        private readonly IEnumerable<string> _destinations;
        private readonly Func<T, IEnumerable<string>> _itemDestinations;

        public DestinationFilterStrategy(IEnumerable<string> destinations, Func<T, IEnumerable<string>> itemDestinations)
        {
            _destinations = destinations;
            _itemDestinations = itemDestinations;
        }

        public bool IsMatch(T item)
        {
            return _itemDestinations(item).Any(destination => _destinations.Contains(destination));
        }
    }
}
