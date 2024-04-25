using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class Account : baseResponse
    {
        public List<AccountData>? data { get; set; }
        public Pagination? pagination { get; set; }
    }
    public class AccountResponse : baseResponse
    {
        public List<AccountData>? Accounts { get; set; }
       
    }
    public class Bundle
    {
        public string? displayName { get; set; }
        public int? majorVersion { get; set; }
        public int? minorVersion { get; set; }
        public string? name { get; set; }
        public List<Surface>? surfaces { get; set; }
        public int? totalSurfaces { get; set; }
    }

    public class AccountData
    {
        public string? accountType { get; set; }
        public int? activeAgents { get; set; }
        public int? agentsInCompleteSku { get; set; }
        public int? agentsInControlSku { get; set; }
        public int? agentsInCoreSku { get; set; }
        public string? billingMode { get; set; }
        public int? completeSites { get; set; }
        public int? controlSites { get; set; }
        public int? coreSites { get; set; }
        public DateTime? createdAt { get; set; }
        public string? creator { get; set; }
        public string? creatorId { get; set; }
        public DateTime? expiration { get; set; }
        public string? externalId { get; set; }
        public string? id { get; set; }
        public bool? isDefault { get; set; }
        public Licenses? licenses { get; set; }
        public string? name { get; set; }
        public int? numberOfSites { get; set; }
        public string? salesforceId { get; set; }
        public List<Sku>? skus { get; set; }
        public string? state { get; set; }
        public int? totalComplete { get; set; }
        public int? totalControl { get; set; }
        public int? totalCore { get; set; }
        public int? totalLicenses { get; set; }
        public bool? unlimitedComplete { get; set; }
        public bool? unlimitedControl { get; set; }
        public bool? unlimitedCore { get; set; }
        public bool? unlimitedExpiration { get; set; }
        public DateTime? updatedAt { get; set; }
        public string? usageType { get; set; }
    }

    public class Licenses
    {
        public List<Bundle>? bundles { get; set; }
        public List<Module>? modules { get; set; }
        public List<Setting>? settings { get; set; }
    }

    public class Module
    {
        public string? displayName { get; set; }
        public int? majorVersion { get; set; }
        public string? name { get; set; }
    }

    //public class Pagination
    //{
    //    public object nextCursor { get; set; }
    //    public int? totalItems { get; set; }
    //}

    public class Root
    {
        //public List<AccountData>? data { get; set; }
        //public Pagination? pagination { get; set; }
    }

    public class Setting
    {
        public string? displayName { get; set; }
        public string? groupName { get; set; }
        public string? setting { get; set; }
        public string? settingGroup { get; set; }
        public string? settingGroupDisplayName { get; set; }
    }

    public class Sku
    {
        public int? agentsInSku { get; set; }
        public int? totalLicenses { get; set; }
        public string? type { get; set; }
        public bool? unlimited { get; set; }
    }

    public class Surface
    {
        public int? count { get; set; }
        public string? name { get; set; }
    }


}
