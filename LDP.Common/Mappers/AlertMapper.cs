using AutoMapper;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.BL.Mappers
{
    public class AlertMapper : Profile
    {
        public AlertMapper()
        {
            CreateMap<Alerts, AlertModel>()
                .ForMember(dest => dest.AlertID, opt => opt.MapFrom(src => src.alert_id))
                .ForMember(dest => dest.OrgID, opt => opt.MapFrom(src => src.org_id))
                .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.tool_id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.AlertDevicePKID, opt => opt.MapFrom(src => src.alert_Device_PKID))
                .ForMember(dest => dest.ObservableTag, opt => opt.MapFrom(src => src.observable_tag))
                .ForMember(dest => dest.ObservableTagID, opt => opt.MapFrom(src => src.observable_tag_ID))
                .ForMember(dest => dest.OwnerUserID, opt => opt.MapFrom(src => src.owner_user_id))
                .ForMember(dest => dest.ownerusername, opt => opt.MapFrom(src => src.owner_user_name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.status_ID))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.score))
                .ForMember(dest => dest.Detectedtime, opt => opt.MapFrom(src => src.detected_time))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.source))
                .ForMember(dest => dest.AutomationStatus, opt => opt.MapFrom(src => src.Automation_Status))
                .ForMember(dest => dest.priorityname, opt => opt.MapFrom(src => src.priority_name))
                .ForMember(dest => dest.priorityid, opt => opt.MapFrom(src => src.priority_id))
                .ForMember(dest => dest.resolvedtime, opt => opt.MapFrom(src => src.resolved_time))
                .ForMember(dest => dest.resolvedtimeDuration, opt => opt.MapFrom(src => src.resolved_time_Duration))
                .ForMember(dest => dest.AlertIncidentMappingId, opt => opt.MapFrom(src => src.alert_incident_mapping_id))
                .ForMember(dest => dest.SLA, opt => opt.MapFrom(src => CalculateSLA(src.detected_time, src.resolved_time)))
                .ForMember(dest => dest.SeverityName, opt => opt.MapFrom(src => src.severity_name))
                .ForMember(dest => dest.SeverityId, opt => opt.MapFrom(src => src.severity_id))
                .ForMember(dest => dest.OrgToolSeverity, opt => opt.MapFrom(src => src.org_tool_severity))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.event_id))
                .ForMember(dest => dest.SourceIp, opt => opt.MapFrom(src => src.source_ip))
                .ForMember(dest => dest.DestinationUser, opt => opt.MapFrom(src => src.destination_user))
                .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.vendor))
                .ForMember(dest => dest.FalsePositive, opt => opt.MapFrom(src => src.false_positive))
                .ForMember(dest => dest.PositiveAnalysis, opt => opt.MapFrom(src => src.positive_analysis))
                .ForMember(dest => dest.PositiveAnalysisId, opt => opt.MapFrom(src => src.positive_analysis_id))
                .ForMember(dest => dest.AlertData, opt => opt.MapFrom(src => src.alert_data))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
               .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date)
               ).ReverseMap();
        }

        string CalculateSLA(DateTime? detectedTime , DateTime? resolvedTime)
        {
            TimeSpan timeSpan;
            //double timeInSeconds = 0;
            string sla = "";
            if (resolvedTime != null )
            {
                timeSpan = (resolvedTime.Value - detectedTime.Value);
            }
            else
            {
                timeSpan = (DateTime.UtcNow - detectedTime.Value);
            }
            sla = String.Format("{0}d{1}h{2}m", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes);
            return sla;
        }

        
    }
}
