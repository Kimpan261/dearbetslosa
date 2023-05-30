using DeArbetslosa.Models;
using DeArbetslosa.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DeArbetslosa.Controllers
{
    public class ArrivalsController : Controller
    {
        public Common c = new Common(); //API call here
        private readonly ILogger<ArrivalsController> _logger;

        public ArrivalsController(ILogger<ArrivalsController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //Actually never sending the model to the view at all,
            //as it is handled by AJAX. that would be an abundant API call
            return View(); 
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<string> getFlightsJson(string date, string time, string term) // TODO return entire Timetable instead and do a direct to getTimeTable?
        {

            Timetable tt = await c.GetTimetable(DateTime.Parse(date), time, term, false);
            var result = JsonConvert.SerializeObject(tt.Flights);
            return result;
        }
    }
}