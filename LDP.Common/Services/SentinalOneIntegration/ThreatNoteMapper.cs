using AutoMapper;
using LDP.Common.Model;

namespace LDP.Common.Services.SentinalOneIntegration
{
    // AlertNoteModel
    public class ThreatNoteMapper: Profile
    {
        public ThreatNoteMapper()
        {
            CreateMap<ThreatNoteData, AlertNoteModel>()
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.text))
            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.creator))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.NotesDate, opt => opt.MapFrom(src => src.createdAt))
            .ReverseMap();

            CreateMap<AddToExclusionRequest, AddToExclusionlistDTO>().ReverseMap();
        }


    }
}
