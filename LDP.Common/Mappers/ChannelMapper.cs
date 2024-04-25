using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;

namespace LDP.Common.Mappers
{
    public class AddChannelMapper: Profile
    {
        public AddChannelMapper() 
        {
            CreateMap<AddChannelRequest, LDCChannel>()
                .ForMember(dest => dest.channel_Name, opt => opt.MapFrom(src => src.ChannelName))
                .ForMember(dest => dest.channel_Description, opt => opt.MapFrom(src => src.ChannelDescription))
                .ForMember(dest => dest.active, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.display_order, opt => opt.MapFrom(src => src.DisplayOrder))
                .ForMember(dest => dest.Created_date, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.msteams_teamsid, opt => opt.MapFrom(src => src.MsTeamsTeamsId))
                .ForMember(dest => dest.msteams_channelid, opt => opt.MapFrom(src => src.MsTeamsChannelId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId)).ReverseMap();

        }
    }

    public class UpdateChannelMapper : Profile
    {
        public UpdateChannelMapper()
        {
            CreateMap<UpdateChannelRequest, LDCChannel>()
                .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
                .ForMember(dest => dest.channel_Name, opt => opt.MapFrom(src => src.ChannelName))
                .ForMember(dest => dest.channel_Description, opt => opt.MapFrom(src => src.ChannelDescription))
                .ForMember(dest => dest.channel_type_id, opt => opt.MapFrom(src => src.ChannelTypeId))
                .ForMember(dest => dest.display_order, opt => opt.MapFrom(src => src.DisplayOrder))
                .ForMember(dest => dest.Modified_date, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.msteams_teamsid, opt => opt.MapFrom(src => src.MsTeamsTeamsId))
                .ForMember(dest => dest.msteams_channelid, opt => opt.MapFrom(src => src.MsTeamsChannelId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId)).ReverseMap();
        }
    }
    public class GetChannelMapper : Profile
    {
        public GetChannelMapper()
        {
            CreateMap<LDCChannel, GetChannelModel>()
            .ForMember(dest => dest.ChannelId, opt => opt.MapFrom(src => src.channel_id))
            .ForMember(dest => dest.ChannelName, opt => opt.MapFrom(src => src.channel_Name))
            .ForMember(dest => dest.ChannelDescription, opt => opt.MapFrom(src => src.channel_Description))
            .ForMember(dest => dest.ChannelTypeId, opt => opt.MapFrom(src => src.channel_type_id))
            .ForMember(dest => dest.ChannelTypeName, opt => opt.MapFrom(src => src.channel_type_name))
            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_user))
            .ForMember(dest => dest.ModifiedUser, opt => opt.MapFrom(src => src.Modified_user))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.active))
            .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(src => src.display_order))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modified_date))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_date))
            .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => src.deleted_date))
            .ForMember(dest => dest.DeletedUser, opt => opt.MapFrom(src => src.deleted_user))
            .ForMember(dest => dest.MsTeamsTeamsId, opt => opt.MapFrom(src => src.msteams_teamsid))
            .ForMember(dest => dest.MsTeamsChannelId, opt => opt.MapFrom(src => src.msteams_channelid))
            .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id)).ReverseMap();
         }
    }

}
