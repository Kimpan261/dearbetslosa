namespace DeArbetslosa.Models
{
    //TODO remove unused properties? quicker runtime?
    public class Timetable
    {
        public string NumberOfFlights { get; set; }
        public List<Flight> Flights { get; set; }
        public To to { get; set; }

        public class To
        {
            public string arrivalAirportIata { get; set; }
            public string arrivalAirportEnglish { get; set; }
            public string flightArrivalDate { get; set; }   
        }
         public class From
        {
            public string departureAirportIata { get; set; }
            public string departureAirportEnglish { get; set; }
            public string flightDepartureDate { get; set; }   
        }

        public class Flight
        {
            public string flightId { get; set; }
            public string arrivalAirportSwedish { get; set; }
            public string arrivalAirportEnglish { get; set; }
            public string departureAirportSwedish { get; set; }
            public string departureAirportEnglish { get; set; }
 
            public AirlineOperator airlineOperator { get; set; }
            public DepartureTime departureTime { get; set; }
            public ArrivalTime arrivalTime { get; set; }
            public LocationAndStatus locationAndStatus { get; set; }
            public CheckIn checkIn { get; set; }
            public object[] codeShareData { get; set; }
            public FlightLegIdentifier flightLegIdentifier { get; set; }
            public object[] viaDestinations { get; set; }
            public object[] remarksEnglish { get; set; }
            public object[] remarksSwedish { get; set; }
            public string diIndicator { get; set; }
        }

        public class AirlineOperator
        {
            public string iata { get; set; }
            public string icao { get; set; }
            public string name { get; set; }
        }

        public class DepartureTime
        {
            public DateTime scheduledUtc { get; set; }
        }
        public class ArrivalTime
        {
            public DateTime scheduledUtc { get; set; }
        }


        public class LocationAndStatus
        {
            public string terminal { get; set; }
            public string flightLegStatus { get; set; }
            public string flightLegStatusSwedish { get; set; }
            public string flightLegStatusEnglish { get; set; }
        }

        public class CheckIn
        {
        }

        public class FlightLegIdentifier
        {
            public string callsign { get; set; }
            public string flightId { get; set; }
            public DateTime flightDepartureDateUtc { get; set; }
            public string departureAirportIata { get; set; }
            public string arrivalAirportIata { get; set; }
            public string departureAirportIcao { get; set; }
            public string arrivalAirportIcao { get; set; }
        }


        public Timetable()
        {

        }
    }

 }
