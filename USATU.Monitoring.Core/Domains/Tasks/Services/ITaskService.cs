using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using USATU.Monitoring.Core.Domains.Tasks.Data.Ping;
using USATU.Monitoring.Core.Domains.Users;

namespace USATU.Monitoring.Core.Domains.Tasks.Services
{
    public interface ITaskService
    {
        public Task<List<PingReply>> PingURL(PingData data);
        public Task<bool> ValidationSSL(string url);
        public Task<TaskMonitoring> GetTask(string id);
        public Task CreateTask(TaskMonitoring taskMonitoring);
        public Task<List<TaskMonitoring>> GetAllTasks();
        public Task UpdateTask(TaskMonitoring taskMonitoring);
        public Task DeleteTask(string id);
    }
}