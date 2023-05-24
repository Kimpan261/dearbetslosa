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

}
