using SoftlineTask.Server.Models.Entities;
using SoftlineTask.Server.Models.ViewModels;
using System.Collections;

namespace SoftlineTask.Server.Services.Interfaces
{
    public interface IWorkTaskService
    {
        public Task<IEnumerable<WorkTask>> GetAllTasks();
        public Task<WorkTask> GetTask(int id);
        public Task AddTask(WorkTask workTask);
        public Task DeleteTask(int id);
        public Task UpdateTask(WorkTask workTask);
    }
}
