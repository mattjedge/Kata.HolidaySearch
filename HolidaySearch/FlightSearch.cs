﻿using HolidaySearch.Models;

namespace HolidaySearch
{
    public record FlightSearchRequest(IEnumerable<string> DepartingFrom, string TravelingTo = null);

    public class FlightSearch(IEnumerable<FlightData> flights)
    {
        public IEnumerable<FlightData> SearchFlights(FlightSearchRequest request)
        {
            return flights
                .Where(x => request.DepartingFrom.Contains(x.From))
                .Where(x => request.TravelingTo == null || x.To == request.TravelingTo)
                .OrderBy(x => x.Price);
        }
    }
}
