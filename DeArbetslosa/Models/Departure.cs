namespace DeArbetslosa.Models
{
    public class Departure
    {
        public From from { get; set; }
        public int numberOfFlights { get; set; }
        public Flight[] flights { get; set; }
    }

    public class From
    {
        public string departureAirportIata { get; set; }
        public string departureAirportIcao { get; set; }
        public string departureAirportSwedish { get; set; }
        public string departureAirportEnglish { get; set; }
        public string flightDepartureDate { get; set; }
    }

    public class Flight
    {
        public string flightId { get; set; }
        public string arrivalAirportSwedish { get; set; }
        public string arrivalAirportEnglish { get; set; }
        public Airlineoperator airlineOperator { get; set; }
        public Departuretime departureTime { get; set; }
        public Locationandstatus locationAndStatus { get; set; }
        public Checkin checkIn { get; set; }
        public object[] codeShareData { get; set; }
        public Flightlegidentifier flightLegIdentifier { get; set; }
        public object[] viaDestinations { get; set; }
        public object[] remarksEnglish { get; set; }
        public object[] remarksSwedish { get; set; }
        public string diIndicator { get; set; }
    }

    public class Airlineoperator
    {
        public string iata { get; set; }
        public string icao { get; set; }
        public string name { get; set; }
    }

    public class Departuretime
    {
        public DateTime scheduledUtc { get; set; }
    }

    public class Locationandstatus
    {
        public string terminal { get; set; }
        public string flightLegStatus { get; set; }
        public string flightLegStatusSwedish { get; set; }
        public string flightLegStatusEnglish { get; set; }
    }

    public class Checkin
    {
    }

    public class Flightlegidentifier
    {
        public string callsign { get; set; }
        public string flightId { get; set; }
        public DateTime flightDepartureDateUtc { get; set; }
        public string departureAirportIata { get; set; }
        public string arrivalAirportIata { get; set; }
        public string departureAirportIcao { get; set; }
        public string arrivalAirportIcao { get; set; }
    }

}
