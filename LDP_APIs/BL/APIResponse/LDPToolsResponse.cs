using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class GetConfiguredLDPToolsResponse:baseResponse
    {
        public List<GetLDPTool>? LDPToolsList { get; set; }
    }
}
