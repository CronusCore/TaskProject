using DTO;
using TODOAPP.Models;
using ViewModels;

namespace Interfaces
{
    public interface ITaskService
    {
        Task<T?> CreateTask<T>(ITaskBase task);
        Task<IEnumerable<TaskDto>> GetAllTasks();
        Task<TaskViewModel> GetOneTask(int id);
        Task<T?> UpdateOneTask<T>(ITaskBase task);
    }
}
