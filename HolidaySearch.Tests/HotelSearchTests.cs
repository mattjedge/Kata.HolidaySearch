using HolidaySearch.FilterStrategies;
using HolidaySearch.Models;
using System.Text.Json;

namespace HolidaySearch.Tests
{
    internal class HotelSearchTests
    {
        private IEnumerable<HotelData> _hotelData;

        [SetUp]
        public void Setup()
        {
            _hotelData = SeedHotelData();
        }

        [Test]
        public void HotelData_maps_to_model()
        {
            Assert.That(_hotelData.First().Id, Is.EqualTo(1));
            Assert.That(_hotelData.First().Name, Is.EqualTo("Iberostar Grand Portals Nous"));
            Assert.That(_hotelData.First().ArrivalDate, Is.EqualTo(new DateOnly(2022, 11, 05)));
            Assert.That(_hotelData.First().PricePerNight, Is.EqualTo(100));
            Assert.That(_hotelData.First().LocalAirports.Contains("TFS"));
            Assert.That(_hotelData.First().NumberOfNights, Is.EqualTo(7));
        }

        [Test]
        public void Filters_on_travel_destination()
        {
            var subject = new HotelSearch(_hotelData);
            var destinationFilter = new HotelDestinationFilterStrategy(["TFS"]);
            var searchFilters = new List<IHotelFilterStrategy>
            {
                new HotelDestinationFilterStrategy(["TFS"])
            };
            
            var results = subject.SearchHotels(new HotelSearchRequest(searchFilters));
           
            Assert.That(results.All(x => x.LocalAirports.Contains("TFS")));

        }

        private static IEnumerable<HotelData> SeedHotelData()
        {
            var hotelDataString = File.ReadAllText(@"Data\hotel-data.json");
            return JsonSerializer.Deserialize<IEnumerable<HotelData>>(hotelDataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
