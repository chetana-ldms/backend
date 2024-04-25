using AutoMapper;
using LDP.Common;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;
using LDP.Common.Services;
using LDP.Common.Services.SentinalOneIntegration;
using LDP_APIs.DAL.Entities;
using System.Text.Json;

namespace LDP_APIs.BL.Mappers
{
    public class SentinalOneIntegrationMapper : Profile
    {
        public SentinalOneIntegrationMapper()
        {
            CreateMap<SentinalThreatDetails, Alerts>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.threatInfo.threatName))
               // .ForMember(dest => dest.org_tool_severity, opt => opt.MapFrom(src => src.severity))
                .ForMember(dest => dest.alert_Device_PKID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.source, opt => opt.MapFrom(src => Constants.Source_SentinalOne))
                .ForMember(dest => dest.detected_time, opt => opt.MapFrom(src => src.threatInfo.createdAt))
                .ForMember(dest => dest.source, opt => opt.MapFrom(src => Constants.Source_SentinalOne))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => Constants.AlertNewStatus))
               //  .ForMember(dest => dest.status_ID, opt => opt.MapFrom(src => Constants.AlertNewStatus))
                .ForMember(dest => dest.alert_data, opt => opt.MapFrom(src => convertToJson(src)))
                .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()))
                .ForMember(dest => dest.false_positive, opt => opt.MapFrom(src => CheckFalsePositive(src.threatInfo.AnalystVerdict)))
                .ForMember(dest => dest.event_id, opt => opt.MapFrom(src => src.threatInfo.threatId))
                .ForMember(dest => dest.source_ip, opt => opt.MapFrom(src => getIpdata(src)))
                .ForMember(dest => dest.destination_user, opt => opt.MapFrom(src => src.AgentDetectionInfo.AgentLastLoggedInUserName))
                .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.threatInfo.updatedAt))
                .ForMember(dest => dest.resolved_time, opt => opt.MapFrom(src => CheckResolvedDateTime(src.threatInfo.IncidentStatus,src.threatInfo.updatedAt)))
                .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => Constants.User_Background_User)).ReverseMap();
        }

        int CheckFalsePositive(string toolResult)
        {
            if (toolResult == Constants.SentinalOne_true_positive)
                return 1;
            else 
                return 0;
        }

        string getIpdata(SentinalThreatDetails json)
        {
            string retvalue = string.Empty;

            retvalue = $"{json.AgentDetectionInfo.AgentIpV4} , Console visible ip : {json.AgentDetectionInfo.ExternalIp} , Domain : {json.AgentRealtimeInfo.agentDomain} , Computer Name : {json.AgentRealtimeInfo.AgentComputerName}";
            return retvalue;
        }
        DateTime? CheckResolvedDateTime(string status , DateTime? updatedAt)
        {
            if (status.ToLower() == Constants.SentinalOne_Resolved_Status.ToLower())
            {
                return updatedAt;
            }
            else
            {
                return null;
            }
        }
        string convertToJson(SentinalThreatDetails obj)
        {
            return JsonSerializer.Serialize(obj).ToString();
        }

        private DateTime ConvertFromUnixTimeMillisecondsToDat(double startTime)
        {
            DateTimeOffset dateTimeOffset2 = DateTimeOffset.FromUnixTimeMilliseconds((long)startTime);
            return dateTimeOffset2.UtcDateTime;

        }
    }

    public class SentinalOneThreatTimelineMapper : Profile
    {
        public SentinalOneThreatTimelineMapper()
        {
            CreateMap<ThreatTimeLine, AlertHistoryModel>()
                .ForMember(dest => dest.HistoryDescription, opt => opt.MapFrom(src => src.primaryDescription))
                .ForMember(dest => dest.HistoryDate, opt => opt.MapFrom(src => src.createdAt))
                .ReverseMap();

           
        }

    
    }


    public class SentinalOneThreatsandAlertsMapper : Profile
    {
        public SentinalOneThreatsandAlertsMapper()
        {
            //CreateMap<AlertModel, Alerts>()
            //    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
            //    // .ForMember(dest => dest.org_tool_severity, opt => opt.MapFrom(src => src.severity))
            //    .ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.AlertID))
            //    .ForMember(dest => dest.alert_incident_mapping_id, opt => opt.MapFrom(src => src.AlertIncidentMappingId))
            //    .ForMember(dest => dest.alert_Device_PKID, opt => opt.MapFrom(src => src.AlertDevicePKID))
            //    .ForMember(dest => dest.detected_time, opt => opt.MapFrom(src => src.Detectedtime))
            //    .ForMember(dest => dest.source, opt => opt.MapFrom(src => Constants.Source_SentinalOne))
            //    .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
            //    .ForMember(dest => dest.status_ID, opt => opt.MapFrom(src => src.StatusID))
            //    .ForMember(dest => dest.alert_data, opt => opt.MapFrom(src => src.AlertData))
            //    .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            //    .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
            //    .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgID))
            //    .ForMember(dest => dest.resolved_time, opt => opt.MapFrom(src => src.resolvedtime))
            //    .ForMember(dest => dest.false_positive, opt => opt.MapFrom(src => src.FalsePositive))
            //    .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
            //    .ForMember(dest => dest.event_id, opt => opt.MapFrom(src => src.EventId))
            //    .ForMember(dest => dest.source_ip, opt => opt.MapFrom(src => src.SourceIp))
            //    .ForMember(dest => dest.destination_user, opt => opt.MapFrom(src => src.DestinationUser))
            //    .ForMember(dest => dest.vendor, opt => opt.MapFrom(src => src.Vendor))

            //    // .ForMember(dest =>GetDetectedTimeString(dest.alert_data), opt => opt.MapFrom(src => src.DetectedtimeString))

            //    .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.ToolID)).ReverseMap();


            //    CreateMap<Alerts, AlertModel>()
            //        .ForMember(dest => dest.AlertID, opt => opt.MapFrom(src => src.alert_id))
            //        .ForMember(dest => dest.OrgID, opt => opt.MapFrom(src => src.org_id))
            //        .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.tool_id))
            //        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            //        .ForMember(dest => dest.AlertDevicePKID, opt => opt.MapFrom(src => src.alert_Device_PKID))
            //        .ForMember(dest => dest.ObservableTag, opt => opt.MapFrom(src => src.observable_tag))
            //        .ForMember(dest => dest.ObservableTagID, opt => opt.MapFrom(src => src.observable_tag_ID))
            //        .ForMember(dest => dest.OwnerUserID, opt => opt.MapFrom(src => src.owner_user_id))
            //        .ForMember(dest => dest.ownerusername, opt => opt.MapFrom(src => src.owner_user_name))
            //        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
            //        .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.status_ID))
            //        .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.score))
            //        .ForMember(dest => dest.Detectedtime, opt => opt.MapFrom(src => src.detected_time))
            //        .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.source))
            //        .ForMember(dest => dest.AutomationStatus, opt => opt.MapFrom(src => src.Automation_Status))
            //        .ForMember(dest => dest.priorityname, opt => opt.MapFrom(src => src.priority_name))
            //        .ForMember(dest => dest.priorityid, opt => opt.MapFrom(src => src.priority_id))
            //        .ForMember(dest => dest.resolvedtime, opt => opt.MapFrom(src => src.resolved_time))
            //        .ForMember(dest => dest.resolvedtimeDuration, opt => opt.MapFrom(src => src.resolved_time_Duration))
            //        .ForMember(dest => dest.AlertIncidentMappingId, opt => opt.MapFrom(src => src.alert_incident_mapping_id))
            //        .ForMember(dest => dest.SLA, opt => opt.MapFrom(src => CalculateSLA(src.detected_time,src.resolved_time)))
            //        .ForMember(dest => dest.SeverityName, opt => opt.MapFrom(src => src.severity_name))
            //        .ForMember(dest => dest.SeverityId, opt => opt.MapFrom(src => src.severity_id))
            //        .ForMember(dest => dest.OrgToolSeverity, opt => opt.MapFrom(src => src.org_tool_severity))
            //        .ForMember(dest => dest.FalsePositive, opt => opt.MapFrom(src => src.false_positive))
            //        .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
            //        .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
            //       .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
            //       .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date)

        }

        string GetDetectedTimeString(string threatData)
        {
            string retValue = string.Empty;

           var apiThreats = JsonSerializer.Deserialize<SentinalThreatDetailsTemp>(threatData,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });

            return apiThreats.threatInfo.createdAt;
        }
    }

    
}
