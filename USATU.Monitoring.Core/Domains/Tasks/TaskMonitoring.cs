using USATU.Monitoring.Core.Domains.Tasks.Enums;

namespace USATU.Monitoring.Core.Domains.Tasks
{
    public class TaskMonitoring
    {
        public string Id { get; set; }
        public TaskType Type { get; set; }
        public TaskStatus Status { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public string Result { get; set; }
    }
}