using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{
    public class OrganizationToolActionMapper : Profile
    {
        public OrganizationToolActionMapper()
        {
            CreateMap<OrganizationToolActionModel, OrganizationToolAction>()
            .ForMember(dest => dest.org_tool_action_id, opt => opt.MapFrom(src => src.OrgToolActionId))
            .ForMember(dest => dest.org_tool_id, opt => opt.MapFrom(src => src.OrgToolId))
            .ForMember(dest => dest.tool_action_id, opt => opt.MapFrom(src => src.ToolActionId))
            .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.ToolId))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            .ForMember(dest => dest.auth_key, opt => opt.MapFrom(src => src.AuthKey))
            .ForMember(dest => dest.api_url, opt => opt.MapFrom(src => src.ApiUrl))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.Active))
            .ForMember(dest => dest.api_verson, opt => opt.MapFrom(src => src.ApiVerson))
            .ForMember(dest => dest.GetData_BatchSize, opt => opt.MapFrom(src => src.GetDataBatchSize))
            .ForMember(dest => dest.Last_Read_AlertDate, opt => opt.MapFrom(src => src.LastReadAlertDate)).ReverseMap();

        }
    }

    public class AddOrganizationToolActionMapper : Profile
    {
        public AddOrganizationToolActionMapper()
        {
            CreateMap<AddOrganizationToolActionModel, OrganizationToolAction>()
            //.ForMember(dest => dest.org_tool_action_id, opt => opt.MapFrom(src => src.OrgToolActionId))
            //.ForMember(dest => dest.org_tool_id, opt => opt.MapFrom(src => src.OrgToolId))
            .ForMember(dest => dest.tool_action_id, opt => opt.MapFrom(src => src.ToolActionId))
           // .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.ToolId))
            //.ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            //.ForMember(dest => dest.auth_key, opt => opt.MapFrom(src => src.AuthKey))
            .ForMember(dest => dest.api_url, opt => opt.MapFrom(src => src.ApiUrl))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.api_verson, opt => opt.MapFrom(src => src.ApiVerson))
            //.ForMember(dest => dest.GetData_BatchSize, opt => opt.MapFrom(src => src.GetDataBatchSize))
            //.ForMember(dest => dest.Last_Read_AlertDate, opt => opt.MapFrom(src => src.LastReadAlertDate))
            .ReverseMap();

        }
    }

    public class UpdateOrganizationToolActionMapper : Profile
    {
        public UpdateOrganizationToolActionMapper()
        {
            CreateMap<UpdateOrganizationToolActionModel, OrganizationToolAction>()
            .ForMember(dest => dest.org_tool_action_id, opt => opt.MapFrom(src => src.OrgToolActionId))
            //.ForMember(dest => dest.org_tool_id, opt => opt.MapFrom(src => src.OrgToolId))
            .ForMember(dest => dest.tool_action_id, opt => opt.MapFrom(src => src.ToolActionId))
            // .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.ToolId))
            //.ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))ethn.luy
            //.ForMember(dest => dest.auth_key, opt => opt.MapFrom(src => src.AuthKey))
            .ForMember(dest => dest.api_url, opt => opt.MapFrom(src => src.ApiUrl))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.api_verson, opt => opt.MapFrom(src => src.ApiVerson))
            //.ForMember(dest => dest.GetData_BatchSize, opt => opt.MapFrom(src => src.GetDataBatchSize))
            //.ForMember(dest => dest.Last_Read_AlertDate, opt => opt.MapFrom(src => src.LastReadAlertDate))
            .ReverseMap();

        }
    }
}
