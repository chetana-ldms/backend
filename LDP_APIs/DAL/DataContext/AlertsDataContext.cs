using LDP_APIs.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.DataContext
{
    public class AlertsDataContext : DbContext 
    {
        public AlertsDataContext(DbContextOptions<AlertsDataContext> options) : base(options)
        {
        }
        public DbSet<Alerts>? alerts { get; set; }
        public DbSet<Alerts>? vm_alerts { get; set; }
    }
}
