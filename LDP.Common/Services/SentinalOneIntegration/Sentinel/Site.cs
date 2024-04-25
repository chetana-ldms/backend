using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class SentinelOneSiteData:baseResponse
    {
        public SiteData? data { get; set; }
        public Pagination? pagination { get; set; }
    }

    public class SentinelOneSiteDataResponse : baseResponse
    {
        public SiteData? data { get; set; }
        
    }
    //public class Surface
    //{
    //    public int? count { get; set; }
    //    public string? name { get; set; }
    //}

    //public class Bundle
    //{
    //    public string? displayName { get; set; }
    //    public int? majorVersion { get; set; }
    //    public int? minorVersion { get; set; }
    //    public string? name { get; set; }
    //    public List<Surface>? surfaces { get; set; }
    //    public int? totalSurfaces { get; set; }
    //}

    //public class Setting
    //{
    //    public string? displayName { get; set; }
    //    public string? groupName { get; set; }
    //    public string? setting { get; set; }
    //    public string? settingGroup { get; set; }
    //    public string? settingGroupDisplayName { get; set; }
    //}

    //public class Licenses
    //{
    //    public List<Bundle>? bundles { get; set; }
    //    public List<module>? modules { get; set; }
    //    public List<Setting>? settings { get; set; }
    //}

    public class module
    {
        public string? name { get; set; }
        public string? displayName { get; set; }
        public int? majorVersion { get; set; }
    }
    public class Site
    {
        public string? accountId { get; set; }
        public string? accountName { get; set; }
        public int? activeLicenses { get; set; }
        public DateTime? createdAt { get; set; }
        public string? creator { get; set; }
        public string? creatorId { get; set; }
        public string? description { get; set; }
        public DateTime? expiration { get; set; }
        public string? externalId { get; set; }
        public bool? healthStatus { get; set; }
        public string? id { get; set; }
        public bool? isDefault { get; set; }
        public Licenses? licenses { get; set; }
        public string? name { get; set; }
        public string? registrationToken { get; set; }
        public string? siteType { get; set; }
        public string? sku { get; set; }
        public string? state { get; set; }
        public string? suite { get; set; }
        public int? totalLicenses { get; set; }
        public bool? unlimitedExpiration { get; set; }
        public bool? unlimitedLicenses { get; set; }
        public DateTime? updatedAt { get; set; }
        public string? usageType { get; set; }
    }

    public class AllSites
    {
        public int? activeLicenses { get; set; }
        public int? totalLicenses { get; set; }
    }

    public class SiteData
    {
        public AllSites? allSites { get; set; }
        public List<Site>? sites { get; set; }
    }

    //public class Pagination
    //{
    //    public object nextCursor { get; set; }
    //    public int totalItems { get; set; }
    //}

    //public class Root
    //{
    //    public Data data { get; set; }
    //    public Pagination pagination { get; set; }
    //}

}
