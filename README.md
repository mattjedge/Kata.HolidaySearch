# Kata.HolidaySearch
Interview kata (Feb 2025)


## Holiday Search
Taking the two JSON files of flights and hotels as source data, please create a
small library of code that provides a basic holiday search feature.
The holiday search results should be ordered such that the first result should
be the best value holiday we can provide, based on the customers
requirements.

Use the test cases listed below to verify the success of your work, add more
tests as you see fit.

Here is an example of how the finished library could work, you're welcome to
put your own spin on it.

```
var holidaySearch = new HolidaySearch({
DepartingFrom: 'MAN',
TravelingTo: 'AGP',
DepartureDate: '2023/07/01'
Duration: 7
});
holidaySearch.Results.First() # Returns the Best of the matching results
holidaySearch.Results.First().TotalPrice # '£900.00'
holidaySearch.Results.First().Flight.Id # 4
holidaySearch.Results.First().Flight.DepartingFrom # 'MAN'
holidaySearch.Results.First().Flight.TravalingTo # 'AGP'
holidaySearch.Results.First().Flight.Price
holidaySearch.Results.First().Hotel.Id # 3
holidaySearch.Results.First().Hotel.Name
holidaySearch.Results.First().Hotel.Price
```


### Test cases
Here are some example test cases
#### Customer #1
##### Input
* Departing from: Manchester Airport (MAN)
* Traveling to: Malaga Airport (AGP)
* Departure Date: 2023/07/01
* Duration: 7 nights
##### Expected result
* Flight 2 and Hotel 9
#### Customer #2
##### Input

* Departing from: Any London Airport
* Traveling to: Mallorca Airport (PMI)
* Departure Date: 2023/06/15
* Duration: 10 nights
##### Expected result
* Flight 6 and Hotel 5
### Customer #3
##### Input
* Departing from: Any Airport
* Traveling to: Gran Canaria Airport (LPA)
* Departure Date: 2022/11/10
* Duration: 14 nights
##### Expected result
* Flight 7 and Hotel 6
