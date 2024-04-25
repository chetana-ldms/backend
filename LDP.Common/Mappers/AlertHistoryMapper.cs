using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{
   

    public class AlertHistoryMapper : Profile
    {
        public AlertHistoryMapper()
        {
            CreateMap<AlertHistory, AlertHistoryModel>()
            .ForMember(dest => dest.AlertHistoryId, opt => opt.MapFrom(src => src.alert_history_id))
            .ForMember(dest => dest.HistoryDate, opt => opt.MapFrom(src => src.history_date))
            .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
            .ForMember(dest => dest.AlertId, opt => opt.MapFrom(src => src.alert_id))
            .ForMember(dest => dest.IncidentId, opt => opt.MapFrom(src => src.incident_id))
            .ForMember(dest => dest.CreatedUserId, opt => opt.MapFrom(src => src.created_user_id))
            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.createed_user))
            .ForMember(dest => dest.HistoryDescription, opt => opt.MapFrom(src => src.history_description))
            .ReverseMap(); ;
        }


    }

}
