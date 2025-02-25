using HolidaySearch.FilterStrategies;
using HolidaySearch.Models;
using System.Text.Json;

namespace HolidaySearch.Tests
{
    internal class FlightSearchTests
    {
        private IEnumerable<FlightData> _flightData;

        [SetUp]
        public void Setup()
        {
            _flightData = SeedFlightData();
        }        

        [Test]
        public void FlightData_maps_to_model()
        {
            Assert.That(_flightData.First().Id, Is.EqualTo(1));
            Assert.That(_flightData.First().Airline, Is.EqualTo("First Class Air"));
            Assert.That(_flightData.First().From, Is.EqualTo("MAN"));
            Assert.That(_flightData.First().To, Is.EqualTo("TFS"));
            Assert.That(_flightData.First().Price, Is.EqualTo(470));
            Assert.That(_flightData.First().DepartureDate, Is.EqualTo(new DateOnly(2023, 07, 01)));
        }

        [Test]
        public void FlightSearch_only_returns_flights_departing_from_request_destination()
        {
            var flightSearch = new FlightSearch(_flightData);
            var searchFilters = new List<IFilterStrategy>
            {
                new DepartureLocationFilterStrategy(["MAN"])
            };

            var searchRequest = new FlightSearchRequest(searchFilters);

            var result = flightSearch.SearchFlights(searchRequest);

            Assert.That(result.All(x => x.From == "MAN"));
        }

        [Test]
        public void Multiple_departure_locations_is_supported()
        {
            var flightSearch = new FlightSearch(_flightData);
            var searchFilters = new List<IFilterStrategy>
            {
                new DepartureLocationFilterStrategy(["MAN", "LTN"])
            };
            var searchRequest = new FlightSearchRequest(searchFilters);

            var result = flightSearch.SearchFlights(searchRequest);

            Assert.That(result.All(x => x.From == "MAN" || x.From == "LTN"));
        }

        [Test]
        // Requirement validation needed - what constitutes best value? Cheapest?
        public void FlightSearch_returns_best_value_offer_as_first_result()
        {
            var flightSearch = new FlightSearch(_flightData);
            var searchFilters = new List<IFilterStrategy>
            {
                new DepartureLocationFilterStrategy(["MAN"])
            };

            var searchRequest = new FlightSearchRequest(searchFilters);

            var result = flightSearch.SearchFlights(searchRequest);

            var idOfCheapestManchesterDeparture = 7;
            Assert.That(result.First().Id, Is.EqualTo(idOfCheapestManchesterDeparture));
        }

        [Test]
        public void Filter_search_on_travel_destination()
        {
            var flightSearch = new FlightSearch(_flightData);
            var searchFilters = new List<IFilterStrategy>
            {
                new DepartureLocationFilterStrategy(["MAN"]),
                new DestinationFilterStrategy(["AGP"])
            };

            var searchRequest = new FlightSearchRequest(searchFilters);

            var result = flightSearch.SearchFlights(searchRequest);

            Assert.That(result.All(x => x.To == "AGP"));
        }

        [Test]
        public void Filter_search_on_departure_date()
        {
            var flightSearch = new FlightSearch(_flightData);
            var departureDate = new DateOnly(2023, 07, 01);
            var searchFilters = new List<IFilterStrategy>
            {
                new DepartureDateFilterStrategy(departureDate),
            };

            var searchRequest = new FlightSearchRequest(searchFilters);

            var result = flightSearch.SearchFlights(searchRequest);

            Assert.That(result.All(x => x.DepartureDate == departureDate));
        }

        private static IEnumerable<FlightData> SeedFlightData()
        {
            var flightDataString = File.ReadAllText(@"Data\flight-data.json");
            return JsonSerializer.Deserialize<IEnumerable<FlightData>>(flightDataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}