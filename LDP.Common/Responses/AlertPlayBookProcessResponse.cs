using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class AlertPlayBookProcessResponse:baseResponse 
    {
    }

    public class GetAlertPlayBookProcessResponse : baseResponse
    {
        public List<GetAlertPlayBookProcessModel>? AlertPlayBookProcessData { get; set; }    
    }
    public class ProcessAlertActionsResponse : baseResponse
    {
    }
    public class AnalyzeAlertsForAutomationResponse : baseResponse
    {
    }
    
}
