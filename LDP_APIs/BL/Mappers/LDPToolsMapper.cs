using AutoMapper;
using LDP_APIs.APIResponse;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.BL.Mappers
{
    
     public class AddLDPToolsMapper : Profile
    {
        //private IMapper _mapper;
        public AddLDPToolsMapper()
        {
            CreateMap<LDP_APIs.DAL.Entities.LDPTool, LDPToolRequest>()
            .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.Tool_Name))
            .ForMember(dest => dest.ToolType, opt => opt.MapFrom(src => src.Tool_Type))
            .ForMember(dest => dest.ToolTypeID, opt => opt.MapFrom(src => src.Tool_Type_ID))
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.Created_user))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_date))
            .ReverseMap(); ;
        }

        
     }

    public class UpdateLDPToolsMapper : Profile
    {
        //private IMapper _mapper;
        public UpdateLDPToolsMapper()
        {
            CreateMap<LDP_APIs.DAL.Entities.LDPTool, UpdateLDPToolRequest>()
             .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.Tool_ID))
            .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.Tool_Name))
            .ForMember(dest => dest.ToolType, opt => opt.MapFrom(src => src.Tool_Type))
            .ForMember(dest => dest.ToolTypeID, opt => opt.MapFrom(src => src.Tool_Type_ID))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.Modified_date))
            .ForMember(dest => dest.UpdatedByUser, opt => opt.MapFrom(src => src.Modified_user))
             .ReverseMap(); ;
        }


    }

    public class GetLDPToolsMapper : Profile
    {
        public GetLDPToolsMapper()
        {
            CreateMap<LDP_APIs.DAL.Entities.LDPTool, GetLDPTool>()
            .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.Tool_ID))
            .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.Tool_Name))
            .ForMember(dest => dest.ToolType, opt => opt.MapFrom(src => src.Tool_Type))
            .ForMember(dest => dest.ToolTypeID, opt => opt.MapFrom(src => src.Tool_Type_ID))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.Modified_date))
            .ForMember(dest => dest.UpdatedByUser, opt => opt.MapFrom(src => src.Modified_date))
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.Created_user))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_date))
            .ForMember(dest => dest.DeletedUser, opt => opt.MapFrom(src => src.deleted_user))
            .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => src.deleted_date))
            .ReverseMap(); ;
        }


    }
}
