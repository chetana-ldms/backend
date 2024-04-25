using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP_APIs.BL.Interfaces
{
    public interface ILDPlattformBL
    {
        GetConfiguredLDPToolsResponse GetConfiguredLDPTools();
        LDPToolResponse NewLDPTool(LDPToolRequest request);
        LDPToolResponse UpdateLDPTool(UpdateLDPToolRequest request);
        GetOrganizationsResponse GetOrganizationList();
        OrganizationResponse AddOrganization(AddOrganizationRequest request);
        OrganizationResponse UpdateOrganization(UpdateOrganizationRequest request);

        GetOrganizationToolsResponse GetOrganizationToolsList();
        OrganizationToolsResponse AddOrganizationTools(AddOrganizationToolsRequest request);
        OrganizationToolsResponse UpdateOrganizationTools(UpdateOrganizationToolsRequest request);

        OrganizationToolModel GetToolConnectionDetails(int OrgID, int toolID);

        Task<string> UpdateLastReadPKID(GetOffenseDTO request);
        Task<GetOffenseDTO> GetLastReadPKID(GetOffenseDTO request);

        LDPMasterDataResponse GetMasterDataByDatType(LDPMasterDataRequest request);


    }
}
