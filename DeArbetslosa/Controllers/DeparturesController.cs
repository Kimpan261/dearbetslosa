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

		public async Task <IActionResult> Index()
		{
			//TODO make fully functional Arrivals page, then rework it for departures
			return View();
		}

		
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		}
}