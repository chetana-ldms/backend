using AutoMapper;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.BL.Mappers
{
    public class AddRuleMapper : Profile
    {
        public AddRuleMapper()
        {
            CreateMap<AddLDPRuleModel, LDPRule>()
            .ForMember(dest => dest.Rule_Name, opt => opt.MapFrom(src => src.RuleName))
            .ForMember(dest => dest.Rule_Catagory_ID, opt => opt.MapFrom(src => src.RuleCatagoryID))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.Processed, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            .ReverseMap(); ;
        }
    }

        public class UpdateRuleMapper : Profile
        {
            public UpdateRuleMapper()
            {
                CreateMap<LDPRule, UPdateLDPRuleModel>()
                .ForMember(dest => dest.RuleID, opt => opt.MapFrom(src => src.Rule_ID))
                .ForMember(dest => dest.RuleName, opt => opt.MapFrom(src => src.Rule_Name))
                .ForMember(dest => dest.RuleCatagoryID, opt => opt.MapFrom(src => src.Rule_Catagory_ID))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
                .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
                //.ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
                .ReverseMap(); ;
            }
        }
        public class GetRuleMapper : Profile
        {
            public GetRuleMapper()
            {
                CreateMap<LDPRule, GetLDPRuleModel>()
                .ForMember(dest => dest.RuleID, opt => opt.MapFrom(src => src.Rule_ID))
                .ForMember(dest => dest.RuleName, opt => opt.MapFrom(src => src.Rule_Name))
                .ForMember(dest => dest.RuleCatagoryID, opt => opt.MapFrom(src => src.Rule_Catagory_ID))
                .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.active))
                //.ForMember(dest => dest.Processed, opt => opt.MapFrom(src => src.Processed))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
                .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
                .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => src.deleted_date))
                .ForMember(dest => dest.DeletedUser, opt => opt.MapFrom(src => src.deleted_user))
                .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
                .ReverseMap(); ;
            }
        }
    
}
