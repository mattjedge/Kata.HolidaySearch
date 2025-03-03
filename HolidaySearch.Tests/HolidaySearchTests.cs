﻿using HolidaySearch.Models;
using HolidaySearch.Search;
using System.Text.Json;

namespace HolidaySearch.Tests
{
    internal class HolidaySearchTests
    {
        private IEnumerable<FlightData> _flightData;
        private IEnumerable<HotelData> _hotelData;
        private HolidaySearch _subject;

        [SetUp]
        public void SetUp()
        {
            // real world scenario these would likely be mocked with Moq to test HolidaySearch
            // without exercising other code paths
            _flightData = SeedFlightData();
            var flightSearch = new FlightSearch(_flightData);
            _hotelData = SeedHotelData();
            var hotelSearch = new HotelSearch(_hotelData);

            _subject = new HolidaySearch(hotelSearch, flightSearch, new PriceCalculator());
        }

        [Test]
        public void CustomerOne()
        {
            var query = new HolidaySearchQuery(["MAN"], ["AGP"], new DateOnly(2023, 07, 01), 7);
            var results = _subject.SearchHolidays(query);
            Assert.That(results.First().Flight.Id, Is.EqualTo(2));
            Assert.That(results.First().Hotel.Id, Is.EqualTo(9));
        }

        [Test]
        public void CustomerTwo()
        {
            var query = new HolidaySearchQuery(["LGW", "LTN"], ["PMI"], new DateOnly(2023, 06, 15), 10);
            var results = _subject.SearchHolidays(query);
            Assert.That(results.First().Flight.Id, Is.EqualTo(6));
            Assert.That(results.First().Hotel.Id, Is.EqualTo(5));
        }

        [Test]
        public void CustomerThree()
        {
            var query = new HolidaySearchQuery([], ["LPA"], new DateOnly(2022, 11, 10), 14);
            var results = _subject.SearchHolidays(query);
            Assert.That(results.First().Flight.Id, Is.EqualTo(7));
            Assert.That(results.First().Hotel.Id, Is.EqualTo(6));
        }

        [Test]
        public void Includes_TotalPrice_for_each_package()
        {
            var query = new HolidaySearchQuery([], ["LPA"], new DateOnly(2022, 11, 10), 14);
            var results = _subject.SearchHolidays(query);

            // flight = 125, hotel = 75 per night, 14 nights = 1050, total = 1175
            Assert.That(results.First().TotalPrice, Is.EqualTo(1175));
        }
        
        private static IEnumerable<FlightData> SeedFlightData()
        {
            var flightDataString = File.ReadAllText(@"Data\flight-data.json");
            return JsonSerializer.Deserialize<IEnumerable<FlightData>>(flightDataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private static IEnumerable<HotelData> SeedHotelData()
        {
            var hotelDataString = File.ReadAllText(@"Data\hotel-data.json");
            return JsonSerializer.Deserialize<IEnumerable<HotelData>>(hotelDataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
