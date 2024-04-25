using LDP_APIs.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.DataContext
{
    public class LDPSecurityDataContext : DbContext 
    {
        public LDPSecurityDataContext(DbContextOptions<LDPSecurityDataContext> options) : base(options)
        {
        }
        public DbSet<User>? vm_users { get; set; }
        public DbSet<Role>? vm_roles { get; set; }
              
}
}
