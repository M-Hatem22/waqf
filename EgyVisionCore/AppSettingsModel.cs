namespace EgyVisionCore
{
    public class AppSettingsModel
    {
        public string EgyVisionContext { get; set; }
        public string STSContext { get; set; }
        public string SessionSeconds { get; set; }
        public string SleepMinutes { get; set; }
        public string logCluster { get; set; }
        public string logDb { get; set; }
        public string logServers { get; set; }
        public string EnableLogTransaction { get; set; }
    }
}
