using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagerProject.Data;
using TaskManagerProject.Models;
using TaskManagerProject.Services;

namespace TaskManagerProject.Controllers
{
    public class TaskManagersController : Controller
    {
        private readonly TaskManagerProjectContext _context;
        private readonly TaskManagerService _taskManagerService;

        public TaskManagersController(TaskManagerProjectContext context, TaskManagerService taskManagerService)
        {
            _context = context;
            _taskManagerService = taskManagerService;
        }

        // GET: TaskManagers/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // GET: TaskManagers
        public async Task<IActionResult> Index()
        {
            return View(await _taskManagerService.GetAllTasksAsync());
        }

        // GET: TaskManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("The requested task or the task database is unavailable.");
            }

            var taskManager = await _taskManagerService.GetTaskByIdAsync(id.Value);
            if (taskManager == null)
            {
                return NotFound($"No task found with ID {id}.");
            }

            return View(taskManager);
        }

        // POST: TaskManagers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskManager taskManager)
        {
            if (ModelState.IsValid)
            {
                await _taskManagerService.AddTaskAsync(taskManager);
                return RedirectToAction(nameof(Index));
            }
            return View(taskManager);
        }

        // POST: TaskManagers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskManager taskManager)
        {
            if (id != taskManager.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _taskManagerService.UpdateTaskAsync(taskManager);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_taskManagerService.TaskExists(taskManager.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskManager);
        }

        // POST: TaskManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskManagerService.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
