using LDP_APIs.BL.APIRequests;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Models;

namespace LDP_APIs.DAL.Interfaces
{
    public interface IAlertsRepository
    {
        Task<string> Addalerts(List<Alerts> request);
        List<Alerts> Getalerts(GetOffenseRequest request);
        double GetAlertsDataCount();
        Task<string> AssignOwner(AssignOwnerRequest request);
        Task<List<Alerts>> GetAlertData(GetAlertRequest request);
        Task<List<Alerts>> GetAlertsByAssignedUser(GetAlertByAssignedOwnerRequest request);
        Task<double> GetAlertsCountByAssignedUser(GetAlertByAssignedOwnerRequest request);

    }
}

