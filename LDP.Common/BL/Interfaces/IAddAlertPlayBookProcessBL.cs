using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;

namespace LDP.Common.BL.Interfaces
{
    public interface IAlertPlayBookProcessBL
    {
        AlertPlayBookProcessResponse AddAddAlertPlayBookProcess(AddAlertPlayBookProcessRequest request);
        AlertPlayBookProcessResponse AnalyzeAlertsForAutomation();

        ProcessAlertActionsResponse ProcessAlertPlaybookProcessActions();

        AnalyzeAlertsForAutomationResponse AnalyzeAlertsForAutomation(AnalyzeAlertsForAutomationRequest request);
        ProcessAlertActionsResponse ProcessAlertPlaybookProcessActions(PlayBookProcessActionRequest request);
    }
}
