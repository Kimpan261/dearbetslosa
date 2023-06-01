using System;
using System.Net.Http;
using System.Threading.Tasks;
using DeArbetslosa.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace DeArbetslosa.Controllers
{
    public class SwedaviaController : Controller
    {
        private readonly string apiKey = "c15de30006de4a5dac95375d4d751feb";
        private readonly string apiUri = "https://api.swedavia.se/waittimepublic/v2/airports";
        private readonly HttpClient _httpClient;

        public SwedaviaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // You can further configure the _httpClient if needed
            // For example: _httpClient.BaseAddress = new Uri("https://api.swedavia.se/");
        }

        public async Task<IActionResult> Security()
        {
            try
            {
              
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
                
                var response = await _httpClient.GetAsync(apiUri + "/ARN");
 

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var waitTimeResponse = JsonConvert.DeserializeObject<SecurityWaitTimeResponse>(content);
               
                    // waitTimeResponse is stored in the ViewData["SecurityWaitTime"] dictionary entry
                    // which will be accessible in the view.
                    ViewData["SecurityWaitTime"] = waitTimeResponse;
   
                    return View("~/Views/Home/Security.cshtml", waitTimeResponse);
                }
                else
                {
                    // Handle non-successful API response
                    // For example, return an error view or display an error message
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred
                // For example, log the exception or return an error view
                return View("Error");
            }
        }
    }
}
