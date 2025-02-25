using System.Text.Json.Serialization;

namespace HolidaySearch.Models
{
    public record FlightData
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Price { get; set; }
        [JsonPropertyName("departure_date")]
        public DateOnly DepartureDate { get; set; }
    }
}
