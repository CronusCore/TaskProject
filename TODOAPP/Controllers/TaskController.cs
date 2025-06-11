using DTO;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;
using TODOAPP.Models;

namespace TODOAPP.Controllers
{
    [Route("task")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        //implementar mi servicio
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        //mostrar todas las tareas
        [HttpGet("")]
        public async Task<IActionResult> Task()
        {
            IEnumerable<TaskDto> tasks = await _taskService.GetAllTasks(); 
            return View(tasks.ToList());
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
          

            TaskModel task = await _taskService.GetOneTask(id);

            return View("TaskEdit", task);

        }

    }
}
