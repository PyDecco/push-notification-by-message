using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using PushNotificationByMessage.Models.Entites;

namespace PushNotificationByMessage.Adapters.SqLiteAdapter.Infrastructure
{
    public class SensediaContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {

        public DbSet<User> Users { get; set; }
        public SensediaContext(DbContextOptions<SensediaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
