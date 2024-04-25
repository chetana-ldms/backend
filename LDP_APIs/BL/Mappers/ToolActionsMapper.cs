using AutoMapper;
using LDP.Common.Model;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.BL.Mappers
{
    public class GetToolActionsMapper:Profile
    {
        public GetToolActionsMapper()
        {
            CreateMap<LDPToolActions, GetToolActionModel>()
            .ForMember(dest => dest.ToolActionID, opt => opt.MapFrom(src => src.Tool_Action_ID))
            .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.Tool_ID))
            .ForMember(dest => dest.ToolTypeActionID, opt => opt.MapFrom(src => src.Tool_Type_Action_ID))


            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_user))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_date))

            .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_user))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_date))

            .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.active))
            .ForMember(dest => dest.Processed, opt => opt.MapFrom(src => src.Processed))
            .ReverseMap(); ;
        }
    }

    public class AddToolActionsMapper : Profile
    {
        public AddToolActionsMapper()
        {
            CreateMap<AddToolActionModel,LDPToolActions >()
            //.ForMember(dest => dest.ToolActionID, opt => opt.MapFrom(src => src.Tool_Action_ID))
            .ForMember(dest => dest.Tool_ID, opt => opt.MapFrom(src => src.ToolID))
            .ForMember(dest => dest.Tool_Type_Action_ID, opt => opt.MapFrom(src => src.ToolTypeActionID))


            .ForMember(dest => dest.Created_user, opt => opt.MapFrom(src => src.CreatedUser))
            .ForMember(dest => dest.Created_user, opt => opt.MapFrom(src => src.CreatedDate))

           // .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_user))
           // .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_date))

            .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            .ReverseMap(); ;
        }
    }

    public class UpdateToolActionsMapper : Profile
    {
        public UpdateToolActionsMapper()
        {
            CreateMap<LDPToolActions, UpdateToolActionModel>()
            .ForMember(dest => dest.ToolActionID, opt => opt.MapFrom(src => src.Tool_Action_ID))
            .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.Tool_ID))
            .ForMember(dest => dest.ToolTypeActionID, opt => opt.MapFrom(src => src.Tool_Type_Action_ID))


            //.ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_user))
            //.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_date))

            .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_user))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_date))

           // .ForMember(dest => 1, opt => opt.MapFrom(src => src.active))
            .ReverseMap(); ;
        }
    }
}
