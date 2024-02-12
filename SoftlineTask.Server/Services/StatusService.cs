using Microsoft.EntityFrameworkCore;
using SoftlineTask.Server.Data;
using SoftlineTask.Server.Models.Entities;
using SoftlineTask.Server.Models.ViewModels;
using SoftlineTask.Server.Services.Interfaces;

namespace SoftlineTask.Server.Services
{
    public class StatusService : IStatusService
    {
        private readonly AppDbContext _context;

        public StatusService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddStatus(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStatus(int id)
        {
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                throw new Exception($"Не найден Status с id = {id}");
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Status>> GetStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task UpdateStatus(Status status)
        {
            _context.Entry(status).State = EntityState.Modified;

            try
            {
                _context.Update(status);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(status.Id))
                {
                    throw new Exception($"Status с id = {status.Id} не обновлен");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.Id == id);
        }
    }
}
