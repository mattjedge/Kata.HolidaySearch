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
        public DateOnly Departure_Date { get; set; }
    }
    public record HotelData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("arrival_date")]
        public DateOnly ArrivalDate { get; set; }
        [JsonPropertyName("price_per_night")]
        public int PricePerNight { get; set; }
        [JsonPropertyName("local_airports")]
        public IList<string> LocalAirports { get; set; }
        [JsonPropertyName("nights")]
        public int NumberOfNights { get; set; }
    }
}
