using Interfaces;
using DTO;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using TODOAPP.Models;
using ViewModels;
using System.Threading;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly IDatabaseService _databaseService;

        public TaskService(IDatabaseService databaseService)
        {
            _databaseService = databaseService; 
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasks()
        {
            return await _databaseService.QueryAsync<TaskDto>("SP_GetAllTasks", "TODOConnectionString", new Dictionary<string, object>());
        }

        public async Task<TaskViewModel> GetOneTask(int id)
        {
            Dictionary<string, object> @params = new Dictionary<string, object>();
            @params.Add("@TaskId", id);
            return (await _databaseService.QueryAsync<TaskViewModel>("SP_GetOneTask", "TODOConnectionString", @params )).First();
            
        }
        public async Task<T?> UpdateOneTask<T>(ITaskBase task)
        {
            Dictionary<string, object> @params = new Dictionary<string, object>();
            @params.Add("@TaskName", task.TaskName);
            @params.Add("@TaskInitDate", task.TaskInitDate);
            @params.Add("@TaskEndDate", task.TaskEndDate);
            @params.Add("@TaskDescription", task.TaskDescription);
            @params.Add("@TaskComments", task.TaskComments);
            @params.Add("@EstatusId", task.EstatusId);
            @params.Add("@TaskId", task.TaskId);

            return await _databaseService.QueryScalar<T>("SP_UpdateOneTask", "TODOConnectionString", @params);
        }

        public async Task<T?> CreateTask<T>(ITaskBase task)
        {

            Dictionary<string, object> @params = new Dictionary<string, object>();
            @params.Add("@TaskName", task.TaskName);
            @params.Add("@TaskInitDate", task.TaskInitDate);
            @params.Add("@TaskEndDate", task.TaskEndDate);
            @params.Add("@TaskDescription", task.TaskDescription);
            @params.Add("@TaskComments", task.TaskComments);
            @params.Add("@EstatusId", task.EstatusId);

            return await _databaseService.QueryScalar<T>("SP_CreateTask", "TODOConnectionString", @params);
        }
    }
}
