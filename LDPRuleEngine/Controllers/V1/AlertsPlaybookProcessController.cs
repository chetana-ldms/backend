using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Alert Playbook process")]
    public class AlertsPlaybookProcessController : ControllerBase
    {
        IAlertPlayBookProcessBL _bl;

        public AlertsPlaybookProcessController(IAlertPlayBookProcessBL bl) 
        {
            _bl = bl;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AlertPlayBookProcess/Add")]
        public AlertPlayBookProcessResponse AddAlertPlayBookProcess(AddAlertPlayBookProcessRequest request)
        {
            return _bl.AddAddAlertPlayBookProcess(request);
  
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AnalyzeAlertsForAutomation")]
        public AlertPlayBookProcessResponse AnalyzeAlertsForAutomation()
        {
            return _bl.AnalyzeAlertsForAutomation();

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ProcessAlertActions")]
        public ProcessAlertActionsResponse ProcessAlertActions()
        {
            return _bl.ProcessAlertPlaybookProcessActions();
        }


    }
}
