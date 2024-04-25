using AutoMapper;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.BL.Mappers
{
    public class GetRoleMapper:Profile 
    {
        public GetRoleMapper()
        {
            CreateMap<Role, GetRoleModel>()
            .ForMember(dest => dest.RoleID,opt => opt.MapFrom(src => src.Role_ID))
            .ForMember(dest => dest.Sysrole, opt => opt.MapFrom(src => src.sys_role))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Created_date))
            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.Created_user))
            .ForMember(dest => dest.Modifieddate, opt => opt.MapFrom(src => src.Modified_date))
            .ForMember(dest => dest.Modifieduser, opt => opt.MapFrom(src => src.Modified_user))
            .ForMember(dest => dest.DeletedUser, opt => opt.MapFrom(src => src.deleted_user))
            .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => src.deleted_date))
            .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
            .ForMember(dest => dest.GlobalAdminRole, opt => opt.MapFrom(src => src.global_admin_role))
            .ForMember(dest => dest.ClientAdminRole, opt => opt.MapFrom(src => src.client_admin_role))

            .ForMember(dest =>dest.RoleName,opt => opt.MapFrom(src => src.Role_Name)
             ).ReverseMap(); ;

        }
    }

    public class AddRoleMapper : Profile
    {
        public AddRoleMapper()
        {
            CreateMap< AddRoleRequest, Role>()

            .ForMember(dest => dest.sys_role, opt => opt.MapFrom(src => src.Sysrole))
            .ForMember(dest => dest.Created_date, opt => opt.MapFrom(src => src.CreatedDate))
            //.ForMember(dest => dest.Created_user, opt => opt.MapFrom(src => src.CreatedUser))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.Role_Name, opt => opt.MapFrom(src => src.RoleName))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            .ForMember(dest => dest.global_admin_role, opt => opt.MapFrom(src => src.GlobalAdminRole))
            .ForMember(dest => dest.client_admin_role, opt => opt.MapFrom(src => src.ClientAdminRole)
             ).ReverseMap(); ;

        }
    }
}
