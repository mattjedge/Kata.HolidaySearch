namespace HolidaySearch;

public record HolidaySearchQuery(string[] DepartingFrom, string[] TravelingTo, DateOnly DepartureDate, int Duration);