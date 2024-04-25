using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class SentinelOneGroup:baseResponse
    {
        public List<GroupData> data { get; set; }
        public Pagination? pagination { get; set; }
    }
    public class SentinelOneGroupResponse : baseResponse
    {
        public List<GroupData> data { get; set; }
    }

    public class GroupData
    {
        public DateTime? createdAt { get; set; }
        public string? creator { get; set; }
        public string? creatorId { get; set; }
        public object? filterId { get; set; }
        public object? filterName { get; set; }
        public string? id { get; set; }
        public bool? inherits { get; set; }
        public bool? isDefault { get; set; }
        public string? name { get; set; }
        public object? rank { get; set; }
        public string? registrationToken { get; set; }
        public string? siteId { get; set; }
        public int? totalAgents { get; set; }
        public string? type { get; set; }
        public DateTime? updatedAt { get; set; }
    }

    //public class Pagination
    //{
    //    public object? nextCursor { get; set; }
    //    public int? totalItems { get; set; }
    //}

    //public class Root
    //{
    //    public DataItem[]? data { get; set; }
    //    public Pagination? pagination { get; set; }
    //}

}
