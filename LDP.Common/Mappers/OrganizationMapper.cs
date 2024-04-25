using AutoMapper;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.BL.Mappers
{
    public class OrganizationMapper:Profile
    {
        public OrganizationMapper()
        {
            // string nullVariable = null;
            CreateMap<AddOrganizationRequest, Organization>()
                .ForMember(dest => dest.Org_Name, opt => opt.MapFrom(src => src.OrgName))
             //   .ForMember(dest => dest.manage_internal_incidents, opt => opt.MapFrom(src => src.ManageInternalIncidents))
                //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                //.ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
                .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src =>src.CreatedDate))
               //.ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => null ))
               //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.UpdatedByUserName))
               //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreadtedByUserName))
               .ReverseMap();

        }
    }

    public class UpdateOrganizationMapper : Profile
    {
        public UpdateOrganizationMapper()
        {
            // string nullVariable = null;
            CreateMap<UpdateOrganizationRequest, Organization>()
                .ForMember(dest => dest.Org_ID, opt => opt.MapFrom(src => src.OrgID))
                .ForMember(dest => dest.Org_Name, opt => opt.MapFrom(src => src.OrgName))
               // .ForMember(dest => dest.manage_internal_incidents, opt => opt.MapFrom(src => src.ManageInternalIncidents))
                .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.UpdatedDate))
               //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.UpdatedByUserName))
               //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreadtedByUserName))
               .ReverseMap();

        }

        public class GetOrganizationsMapper : Profile
        {
            public GetOrganizationsMapper()
            {
                // string nullVariable = null;
                CreateMap<GettOrganizationsModel, Organization>()
                    .ForMember(dest => dest.Org_ID, opt => opt.MapFrom(src => src.OrgID))
                   .ForMember(dest => dest.Org_Name, opt => opt.MapFrom(src => src.OrgName))
                //   .ForMember(dest => dest.manage_internal_incidents, opt => opt.MapFrom(src => src.ManageInternalIncidents))
                   //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                   //.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                   //.ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
                   .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
                   .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.UpdatedDate))
                   .ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.UpdatedByUserName))
                   .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreadtedByUserName))
                   .ReverseMap();

            }
        }
    }
}
