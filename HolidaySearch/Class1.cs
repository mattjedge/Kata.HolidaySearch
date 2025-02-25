using HolidaySearch.Models;

namespace HolidaySearch
{
    public record FlightSearchRequest(string DepartingFrom);

    public class FlightSearch(IEnumerable<FlightData> flights)
    {
        public IEnumerable<FlightData> SearchFlights(FlightSearchRequest request)
        {
            return flights.Where(x => x.From == request.DepartingFrom).OrderBy(x => x.Price);
        }
    }
}
