using System.Collections.Generic;
using System.Threading.Tasks;
using USATU.Monitoring.Core.Domains.Tasks.Enums;

namespace USATU.Monitoring.Core.Domains.Tasks.Repositories
{
    public interface ITaskRepository
    {
        public Task<TaskMonitoring> GetTask(string id);
        public Task<TaskMonitoring> GetFirstTask(USATU.Monitoring.Core.Domains.Tasks.Enums.TaskStatus status);
        public Task CreateTask(TaskMonitoring taskMonitoring);
        public Task<List<TaskMonitoring>> GetAllTasks();
        public Task UpdateTask(TaskMonitoring taskMonitoring);
        public Task DeleteTask(string id);
        public Task<bool> IsTaskExists(string id);
    }
}