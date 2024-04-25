using LDP_APIs.BL.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public interface ISentinelService
    {
        Task<Exclusions> GetExclusions(ExclusionRequest apiRequest , OrganizationToolModel request, string nextcursor = null);
        Task<BlockList> GetBlockList(BlockListRequest apiRequest, OrganizationToolModel request, string nextcursor = null);
        Task<Account> GetAccounts(GetAccountsRequest apiRequest, OrganizationToolModel request, string nextcursor = null);
        Task<AccountPolicy> GetAccountPolicy(GetAccountPolicyRequest apiRequest, OrganizationToolModel request, string nextcursor = null);
        Task<SentinelOneSiteData> GetSites(GetSitesRequest apiRequest, OrganizationToolModel request, string nextcursor = null);
        Task<SentinelOneGroup> GetGroups(GetGroupsRequest apiRequest, OrganizationToolModel request, string nextcursor = null);

    }
}
