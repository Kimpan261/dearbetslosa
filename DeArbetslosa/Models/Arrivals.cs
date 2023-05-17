namespace DeArbetslosa.Models
{
	public class Arrivals
	{
		public string NumberOfFlights { get; set; }
		public F[] Flights { get; set; }
        
        public class F // Flights
		{
			public string DepartureAirportEnglish;
			public string FlightId;
			public string Status;
			public AT ArrivalTime;
			public LAS LocationAndStatus;

			public class LAS //LocationAndStatus
			{
			public string FlightLegStatusEnglish;
			}

			public class AT //ArrivalTime
			{
				public DateTime ScheduledUtc;
				public DateTime EstimatedUtc;
			}
		}
		public Arrivals()
		{
			
		}
	}
}
