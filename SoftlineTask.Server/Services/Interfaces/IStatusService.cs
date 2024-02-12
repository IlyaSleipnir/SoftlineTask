using SoftlineTask.Server.Models.Entities;

namespace SoftlineTask.Server.Services.Interfaces
{
    public interface IStatusService
    {
        public Task<IEnumerable<Status>> GetStatuses();
        public Task AddStatus(Status status);
        public Task UpdateStatus(Status status);
        public Task DeleteStatus(int id);
    }
}
