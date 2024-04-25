using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Models;
using static LDP.Common.Requests.DeleteToolActionRequest;

namespace LDP_APIs.DAL
{
    public interface ILDPlattformRepository
    {
        Task<List<LDPTool>> GetConfiguredLDPTools();

        Task<List<LDPTool>> GetConfiguredLDPTools(GetLDPToolRequest request);

        Task<string> NewLDPTool(LDPTool request);
        Task<string> UpdateLDPTool(LDPTool request);
        Task<LDPTool> GetLDPToolByID(int tooid);

        Task<string> DeleteLDPTool(DeleteLDPToolRequest request,string deletedUserName);

        Task<LDPTool> GetLDPToolByName(string name);

        Task<LDPTool> GetLDPToolByNameOnUpdate(string name, int id);

        Task<List<Organization>> GetOrganizationList();
        Task<string> AddOrganization(Organization request);
        Task<string> UpdateOrganization(Organization request);
        Task<string> DeleteOrganization(DeleteOrganizationRequest request, string deletedUserName);

        Task<Organization> GetOrganizationByName(string name);

        Task<Organization> GetOrganizationByNameOnUpdate(string name, int id);

        Task<Organization> GetOrganizationByID(int id);

        Task<List<OrganizationToolModel>> GetOrganizationToolList();
        Task<OrganizationToolModel> GetOrganizationToolByID(int id);
        Task<List<OrganizationToolActionModel>> GetOrganizationToolActions(int orgtoolid);
        Task<List<OrganizationToolModel>> GetOrganizationToolsByToolType(string toolType);
        Task<string> AddOrganizationTool(OganizationTool request);
        Task<string> UpdateOrganizationTool(OganizationTool request);
        Task<string> DeleteOrganizationTool(DeleteOrganizationToolsRequest request, string deletedUserName);
        Task<OganizationTool> GetToolConnectionDetails(int OrgIDint, int tooltypeID );

        Task<string> AddOrganizationToolAction(List<OrganizationToolAction> request);
        Task<string> UpdateOrganizationToolActions(List<OrganizationToolAction> request);
        Task<string> UpdateLastReadPKID(GetOffenseDTO request);

        Task<string> UpdateLastReadAlertDate(OrganizationToolModel request);

        Task<GetOffenseDTO> GetLastReadPKID(GetOffenseDTO request);

        Task<List<LDPMasterData>> GetMasterData(string MasterDataType);

        Task<List<LDPMasterData>> GetMasterDataByMultipleTypes(List<string> MasterDataTypes);

        Task<LDPMasterData> GetMasterDataByDataValue(string MasterDataType, string MasterDataValue);

        Task<LDPMasterData> GetMasterDataValueByDataId(string MasterDataType, int MasterDataId);

        Task<LDPMasterData> GetLDPMasterDataByID(int id);


        Task<List<GetToolTypeActionModel>> GetToolTypeActions();

        Task<List<GetToolTypeActionModel>> GetToolTypeActionsByToolType(GetToolTypeActinByToolTypeRequest request);

        Task<string> AddToolTypeAction(ToolTypeAction request);
        Task<string> UpdateToolTypeAction(ToolTypeAction request);

        Task<string> DeleteToolTypeAction(DeleteToolTypeActionRequest request, string deletedUserName);
       
        Task<ToolTypeAction> GetToolTypeActionByName(string name);

        Task<ToolTypeAction> GetToolTypeActionByID(int id);
        
        Task<GetToolTypeActionModel> GetTooltypeActionByToolActionID(int id);

        Task<ToolTypeAction> GetToolTypeActionByNameOnUpdate(string name, int id);

        Task<List<GetToolActionModel>> GetToolActions();

        Task<List<GetToolActionModel>> GetToolActionsByTool(GetActionRequest request);
        Task<GetToolActionModel> GetToolActionByID(int id);
        Task<string> AddTooolAction(LDPToolActions request);
        Task<string> UpdateToolAction(LDPToolActions request);
        Task<string> DeleteToolAction(DeleteToolActionRequest request, string deletedUserName);
   }

}
