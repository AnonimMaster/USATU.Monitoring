using USATU.Monitoring.Core.Domains.Tasks.Enums;

namespace USATU.Monitoring.Web.Controllers.Tasks.Dto
{
    public class TaskMonitoringUpdateDto
    {
        public TaskStatus Status { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public string Result { get; set; }
    }
}