using DeArbetslosa.Models;
using DeArbetslosa.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeArbetslosa.Controllers
{
    public class DeparturesController : Controller
    {
        public Common c = new Common(); //API call here
        private readonly ILogger<DeparturesController> _logger;

        public DeparturesController(ILogger<DeparturesController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await c.MakeRequest(DateTime.Now));
        }

        public async Task<IActionResult> SetDate(string date)
        {
            //defaults to today
            if (string.IsNullOrEmpty(date)) return View("Index", await c.MakeRequest(DateTime.Now));
            return View("Index", await c.MakeRequest(DateTime.Parse(date)));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}