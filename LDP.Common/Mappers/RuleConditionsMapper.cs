using AutoMapper;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.BL.Mappers
{
    public class AddRuleConditionsMapper: Profile
    {
        public AddRuleConditionsMapper()
        {
            CreateMap<RuleCondition, AddRuleConditionModel>()
            .ForMember(dest => dest.RulesConditionTypeID, opt => opt.MapFrom(src => src.Rules_Condition_Type_ID))
            .ReverseMap(); ;
        }

    }

    public class UpdateRuleConditionsMapper : Profile
    {
        public UpdateRuleConditionsMapper()
        {
            CreateMap<RuleCondition, UpdateRuleConditionModel>()
            .ForMember(dest => dest.RulesConditionID, opt => opt.MapFrom(src => src.Rules_Condition_ID))
            .ForMember(dest => dest.RulesConditionTypeID, opt => opt.MapFrom(src => src.Rules_Condition_Type_ID))
            .ReverseMap(); ;
        }

    }

    public class GetRuleConditionsMapper : Profile
    {
        public GetRuleConditionsMapper()
        {
            CreateMap<RuleCondition, GetRuleConditionModel>()
            .ForMember(dest => dest.RulesConditionID, opt => opt.MapFrom(src => src.Rules_Condition_ID))
            .ForMember(dest => dest.RulesConditionTypeID, opt => opt.MapFrom(src => src.Rules_Condition_Type_ID))
            //.ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
            //.ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
            //.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
            //.ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
            .ReverseMap(); ;
        }

    }
}
