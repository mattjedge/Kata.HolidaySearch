﻿using HolidaySearch.Models;

namespace HolidaySearch
{
    public interface IPriceCalculator
    {
        public double GetTotalPrice(HotelData hotel, FlightData flight);
    }
}
