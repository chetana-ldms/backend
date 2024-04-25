using AutoMapper;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{
    public class MasterDataExtnFieldsMapper : Profile
    {
        public MasterDataExtnFieldsMapper()
        {
            
         CreateMap<MasterDataExtnFieldsModel, MasterDataExtnFields>()
        .ForMember(dest => dest.data_extn_fields_id, opt => opt.MapFrom(src => src.DataExtnFieldsId))
        .ForMember(dest => dest.data_type, opt => opt.MapFrom(src => src.DataType))
        .ForMember(dest => dest.data_id, opt => opt.MapFrom(src => src.DataId))
        .ForMember(dest => dest.data_field_name, opt => opt.MapFrom(src => src.DataFieldName))
        .ForMember(dest => dest.data_field_value_type, opt => opt.MapFrom(src => src.DataFieldValueType))
        .ForMember(dest => dest.data_field_value, opt => opt.MapFrom(src => src.DataFieldValue))
        .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.Active))
        .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
        .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
        .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
        .ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser)
         ).ReverseMap();
        }
    }
}
