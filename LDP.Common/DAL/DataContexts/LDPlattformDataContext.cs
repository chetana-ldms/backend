using LDP.Common.DAL.Entities;
using LDP_APIs.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.DataContext
{
    public class LDPlattformDataContext : DbContext 
    {
        public LDPlattformDataContext(DbContextOptions<LDPlattformDataContext> options) : base(options)
        {
        }
        public DbSet<LDPTool>? vm_ldp_tools { get; set; }
        public DbSet<Organization>? vm_organizations { get; set; }
        public DbSet<OganizationTool>? vm_organization_tools { get; set; }

        public DbSet<OrganizationToolAction>? vm_organization_tool_actions { get; set; }

        public DbSet<LDPMasterData>? vm_ldp_masterdata { get; set; }

        public DbSet<ToolTypeAction>? vm_ldp_tool_type_actions { get; set; }
        public DbSet<LDPToolActions>? vm_ldp_tool_actions { get; set; }

    }
}
