using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;
using TaskManager.API.Repositories.Helpers;

namespace TaskManagerAPI.Repositories
{
    public class TaskManagerDbContext: IdentityDbContext<AppUser>
    {
        public TaskManagerDbContext(DbContextOptions options) 
            : base(options)
        {
        }
         public DbSet<Customer> Customers { get; set; }
         public DbSet<AppTask> AppTasks { get; set; }

         protected override void OnModelCreating(ModelBuilder builder) 
         {
            base.OnModelCreating(builder);

            builder.RemovePluralizingTableNameConvention();
         }
    }
}