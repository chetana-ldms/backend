using AutoMapper;
using LDP.Common.BL;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{

    public class NofificationDetailMapper : Profile
    {
        public NofificationDetailMapper()
        {
            CreateMap<NotificaionDetail, NotificationDetailModel>()
                .ForMember(dest => dest.NotificationId, opt => opt.MapFrom(src => src.notification_id))
                .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
                .ForMember(dest => dest.NotificationFeature, opt => opt.MapFrom(src => src.notification_feature))
                .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.notification_type))
                .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.message_id))
                .ForMember(dest => dest.NotificationDate, opt => opt.MapFrom(src => src.notification_Date))
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.from))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.to))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.subject))
                .ForMember(dest => dest.BodyContent, opt => opt.MapFrom(src => src.body_content))
                .ForMember(dest => dest.ReplyDate, opt => opt.MapFrom(src => src.reply_Date))
                .ForMember(dest => dest.ReplyFrom, opt => opt.MapFrom(src => src.reply_from))
                .ForMember(dest => dest.ReplyContent, opt => opt.MapFrom(src => src.reply_content))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_Date))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_Date))
                .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_User))
                .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_User));
        }
     }
 }
