using LDP.Common.DAL.Entities.Common;
using LDP.Common.Requests;

namespace LDP.Common.DAL.Interfaces
{
    public interface IAlertHistoryRepository
    {
        Task<string> AddalertHistory(AlertHistory request);

        Task<string> AddRangealertHistory(List<AlertHistory> request);
        Task<List<AlertHistory>> GetalertHistory(GetAlertHistoryRequest request);

        Task<List<AlertHistory>> GetIncidentHistory(GetIncidentHistoryRequest request);

    }
}
