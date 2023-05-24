using DeArbetslosa.Models;
using DeArbetslosa.Utilities;
using Microsoft.AspNetCore.Mvc;
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
            return View(await c.GetTimetable(DateTime.Now, false));
        }
        public async Task<IActionResult> SetDate(string date)
        {
            //defaults to today
            if (string.IsNullOrEmpty(date)) return View("Index", await c.GetTimetable(DateTime.Now, false));            
            return View("Index", await c.GetTimetable(DateTime.Parse(date), false));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}