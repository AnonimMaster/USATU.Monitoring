using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USATU.Monitoring.Core.Domains.Tasks;
using USATU.Monitoring.Core.Domains.Tasks.Repositories;
using USATU.Monitoring.Core.Exceptions;

namespace USATU.Monitoring.Data.Tasks.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<TaskMonitoring> GetTask(string id)
        {
            var entityTask = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

            if (entityTask == null)
            {
                throw new ObjectNotFoundException($"Задачи с Id = {id} не существует.");
            }

            return new TaskMonitoring()
            {
                Id = entityTask.Id,
                Data = entityTask.Data,
                Status = entityTask.Status,
                Type = entityTask.Type
            };
        }

        public async Task<TaskMonitoring> GetFirstTask(USATU.Monitoring.Core.Domains.Tasks.Enums.TaskStatus status)
        {
            var entityTask = await _context.Tasks.AsNoTracking()
                .FirstOrDefaultAsync(i => i.Status == status);

            if (entityTask == null)
            {
                return null;
            }

            return new TaskMonitoring()
            {
                Data = entityTask.Data,
                Type = entityTask.Type,
                Status = status,
                Description = entityTask.Description,
                Id = entityTask.Id,
            };
        }

        public Task CreateTask(TaskMonitoring taskMonitoring)
        {
            var entityTask = new TaskMonitoringDbModel()
            {
                Id = Guid.NewGuid().ToString(),
                Data = taskMonitoring.Data,
                Type = taskMonitoring.Type,
                Status = taskMonitoring.Status
            };

            return _context.Tasks.AddAsync(entityTask).AsTask();
        }

        public Task<List<TaskMonitoring>> GetAllTasks()
        {
            return _context.Tasks
                .AsNoTracking()
                .Select(i => new TaskMonitoring()
                {
                    Id = i.Id,
                    Data = i.Data,
                    Status = i.Status,
                    Type = i.Type,
                    Result = i.Result
                }).ToListAsync();
        }

        public async Task UpdateTask(TaskMonitoring taskMonitoring)
        {
            var entityTask = await _context.Tasks.FirstOrDefaultAsync(i => i.Id == taskMonitoring.Id);

            if (entityTask == null)
            {
                throw new ObjectNotFoundException($"Задача с Id = {taskMonitoring.Id} не существует.");
            }

            entityTask.Status =taskMonitoring.Status;
            entityTask.Data = taskMonitoring.Data;
            entityTask.Result = taskMonitoring.Result;
        }

        public async Task DeleteTask(string id)
        {
            var entityTask = await _context.Tasks.FirstOrDefaultAsync(i => i.Id == id);

            if (entityTask == null)
            {
                throw new ObjectNotFoundException($"Задача с Id = {id} не существует.");
            }

            _context.Remove(entityTask);
        }

        public Task<bool> IsTaskExists(string id)
        {
            return _context.Tasks.AnyAsync(i => i.Id == id);
        }
    }
}