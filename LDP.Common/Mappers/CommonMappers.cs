using AutoMapper;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model.Common;
using LDP.Common.Requests.Common;
namespace LDP.Common.Mappers
{
    public class LdcActivitiesProfile : Profile
    {
        public LdcActivitiesProfile()
        {
            CreateMap<GetActivityModel, LdcActivity>()
                .ForMember(dest => dest.activity_id, opt => opt.MapFrom(src => src.ActivityId))
                .ForMember(dest => dest.activity_type_id, opt => opt.MapFrom(src => src.ActivityTypeId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                .ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.AlertId))
                .ForMember(dest => dest.incident_id, opt => opt.MapFrom(src => src.IncidentId))
                .ForMember(dest => dest.activity_date, opt => opt.MapFrom(src => src.ActivityDate))
                .ForMember(dest => dest.created_user_id, opt => opt.MapFrom(src => src.CreatedUserId))
                .ForMember(dest => dest.created_user, opt => opt.MapFrom(src => src.CreateedUser))
                .ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.primary_description, opt => opt.MapFrom(src => src.PrimaryDescription))
                .ForMember(dest => dest.secondary_description, opt => opt.MapFrom(src => src.SecondaryDescription))
                .ForMember(dest => dest.source, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.activity_exist_tool_and_ldc, opt => opt.MapFrom(src => src.ActivityExistToolAndLDC))
                .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.ToolId))
                .ReverseMap();
        }
    }

    public class AddActivitiesProfile : Profile
    {
        public AddActivitiesProfile()
        {
            CreateMap<AddActivityRequest, LdcActivity>()
                .ForMember(dest => dest.activity_type_id, opt => opt.MapFrom(src => src.ActivityTypeId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                .ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.AlertId))
                .ForMember(dest => dest.incident_id, opt => opt.MapFrom(src => src.IncidentId))
                .ForMember(dest => dest.activity_date, opt => opt.MapFrom(src => src.ActivityDate.Value.ToUniversalTime()))
                .ForMember(dest => dest.created_user_id, opt => opt.MapFrom(src => src.CreatedUserId))
                .ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate.Value.ToUniversalTime()))
                .ForMember(dest => dest.primary_description, opt => opt.MapFrom(src => src.PrimaryDescription))
                .ForMember(dest => dest.secondary_description, opt => opt.MapFrom(src => src.SecondaryDescription))
                .ForMember(dest => dest.source, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.activity_exist_tool_and_ldc, opt => opt.MapFrom(src => src.ActivityExistToolAndLDC))
                .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.ToolId))
                .ReverseMap();
        }
    }
    public class LdcActivityTypesProfile : Profile
    {
        public LdcActivityTypesProfile()
        {
            CreateMap<ActivityTypeModel, ActivityType>()
                .ForMember(dest => dest.activity_type_id, opt => opt.MapFrom(src => src.ActivityTypeId))
                .ForMember(dest => dest.type_name, opt => opt.MapFrom(src => src.TypeName))
                .ForMember(dest => dest.template, opt => opt.MapFrom(src => src.Template))
                .ForMember(dest => dest.createed_user, opt => opt.MapFrom(src => src.CreatedUser))
                .ForMember(dest => dest.Created_date, opt => opt.MapFrom(src => src.CreatedDate)).ReverseMap();
        }

    }
 }
