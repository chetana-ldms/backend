using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{
    public class AddApplicationLogProfile : Profile
    {
        public AddApplicationLogProfile()
        {
            CreateMap<ApplicationLogModel, ApplicationLog>()
               // .ForMember(dest => dest.log_id, opt => opt.MapFrom(src => src.LogId))
                //.ForMember(dest => dest.timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.severity, opt => opt.MapFrom(src => src.Severity))
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ip_address, opt => opt.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.log_source, opt => opt.MapFrom(src => src.LogSource))
                .ForMember(dest => dest.message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.stack_trace, opt => opt.MapFrom(src => src.StackTrace))
                .ForMember(dest => dest.request_data, opt => opt.MapFrom(src => src.RequestData))
                .ForMember(dest => dest.response_data, opt => opt.MapFrom(src => src.ResponseData))
                .ForMember(dest => dest.additional_info, opt => opt.MapFrom(src => src.AdditionalInfo)).ReverseMap();

        }
    }

    public class GetApplicationLogProfile : Profile
    {
        public GetApplicationLogProfile()
        {
            CreateMap<GetApplicationLogModel, ApplicationLog>()
                .ForMember(dest => dest.log_id, opt => opt.MapFrom(src => src.LogId))
                .ForMember(dest => dest.log_date, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.severity, opt => opt.MapFrom(src => src.Severity))
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ip_address, opt => opt.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.log_source, opt => opt.MapFrom(src => src.LogSource))
                .ForMember(dest => dest.message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.stack_trace, opt => opt.MapFrom(src => src.StackTrace))
                .ForMember(dest => dest.request_data, opt => opt.MapFrom(src => src.RequestData))
                .ForMember(dest => dest.response_data, opt => opt.MapFrom(src => src.ResponseData))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                .ForMember(dest => dest.additional_info, opt => opt.MapFrom(src => src.AdditionalInfo)).ReverseMap();
        }
    }
}
