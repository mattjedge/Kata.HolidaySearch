using HolidaySearch.Models;

namespace HolidaySearch
{
    public class PriceCalculator : IPriceCalculator
    {
        public double GetTotalPrice(HotelData hotel, FlightData flight) => (hotel.PricePerNight * hotel.NumberOfNights) + flight.Price;
    }
}

