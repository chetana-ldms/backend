using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class AlertIncidentMappingResponse: baseResponse
    {
        public int AlertIncidentMappingId { get; set; }
    }

    public class GetAlertIncidentMappingResponse : baseResponse
    {
        public List<GetAlertIncidentMappingModel>? AlertIncidentMappingData { get; set; }
    }

    public class GetAlertIncidentMappingSingleResponse : baseResponse
    {
        public GetAlertIncidentMappingModel? AlertIncidentMappingData { get; set; }
    }
}
