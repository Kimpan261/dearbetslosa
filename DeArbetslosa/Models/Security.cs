namespace DeArbetslosa.Models
{
    public class Security
    {
        public string airport { get; set; }
        public string flightId { get; set; }
        public string date { get; set; }
        public int currentProjectWaitTime { get; set; }

        public int Id { get; set; }
        public string QueueName { get; set; }
        public string CurrentTime { get; set; }
        public int CurrentProjectedWaitTime { get; set; }
        public string Terminal { get; set; }
        public bool Overflow { get; set; }
    }
    public class SecurityWaitTimeResponse
    {
        public int ActiveMeasurementStations { get; set; }
        public List<Security> WaitTimes { get; set; } 
    }
}
