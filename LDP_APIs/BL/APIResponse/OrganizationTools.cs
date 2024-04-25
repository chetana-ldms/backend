using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class GetOrganizationToolsResponse:baseResponse
    {
        public List<OrganizationToolModel>? OrganizationToolList { get; set; }
    }

    public class OrganizationToolsResponse : baseResponse
    {

    }
}
