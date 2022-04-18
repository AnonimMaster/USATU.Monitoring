using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Newtonsoft.Json;
using USATU.Monitoring.Core.Domains.Tasks;
using USATU.Monitoring.Core.Domains.Tasks.Data.Ping;
using USATU.Monitoring.Core.Domains.Tasks.Services;
using USATU.Monitoring.Web.Controllers.Tasks.Dto;
using USATU.Monitoring.Web.Logging;
using TaskStatus = USATU.Monitoring.Core.Domains.Tasks.Enums.TaskStatus;

namespace USATU.Monitoring.Web.Controllers.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ILogger _logger;

        public TasksController(ITaskService taskService, ILogger logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet("PingURL")]
        public async Task<List<PingResultDto>> PingURL(string url, int timeout, int iteration)
        {
            PingData pingData = new PingData()
            {
                URL = url,
                Iteration = iteration,
                Timeout = timeout
            };
            var result = await _taskService.PingURL(pingData);
            List<PingResultDto> pingList = new List<PingResultDto>();
            foreach (PingReply pingReply in result)
            {
                pingList.Add(new PingResultDto
                {
                    Status = $"{pingReply.Status}",
                    Address = $"{pingReply.Address}",
                    RoundtripTime = pingReply.RoundtripTime
                });
            }

            return pingList;
        }

        [HttpGet("ValidationSSL")]
        public async Task<bool> ValidationSSL(string url)
        {
            return await _taskService.ValidationSSL(url);
        }

        [HttpGet("{userId}")]
        public async Task<TaskMonitoring> GetUser(string userId)
        {
            var model = await _taskService.GetTask(userId);

            return new TaskMonitoring()
            {
                Id = model.Id,

            };
        }


        [HttpGet]
        public async Task<List<TaskMonitoringDto>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasks();

            return tasks.Select(i => new TaskMonitoringDto()
            {
                Id = i.Id,
                Data = i.Data,
                Description = i.Description,
                Result = i.Result,
                Status = i.Status,
                Type = i.Type

            }).ToList();
        }

        [HttpPost]
        public Task Create(TaskMonitoringCreateDto model)
        {
            return _taskService.CreateTask(new TaskMonitoring()
            {
                Data = model.Data,
                Description = model.Description,
                Status = TaskStatus.Waiting,
                Type = model.Type
            });
        }

        [HttpPut("{taskId}")]
        public Task Update(string taskId, TaskMonitoringUpdateDto model)
        {
            return _taskService.UpdateTask(new TaskMonitoring()
            {
                Id = taskId,
                Data = model.Data,
                Description = model.Description,
                Result = model.Result,
                Status = model.Status

            });
        }

        [HttpDelete("{taskId}")]
        public Task Delete(string taskId)
        {
            return _taskService.DeleteTask(taskId);
        }

        [HttpGet("CreatePingData")]
        public string CreatePingData(string url, int timeout, int iteration)
        {
            PingData data = new PingData()
            {
                Iteration = iteration,
                URL = url,
                Timeout = timeout
            };

            return JsonConvert.SerializeObject(data);
        }
    }
}