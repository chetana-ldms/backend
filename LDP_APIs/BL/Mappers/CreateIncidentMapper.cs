using AutoMapper;
using LDP.Common.Requests;
using LDP_APIs.Models;

namespace LDP_APIs.BL.ToolActions
{
    public class CreateIncidentMapper:Profile
    {
        public CreateIncidentMapper()
        {
            CreateMap<FreshDeskTicketingToolActionRequest, CreateIncidentRequest>()
                .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.OrgID)).ReverseMap();
                //)
                //.ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.ToolID))
                //.ForMember(dest => dest.AlertData, opt => opt.MapFrom(src => src.IncidentdtlsList));
                //.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                //.ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
                //.ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
               //.ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => null ))
               //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.UpdatedByUserName))
               //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreadtedByUserName)).ReverseMap();

        }
    }

    public class IncidentDtlMapper : Profile
    {
        public IncidentDtlMapper()
        {
            CreateMap<Incidentdtls, Incidentdtls>();
               // .ForMember(dest => dest.OrgID, opt => opt.MapFrom(src => src.OrgID))
               // .ForMember(dest => dest.ToolID, opt => opt.MapFrom(src => src.ToolID))
              //  .ForMember(dest => dest.IncidentdtlsList, opt => opt.MapFrom(src => src.IncidentdtlsList))
                //.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                //.ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
                //.ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
                //.ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => null ))
                //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.UpdatedByUserName))
                //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreadtedByUserName)).ReverseMap();

        }
    }
}
