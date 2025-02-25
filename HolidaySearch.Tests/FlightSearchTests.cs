using HolidaySearch.FilterStrategies;
using HolidaySearch.Models;
using System.Text.Json;

namespace HolidaySearch.Tests
{
    internal class FlightSearchTests
    {
        private IEnumerable<FlightData> _flightData;
        private FlightSearch _subject;
        private List<IFilterStrategy<FlightData>> _searchFilters;

        [SetUp]
        public void Setup()
        {
            _flightData = SeedFlightData();
            _subject = new FlightSearch(_flightData);
            _searchFilters = [];
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
            _searchFilters.Add(new DepartureLocationFilterStrategy(["MAN"]));
            var searchRequest = new FlightSearchRequest(_searchFilters);

            var result = _subject.SearchFlights(searchRequest);

            Assert.That(result.All(x => x.From == "MAN"));
        }

        [Test]
        public void Multiple_departure_locations_is_supported()
        {
            _searchFilters.Add(new DepartureLocationFilterStrategy(["MAN", "LTN"]));
            var searchRequest = new FlightSearchRequest(_searchFilters);

            var result = _subject.SearchFlights(searchRequest);

            Assert.That(result.All(x => x.From == "MAN" || x.From == "LTN"));
        }

        [Test]
        public void FlightSearch_returns_best_value_offer_as_first_result()
        {
            _searchFilters.Add(new DepartureLocationFilterStrategy(["MAN"]));
            var searchRequest = new FlightSearchRequest(_searchFilters);

            var result = _subject.SearchFlights(searchRequest);

            var idOfCheapestManchesterDeparture = 7;
            Assert.That(result.First().Id, Is.EqualTo(idOfCheapestManchesterDeparture));
        }

        [Test]
        public void Filter_search_on_travel_destination()
        {
            _searchFilters.Add(new DepartureLocationFilterStrategy(["MAN"]));
            _searchFilters.Add(new DestinationFilterStrategy<FlightData>(["AGP"], flight => [flight.To]));
            var searchRequest = new FlightSearchRequest(_searchFilters);

            var result = _subject.SearchFlights(searchRequest);

            Assert.That(result.All(x => x.To == "AGP"));
        }

        [Test]
        public void Filter_search_on_departure_date()
        {
            var departureDate = new DateOnly(2023, 07, 01);
            _searchFilters.Add(new DatesFilterStrategy<FlightData>(departureDate, flight => flight.DepartureDate));
            var searchRequest = new FlightSearchRequest(_searchFilters);

            var result = _subject.SearchFlights(searchRequest);

            Assert.That(result.All(x => x.DepartureDate == departureDate));
        }

        [Test]
        public void Returns_best_value_result_for_customer_one()
        {
            _searchFilters.Add(new DestinationFilterStrategy<FlightData>(["AGP"], flight => [flight.To]));
            _searchFilters.Add(new DatesFilterStrategy<FlightData>(new DateOnly(2023, 07, 01), flight => flight.DepartureDate));
            _searchFilters.Add(new DepartureLocationFilterStrategy(["MAN"]));

            var results = _subject.SearchFlights(new FlightSearchRequest(_searchFilters));

            var expectedFlightId = 2;
            Assert.That(results.First().Id, Is.EqualTo(expectedFlightId));
        }

        [Test]
        public void Returns_best_value_result_for_customer_two()
        {
            _searchFilters.Add(new DestinationFilterStrategy<FlightData>(["PMI"], flight => [flight.To]));
            _searchFilters.Add(new DatesFilterStrategy<FlightData>(new DateOnly(2023, 06, 15), flight => flight.DepartureDate));
            _searchFilters.Add(new DepartureLocationFilterStrategy(["LTN","LGW"]));

            var results = _subject.SearchFlights(new FlightSearchRequest(_searchFilters));

            var expectedFlightId = 6;
            Assert.That(results.First().Id, Is.EqualTo(expectedFlightId));
        }

        [Test]
        public void Returns_best_value_result_for_customer_three()
        {
            _searchFilters.Add(new DestinationFilterStrategy<FlightData>(["LPA"], flight => [flight.To]));
            _searchFilters.Add(new DatesFilterStrategy<FlightData>(new DateOnly(2022, 11, 10), flight => flight.DepartureDate));

            var results = _subject.SearchFlights(new FlightSearchRequest(_searchFilters));

            var expectedFlightId = 7;
            Assert.That(results.First().Id, Is.EqualTo(expectedFlightId));
        }

        private static IEnumerable<FlightData> SeedFlightData()
        {
            var flightDataString = File.ReadAllText(@"Data\flight-data.json");
            return JsonSerializer.Deserialize<IEnumerable<FlightData>>(flightDataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}