using TaskTracker.Application.Repositories.Interfaces;
using TaskTracker.Application.Services.Interfaces;
using TaskTracker.Domain.Entities;

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

        public Task<IEnumerable<TaskItem>> GetAllAsync() => _taskRepository.GetAllAsync();

        public Task<TaskItem?> GetByIdAsync(Guid id) => _taskRepository.GetByIdAsync(id);

        public Task<TaskItem> UpdateAsync(TaskItem task) => _taskRepository.UpdateAsync(task);
    }
}
