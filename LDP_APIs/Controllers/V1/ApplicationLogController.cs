using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("ApplicationLogController.cs")]
    public class ApplicationLogController : ControllerBase
    {
        IApplicationLogBL _bl;

        public ApplicationLogController(IApplicationLogBL bl) 
        {
            _bl = bl;
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ApplicationLog/Add")]
        public ApplicationLogResponse AddApplicationLog(ApplicationLogRequest request)
        {
            return _bl.AddLog(request);

        }


    }
}
