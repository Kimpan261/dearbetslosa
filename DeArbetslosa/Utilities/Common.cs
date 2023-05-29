using DeArbetslosa.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DeArbetslosa.Utilities
{
    public class Common
    {

        public async Task<Timetable> GetTimetable(DateTime d, bool requestDepartures = true)
        {
            string direction = requestDepartures ? "departures" : "arrivals";
            string t = d.ToString("yyyy-MM-dd");
            string IATA = "GOT"; //TODO hardcoded?

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b137901e98094345a598042dbdc5827d"); //TODO external key

            var uri = "https://api.swedavia.se/flightinfo/v2/" + IATA + "/" + direction + "/" + t;
            var response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            Timetable tt = JsonConvert.DeserializeObject<Timetable>(responseContent);
            tt.Flights = tt.Flights.Where(f => f.locationAndStatus.flightLegStatus != "DEL").ToList();
            tt.Flights = tt.Flights.Where(f => f.arrivalTime.scheduledUtc > DateTime.UtcNow).ToList();

            if (requestDepartures) tt.Flights = tt.Flights
                    .OrderBy(f => f.departureTime.scheduledUtc)
                    .ToList();
            else
                tt.Flights = tt.Flights.Where(f => f.locationAndStatus.flightLegStatus != "DEL")
                    .OrderBy(f => f.arrivalTime.scheduledUtc)
                    .ToList();
            return tt;
        }

    }
}
