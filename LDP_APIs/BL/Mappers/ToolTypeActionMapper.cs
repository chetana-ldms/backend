using AutoMapper;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.BL.Mappers
{
    public class AddToolTypeActionMapper:Profile
    {
        public AddToolTypeActionMapper()
        {
            CreateMap<AddToolTypeActionRequest,ToolTypeAction>()
            .ForMember(dest => dest.Tool_Type_ID, opt => opt.MapFrom(src => src.ToolTypeID))
            .ForMember(dest => dest.Tool_Action, opt => opt.MapFrom(src => src.ToolAction))
            .ForMember(dest => dest.Created_user, opt => opt.MapFrom(src => src.CreatedUser))
            .ForMember(dest => dest.Created_date, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest =>dest.active, opt => opt.MapFrom(src => 1))
            .ReverseMap(); ;
        }
    }

    public class UpdateToolTypeActionMapper : Profile
    {
        public UpdateToolTypeActionMapper()
        {
            CreateMap<ToolTypeAction, UpdateToolTypeActionRequest>()
           .ForMember(dest => dest.ToolTypeActionID, opt => opt.MapFrom(src => src.Tool_Type_Action_ID))
            .ForMember(dest => dest.ToolTypeID, opt => opt.MapFrom(src => src.Tool_Type_ID))
            .ForMember(dest => dest.ToolAction, opt => opt.MapFrom(src => src.Tool_Action))
             .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_user))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_date))
            .ReverseMap(); ;
        }
    }

    public class GetToolTypeActionMapper : Profile
    {
        public GetToolTypeActionMapper()
        {
            CreateMap<ToolTypeAction, GetToolTypeActionModel>()
            .ForMember(dest => dest.ToolTypeActionID, opt => opt.MapFrom(src => src.Tool_Type_Action_ID))
            .ForMember(dest => dest.ToolTypeID, opt => opt.MapFrom(src => src.Tool_Type_ID))
            .ForMember(dest => dest.ToolAction, opt => opt.MapFrom(src => src.Tool_Action))

            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_user))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_date))
            .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_user))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_date))
            .ReverseMap(); ;
        }
    }
}
