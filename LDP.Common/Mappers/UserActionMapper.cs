using AutoMapper;
using LDP.Common;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP_APIs.DAL.Entities;

namespace LDP.Common.Mappers
{


    public class UserActionMapper : Profile
    {
        public UserActionMapper()
        {
            CreateMap<useraction, UserActionRequest>()
                .ForMember(dest => dest.ActionId, opt => opt.MapFrom(src => src.action_id))
                .ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => src.action_Date))
                .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => src.action_type))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ActionTypeRefid, opt => opt.MapFrom(src => src.action_type_refid))
                .ForMember(dest => dest.ActionStatus, opt => opt.MapFrom(src => src.action_Status))
                .ForMember(dest => dest.ActionStatusName, opt => opt.MapFrom(src => src.Action_Status_Name))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.score))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority_Name))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner_Name))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity))
                .ForMember(dest => dest.SeverityId, opt => opt.MapFrom(src => src.severity_id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
               .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
               .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date)
               ).ReverseMap();
        }


    }

    public class UserActionModelMapper : Profile
    {
        public UserActionModelMapper()
        {
            CreateMap<useraction, UserActionModel>()
                .ForMember(dest => dest.ActionId, opt => opt.MapFrom(src => src.action_id))
                .ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => src.action_Date))
                .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => src.action_type))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ActionTypeRefid, opt => opt.MapFrom(src => src.action_type_refid))
                .ForMember(dest => dest.ActionStatus, opt => opt.MapFrom(src => src.action_Status))
                .ForMember(dest => dest.ActionStatusName, opt => opt.MapFrom(src => src.Action_Status_Name))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.score))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority_Name))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner_Name))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity))
                .ForMember(dest => dest.SeverityId, opt => opt.MapFrom(src => src.severity_id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
               .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
               .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date)
               ).ReverseMap();
        }


    }

    public class AlertAndUserActionMapper : Profile
    {
        public AlertAndUserActionMapper()
        {
            CreateMap<Alerts, UserActionRequest>()
                .ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => src.Modified_Date))
                .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => Constants.User_Action_Alert_Type))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.ActionTypeRefid, opt => opt.MapFrom(src => src.alert_id))
                .ForMember(dest => dest.ActionStatus, opt => opt.MapFrom(src => src.status_ID))
                .ForMember(dest => dest.ActionStatusName, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.score))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.priority_id))
                .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.priority_name))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.owner_user_name))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.owner_user_id))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.severity))
                .ForMember(dest => dest.SeverityId, opt => opt.MapFrom(src => src.severity_id))
                // .ForMember(dest => dest.SeverityName, opt => opt.MapFrom(src => src.severity))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Modified_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Modified_User))
               .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
               .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date)
               ).ReverseMap();
        }

    }

    public class IncidentAndUserActionMapper : Profile
    {
        public IncidentAndUserActionMapper()
        {
            CreateMap<Incident, UserActionRequest>()
                .ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => src.Modified_Date))
                .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => Constants.User_Action_Incident_Type))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ActionTypeRefid, opt => opt.MapFrom(src => src.Incident_ID))
                .ForMember(dest => dest.ActionStatus, opt => opt.MapFrom(src => src.Incident_Status))
                .ForMember(dest => dest.ActionStatusName, opt => opt.MapFrom(src => src.Incident_Status_Name))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority_Name))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner_Name))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity_Name))
               // .ForMember(dest => dest.SeverityId, opt => opt.MapFrom(src => src.severity_id))

                // .ForMember(dest => dest.SeverityName, opt => opt.MapFrom(src => src.severity))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Modified_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Modified_User))
               .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
               .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date)
               ).ReverseMap();
        }

    }

}
