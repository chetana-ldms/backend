using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class AccountStructureResponse:baseResponse
    {
         public List<AccountStructure>? Accounts { get; set; }
    }

    public class AccountStructure
    {
        public string? AccountId { get; set; }
        public string? Name { get; set; }
        public int? TotalSites { get; set; }
        public int? TotalEndpoints { get; set; }
        public List<SiteStructure>? Sites { get; set; }
    }

    public class SiteStructure
    {
        public string? SiteId { get; set; }
        public int? TotalGroups { get; set; }
       // public int? TotalEndpoints { get; set; }
        public string? Name { get; set; }
        public bool? IsDefault { get; set; }
        public int? ActiveLicenses { get; set; }
        public List<GroupStructure>? Groups { get; set; }
    }

    public class GroupStructure
    {
        public string? GroupId { get; set; }
        public string? Name { get; set; }
        public int? TotalAgents { get; set; }
    }

}
