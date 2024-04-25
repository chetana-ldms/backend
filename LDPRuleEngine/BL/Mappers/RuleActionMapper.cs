using AutoMapper;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.BL.Mappers
{
    public class AddRuleActionMapper : Profile
    {
        public AddRuleActionMapper()
        {
                    CreateMap<AddRuleActionModel,RuleAction>()
            // .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.Tool_ID))
            .ForMember(dest => dest.rule_Action_Name, opt => opt.MapFrom(src => src.ruleActionName))
            .ForMember(dest => dest.Tool_Type_ID, opt => opt.MapFrom(src => src.ToolTypeID))
            .ForMember(dest => dest.Tool_ID, opt => opt.MapFrom(src => src.ToolID))
            .ForMember(dest => dest.Tool_Action_ID, opt => opt.MapFrom(src => src.ToolActionID))
            .ForMember(dest => dest.Rule_Generel_Action_ID, opt => opt.MapFrom(src => src.RuleGenerelActionID))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            //.ForMember(dest => dest.pro, opt => opt.MapFrom(src => 0))
            //.ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.Modified_date))
            //.ForMember(dest => dest.UpdatedByUser, opt => opt.MapFrom(src => src.Modified_date))
            .ForMember(dest => dest.Created_date, opt => opt.MapFrom(src => src.Createddate))
            .ForMember(dest => dest.Created_user, opt => opt.MapFrom(src => src.Createduser))
            .ReverseMap(); ;
    
        }
}

    public class UpdateRuleActionMapper : Profile
    {
        public UpdateRuleActionMapper()
        {
            CreateMap<UpdateRuleActionModel,RuleAction>()
            .ForMember(dest => dest.rule_Action_ID, opt => opt.MapFrom(src => src.ruleActionID))
            .ForMember(dest => dest.rule_Action_Name, opt => opt.MapFrom(src => src.ruleActionName))
            .ForMember(dest => dest.Tool_Type_ID, opt => opt.MapFrom(src => src.ToolTypeID))
            .ForMember(dest => dest.Tool_ID, opt => opt.MapFrom(src => src.ToolID))
            .ForMember(dest => dest.Tool_Action_ID, opt => opt.MapFrom(src => src.ToolActionID))
            .ForMember(dest => dest.Rule_Generel_Action_ID, opt => opt.MapFrom(src => src.RuleGenerelActionID))
            .ForMember(dest => dest.active, opt => opt.Ignore())
            .ForMember(dest => dest.Processed, opt => opt.Ignore())
            .ForMember(dest => dest.Modified_date, opt => opt.MapFrom(src => src.Modifieddate))
            .ForMember(dest => dest.Modified_user, opt => opt.MapFrom(src => src.Modifieduser))
            //.ForMember(dest => dest.Created_date, opt => opt.MapFrom(src => src.Createddate))
           // .ForMember(dest => dest.Created_user, opt => opt.MapFrom(src => src.Createduser))
    .ReverseMap(); ;

        }
    }

    public class GetRuleActionMapper : Profile
    {
        public GetRuleActionMapper()
        {
            CreateMap<RuleAction, GetRuleActionModel>()
            .ForMember(dest => dest.ruleActionID, opt => opt.MapFrom(src => src.rule_Action_ID))
            .ForMember(dest => dest.ruleActionName, opt => opt.MapFrom(src => src.rule_Action_Name))
            .ForMember(dest => dest.ToolTypeID, opt => opt.MapFrom(src => src.Tool_Type_ID))
            .ForMember(dest => dest.ToolActionID, opt => opt.MapFrom(src => src.Tool_Action_ID))
            .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.Tool_ID))
            .ForMember(dest => dest.RuleGenerelActionID, opt => opt.MapFrom(src => src.Rule_Generel_Action_ID))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.active))
            .ForMember(dest => dest.Processed, opt => opt.MapFrom(src => src.Processed))
            .ForMember(dest => dest.Modifieddate, opt => opt.MapFrom(src => src.Modified_date))
            .ForMember(dest => dest.Modifieduser, opt => opt.MapFrom(src => src.Modified_user))
            .ForMember(dest => dest.Createddate, opt => opt.MapFrom(src => src.Created_date))
            .ForMember(dest => dest.Createduser, opt => opt.MapFrom(src => src.Created_user))
    .ReverseMap(); ;

        }
    }
}
