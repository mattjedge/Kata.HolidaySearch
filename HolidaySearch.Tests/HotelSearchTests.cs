using HolidaySearch.FilterStrategies;
using HolidaySearch.Models;
using HolidaySearch.Search;
using System.Text.Json;

namespace HolidaySearch.Tests
{
    internal class HotelSearchTests
    {
        private IEnumerable<HotelData> _hotelData;
        private HotelSearch _subject;
        private List<IFilterStrategy<HotelData>> _searchFilters;

        [SetUp]
        public void Setup()
        {
            _hotelData = SeedHotelData();
            _subject = new HotelSearch(_hotelData);
            _searchFilters = [];
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
            _searchFilters.Add(new DestinationFilterStrategy<HotelData>(["TFS"], hotel => hotel.LocalAirports));

            var results = _subject.Search(_searchFilters);
           
            Assert.That(results.All(x => x.LocalAirports.Contains("TFS")));
        }

        [Test]
        public void Filters_on_departure_date()
        {
            var departureDate = new DateOnly(2023, 07, 01);
            _searchFilters.Add(new DatesFilterStrategy<HotelData>(departureDate, hotel => hotel.ArrivalDate));

            var results = _subject.Search(_searchFilters);

            Assert.That(results.All(x => x.ArrivalDate == departureDate));
        }

        [Test]
        public void Filters_on_holiday_duration()
        {
            var numberOfNights = 7;
            _searchFilters.Add(new DurationFilterStrategy(numberOfNights));

            var results = _subject.Search(_searchFilters);

            Assert.That(results.All(x => x.NumberOfNights == numberOfNights));
        }

        [Test]
        public void Returns_best_value_result_for_customer_one()
        {
            _searchFilters.Add(new DurationFilterStrategy(7));
            _searchFilters.Add(new DestinationFilterStrategy<HotelData>(["AGP"], hotel => hotel.LocalAirports));
            _searchFilters.Add(new DatesFilterStrategy<HotelData>(new DateOnly(2023, 07, 01), hotel => hotel.ArrivalDate));

            var results = _subject.Search(_searchFilters);

            var expectedHotelId = 9;
            Assert.That(results.First().Id, Is.EqualTo(expectedHotelId));
        }

        [Test]
        public void Returns_best_value_result_for_customer_two()
        {
            _searchFilters.Add(new DurationFilterStrategy(10));
            _searchFilters.Add(new DestinationFilterStrategy<HotelData>(["PMI"], hotel => hotel.LocalAirports));
            _searchFilters.Add(new DatesFilterStrategy<HotelData>(new DateOnly(2023, 06, 15), hotel => hotel.ArrivalDate));

            var results = _subject.Search(_searchFilters);

            var expectedHotelId = 5;
            Assert.That(results.First().Id, Is.EqualTo(expectedHotelId));
        }

        [Test]
        public void Returns_best_value_result_for_customer_three()
        {
            _searchFilters.Add(new DurationFilterStrategy(14));
            _searchFilters.Add(new DestinationFilterStrategy<HotelData>(["LPA"], hotel => hotel.LocalAirports));
            _searchFilters.Add(new DatesFilterStrategy<HotelData>(new DateOnly(2022, 11, 10), hotel => hotel.ArrivalDate));

            var results = _subject.Search(_searchFilters);

            var expectedHotelId = 6;
            Assert.That(results.First().Id, Is.EqualTo(expectedHotelId));
        }

        private static IEnumerable<HotelData> SeedHotelData()
        {
            var hotelDataString = File.ReadAllText(@"Data\hotel-data.json");
            return JsonSerializer.Deserialize<IEnumerable<HotelData>>(hotelDataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
