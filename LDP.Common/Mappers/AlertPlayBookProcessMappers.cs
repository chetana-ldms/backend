using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{
    public class GetAlertPlayBookProcessMapper:Profile
    {
        public GetAlertPlayBookProcessMapper()
        {
            CreateMap<AlertPlayBookProcess, GetAlertPlayBookProcessModel>()
                .ForMember(dest => dest.alertplaybooksprocessid, opt => opt.MapFrom(src => src.alert_id))
                .ForMember(dest => dest.alertid, opt => opt.MapFrom(src => src.alert_id))
                .ForMember(dest => dest.orgid, opt => opt.MapFrom(src => src.org_id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
                .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date)
               ).ReverseMap();

        }

      
    }
    public class AddAlertPlayBookProcessMapper : Profile
    {
        public AddAlertPlayBookProcessMapper()
        {
            CreateMap<AlertPlayBookProcess, AddAlertPlayBookProcessModel>()
                .ForMember(dest => dest.alertid, opt => opt.MapFrom(src => src.alert_id))
                .ForMember(dest => dest.orgid, opt => opt.MapFrom(src => src.org_id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User)
             ).ReverseMap();

        }

    }
    public class UpdateAlertPlayBookProcessMapper:Profile
    {
        public UpdateAlertPlayBookProcessMapper()
        {
            CreateMap<AlertPlayBookProcess, UpdateAlertPlayBookProcessModel>()
                .ForMember(dest => dest.alertplaybooksprocessid, opt => opt.MapFrom(src => src.alert_id))
                .ForMember(dest => dest.alertid, opt => opt.MapFrom(src => src.alert_id))
                .ForMember(dest => dest.orgid, opt => opt.MapFrom(src => src.org_id))
                .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date)
               ).ReverseMap();

        }

    }
}
