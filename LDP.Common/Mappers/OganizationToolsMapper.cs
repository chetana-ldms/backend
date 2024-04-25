using AutoMapper;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.BL.Mappers
{
    public class AddOganizationToolsMapper : Profile
    {
          //private IMapper _mapper;
            public AddOganizationToolsMapper()
            {
                CreateMap<LDP_APIs.DAL.Entities.OganizationTool, AddOrganizationToolsRequest>()
                .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.Tool_ID))
                .ForMember(dest => dest.OrgID, opt => opt.MapFrom(src => src.Org_ID))
               // .ForMember(dest => dest.ApiUrl, opt => opt.MapFrom(src => src.Api_Url))
                .ForMember(dest => dest.AuthKey, opt => opt.MapFrom(src => src.Auth_Key))
               // .ForMember(dest => dest.LastReadPKID, opt => opt.MapFrom(src => src.Last_Read_PKID))
               // .ForMember(dest => dest.ApiVerson, opt => opt.MapFrom(src => src.api_verson))
                //.ForMember(dest => dest.GetDataBatchSize, opt => opt.MapFrom(src => src.GetData_BatchSize))
              //  .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                // .ForMember(dest => dest.LastReadAlertDate, opt => opt.MapFrom(src => src.Last_Read_AlertDate))
                .ReverseMap(); ;
            }
    }

    public class UpdateOganizationToolsMapper : Profile
    {
        //private IMapper _mapper;
        public UpdateOganizationToolsMapper()
        {
            CreateMap<LDP_APIs.DAL.Entities.OganizationTool, UpdateOrganizationToolsRequest>()
            .ForMember(dest => dest.OrgToolID, opt => opt.MapFrom(src => src.Org_Tool_ID))
            .ForMember(dest => dest.OrgID, opt => opt.MapFrom(src => src.Org_ID))
            .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.Tool_ID))
            //.ForMember(dest => dest.ApiUrl, opt => opt.MapFrom(src => src.Api_Url))
            .ForMember(dest => dest.AuthKey, opt => opt.MapFrom(src => src.Auth_Key))
           // .ForMember(dest => dest.LastReadPKID, opt => opt.MapFrom(src => src.Last_Read_PKID))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
           // .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
            //.ForMember(dest => dest.ApiVerson, opt => opt.MapFrom(src => src.api_verson))
            //.ForMember(dest => dest.GetDataBatchSize, opt => opt.MapFrom(src => src.GetData_BatchSize))
            //.ForMember(dest => dest.LastReadAlertDate, opt => opt.MapFrom(src => src.Last_Read_AlertDate))
            .ReverseMap(); ;
        }
    }

    public class GetOganizationToolsMapper : Profile
    {
        //private IMapper _mapper;
        public GetOganizationToolsMapper()
        {
            CreateMap<OrganizationToolModel, OganizationTool>()
            .ForMember(dest => dest.Org_Tool_ID, opt => opt.MapFrom(src => src.OrgToolID))
            .ForMember(dest => dest.Org_ID, opt => opt.MapFrom(src => src.OrgID))
            .ForMember(dest => dest.Tool_ID, opt => opt.MapFrom(src => src.ToolID))
            //.ForMember(dest => dest.Api_Url, opt => opt.MapFrom(src => src.ApiUrl))
            .ForMember(dest => dest.Auth_Key, opt => opt.MapFrom(src => src.AuthKey))
            //.ForMember(dest => dest.Last_Read_PKID, opt => opt.MapFrom(src => src.LastReadPKID))
            //.ForMember(dest => dest.GetData_BatchSize, opt => opt.MapFrom(src => src.GetDataBatchSize))
            //.ForMember(dest => dest.api_verson, opt => opt.MapFrom(src => src.ApiVerson))
            .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
            //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
            .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
            .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            //.ForMember(dest => dest.Last_Read_AlertDate, opt => opt.MapFrom(src => src.LastReadAlertDate))
            .ReverseMap(); ;
        }
    }
}
