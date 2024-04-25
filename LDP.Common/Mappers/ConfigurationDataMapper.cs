using AutoMapper;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{

    public class ConfigurationDataMapper : Profile
        {
            public ConfigurationDataMapper()
            {
                CreateMap<ConfigurationData, ConfigurationDataModel>()
                    .ForMember(dest => dest.DataID, opt => opt.MapFrom(src => src.data_id))
                    .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.data_type))
                    .ForMember(dest => dest.DataName, opt => opt.MapFrom(src => src.data_name))
                    .ForMember(dest => dest.DataValue, opt => opt.MapFrom(src => src.data_value))
                    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                    .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
                   .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
                   .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User)
                   )
                .ReverseMap();

            }
        }
    
}
