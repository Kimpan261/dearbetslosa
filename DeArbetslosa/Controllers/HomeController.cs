using DeArbetslosa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Xml;

namespace DeArbetslosa.Controllers
{
	public class HomeController : Controller
	{

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async Task <IActionResult> Index()
		{
			string r = await MakeRequest();
			Arrivals a = new Arrivals();
			a = JsonConvert.DeserializeObject<Arrivals>(r);
            return View(a);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<string> MakeRequest()
		{
			string t = DateTime.Now.ToString("yyyy-MM-dd");
			string IATA = "GOT";

			var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
			client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b137901e98094345a598042dbdc5827d"); //TODO external key

			var uri = "https://api.swedavia.se/flightinfo/v2/" + IATA + "/arrivals/" + t;
            
            var response = await client.GetAsync(uri);
			var responseContent = await response.Content.ReadAsStringAsync();
			return responseContent;
			
			/*
			a = JsonConvert.DeserializeObject<Arrival>(responseContent);	
            await Console.Out.WriteLineAsync(a.NumberOfFlights);
			await Console.Out.WriteLineAsync(responseContent);

            await Console.Out.WriteLineAsync(a.Flights[1].ArrivalTime.ScheduledUtc.ToString());
            await Console.Out.WriteLineAsync(a.Flights[1].DepartureAirportEnglish);
            await Console.Out.WriteLineAsync(a.Flights[1].FlightId);
            await Console.Out.WriteLineAsync(a.Flights[1].FlightLegStatusEnglish);
			*/
        }
	}
}