using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
    public interface IFilterStrategy<T>
    {
        bool IsMatch(T item);
    }

    public interface IFlightFilterStrategy : IFilterStrategy<FlightData>
    {
    }

    public interface IHotelFilterStrategy : IFilterStrategy<HotelData>
    {
    }
}
