using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerProject.Models;

namespace TaskManagerProject.Data
{
    public class TaskManagerProjectContext : DbContext
    {
        public TaskManagerProjectContext (DbContextOptions<TaskManagerProjectContext> options)
            : base(options)
        {
        }

        public DbSet<TaskManagerProject.Models.TaskManager> TaskManager { get; set; } = default!;
    }
}
