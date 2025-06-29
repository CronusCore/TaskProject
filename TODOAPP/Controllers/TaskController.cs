using DTO;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;
using TODOAPP.Models;
using ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TODOAPP.Controllers
{
    [Route("task")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly CatalogService _catalogService;
        //implementar mi servicio
        public TaskController(ITaskService taskService, CatalogService catService)
        {
            _taskService = taskService;
            _catalogService = catService;
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
          

            TaskViewModel task = await _taskService.GetOneTask(id);
            task.Status = (await _catalogService.FillListAsync<EstatusModel>("SP_GetAllStatus", new Dictionary<string, object>(), list => list.Select(item => new SelectListItem
            {
                Value = item.EstatusId.ToString(),
                Text = item.EstatusName.ToString()

            }))).ToList();
            return View("TaskForm", task);

        }
        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] TaskViewModel taskEdit )
        {
            //validar la información
            if (!ModelState.IsValid)
            {
                taskEdit.Status = (await _catalogService.FillListAsync<EstatusModel>("SP_GetAllStatus", new Dictionary<string, object>(), list => list.Select(item => new SelectListItem
                {
                    Value = item.EstatusId.ToString(),
                    Text = item.EstatusName.ToString()

                }))).ToList();

                return View("TaskForm",taskEdit);
            }

            //guardar en la base de datos
            int isUpdated = await _taskService.UpdateOneTask<int>(taskEdit);
            if (isUpdated == 1)
            {
                TempData["messageUpdated"] = "The Task has been updated!";
            }
            else {
                TempData["messageUpdated"] = "A problem has ocurred!";
            }

            return RedirectToAction(nameof(Task));
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            TaskViewModel task = new TaskViewModel();
            task.Status = (await _catalogService.FillListAsync<EstatusModel>("SP_GetAllStatus", new Dictionary<string, object>(), list => list.Select(item => new SelectListItem
            {
                Value = item.EstatusId.ToString(),
                Text = item.EstatusName.ToString()

            }))).ToList();
            return View("TaskForm", task);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] TaskViewModel task)
        {
            if (!ModelState.IsValid)
            {
                task.Status = (await _catalogService.FillListAsync<EstatusModel>("SP_GetAllStatus", new Dictionary<string, object>(), list => list.Select(item => new SelectListItem
                {
                    Value = item.EstatusId.ToString(),
                    Text = item.EstatusName.ToString()

                }))).ToList();

                return View("TaskForm", task);
            }

            //guardar en la base de datos
            int isUpdated = await _taskService.CreateTask<int>(task);
            if (isUpdated == 1)
            {
                TempData["messageUpdated"] = "The Task has been added!";
            }
            else
            {
                TempData["messageUpdated"] = "A problem has ocurred!";
            }

            return RedirectToAction(nameof(Task));

        }

    }
}
