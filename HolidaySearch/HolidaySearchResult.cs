using HolidaySearch.Models;

namespace HolidaySearch;

public record HolidaySearchResult(FlightData Flight, HotelData Hotel, double TotalPrice);