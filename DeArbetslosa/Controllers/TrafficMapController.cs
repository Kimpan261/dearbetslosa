using DeArbetslosa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Xml;

namespace DeArbetslosa.Controllers
{
	public class TrafficMapController : Controller
	{
		public IActionResult GetTrafficInformation()
		{
		//URL Step 3 Controller
							 
            string apiUrl = "https://api.trafikinfo.trafikverket.se/v2/data.xml/";
			// XML request
            string xmlRequest = """
                    <REQUEST>
                          <LOGIN authenticationkey="4cb9b85acc0b4d27973446dafe0633b4" />
                          <QUERY objecttype="Situation" schemaversion="1.2">
                                <FILTER>
                                      <WITHIN name="Deviation.Geometry.SWEREF99TM" shape="center" value="665909 6615094" radius="20000" />
                                </FILTER>																	
                                <INCLUDE>Deviation.Header</INCLUDE>
                                <INCLUDE>Deviation.IconId</INCLUDE>
                                <INCLUDE>Deviation.Message</INCLUDE>
                                <INCLUDE>Deviation.MessageCode</INCLUDE>
                                <INCLUDE>Deviation.MessageType</INCLUDE>
                          </QUERY>
                    </REQUEST>
                    """;


            // Create the web request

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
			request.Method = "POST";
			request.ContentType = "text/xml";

			try
			{
				//Send the Request
				using(StreamWriter writer = new StreamWriter(request.GetRequestStream()))
				{
					writer.Write(xmlRequest);
				}

				// Get Response 
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{

					// Read the response 
					string jsonResponse = reader.ReadToEnd();

					// Parse the json
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.LoadXml(jsonResponse);

					// Process the data 
					List<Situation> situations = new List<Situation>();
					XmlNodeList situationNodes = xmlDoc.SelectNodes("//RESPONSE/RESULT/Situation");
					foreach (XmlNode situationNode in situationNodes) 
					{
						// extract information from the xml node
						XmlNodeList deviationNodes = situationNode.SelectNodes("Deviation");
						foreach (XmlNode deviationNode in deviationNodes)
						{
							Situation situation = new Situation();
							situation.MessageType = deviationNode.SelectSingleNode("MessageType").InnerText;
							//situation.LocationDescriptor = deviationNode.SelectSingleNode("LocationDescriptor").InnerText;

							// Here we can extract further information if needed
							// Add the situation to the list 
							situations.Add(situation);
						}
					}
					return View(situations);
				}
			}
			catch (WebException ex) 
			{
				ViewBag.ErrorMessage = "Request failed: " + ex.Message;
			}
			catch (Exception ex) 
			{
				ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
			}
			return View();
		}
	}
}
