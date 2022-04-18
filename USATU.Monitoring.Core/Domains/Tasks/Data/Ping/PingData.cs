namespace USATU.Monitoring.Core.Domains.Tasks.Data.Ping
{
    public class PingData
    {
        public string URL { get; set; }
        public int Timeout { get; set; }
        public int Iteration { get; set; }
    }
}