using Microsoft.EntityFrameworkCore;
using SoftlineTask.Server.Data;
using SoftlineTask.Server.Models.Entities;
using SoftlineTask.Server.Models.ViewModels;
using SoftlineTask.Server.Services.Interfaces;

namespace SoftlineTask.Server.Services
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly AppDbContext _context;

        public WorkTaskService(AppDbContext context) 
        {  
            _context = context; 
        }
        public async Task AddTask(WorkTask workTask)
        {
            var status = await _context.Statuses.FindAsync(workTask.StatusId);

            if (status == null)
            {
                throw new Exception($"Не найден Status с id = {workTask.StatusId}");
            }

            workTask.Status = status;

            _context.WorkTasks.Add(workTask);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteTask(int id)
        {
            var task = await _context.WorkTasks.FindAsync(id);

            if (task == null)
            {
                throw new Exception($"Не найден WorkTask с id = {id}");
            }

            _context.WorkTasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkTask>> GetAllTasks()
        {
            return await _context.WorkTasks
                .Include(x => x.Status)
                //.Select(x => new WorkTaskVM
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    Description = x.Description,
                //    StatusName = x.Status.Name
                //})
                .ToListAsync();
        }

        public async Task<WorkTask> GetTask(int id)
        {
            var task = await _context.WorkTasks.FindAsync(id);

            if (task == null)
            {
                throw new Exception($"Не найден WorkTask с id = {id}");
            }

            return task;
        }

        public async Task UpdateTask(WorkTask workTask)
        {
            var status = await _context.Statuses.FindAsync(workTask.StatusId);

            if (status == null)
            {
                throw new Exception($"Не найден Status с id = {workTask.StatusId}");
            }

            workTask.Status = status;

            _context.Entry(workTask).State = EntityState.Modified;

            try
            {
                _context.Update(workTask);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTaskExists(workTask.Id))
                {
                    throw new Exception($"WorkTask с id = {workTask.Id} не обновлен");
                }
                else 
                { 
                    throw;
                }
            }
        }

        private bool WorkTaskExists(int id)
        {
            return _context.WorkTasks.Any(e => e.Id == id);
        }
    }
}
