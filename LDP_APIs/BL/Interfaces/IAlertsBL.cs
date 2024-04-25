using LDP_APIs.BL.APIRequests;
using LDP_APIs.Models;

namespace LDP_APIs.BL.Interfaces
{
    public interface IAlertsBL
    {
       getAlertsResponse GetAlerts(GetOffenseRequest request);
       baseResponse AssignOwner(AssignOwnerRequest request);

        getAlertResponse GetAlertData(GetAlertRequest request);

        getAlertsResponse GetAlertsByAssignedUser(GetAlertByAssignedOwnerRequest request);
    }
}
