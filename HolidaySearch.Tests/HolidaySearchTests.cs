using HolidaySearch.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace HolidaySearch.Tests
{
    internal class HolidaySearchTests
    {
        private IEnumerable<FlightData> _flightData;
        private IEnumerable<HotelData> _hotelData;

        [SetUp]
        public void Setup()
        {
            _flightData = SeedFlightData();
            _hotelData = SeedHotelData();
        }        

        [Test]
        public void FlightData_maps_to_model()
        {
            Assert.That(_flightData.First().Id, Is.EqualTo(1));
            Assert.That(_flightData.First().Airline, Is.EqualTo("First Class Air"));
            Assert.That(_flightData.First().From, Is.EqualTo("MAN"));
            Assert.That(_flightData.First().To, Is.EqualTo("TFS"));
            Assert.That(_flightData.First().Price, Is.EqualTo(470));
            Assert.That(_flightData.First().Departure_Date, Is.EqualTo(new DateOnly(2023, 07, 01)));
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
        public async Task FlightSearch_only_returns_flights_departing_from_request_destination()
        {
            var flightSearch = new FlightSearch(_flightData);
            var searchRequest = new FlightSearchRequest("MAN");

            var result = flightSearch.SearchFlights(searchRequest);

            Assert.That(result.All(x => x.From == "MAN"));
        }

        private static IEnumerable<HotelData> SeedHotelData()
        {
            var hotelDataString = File.ReadAllText(@"Data\hotel-data.json");
            return JsonSerializer.Deserialize<IEnumerable<HotelData>>(hotelDataString, new JsonSerializerOptions {  PropertyNameCaseInsensitive = true});
        }

        private static IEnumerable<FlightData> SeedFlightData()
        {
            var flightDataString = File.ReadAllText(@"Data\flight-data.json");
            return JsonSerializer.Deserialize<IEnumerable<FlightData>>(flightDataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}