using LDP.Common.DAL.Entities.Common;

namespace LDP.Common.DAL.Interfaces
{
    public interface ILDCAPIUrlRepository
    {
        Task<List<LDCApiUrls>> GetLDCUrls(int orgId);

        Task<List<LDCApiUrls>> GetLDCUrlsByGroup(string groupName);

        Task<List<LDCApiUrls>> GetLDCUrlsByGroupAndAction(string groupName, string actionName);

    }
}
