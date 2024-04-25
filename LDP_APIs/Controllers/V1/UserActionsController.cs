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
    [ControllerName("UserActions")]
    public class UserActionsController : ControllerBase
    {
        IUserActionBL _bl;

        public UserActionsController(IUserActionBL bl) 
        {
            _bl = bl;
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("UserActionsByUser")]
        public GetUserActionsResponse UserActionsByUser(GetUserActionRequest request)
        {
            return _bl.GetUserActionsByUser(request);

        }


    }
}
