using AutoMapper;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.BL.Mappers
{
    public class AddRuleEngineMasterDataMapper:Profile 
    {
       
            public AddRuleEngineMasterDataMapper()
            {
                CreateMap<RulesEngineMasterData, AddRulesEngineMasterDataModel>()
                //.ForMember(dest => dest.RulesConditionValueID, opt => opt.MapFrom(src => src.Rules_Condition_Value_ID))
                .ForMember(dest => dest.masterdataname, opt => opt.MapFrom(src => src.master_data_name))
                .ForMember(dest => dest.masterdatatype, opt => opt.MapFrom(src => src.master_data_type))
                .ForMember(dest => dest.masterdatavalue, opt => opt.MapFrom(src => src.master_data_value))
                .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.Processed, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.MapRefIdentifier, opt => opt.MapFrom(src => src.Map_Ref_Identifier))
                //.ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
                //.ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
                .ReverseMap(); ;
            }
 
    }

    public class UpdateRuleEngineMasterDataMapper : Profile
    {

        public UpdateRuleEngineMasterDataMapper()
        {
            CreateMap<RulesEngineMasterData, UpdateRulesEngineMasterDataModel>()
            .ForMember(dest => dest.masterdataid, opt => opt.MapFrom(src => src.master_data_id))
            .ForMember(dest => dest.masterdataname, opt => opt.MapFrom(src => src.master_data_name))
            .ForMember(dest => dest.masterdatatype, opt => opt.MapFrom(src => src.master_data_type))
            .ForMember(dest => dest.masterdatavalue, opt => opt.MapFrom(src => src.master_data_value))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.Processed, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.MapRefIdentifier, opt => opt.MapFrom(src => src.Map_Ref_Identifier))
            .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
            //.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
           // .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
            .ReverseMap(); ;
        }

    }

    public class GetRuleEngineMasterDataMapper : Profile
    {

        public GetRuleEngineMasterDataMapper()
        {
            CreateMap<RulesEngineMasterData, GetRulesEngineMasterDataModel>()
            .ForMember(dest => dest.masterdataid, opt => opt.MapFrom(src => src.master_data_id))
            .ForMember(dest => dest.masterdataname, opt => opt.MapFrom(src => src.master_data_name))
            .ForMember(dest => dest.masterdatatype, opt => opt.MapFrom(src => src.master_data_type))
            .ForMember(dest => dest.masterdatavalue, opt => opt.MapFrom(src => src.master_data_value))
            .ForMember(dest => dest.MapRefIdentifier, opt => opt.MapFrom(src => src.Map_Ref_Identifier))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.active))
            .ForMember(dest => dest.Processed, opt => opt.MapFrom(src => src.Processed))
            .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
            .ReverseMap(); ;
        }

    }

}
