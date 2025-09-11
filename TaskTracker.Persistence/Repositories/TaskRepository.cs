using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Repositories.Interfaces;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskTrackerDbContext _context;
        public TaskRepository(TaskTrackerDbContext context)
        {

            _context = context;
        }
        public async Task<TaskItem> AddAsync(TaskItem item)
        {
            _context.Tasks.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync() => await _context.Tasks.ToListAsync();


        public async Task<TaskItem?> GetByIdAsync(Guid id) => await _context.Tasks.FindAsync(id);

        public async Task<TaskItem> UpdateAsync(TaskItem task)
        {
            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null)
            {
                return null;
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.IsCompleted = task.IsCompleted;

            await _context.SaveChangesAsync();
            return existingTask;
        }
    }
}
