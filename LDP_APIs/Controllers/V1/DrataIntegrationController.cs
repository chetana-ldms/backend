using LDP.Common.Services.DrataIntegration;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LDP_APIs.Controllers.V1
{
    //[Route("api/[controller]")]
    //[ApiController]

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Drata Integration")]
    public class DrataIntegrationController : ControllerBase
    {

        IDrataOperationsBL _bl;
        // GET: api/<QRadarIntegration>
        //[HttpGet]
        public DrataIntegrationController(IDrataOperationsBL bl)
        {
           _bl = bl;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Drata/controls")]
        public GetControlsResponse GetControls(GetControlsRequest request)
        {
            return _bl.GetControls(request);
        }

       
    }
}
                                                                                                                    
            








