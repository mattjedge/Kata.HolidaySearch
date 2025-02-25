using HolidaySearch.Models;

namespace HolidaySearch
{
    public record FlightSearchRequest(string From);

    public class FlightSearch(IEnumerable<FlightData> flights)
    {
        public IEnumerable<FlightData> SearchFlights(FlightSearchRequest request)
        {
            return flights.Where(x => x.From == request.From);
        }
    }
}
