using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.DTO;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Services.Interfaces
{
    public interface ITaskService
    {
        Task<PagedResult<TaskItem>> GetAllAsync(int pageNumber, int PageSize);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<TaskItem> AddAsync(TaskItem item);
        Task<bool> DeleteAsync(Guid id);
        Task<TaskItem> UpdateAsync(TaskItem task);
    }
}
