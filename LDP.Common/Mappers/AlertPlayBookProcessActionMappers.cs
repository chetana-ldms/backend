using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{
    public class GetAlertPlayBookProcessActionMapper:Profile
    {
        public GetAlertPlayBookProcessActionMapper()
        {
            CreateMap<AlertPlayBookProcessAction, GetAlertPlayBookProcessActionModel>()
                .ForMember(dest => dest.alertplaybooksprocessactionid, opt => opt.MapFrom(src => src.alert_playbooks_process_action_id))
                .ForMember(dest => dest.alertplaybooksprocessid, opt => opt.MapFrom(src => src.alert_playbooks_process_id))
                .ForMember(dest => dest.playbookid, opt => opt.MapFrom(src => src.play_book_id))
                .ForMember(dest => dest.tooltypeid, opt => opt.MapFrom(src => src.tool_type_id))
                .ForMember(dest => dest.toolid, opt => opt.MapFrom(src => src.tool_id))
                .ForMember(dest => dest.toolactionid, opt => opt.MapFrom(src => src.tool_action_id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
                .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date)
               ).ReverseMap();

        }

      
    }
    public class AddAlertPlayBookProcessActionMapper : Profile
    {
        public AddAlertPlayBookProcessActionMapper()
        {
            CreateMap<AlertPlayBookProcessAction, AddAlertPlayBookProcessActionModel>()
               .ForMember(dest => dest.playbookid, opt => opt.MapFrom(src => src.play_book_id))
               .ForMember(dest => dest.tooltypeid, opt => opt.MapFrom(src => src.tool_type_id))
               .ForMember(dest => dest.toolid, opt => opt.MapFrom(src => src.tool_id))
               .ForMember(dest => dest.toolactionid, opt => opt.MapFrom(src => src.tool_action_id)
             ).ReverseMap();

        }

    }
    public class UpdateAlertPlayBookProcessActionMapper : Profile
    {
        public UpdateAlertPlayBookProcessActionMapper()
        {
            CreateMap<AlertPlayBookProcessAction, UpdateAlertPlayBookProcessActionModel>()
               .ForMember(dest => dest.alertplaybooksprocessactionid, opt => opt.MapFrom(src => src.alert_playbooks_process_action_id))
               .ForMember(dest => dest.alertplaybooksprocessactionid, opt => opt.MapFrom(src => src.alert_playbooks_process_id))
               .ForMember(dest => dest.playbookid, opt => opt.MapFrom(src => src.play_book_id))
               .ForMember(dest => dest.tooltypeid, opt => opt.MapFrom(src => src.tool_type_id))
               .ForMember(dest => dest.toolid, opt => opt.MapFrom(src => src.tool_id))
               .ForMember(dest => dest.toolactionid, opt => opt.MapFrom(src => src.tool_action_id)
               //)
              // .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
               //.ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
              // .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User))
              // .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date)
              ).ReverseMap();

        }

    }
}
