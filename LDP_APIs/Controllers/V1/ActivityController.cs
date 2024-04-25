using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Requests.Common;
using LDP.Common.Responses;
using LDP_APIs.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Activity")]
    public class ActivityController : ControllerBase
    {
        ICommonBL _bl;
        ILDPSecurityBL _securityBl;
        public ActivityController(ICommonBL bl, ILDPSecurityBL securityBl)
        {
            _bl = bl;
            _securityBl = securityBl;
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ActivityTypes")]
        public ActivityTypesresponse GetActivityTypes()
        {
            return _bl.GetActivityTypes();
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Add")]
        public ActivityResponse AddActivity(AddActivityRequest request)
        {
            ActivityResponse response = new ActivityResponse();

            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {

                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            return _bl.AddActivity(request, userdata.Userdata.Name);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("activities")]
        public GetActivitiesResponse GetActiviites(GetActivitiesRequest request)
        {
           return  _bl.GetActiviites(request);
        }


    }
}
