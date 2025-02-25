using System.Text.Json.Serialization;

namespace HolidaySearch.Models
{
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
