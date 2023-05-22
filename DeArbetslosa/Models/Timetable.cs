namespace DeArbetslosa.Models
{
    public class Timetable
    {
        public string NumberOfFlights { get; set; }
        public F[] Flights { get; set; }

        public class F // Flights
        {
            //When making an api to fill data to a Timetable, will only fill arrival/departure relevant
            
            //Time
            public AT ArrivalTime;
            public AT DepartureTime;
            public class AT //ArrivalTime
            {
                public DateTime ScheduledUtc;
                public DateTime EstimatedUtc;

            }
            public class DT //DepartureTime
            {
                public DateTime ScheduledUtc;
                public DateTime EstimatedUtc;
            }

            //Airport
            public string ArrivalAirportEnglish;
            public string DepartureAirportEnglish;

            //ID
            public string FlightId;

            //Status
            public LAS LocationAndStatus;
            public class LAS //LocationAndStatus
            {
                public string FlightLegStatusEnglish;
            }

        }
        public Timetable()
        {

        }
    }
}
