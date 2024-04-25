using LDP.Common.DAL.Entities;
using LDP_APIs.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.DataContext
{
    public class AlertsDataContext : DbContext 
    {
        public AlertsDataContext(DbContextOptions<AlertsDataContext> options) : base(options)
        {
        }
        public DbSet<Alerts>? vm_alerts { get; set; }

        public DbSet<alert_note>? vm_alerts_notes { get; set; }

        public DbSet<AlertIncidentMapping>? vm_alert_incident_mapping { get; set; }

        public DbSet<AlertIncidentMappingDtl>? vm_alert_incident_mapping_dtl { get; set; }
        public DbSet<AlertPlayBookProcess>? vm_alert_playbooks_process { get; set; }
        public DbSet<AlertPlayBookProcessAction>? vm_alert_playbooks_process_actions { get; set; }
        public DbSet<Incident>? vm_Incidents { get; set; }
        public DbSet<AlertExtnField>? vm_alerts_extn_fields { get; set; }
        public DbSet<AlertsAccountStructure>? vm_alerts_account_structure { get; set; }

    }
}
