using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class AddAlertIncidentMappingRequest: AddAlertIncidentMappingModel
    {
        public List<AddAlertIncidentMappingDtlModel>? AlertIncidentMappingDtl { get; set; }
    }

    public class UpdateAlertIncidentMappingRequest : UpdateAlertIncidentMappingModel
    {
        public List<UpdateAlertIncidentMappingDtlModel>? AlertIncidentMappingDtl { get; set; }
    }

    public class GetAlertIncidentMappingRequest : GetAlertIncidentMappingModel
    {
        public List<GetAlertIncidentMappingDtlModel>? AlertIncidentMappingDtl { get; set; }
    }
    public class GetAlertsByIncidentIdRequest
    {
        public double IncidentId { get; set; }
    }

}
