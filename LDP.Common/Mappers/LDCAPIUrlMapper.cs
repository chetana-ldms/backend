using AutoMapper;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{
    public class GetLDCAPIUrlMapper : Profile
    {
        public GetLDCAPIUrlMapper()
        {
            CreateMap<LDCUrlModel, LDCApiUrls>()
            .ForMember(dest => dest.url_id, opt => opt.MapFrom(src => src.UrlId))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            .ForMember(dest => dest.url_group, opt => opt.MapFrom(src => src.UrlGroup))
            .ForMember(dest => dest.action_name, opt => opt.MapFrom(src => src.ActionName))
            .ForMember(dest => dest.action_url, opt => opt.MapFrom(src => src.ActionUrl))
            .ForMember(dest => dest.auth_token, opt => opt.MapFrom(src => src.AuthToken))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.Active))
            .ForMember(dest => dest.created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
            .ForMember(dest => dest.created_User, opt => opt.MapFrom(src => src.CreatedUser))
            .ForMember(dest => dest.modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
            .ReverseMap();

        }
    }
}
