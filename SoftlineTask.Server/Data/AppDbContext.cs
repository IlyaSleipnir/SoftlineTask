using Microsoft.EntityFrameworkCore;
using SoftlineTask.Server.Models.Entities;

namespace SoftlineTask.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<WorkTask> WorkTasks { get; set; } = default!;
        public DbSet<Status> Statuses { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status[]
                {
                    new Status { Id = 1, Name = "Создана"},
                    new Status { Id = 2, Name = "В работе"},
                    new Status { Id = 3, Name = "Завершена"}
                });
        }
    }
}
