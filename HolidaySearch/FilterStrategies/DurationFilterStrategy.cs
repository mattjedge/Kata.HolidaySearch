using HolidaySearch.Models;

namespace HolidaySearch.FilterStrategies
{
    public class DurationFilterStrategy : IFilterStrategy<HotelData>
    {
        private readonly int _holidayDuration;

        public DurationFilterStrategy(int holidayDuration)
        {
            _holidayDuration = holidayDuration;
        }

        public bool IsMatch(HotelData hotel)
        {
            return hotel.NumberOfNights == _holidayDuration;
        }
    }
}
