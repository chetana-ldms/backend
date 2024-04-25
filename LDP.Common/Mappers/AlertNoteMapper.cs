using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{


    public class GetAlertNoteMapper : Profile
    {
        public GetAlertNoteMapper()
        {
            CreateMap<alert_note, AlertNoteModel>()
            .ForMember(dest => dest.AlertsNotesId, opt => opt.MapFrom(src => src.alerts_notes_id))
            .ForMember(dest => dest.AlertId, opt => opt.MapFrom(src => src.alert_id))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.notes))
            .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => src.action_type))
            .ForMember(dest => dest.ActionName, opt => opt.MapFrom(src => src.action_name))
            .ForMember(dest => dest.ActionId, opt => opt.MapFrom(src => src.action_id))
            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_user))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_date))
            .ForMember(dest => dest.NotesDate, opt => opt.MapFrom(src => src.notes_date))
            .ForMember(dest => dest.NotesToUserid, opt => opt.MapFrom(src => src.notes_to_userid))
            .ReverseMap(); ;
        }


    }
}
