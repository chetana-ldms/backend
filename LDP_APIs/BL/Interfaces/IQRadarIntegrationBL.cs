using LDP_APIs.Models;

namespace LDP_APIs.Interfaces
{
    public interface IQRadarIntegrationBL
    {
        Task<getOffenseResponse> Getoffenses(GetOffenseRequest request);
        //getAlertsResponse GetAlerts(GetOffenseRequest request);
    }
}
