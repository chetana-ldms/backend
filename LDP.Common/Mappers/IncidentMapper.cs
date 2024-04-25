using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP_APIs.Models;

namespace LDP.Common.Mappers
{
    public class AddInternalIncidentMapper : Profile
    {
        public AddInternalIncidentMapper()
        {
            CreateMap<CreateIncidentRequest, Incident>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.incident_subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity))
            .ForMember(dest => dest.type_id, opt => opt.MapFrom(src => src.TypeId))
            .ForMember(dest => dest.Event_ID, opt => opt.MapFrom(src => src.EventID))
            .ForMember(dest => dest.Destination_User, opt => opt.MapFrom(src => src.DestinationUser))
            .ForMember(dest => dest.Source_IP, opt => opt.MapFrom(src => src.SourceIP))
            .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            //.ForMember(dest => dest.Incident_Status, opt => opt.MapFrom(src => src.IncidentStatus))
            .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreateDate))
            //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.cre))
            //.ForMember(dest => dest.Priority_Name, opt => opt.MapFrom(src => src.PriorityName))
            //.ForMember(dest => dest.Severity_Name, opt => opt.MapFrom(src => src.SeverityName))
            //.ForMember(dest => dest.Owner_Name, opt => opt.MapFrom(src => src.OwnerName))
            .ForMember(dest => dest.Incident_Date, opt => opt.MapFrom(src => src.IncidentDate))
            //.ForMember(dest => dest.Incident_Status_Name, opt => opt.MapFrom(src => src.IncidentStatusName))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            //.ForMember(dest => dest.type_id, opt => opt.MapFrom(src => src.TypeId))
            //.ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.ToolId))
            .ReverseMap();
        }
    }

    public class GetInternalIncidentMapper : Profile
    {
        public GetInternalIncidentMapper()
        {
            CreateMap<GetIncidentModel, Incident>()
            .ForMember(dest => dest.Incident_ID, opt => opt.MapFrom(src => src.IncidentID))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.incident_subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Event_ID, opt => opt.MapFrom(src => src.EventID))
            .ForMember(dest => dest.Destination_User, opt => opt.MapFrom(src => src.DestinationUser))
            .ForMember(dest => dest.Source_IP, opt => opt.MapFrom(src => src.SourceIP))
            .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Incident_Status, opt => opt.MapFrom(src => src.IncidentStatus))
            .ForMember(dest => dest.incident_subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
            .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
            .ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
            //.ForMember(dest => dest.Processed, opt => opt.MapFrom(src => src.Processed))

            .ForMember(dest => dest.Priority_Name, opt => opt.MapFrom(src => src.PriorityName))
            .ForMember(dest => dest.Severity_Name, opt => opt.MapFrom(src => src.SeverityName))
            .ForMember(dest => dest.Owner_Name, opt => opt.MapFrom(src => src.OwnerName))
            .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score))
            .ForMember(dest => dest.Incident_Date, opt => opt.MapFrom(src => src.IncidentDate))
            .ForMember(dest => dest.Incident_Status_Name, opt => opt.MapFrom(src => src.IncidentStatusName))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            .ForMember(dest => dest.internal_incident, opt => opt.MapFrom(src => src.InternalIncident))
            //.ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.ToolId))
            //.ForMember(dest => dest.type_id, opt => opt.MapFrom(src => src.TypeId))
            .ReverseMap();
        }
    }

    public class GetInternalIncidentMapper_FreshDesk : Profile
    {
        public GetInternalIncidentMapper_FreshDesk()
        {
            CreateMap<GetIncidentModel, FreshDeskIncidentDtls>()
           //.ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IncidentID))
           .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.subject, opt => opt.MapFrom(src => src.Subject))
           .ForMember(dest => dest.priority, opt => opt.MapFrom(src => src.Priority))
            //.ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity))
            //.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            //.ForMember(dest => dest.Event_ID, opt => opt.MapFrom(src => src.EventID))
            //.ForMember(dest => dest.Destination_User, opt => opt.MapFrom(src => src.DestinationUser))
            //.ForMember(dest => dest.Source_IP, opt => opt.MapFrom(src => src.SourceIP))
            //.ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor))
            //.ForMember(dest => dest.ag, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.IncidentStatus))
            //.ForMember(dest => dest.created_at, opt => opt.MapFrom(src => src.CreatedDate))
            //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
           // .ForMember(dest => dest.updated_at, opt => opt.MapFrom(src => src.ModifiedDate))
            //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
            //.ForMember(dest => dest.Processed, opt => opt.MapFrom(src => src.Processed))

            //.ForMember(dest => dest.Priority_Name, opt => opt.MapFrom(src => src.PriorityName))
            //.ForMember(dest => dest.Severity_Name, opt => opt.MapFrom(src => src.SeverityName))
            //.ForMember(dest => dest., opt => opt.MapFrom(src => src.OwnerName))
            //.ForMember(dest => dest.score, opt => opt.MapFrom(src => src.Score))
            //.ForMember(dest => dest.Incident_Date, opt => opt.MapFrom(src => src.IncidentDate))
            //.ForMember(dest => dest.Incident_Status_Name, opt => opt.MapFrom(src => src.IncidentStatusName))

            .ReverseMap();
        }
    }
}
