﻿using HolidaySearch.FilterStrategies;
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
            var searchFilters = new List<IFilterStrategy<HotelData>>
            {
                new DestinationFilterStrategy<HotelData>(["TFS"], hotel => hotel.LocalAirports)
            };
            
            var results = subject.SearchHotels(new HotelSearchRequest(searchFilters));
           
            Assert.That(results.All(x => x.LocalAirports.Contains("TFS")));
        }

        [Test]
        public void Filters_on_departure_date()
        {
            var subject = new HotelSearch(_hotelData);
            var departureDate = new DateOnly(2023, 07, 01);
            var searchFilters = new List<IFilterStrategy<HotelData>>
            {
                new DatesFilterStrategy<HotelData>(departureDate, hotel => hotel.ArrivalDate)
            };

            var results = subject.SearchHotels(new HotelSearchRequest(searchFilters));

            Assert.That(results.All(x => x.ArrivalDate == departureDate));
        }

        [Test]
        public void Filters_on_holiday_duration()
        {
            var subject = new HotelSearch(_hotelData);
            var numberOfNights = 7;
            var searchFilters = new List<IFilterStrategy<HotelData>>
            {
                new DurationFilterStrategy(numberOfNights)
            };

            var results = subject.SearchHotels(new HotelSearchRequest(searchFilters));

            Assert.That(results.All(x => x.NumberOfNights == numberOfNights));
        }

        private static IEnumerable<HotelData> SeedHotelData()
        {
            var hotelDataString = File.ReadAllText(@"Data\hotel-data.json");
            return JsonSerializer.Deserialize<IEnumerable<HotelData>>(hotelDataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
