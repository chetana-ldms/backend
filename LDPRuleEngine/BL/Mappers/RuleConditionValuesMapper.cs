using AutoMapper;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.BL.Mappers
{
    public class AddRuleConditionValuesMapper : Profile
    {
        public AddRuleConditionValuesMapper()
        {
            CreateMap<RuleConditionValue, AddRuleConditionValueModel>()
            //.ForMember(dest => dest.RulesConditionID, opt => opt.MapFrom(src => src.Rules_Condition_ID))
            .ForMember(dest => dest.RulesConditionValue, opt => opt.MapFrom(src => src.Rules_Condition_Value))
            //.ForMember(dest => dest.RulesConditionTypeID, opt => opt.MapFrom(src => src.Rules_Condition_Type_ID))
            // .ForMember(dest => 1, opt => opt.MapFrom(src => 1))
            //.ForMember(dest => dest.Processed, opt => opt.MapFrom(src =>0))
            //.ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
            //.ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
            //.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
            //.ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
            .ReverseMap(); ;
        }
    }

    public class UpdateRuleConditionValuesMapper : Profile
    {
        public UpdateRuleConditionValuesMapper()
        {
            CreateMap<RuleConditionValue, UpdateRuleConditionValueModel>()
            // .ForMember(dest => dest.RulesConditionID, opt => opt.MapFrom(src => src.Rules_Condition_ID))
            .ForMember(dest => dest.RulesConditionValue, opt => opt.MapFrom(src => src.Rules_Condition_Value))
            //.ForMember(dest => dest.RulesConditionTypeID, opt => opt.MapFrom(src => src.Rules_Condition_Type_ID))
            //.ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            //.ForMember(dest => dest.Processed, opt => opt.MapFrom(src => 0))
            // .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
            // .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
            //.ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.Created_Date))
            // .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.Created_User))
            .ReverseMap(); ;
        }
    }

    public class GetRuleConditionValuesMapper : Profile
    {
        public GetRuleConditionValuesMapper()
        {
            CreateMap<RuleConditionValue, GetRuleConditionValueModel>()
            .ForMember(dest => dest.RulesConditionValueID, opt => opt.MapFrom(src => src.Rules_Condition_Value_ID))
            // .ForMember(dest => dest.rul, opt => opt.MapFrom(src => src.Rules_Condition_ID))
            .ForMember(dest => dest.RulesConditionValue, opt => opt.MapFrom(src => src.Rules_Condition_Value))
            // .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.active))
            //.ForMember(dest => dest.Processed, opt => opt.MapFrom(src => src.Processed))
            .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
            .ReverseMap(); ;
        }
    }

}
