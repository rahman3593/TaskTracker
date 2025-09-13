using System.Linq;
using TaskTracker.Application.DTO;
using TaskTracker.Application.Repositories.Interfaces;
using TaskTracker.Application.Services.Interfaces;
using TaskTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace TaskTracker.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public Task<TaskItem> AddAsync(TaskItem item) => _taskRepository.AddAsync(item);

        public Task<bool> DeleteAsync(Guid id) => _taskRepository.DeleteAsync(id);

        public async Task<PagedResult<TaskItem>> GetAllAsync(int pageNumber, int pageSize, bool? isCompleted)
        {
            var query = _taskRepository.GetAll();

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TaskItem>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public Task<TaskItem?> GetByIdAsync(Guid id) => _taskRepository.GetByIdAsync(id);

        public Task<TaskItem> UpdateAsync(TaskItem task) => _taskRepository.UpdateAsync(task);
    }
}
