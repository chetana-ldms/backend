namespace LDP.Common.Mappers
{
    using AutoMapper;
    using LDP.Common.DAL.Entities;
    using LDP.Common.Model;
    using LDP.Common.Requests;

    public class MSTeamsMapper : Profile
    {
        public MSTeamsMapper()
        {
            CreateMap<MsTeamModel, MSTeam>()
                .ForMember(dest => dest.team_id, opt => opt.MapFrom(src => src.TeamId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                .ForMember(dest => dest.team_name, opt => opt.MapFrom(src => src.TeamName))
                .ForMember(dest => dest.ms_team_id, opt => opt.MapFrom(src => src.MsTeamId))
                .ForMember(dest => dest.default_team, opt => opt.MapFrom(src => src.DefaultTeam))
                .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.modified_date, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.created_user, opt => opt.MapFrom(src => src.CreatedUser))
                .ForMember(dest => dest.modified_user, opt => opt.MapFrom(src => src.ModifiedUser))
                .ReverseMap();

        }
    }
    public class MSTeamsCreateChannelServiceMapper : Profile
    {
        public MSTeamsCreateChannelServiceMapper()
        {
            CreateMap<TeamscreateChannelRequest, TeamsCreatechannelServiceRequest>()
            .ReverseMap();

        }
    }
}
