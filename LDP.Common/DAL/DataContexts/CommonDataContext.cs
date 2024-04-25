using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.DataContexts
{
    public class CommonDataContext : DbContext
    {
        public CommonDataContext(DbContextOptions<CommonDataContext> options) : base(options)
        {
        }

        public DbSet<useraction>? vm_user_actions { get; set; }
        public DbSet<AlertHistory>? vm_alerts_history { get; set; }

        public DbSet<MasterDataExtnFields>? vm_ldp_masterdata_extn_fields { get; set; }

        public DbSet<OrgMasterData>? vm_ldp_org_masterdata { get; set; }

        public DbSet<ConfigurationData>? vm_ldp_configurationdata { get; set; }

        public DbSet<NotificaionDetail>? vm_notification_Details { get; set; }

        public DbSet<LDCApiUrls>? vm_ldc_api_urls { get; set; }

        public DbSet<ApplicationLog>? vm_application_logs { get; set; }

        public DbSet<Tasks>? vm_tasks { get; set; }

        public DbSet<ActivityType>? vm_ldc_activity_types { get; set; }

        public DbSet<LdcActivity>? vm_ldc_activities { get; set; }


    }
}
