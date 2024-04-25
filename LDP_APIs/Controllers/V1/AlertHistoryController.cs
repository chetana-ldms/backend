using LDP.Common.BL;
using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Alert History")]
    public class AlertHistoryController : ControllerBase
    {
        IAlertHistoryBL _bl;

        public AlertHistoryController(IAlertHistoryBL bl) 
        {
            _bl = bl;
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ActionHistory/Add")]
        public AlertHistoryResponse AddalertHistory(AlertHistoryRequest request)
        {
            return _bl.AddalertHistory(request);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetalertHistory")]
        public GetAlertHistoryResponse GetalertHistory(GetAlertHistoryRequest request)
        {
            return _bl.GetalertHistory(request);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetIncidentHistory")]
        public GetAlertHistoryResponse GetIncidentHistory(GetIncidentHistoryRequest request)
        {
            return _bl.GetIncidentHistory(request);

        }
    }
}
