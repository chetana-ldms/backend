using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{
    public class GetChannelSubItemMapper : Profile
    {
        public GetChannelSubItemMapper()
        {
            CreateMap<GetChannelSubItemModel, ChannelSubItem>()
           .ForMember(dest => dest.channel_subitem_id, opt => opt.MapFrom(src => src.ChannelSubItemId))
           .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
           .ForMember(dest => dest.channel_subitem_Name, opt => opt.MapFrom(src => src.ChannelSubItemName))
           .ForMember(dest => dest.api_url, opt => opt.MapFrom(src => src.ApiUrl))
           .ForMember(dest => dest.document_url, opt => opt.MapFrom(src => src.DocumentUrl))
           .ForMember(dest => dest.channel_subitem_Description, opt => opt.MapFrom(src => src.ChannelSubItemDescription))
           .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.Active))
           .ForMember(dest => dest.Created_date, opt => opt.MapFrom(src => src.CreatedDate))
           .ForMember(dest => dest.Modified_date, opt => opt.MapFrom(src => src.ModifiedDate))
           .ForMember(dest => dest.Created_user, opt => opt.MapFrom(src => src.CreatedUser))
           .ForMember(dest => dest.Modified_user, opt => opt.MapFrom(src => src.ModifiedUser))
            .ReverseMap();

        }
    }

    public class AddChannelSubItemMapper : Profile
    {
        public AddChannelSubItemMapper()
        {
            CreateMap<AddChannelSubItemModel, ChannelSubItem>()
           .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
           .ForMember(dest => dest.channel_subitem_Name, opt => opt.MapFrom(src => src.ChannelSubItemName))
           .ForMember(dest => dest.api_url, opt => opt.MapFrom(src => src.ApiUrl))
           .ForMember(dest => dest.document_url, opt => opt.MapFrom(src => src.DocumentUrl))
           .ForMember(dest => dest.channel_subitem_Description, opt => opt.MapFrom(src => src.ChannelSubItemDescription))
           .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
           .ForMember(dest => dest.Created_date, opt => opt.MapFrom(src => src.CreatedDate))
            .ReverseMap();
        }
    }

    public class UpdateChannelSubItemMapper : Profile
    {
        public UpdateChannelSubItemMapper()
        {
            CreateMap<UpdateChannelSubItemModel, ChannelSubItem>()
           .ForMember(dest => dest.channel_subitem_id, opt => opt.MapFrom(src => src.ChannelSubItemId))
           .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
           .ForMember(dest => dest.channel_subitem_Name, opt => opt.MapFrom(src => src.ChannelSubItemName))
           .ForMember(dest => dest.api_url, opt => opt.MapFrom(src => src.ApiUrl))
           .ForMember(dest => dest.document_url, opt => opt.MapFrom(src => src.DocumentUrl))
           .ForMember(dest => dest.channel_subitem_Description, opt => opt.MapFrom(src => src.ChannelSubItemDescription))
           .ForMember(dest => dest.Modified_date, opt => opt.MapFrom(src => src.ModifiedDate))
           .ReverseMap();
        }
    }

}
