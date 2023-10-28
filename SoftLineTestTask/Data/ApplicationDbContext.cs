using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SoftLineTestTask.Models;

namespace SoftLineTestTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _config;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }
        
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId);

            modelBuilder.Entity<Status>()
                .HasData(
                    new Status { Id = 1, Name = "Создана" },
                    new Status { Id = 2, Name = "В процессе" },
                    new Status { Id = 3, Name = "Завершена" }
                );
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }
    }
}