using LDP_APIs.Controllers.V1;
using Microsoft.AspNetCore.Mvc;

namespace LDP_APIs.Controllers
{
    public class CommonController: ControllerBase
    {
        private readonly ILogger _logger;
        public CommonController(ILogger<AlertsController> logger)
        {
            _logger = logger;

        }
    }
}
