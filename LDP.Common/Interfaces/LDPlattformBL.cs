using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;
using static LDP.Common.Requests.DeleteToolActionRequest;

namespace LDP_APIs.BL.Interfaces
{
    public interface ILDPlattformBL
    {
        GetConfiguredLDPToolsResponse GetConfiguredLDPTools();

        GetConfiguredLDPToolsResponse GetConfiguredLDPTools(GetLDPToolRequest request);

        LDPToolResponse NewLDPTool(LDPToolRequest request);
        LDPToolResponse UpdateLDPTool(UpdateLDPToolRequest request);

        GetConfiguredLDPToolResponse GetLDPToolByID(int tooid);

        DeleteLDPToolResponse DeleteLDPTool(DeleteLDPToolRequest request);

        GetOrganizationsResponse GetOrganizationList();

        GetOrganizationResponse GetOrganizationByID(int id);

        OrganizationResponse AddOrganization(AddOrganizationRequest request);
        OrganizationResponse UpdateOrganization(UpdateOrganizationRequest request);

        DeleteOrganizationsResponse DeleteOrganization(DeleteOrganizationRequest request);

        GetOrganizationToolsResponse GetOrganizationToolsList();
        GetOrganizationToolResponse GetOrganizationToolByID(int id );

        GetOrganizationToolsResponse GetOrganizationToolsByToolType(string toolType);
        OrganizationToolsResponse AddOrganizationTool(AddOrganizationToolsRequest request);
        OrganizationToolsResponse UpdateOrganizationTool(UpdateOrganizationToolsRequest request);

        OrganizationToolsResponse DeleteOrganizationTool(DeleteOrganizationToolsRequest request);

        OrganizationToolModel GetToolConnectionDetails(int OrgID, int tooltypeID);

        OrganizationToolModel FilterConnectionAction(OrganizationToolModel orgtool , string actionname);

        Task<string> UpdateLastReadPKID(GetOffenseDTO request);

        string UpdateLastReadAlertDate(OrganizationToolModel request);

        Task<GetOffenseDTO> GetLastReadPKID(GetOffenseDTO request);

        
       // Task<string> UpdateLastReadAlertDate(OrganizationToolModel request);
        LDPMasterDataResponse GetMasterDataByDatType(LDPMasterDataRequest request);
        LDPMasterDataResponse GetMasterDataByMultipleTypes(LDPMasterDataByMultipleTypesRequest MasterDataTypes);

        LDPMasterDataByIDResponse GetLDPMasterDataByID(int id);

        int GetMasterDataByDataValue(string MasterDataType, string MasterDataValue);

        LDPMasterDataModel GetMasterDataobjectByDataValue(string MasterDataType, string MasterDataValue);

        public string GetMasterDataValueByDataId(string MasterDataType, int MasterDataId);

        GetToolTypeActionResponse GetToolTypeActions();

        GetToolTypeActionResponse GetToolTypeActionsByToolType(GetToolTypeActinByToolTypeRequest request);

        ToolTypeActionResponse AddToolTypeAction(AddToolTypeActionRequest request);
        ToolTypeActionResponse UpdateToolTypeAction(UpdateToolTypeActionRequest request);

        DeleteToolTypeActionResponse DeleteToolTypeAction(DeleteToolTypeActionRequest request);

        GetToolActionResponse GetToolActionList();

        GetToolActionResponse GetToolActionsByTool(GetActionRequest request);

        GetToolActionSingleResponse GetToolActionByID(int id );

        ToolActionResponse AddToolAction(AddToolActionRequest request);
        ToolActionResponse UpdateToolAction(UpdateToolActionRequest request);

        DeleteToolActionResponse DeleteToolAction(DeleteToolActionRequest request);
        GetToolTypeActionSingleResponse GetToolTypeActionByID(int id);
        GetToolTypeActionSingleResponse GetTooltypeActionByToolActionID(int id);
    }
}
