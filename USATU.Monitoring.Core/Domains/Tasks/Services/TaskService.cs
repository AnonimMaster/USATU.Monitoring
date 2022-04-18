using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FluentValidation;
using USATU.Monitoring.Core.Domains.Tasks.Data.Ping;
using USATU.Monitoring.Core.Domains.Tasks.Repositories;
using USATU.Monitoring.Core.Domains.Users.Repositories;
using USATU.Monitoring.Core.Domains.Users;

namespace USATU.Monitoring.Core.Domains.Tasks.Services
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;

        public TaskService(IUnitOfWork unitOfWork, ITaskRepository taskRepository, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _taskRepository = taskRepository;
            _httpClient = httpClient;
        }

        public async Task<List<PingReply>> PingURL(PingData data)
        {
            var ping = new Ping();
            List<PingReply> pingReplyList = new List<PingReply>();
            for (int i = 0; i < data.Iteration; i++)
            {
                pingReplyList.Add(await ping.SendPingAsync(data.URL, data.Timeout));
            }

            return pingReplyList;
        }

        public async Task<bool> ValidationSSL(string url)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = ServerCertificateValidationCallback;
            var client = new HttpClient(handler);
            try
            {
                using (var msg = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    using (var response = await client.SendAsync(msg))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                Console.WriteLine("Certificate ERROR");
                return false;
            }
            Console.WriteLine("Certificate OK");
            return true;
        }

        public Task<TaskMonitoring> GetTask(string id)
        {
            return _taskRepository.GetTask(id);
        }

        public async Task CreateTask(TaskMonitoring taskMonitoring)
        {
            await _taskRepository.CreateTask(taskMonitoring);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<List<TaskMonitoring>> GetAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public async Task UpdateTask(TaskMonitoring taskMonitoring)
        {
            await _taskRepository.UpdateTask(taskMonitoring);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTask(string id)
        {
            var isTaskExists = await _taskRepository.IsTaskExists(id);

            if (!isTaskExists)
            {
                throw new ValidationException($"Задачи с Id = {id} нет.");
            }

            await _taskRepository.DeleteTask(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}