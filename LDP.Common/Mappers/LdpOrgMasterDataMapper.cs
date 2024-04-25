using AutoMapper;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{

    public class LdpOrgMasterDatMapper : Profile
    {
        public LdpOrgMasterDatMapper()
        {
            CreateMap<OrgMasterDataModel, OrgMasterData>()
                .ForMember(dest => dest.org_masterdata_id, opt => opt.MapFrom(src => src.OrgMasterDataId))
                .ForMember(dest => dest.data_type, opt => opt.MapFrom(src => src.DataType))
                .ForMember(dest => dest.data_id, opt => opt.MapFrom(src => src.DataId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                .ForMember(dest => dest.org_data_id, opt => opt.MapFrom(src => src.OrgDataId))
                .ForMember(dest => dest.org_data_type, opt => opt.MapFrom(src => src.OrgDataType))
                .ForMember(dest => dest.org_data_name, opt => opt.MapFrom(src => src.OrgDataName))
                .ForMember(dest => dest.org_data_value, opt => opt.MapFrom(src => src.OrgDataValue))
                .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
                .ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser)
                ).ReverseMap();
        }
    }
}
