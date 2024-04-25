using LDP_APIs.BL.Models;
using LDP_APIs.Models;
using System.Collections.Generic;

namespace LDP_APIs.BL.APIResponse
{
    public class OrganizationResponse:baseResponse
    {
    }

    public class GetOrganizationsResponse : baseResponse
    {
        public List<GettOrganizationsModel> OrganizationList { get; set; }
    }
}
