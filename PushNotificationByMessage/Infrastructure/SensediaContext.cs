using Microsoft.EntityFrameworkCore;
using PushNotificationByMessage.Core.Entities;
using System.Reflection;

namespace PushNotificationByMessage.Infrastructure
{
    public class SensediaContext : DbContext
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
