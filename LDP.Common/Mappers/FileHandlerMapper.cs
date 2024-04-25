using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;

namespace LDP.Common.Mappers
{
    public class FileUploadMapper : Profile
    {
        public FileUploadMapper()
        {
            CreateMap<AddFileUploadFileModel,ChannelUploadFile > ()
                   .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                   .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
                   .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
                   .ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate))
                   .ForMember(dest => dest.subitem_id, opt => opt.MapFrom(src => src.SubitemId))
                   .ReverseMap();
        }
    }

    public class GetFileUploadedFilesMapper : Profile
    {
        public GetFileUploadedFilesMapper()
        {
            CreateMap<GetChannelUploadFileModel, ChannelUploadFile>()
                    .ForMember(dest => dest.file_id, opt => opt.MapFrom(src => src.FileId))
                   .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                   .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
                   .ForMember(dest => dest.subitem_id, opt => opt.MapFrom(src => src.SubitemId))
                   .ForMember(dest => dest.file_name, opt => opt.MapFrom(src => src.FileName))
                   .ForMember(dest => dest.file_url, opt => opt.MapFrom(src => src.FileUrl))
                   .ForMember(dest => dest.file_physical_path, opt => opt.MapFrom(src => src.FilePhysicalPath))
                   .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.Active))
                   .ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate))
                   .ForMember(dest => dest.created_user, opt => opt.MapFrom(src => src.CreatedUser))
                   //.ForMember(dest => dest.modified_user, opt => opt.MapFrom(src => src.ModifiedUser))
                   //.ForMember(dest => dest.modified_date, opt => opt.MapFrom(src => src.ModifiedDate))

                   .ReverseMap();
        }
    }


    
}
