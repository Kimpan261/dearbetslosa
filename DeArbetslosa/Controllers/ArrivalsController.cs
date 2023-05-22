using DeArbetslosa.Models;
using DeArbetslosa.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            return View(await c.MakeRequest(DateTime.Now, 1));
        }
        public async Task<IActionResult> SetDate(string date)
        {
            //defaults to today
            if (string.IsNullOrEmpty(date)) return View("Index", await c.MakeRequest(DateTime.Now, 1));
            return View("Index", await c.MakeRequest(DateTime.Parse(date), 1));

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}