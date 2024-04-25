using LDP_APIs.Models;
using System.ComponentModel;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class ExclusionRequest
    {
        public int OrgId { get; set; }

        public bool IncludeChildren { get; set; }

        public bool IncludeParents { get; set; }

        public string? ExclusionListItemId { get; set; }

        public List<AccountStructureLevel>? OrgAccountStructureLevel { get; set; }
    }

    public class BlockListRequest
    {
        public int OrgId { get; set; }

        public bool IncludeChildren { get; set; }

        public bool IncludeParents { get; set; }

        public string? BlockListItemId { get; set; }

        public List<AccountStructureLevel>? OrgAccountStructureLevel { get; set; }
    }

    public class GetAccountsRequest
    {
        public int OrgId { get; set; }

     
    }

    public class GetAccountPolicyRequest
    {
        public int OrgId { get; set; }
        [DefaultValue(false)]
        public bool TenantPolicyScope { get; set; }
        public bool AccountPolicyScope { get; set; }
        [DefaultValue(false)]
        public bool SitePolicyScope { get; set; }
        [DefaultValue(false)]
        public bool GroupPolicyScope { get; set; }
        public string? ScopeId { get; set; }

        public List<AccountStructureLevel>? OrgAccountStructureLevel { get; set; }
    }

    public class GetSitesRequest
    {
        public int OrgId { get; set; }
        public string? AccountId { get; set; }

    }

    public class GetGroupsRequest
    {
        public int OrgId { get; set; }
        public string? SiteId { get; set; }

    }
    public class GetAccountStructureRequest
    {
        public int OrgId { get; set; }
    }



}
