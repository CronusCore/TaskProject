using Interfaces;
using DTO;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using TODOAPP.Models;

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

        public async Task<TaskModel> GetOneTask(int id)
        {
            Dictionary<string, object> @params = new Dictionary<string, object>();
            @params.Add("@TaskId", id);
            return (await _databaseService.QueryAsync<TaskModel>("SP_GetOneTask", "TODOConnectionString", @params )).First();
            
        }
    }
}
