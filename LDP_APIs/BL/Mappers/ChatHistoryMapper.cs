using AutoMapper;
using LDP_APIs.APIResponse;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.BL.Mappers
{

    public class GetChatHistoryProfile : Profile
    {
        public GetChatHistoryProfile()
        {

            CreateMap<ChatHistory, GetChatMessageHistoryModel>()
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.chat_id))
                .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
                .ForMember(dest => dest.FromUserID, opt => opt.MapFrom(src => src.from_user_id))
                .ForMember(dest => dest.FromUserName, opt => opt.MapFrom(src => src.from_user_name))
                .ForMember(dest => dest.ToUserID, opt => opt.MapFrom(src => src.to_user_id))
                .ForMember(dest => dest.ToUserName, opt => opt.MapFrom(src => src.to_user_name))
                .ForMember(dest => dest.ChatMessage, opt => opt.MapFrom(src => src.chat_message))
                .ForMember(dest => dest.ChatSubject, opt => opt.MapFrom(src => src.chat_subject))
                .ForMember(dest => dest.ChatSubject, opt => opt.MapFrom(src => src.chat_subject))
                .ForMember(dest => dest.SubjectRefID, opt => opt.MapFrom(src => src.subject_refid))
                .ForMember(dest => dest.AttachmentUrl, opt => opt.MapFrom(src => src.attachment_url))
                .ForMember(dest => dest.AttachmentPhysicalPath, opt => opt.MapFrom(src => src.attachment_physical_path))
                 .ForMember(dest => dest.MessageType, opt => opt.MapFrom(src => src.message_type))
                .ForMember(dest => dest.MesssageDate, opt => opt.MapFrom(src => src.created_date))
                .ReverseMap();
        }
    }
    public class AddChatHistoryProfile : Profile
    {
        public AddChatHistoryProfile()
        {
            CreateMap<ChatHistory, AddChatMessageModel>()
                .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
                .ForMember(dest => dest.FromUserID, opt => opt.MapFrom(src => src.from_user_id))
                .ForMember(dest => dest.ToUserID, opt => opt.MapFrom(src => src.to_user_id))
                .ForMember(dest => dest.ChatMessage, opt => opt.MapFrom(src => src.chat_message))
                .ForMember(dest => dest.ChatSubject, opt => opt.MapFrom(src => src.chat_subject))
                .ForMember(dest => dest.SubjectRefID, opt => opt.MapFrom(src => src.subject_refid))
                //.ForMember(dest => dest.ChannelId, opt => opt.MapFrom(src => src.channel_id))
                .ForMember(dest => dest.MesssageDate, opt => opt.MapFrom(src => src.created_date))
                .ReverseMap();
        }
    }
}