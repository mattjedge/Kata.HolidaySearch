using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
    public interface IFilterStrategy<T>
    {
        bool IsMatch(T item);
    }

    // Flights filter on
    // DepartureLocation, TravelLocation, DepartureDate
    // Hotels filter on
    // TravelLocation (but differs to Flights as can have multiple local airports), DepartureDate, Duration
    
}
