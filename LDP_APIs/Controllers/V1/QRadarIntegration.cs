using LDP_APIs.Interfaces;
using LDP_APIs.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LDP_APIs.Controllers.V1
{
    //[Route("api/[controller]")]
    //[ApiController]

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("QRadar Integration")]
    public class QRadarIntegration : ControllerBase
    {

        IQRadarIntegrationBL _bl;
        // GET: api/<QRadarIntegration>
        //[HttpGet]
        public QRadarIntegration(IQRadarIntegrationBL bl)
        {
           _bl = bl;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("QRadar/offenses")]
        public getOffenseResponse Getoffenses(GetOffenseRequest request)
        {
            return _bl.Getoffenses(request);
        }

       
    }
}
                                                                                                                    
            








