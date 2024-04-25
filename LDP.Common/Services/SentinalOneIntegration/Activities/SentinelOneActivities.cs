namespace LDP.Common.Services.SentinalOneIntegration.Activities
{
    public class SentinelOneActivities
    {
        public List<ActivityData>? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }

public class ActivityData
    {
        public string? AccountId { get; set; }
        public string? AccountName { get; set; }
        public int? ActivityType { get; set; }
        public string? ActivityUuid { get; set; }
        public string? AgentId { get; set; }
        public string? AgentUpdatedVersion { get; set; }
        public string? Comments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public MoreData? Data { get; set; }
        public string? Description { get; set; }
        public string? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? Hash { get; set; }
        public string? Id { get; set; }
        public string? OsFamily { get; set; }
        public string? PrimaryDescription { get; set; }
        public string? SecondaryDescription { get; set; }
        public string? SiteId { get; set; }
        public string? SiteName { get; set; }
        public string? ThreatId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UserId { get; set; }
    }

    public class MoreData
    {
        public string? ByUser { get; set; }
        public string? Expiration { get; set; }
        public string? FullScopeDetails { get; set; }
        public string? FullScopeDetailsPath { get; set; }
        public string? GroupName { get; set; }
        public string? IPAddress { get; set; }
        public string? LicensesDescription { get; set; }
        public string? Role { get; set; }
        public string? RoleName { get; set; }
        public string? ScopeLevel { get; set; }
        public string? ScopeLevelName { get; set; }
        public string? ScopeName { get; set; }
        public string? SiteName { get; set; }
        public string? SourceType { get; set; }
        public string? UserScope { get; set; }
        public string? Username { get; set; }
        public bool? NewValue { get; set; }
        public string? Reason { get; set; }
        public string? Source { get; set; }
    }

    //public class Pagination
    //{
    //    public string? NextCursor { get; set; }
    //    public int? TotalItems { get; set; }
    //}

    //public class RootObject
    //{
    //    public List<ActivityData>? Data { get; set; }
    //    public Pagination? Pagination { get; set; }
    //}


}
