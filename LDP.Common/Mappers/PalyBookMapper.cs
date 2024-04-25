using AutoMapper;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.BL.Mappers
{
    public class AddPlayBookMapper : Profile
    {
        public AddPlayBookMapper()
        {
            CreateMap<AddPlayBookModel, PlayBook>()
            .ForMember(dest => dest.Play_Book_Name, opt => opt.MapFrom(src => src.PlayBookName))
            .ForMember(dest => dest.Alert_Catogory, opt => opt.MapFrom(src => src.AlertCatogory))
            .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            .ReverseMap(); 
        }
    }

    public class UpdatePlayBookMapper : Profile
    {
        public UpdatePlayBookMapper()
        {
            CreateMap<UpdatePlayBookModel, PlayBook>()
            .ForMember(dest => dest.Play_Book_ID, opt => opt.MapFrom(src => src.PlayBookID))
            .ForMember(dest => dest.Play_Book_Name, opt => opt.MapFrom(src => src.PlayBookName))
            .ForMember(dest => dest.Alert_Catogory, opt => opt.MapFrom(src => src.AlertCatogory))
            .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks))
           // .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
            //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            .ReverseMap(); 
        }
    }

    public class GetPlayBookMapper : Profile
    {
        public GetPlayBookMapper()
        {
            CreateMap<GetPlayBookModel, PlayBook>()
            .ForMember(dest => dest.Play_Book_ID, opt => opt.MapFrom(src => src.PlayBookID))
            .ForMember(dest => dest.Play_Book_Name, opt => opt.MapFrom(src => src.PlayBookName))
            .ForMember(dest => dest.Alert_Catogory, opt => opt.MapFrom(src => src.AlertCatogory))
            .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.active))
            .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate ))
            .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
            .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
            .ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
            .ForMember(dest => dest.deleted_date, opt => opt.MapFrom(src => src.DeletedDate))
            .ForMember(dest => dest.deleted_user, opt => opt.MapFrom(src => src.DeletedUser))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
           // .ForMember(dest => dest.Processed, opt => opt.MapFrom(src => src.Processed))
            .ReverseMap();

        }
    }
}
