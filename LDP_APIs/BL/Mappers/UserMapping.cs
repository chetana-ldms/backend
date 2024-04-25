using AutoMapper;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.BL.Mappers
{
    public class AddUserMapper : Profile
    {
        public AddUserMapper()
        {
            CreateMap< AdduserModel, User>()
            .ForMember(dest => dest.Role_ID, opt => opt.MapFrom(src => src.RoleID))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            .ForMember(dest => dest.User_Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Created_date, opt => opt.MapFrom(src => src.CreatedDete))
            .ForMember(dest => dest.sys_user, opt => opt.MapFrom(src => src.SysUser))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.Created_user, opt => opt.MapFrom(src => src.CreatedByUserName)
        ).ReverseMap();

        }
    }
    public class UpdateUserMapper : Profile
    {
        public UpdateUserMapper()
        {
            CreateMap<User, UpdateUserModel>()
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.User_ID))
            .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
            .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.Role_ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User_Name))
            .ForMember(dest => dest.UpdatedByUserName, opt => opt.MapFrom(src => src.Modified_user))
            .ForMember(dest => dest.SysUser, opt => opt.MapFrom(src => src.sys_user))
            .ForMember(dest => dest.UpdatedDete, opt => opt.MapFrom(src => src.Modified_date)
            ).ReverseMap();

        }
    }
    public class SelectUserMapper : Profile
        {
            public SelectUserMapper()
            {
                CreateMap<User, SelectUserModel>()
               .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.User_ID))
                .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.Role_ID))
                .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User_Name))
                .ForMember(dest => dest.CreatedDete, opt => opt.MapFrom(src => src.Created_date))
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.Created_user))
                .ForMember(dest => dest.UpdatedByUserName, opt => opt.MapFrom(src => src.Modified_user))
                .ForMember(dest => dest.SysUser, opt => opt.MapFrom(src => src.sys_user))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.active))
                .ForMember(dest => dest.UpdatedDete, opt => opt.MapFrom(src => src.Modified_date)
            ).ReverseMap();
            }
        }
    }


 
            
        
    
