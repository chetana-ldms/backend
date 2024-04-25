using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IAlertHistoryBL
    {
        AlertHistoryResponse AddalertHistory(AlertHistoryRequest request);

       // AlertHistoryResponse AddRangealertHistory(List<AlertHistoryRequest> request);

        AlertHistoryResponse AddRangealertHistory(List<AlertHistoryModel> request);

        GetAlertHistoryResponse GetalertHistory(GetAlertHistoryRequest request);
        GetAlertHistoryResponse GetIncidentHistory(GetIncidentHistoryRequest request);

    }
}
