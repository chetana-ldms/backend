using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IAlertPlayBookProcessActionBL
    {
        AlertPlaybookProcessActionResponse AddAlertPlayBookProcessAction(DAL.Entities.AlertPlayBookProcessAction request);
        AlertPlaybookProcessActionResponse AddRangeAlertPlayBookProcessActions(List<DAL.Entities.AlertPlayBookProcessAction> request);

        GetAlertPlayBookProcessactionResponse GetAlertPlayBookProcessActionsByStatus(GetAlertPlayBookProcessActionsByStatusRequest request);
        double GetCountAlertPlayBookProcessActionsByStatus(GetAlertPlayBookProcessActionsByStatusRequest request);

        string UpdateActionStatus(UpdateActionStatusRequest request);
    }
}
