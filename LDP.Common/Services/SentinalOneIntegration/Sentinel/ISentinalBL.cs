namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public interface ISentinalBL
    {
        ExclustionsResponse GetExclusions(ExclusionRequest request);
        BlockListResponse GetBlockList(BlockListRequest apiRequest);
        AccountResponse GetAccounts(GetAccountsRequest request);
        AccountPolicy GetAccountPolicy(GetAccountPolicyRequest request);

        SentinelOneSiteDataResponse GetSites(GetSitesRequest request);

        SentinelOneGroupResponse GetGroups(GetGroupsRequest request);

        AccountStructureResponse GetAccountStructure(GetAccountStructureRequest request);
        
    }
}
