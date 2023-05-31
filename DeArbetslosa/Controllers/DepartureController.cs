using DeArbetslosa.Models;
using DeArbetslosa.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using static DeArbetslosa.Models.Timetable;
using Flight = DeArbetslosa.Models.Flight;

namespace DeArbetslosa.Controllers
{
    public class DepartureController : Controller
    {
        private static string _airportIATA = "GOT";
        private static string _date = "";
        private static string _time = "";
        private static string _filter = "";


        private readonly ILogger<DepartureController> _logger;

        public DepartureController(ILogger<DepartureController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            ViewData["Date"] = _date = (DateTime.Now).ToString("yyyy-MM-dd");
            ViewData["Time"] = _time = "Tid";
            ViewData["Filter"] = _filter = "";

            ViewData["Flights"] =  await GetAllDepartureFlightInfo(_date, _airportIATA); ;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? date, string? time, string? filter)
        {

            var flights = await GetAllDepartureFlightInfo(_date, _airportIATA);

            // Filter Date
            if (date != null)
            {
                _date = date;
                _time = "Tid";

                if (_filter != "")
                {
                    var result = flights.Where(f => f.arrivalAirportSwedish.ToLower() == _filter.ToLower() || f.flightId == (_filter.ToUpper()));
                    ViewData["Flights"] = result;

                }
                else
                {
                    ViewData["Flights"] = await GetAllDepartureFlightInfo(_date, _airportIATA);
                }
            }

            // Filter Time
            if (time != null)
            {
                _time = time;

                if (time == "Tid")
                {
                    if (_filter != "")
                    {
                        var result = flights.Where(f => f.arrivalAirportSwedish.ToLower() == _filter.ToLower() || f.flightId == (_filter.ToUpper()));
                        ViewData["Flights"] = result;
                    }
                    else
                    {
                        ViewData["Flights"] = flights;
                    }
                }
                else
                {
                    var filterTime = DateTime.Parse($"{_date} {_time}");

                    if (_filter != "")
                    {
                        var result = flights.Where(f => f.departureTime.scheduledUtc > filterTime)
                                            .Where(f => f.arrivalAirportSwedish.ToLower() == _filter.ToLower() || f.flightId == (_filter.ToUpper()));
                        ViewData["Flights"] = result;
                    }
                    else
                    {
                        var result = flights.Where(f => f.departureTime.scheduledUtc > filterTime);
                        ViewData["Flights"] = result;
                    }

                }

            }

            // Filter by flight or destination 
            if (filter != null)
            {
                _time = "Tid";
                _filter = filter;

                var result = flights.Where(f => f.arrivalAirportSwedish.ToLower() == _filter.ToLower() || f.flightId == (_filter.ToUpper()));
                ViewData["Flights"] = result;
            }

            // Filter = Null
            if (date == null && time == null && filter == null)
            {
                _date = (DateTime.Now).ToString("yyyy-MM-dd");
                _time = "tid";
                _filter = "";

                ViewData["Flights"] = await GetAllDepartureFlightInfo(_date, _airportIATA);
            }

            ViewData["Date"] = _date;
            ViewData["Time"] = _time;
            ViewData["Filter"] = _filter;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // *********************************************************************************************************
        // private Functions 
        private static async Task<List<Flight>> GetAllDepartureFlightInfo(string date, string IATA)
        {
            var client = new HttpClient();
            //string today = (DateTime.Now).ToString("yyyy-MM-dd");
            //string airportIATA = "GOT";


            // RequestHeaders
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "776827c8b0fd4515904e6ce6935eda67");

            var uri = $"https://api.swedavia.se/flightinfo/v2/{IATA}/departures/{date}";


            var response = await client.GetAsync(uri);
            string responseBody = await response.Content.ReadAsStringAsync();


            var responseBodyToOpject = JsonConvert.DeserializeObject<Departure>(responseBody);
            List<Flight> listFlightInfo = responseBodyToOpject.flights.ToList();

            // Filter flight Like flightLegStatus IS NOTE "DEL"
            var result = listFlightInfo.Where(f => f.locationAndStatus.flightLegStatus != "DEL")
                                        .OrderBy(f => f.departureTime.scheduledUtc).ToList();

            return result;

        }

    }
}