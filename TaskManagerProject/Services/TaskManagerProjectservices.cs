using TaskManagerProject.Data;
using TaskManagerProject.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TaskManagerProject.Services
{
    public class TaskManagerService
    {
        private readonly TaskManagerProjectContext _context;

        public TaskManagerService(TaskManagerProjectContext context)
        {
            _context = context;
        }

        // Fetch all tasks
        public async Task<IEnumerable<TaskManager>> GetAllTasksAsync()
        {
            return await _context.TaskManager.ToListAsync();
        }

        public TaskManagerProjectContext Get_context()
        {
            return _context;
        }

        // Fetch a specific task by ID
        public async Task<TaskManager> GetTaskByIdAsync(int id, TaskManagerProjectContext _context)
        {
            return await _context.TaskManager.FirstOrDefaultAsync(m => m.ID == id);
        }

        // Add a new task
        public async Task AddTaskAsync(TaskManager task)
        {
            _context.Add(task);
            await _context.SaveChangesAsync();
        }

        // Update an existing task
        public async Task UpdateTaskAsync(TaskManager task)
        {
            _context.Update(task);
            await _context.SaveChangesAsync();
        }

        // Delete a task by ID
        public async Task DeleteTaskAsync(int id)
        {
            var task = await GetTaskByIdAsync(id, Get_context());
            if (task != null)
            {
                _context.TaskManager.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        // Check if a task exists by ID
        public bool TaskExists(int id)
        {
            return _context.TaskManager.Any(e => e.ID == id);
        }

        internal Task<string?> GetTaskByIdAsync(int value)
        {
            throw new NotImplementedException();
        }
    }
}
