using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.API.Models;
using TaskManager.API.ViewModels;

namespace TaskManager.API.Repositories
{
    public interface IAppTaskRepository
    {
        IEnumerable<AppTask> GetAll();
        void Create(AppTask newTask);
        void Update(int taskId, AppTask updatedTask);
        void Delete(int taskId);
    }
}