using DeArbetslosa.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace DeArbetslosa.Controllers
{
    public class DeparturesController : Controller
    {

        private readonly ILogger<DeparturesController> _logger;

        public DeparturesController(ILogger<DeparturesController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string today = (DateTime.Now).ToString("yyyy-MM-dd");
            string airportIATA = "GOT";

            var listFlightInfo = GetDepartureFlightInfo(today, airportIATA);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // *********************************************************************************************************
        // private Functions 
        private static async Task<List<Flight>> GetDepartureFlightInfo(string date, string IATA)
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
            var result = listFlightInfo.Where(f => f.locationAndStatus.flightLegStatus != "DEL").ToList();


            return result;

        }

    }
}