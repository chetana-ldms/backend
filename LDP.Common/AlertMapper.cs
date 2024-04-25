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
                //.ForMember(dest => dest.DetectedTime, opt => opt.MapFrom(src => src.detected_time))
                .ForMember(dest => dest.ObservableTag, opt => opt.MapFrom(src => src.org_id))
                .ForMember(dest => dest.OwnerUserID, opt => opt.MapFrom(src => src.owner_user_id))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.severity))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.score))
                //.ForMember(dest => dest.detected_time, opt => opt.MapFrom(src => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(src.start_time).ToShortDateString()))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.source))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status)
                //)
                //.ForMember(dest => dest.AlertData, opt => opt.MapFrom(src => src.alert_data)
               //)
               // .ForMember(dest => dest.Createddate, opt => opt.MapFrom(src => src.Createddate))
               // .ForMember(dest =>dest.Modifieddate,null )
               //.ForMember(dest => dest.Modifieduser, opt => opt.MapFrom(src => nullVariable))
               //.ForMember(dest => dest.Createduser, opt => opt.MapFrom(src => src.Createduser)
               ).ReverseMap();

        }


    }
}
