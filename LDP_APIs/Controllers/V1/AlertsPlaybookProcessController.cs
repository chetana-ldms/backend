using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using Microsoft.AspNetCore.Http;
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

        public AlertsPlaybookProcessController(IAlertPlayBookProcessBL bl, ILogger<AlertsController> logger) 
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
    }
}
