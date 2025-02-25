using HolidaySearch.Models;

namespace HolidaySearch
{
    public record FlightSearchRequest(IEnumerable<string> DepartingFrom);

    public class FlightSearch(IEnumerable<FlightData> flights)
    {
        public IEnumerable<FlightData> SearchFlights(FlightSearchRequest request)
        {
            return flights
                .Where(x => request.DepartingFrom.Contains(x.From))
                .OrderBy(x => x.Price);
        }
    }
}
