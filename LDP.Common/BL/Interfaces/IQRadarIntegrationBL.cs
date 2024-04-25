using LDP_APIs.Models;

namespace LDP_APIs.Interfaces
{
    public interface IQRadarIntegrationBL
    {
        getOffenseResponse Getoffenses(GetOffenseRequest request);
        Task<getOffenseResponse> GetoffensesWithData(GetOffenseRequest request);
        //getAlertsResponse GetAlerts(GetOffenseRequest request);
    }
}
