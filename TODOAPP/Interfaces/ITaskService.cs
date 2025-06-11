using DTO;
using TODOAPP.Models;

namespace Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasks();
        Task<TaskModel> GetOneTask(int id);
    }
}
