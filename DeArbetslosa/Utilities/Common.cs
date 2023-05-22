using DeArbetslosa.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DeArbetslosa.Utilities
{
    public class Common
    {

	public async Task<Timetable> MakeRequest(DateTime d, int type=-1) //-1 departure, 1 arrival
		{
			string direction = type == -1 ? "departures" : "arrivals";
			string t = d.ToString("yyyy-MM-dd");
			string IATA = "GOT"; //TODO hardcoded?

			var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
			client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b137901e98094345a598042dbdc5827d"); //TODO external key

			var uri = "https://api.swedavia.se/flightinfo/v2/" + IATA + "/"+  direction + "/" + t; 
			var response = await client.GetAsync(uri);
			var responseContent = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<Timetable>(responseContent);
		}

    }
}
