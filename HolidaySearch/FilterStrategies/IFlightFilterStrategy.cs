namespace HolidaySearch.FilterStrategies
{
    public interface IFilterStrategy<T>
    {
        bool IsMatch(T item);
    }    
}
