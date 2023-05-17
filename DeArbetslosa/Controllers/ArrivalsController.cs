using DeArbetslosa.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace DeArbetslosa.Controllers
{
	public class ArrivalsController : Controller
	{
		private readonly ILogger<ArrivalsController> _logger;

		public ArrivalsController(ILogger<ArrivalsController> logger)
		{
			_logger = logger;
		}

		public async Task <IActionResult> Index()
		{

			return View(JsonConvert.DeserializeObject<Arrivals>(await MakeRequest(DateTime.Now)));
		}

		public async Task<IActionResult> SetDate(string date)
		{
			//defaults to today
			if (string.IsNullOrEmpty(date)) return View("Index", JsonConvert.DeserializeObject<Arrivals>(await MakeRequest(DateTime.Now))); 
            return View("Index", JsonConvert.DeserializeObject<Arrivals>(await MakeRequest(DateTime.Parse(date)))); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<string> MakeRequest(DateTime d)
		{
			string t = d.ToString("yyyy-MM-dd");
			string IATA = "GOT";

			var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
			client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b137901e98094345a598042dbdc5827d"); //TODO external key

			var uri = "https://api.swedavia.se/flightinfo/v2/" + IATA + "/arrivals/" + t;

			var response = await client.GetAsync(uri);
			var responseContent = await response.Content.ReadAsStringAsync();
			return responseContent;
		}
	}
}