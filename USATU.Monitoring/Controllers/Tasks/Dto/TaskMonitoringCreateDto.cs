using USATU.Monitoring.Core.Domains.Tasks.Enums;

namespace USATU.Monitoring.Web.Controllers.Tasks.Dto
{
    public class TaskMonitoringCreateDto
    {
        public TaskType Type { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
    }
}