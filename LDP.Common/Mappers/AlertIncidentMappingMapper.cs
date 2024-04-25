using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;

namespace LDP.Common.Mappers
{
    

    public class AddAlertIncidentMappingMapper : Profile
    {
        public AddAlertIncidentMappingMapper()
        {
             CreateMap<AddAlertIncidentMappingRequest, AlertIncidentMapping>()
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.orgid))
            .ForMember(dest => dest.tool_type_id, opt => opt.MapFrom(src => src.tooltypeid))
            .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.toolid))
            //.ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.alertid))
            .ForMember(dest => dest.incident_number, opt => opt.MapFrom(src => src.incidentnumber))
            .ForMember(dest => dest.incident_data, opt => opt.MapFrom(src => src.incidentdata))
            .ForMember(dest => dest.significant_incident, opt => opt.MapFrom(src => src.SignificantIncident))
            .ForMember(dest => dest.client_tool_incident_id, opt => opt.MapFrom(src => src.ClientToolIncidentId))
            .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreateDate))
            .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreateUser))
            .ReverseMap();
        }
    }
    public class UpdateAlertIncidentMappingMapper : Profile
    {
        public UpdateAlertIncidentMappingMapper()
        {
            CreateMap<UpdateAlertIncidentMappingRequest, AlertIncidentMapping>()
            .ForMember(dest => dest.alert_incident_mapping_id, opt => opt.MapFrom(src => src.alertincientmappingid))
           .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.orgid))
           .ForMember(dest => dest.tool_type_id, opt => opt.MapFrom(src => src.tooltypeid))
           .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.toolid))
           //.ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.alertid))
           .ForMember(dest => dest.incident_number, opt => opt.MapFrom(src => src.incidentdata))
           .ForMember(dest => dest.significant_incident, opt => opt.MapFrom(src => src.SignificantIncident))
           .ForMember(dest => dest.client_tool_incident_id, opt => opt.MapFrom(src => src.ClientToolIncidentId))
           .ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
           .ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
           .ReverseMap();
        }
    }

    public class GetAlertIncidentMappingMapper : Profile
    {
        public GetAlertIncidentMappingMapper()
        {
            CreateMap<GetAlertIncidentMappingRequest, AlertIncidentMapping>()
            .ForMember(dest => dest.alert_incident_mapping_id, opt => opt.MapFrom(src => src.alertincientmappingid))
           .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.orgid))
           .ForMember(dest => dest.tool_type_id, opt => opt.MapFrom(src => src.tooltypeid))
           .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.toolid))
           //.ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.alertid))
           .ForMember(dest => dest.incident_number, opt => opt.MapFrom(src => src.incidentdata))
           .ForMember(dest => dest.significant_incident, opt => opt.MapFrom(src => src.SignificantIncident))
           .ForMember(dest => dest.client_tool_incident_id, opt => opt.MapFrom(src => src.ClientToolIncidentId))
           // .ForMember(dest => dest.incident_data, opt => null )
           //.ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
           //.ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
           //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.SModifiedUser))
           .ReverseMap();
        }
    }

    public class GetAlertIncidentMappingModelMapper : Profile
    {
        public GetAlertIncidentMappingModelMapper()
        {
            CreateMap<GetAlertIncidentMappingModel, AlertIncidentMapping>()
            .ForMember(dest => dest.alert_incident_mapping_id, opt => opt.MapFrom(src => src.alertincientmappingid))
           .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.orgid))
           .ForMember(dest => dest.tool_type_id, opt => opt.MapFrom(src => src.tooltypeid))
           .ForMember(dest => dest.tool_id, opt => opt.MapFrom(src => src.toolid))
          // .ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.alertid))
           .ForMember(dest => dest.incident_number, opt => opt.MapFrom(src => src.incidentnumber))
           .ForMember(dest => dest.significant_incident, opt => opt.MapFrom(src => src.SignificantIncident))
           .ForMember(dest => dest.client_tool_incident_id, opt => opt.MapFrom(src => src.ClientToolIncidentId))
           // .ForMember(dest => dest.incident_data, opt => null )
           //.ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
           //.ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
           //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
           .ReverseMap();
        }
    }
    public class AddAlertIncidentMappingDtlMapper : Profile
    {
        public AddAlertIncidentMappingDtlMapper()
        {
            CreateMap<AddAlertIncidentMappingDtlModel, AlertIncidentMappingDtl>()
           .ForMember(dest => dest.alert_incident_mapping_id, opt => opt.MapFrom(src => src.alertincidentmappingid))
           .ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.alertid))
           .ReverseMap();
        }
    }

    public class UpdateAlertIncidentMappingDtlMapper : Profile
    {
        public UpdateAlertIncidentMappingDtlMapper()
        {
            CreateMap<UpdateAlertIncidentMappingDtlModel, AlertIncidentMappingDtl>()
           .ForMember(dest => dest.alert_incident_mapping_dtl_id, opt => opt.MapFrom(src => src.alertincidentmappingdtlid))
           .ForMember(dest => dest.alert_incident_mapping_id, opt => opt.MapFrom(src => src.alertincidentmappingid))
           .ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.alertid))
           .ReverseMap();
        }
    }

    public class GetAlertIncidentMappingDtlMapper : Profile
    {
        public GetAlertIncidentMappingDtlMapper()
        {
            CreateMap<GetAlertIncidentMappingDtlModel, AlertIncidentMappingDtl>()
           .ForMember(dest => dest.alert_incident_mapping_dtl_id, opt => opt.MapFrom(src => src.alertincidentmappingdtlid))
           .ForMember(dest => dest.alert_incident_mapping_id, opt => opt.MapFrom(src => src.alertincidentmappingid))
           .ForMember(dest => dest.alert_id, opt => opt.MapFrom(src => src.alertid))
           .ReverseMap();
        }
    }
}
