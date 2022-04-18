using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using USATU.Monitoring.Core.Domains.Tasks.Data.Ping;
using USATU.Monitoring.Core.Domains.Tasks.Enums;
using USATU.Monitoring.Core.Domains.Tasks.Repositories;
using USATU.Monitoring.Core.Domains.Tasks.Services;
using USATU.Monitoring.Web.Logging;
using Newtonsoft.Json;
using USATU.Monitoring.Core;
using TaskStatus = USATU.Monitoring.Core.Domains.Tasks.Enums.TaskStatus;
using USATU.Monitoring.Data;

namespace USATU.Monitoring.Web.HostedServices.TaskWorker
{
    public class TaskWorkerHostedService: BackgroundService
    {
        private readonly ILogger _logger;
        private readonly BackgroundSettings _settings;
        private readonly IServiceProvider _services;
        private readonly ITaskService _taskService;

        public TaskWorkerHostedService(ILogger logger, BackgroundSettings settings, IServiceProvider services, ITaskService taskService)
        {
            _logger = logger;
            _settings = settings;
            _services = services;
            _taskService = taskService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
                _logger.Log($" TaskWorker background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var database =
                        scope.ServiceProvider
                            .GetService<DataContext>(); 
                    using (var scope2 = _services.CreateScope())
                    {
                        var taskRepository = scope2.ServiceProvider.GetService<ITaskRepository>();
                        using (var scope3 = _services.CreateScope())
                        {
                            var taskService = scope3.ServiceProvider.GetService<ITaskService>();

                            var task = await taskRepository.GetFirstTask(TaskStatus.Waiting);

                            if (task != null)
                            {
                                _logger.Log($"{task.Data}");
                                if (task.Type == TaskType.Ping)
                                {
                                    try
                                    {
                                        PingData data = JsonConvert.DeserializeObject<PingData>(task.Data);
                                        if (data == null)
                                            _logger.Log("ПИЗДЕЦ");
                                        var result = await taskService.PingURL(data);
                                        List<PingResult> pingList = new List<PingResult>();
                                        foreach (PingReply pingReply in result)
                                        {
                                            pingList.Add(new PingResult
                                            {
                                                Status = $"{pingReply.Status}",
                                                Address = $"{pingReply.Address}",
                                                RoundtripTime = pingReply.RoundtripTime
                                            });
                                        }

                                        task.Result = JsonConvert.SerializeObject(pingList);
                                        task.Status = TaskStatus.Completed;
                                    }
                                    catch (JsonReaderException ex)
                                    {
                                        task.Status = TaskStatus.Error;
                                    }

                                }

                                await taskRepository.UpdateTask(task);
                                database.SaveChanges();
                            }
                        }
                    }
                }
                await Task.Delay(_settings.CheckUpdateTime, stoppingToken);
            }
        }
    }
}