using LDP_APIs.DAL.Entities;
using LDP_APIs.Models;

namespace LDP_APIs.DAL
{
    public interface ILDPlattformRepository
    {
        Task<List<LDPTool>> GetConfiguredLDPTools();
        Task<string> NewLDPTool(LDPTool request);
        Task<string> UpdateLDPTool(LDPTool request);

        Task<List<Organization>> GetOrganizationList();
        Task<string> AddOrganization(Organization request);
        Task<string> UpdateOrganization(Organization request);

        Task<List<OganizationTool>> GetOrganizationToolList();
        Task<string> AddOrganizationTool(OganizationTool request);
        Task<string> UpdateOrganizationTool(OganizationTool request);

        Task<OganizationTool> GetToolConnectionDetails(int OrgID, int ToolID);

        Task<string> UpdateLastReadPKID(GetOffenseDTO request);

        Task<GetOffenseDTO> GetLastReadPKID(GetOffenseDTO request);

        Task<List<LDPMasterData>> GetMasterData(string MasterDataType);


    }

}
