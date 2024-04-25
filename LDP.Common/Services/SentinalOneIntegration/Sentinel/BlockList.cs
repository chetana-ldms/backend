using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class BlockList:baseResponse
    {
        public List<BlockListData>? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }

    public class BlocklistScope
    {
        public bool? Tenant { get; set; }
        public List<string>? GroupIds { get; set; }
        public List<string>? SiteIds { get; set; }
    }

    public class BlockListData
    {
        public DateTime? CreatedAt { get; set; }
        public string? Description { get; set; }
        public string? Id { get; set; }
        public bool? Imported { get; set; }
        public bool? IncludeChildren { get; set; }
        public bool? IncludeParents { get; set; }
        public string? NotRecommended { get; set; }
        public string? OsType { get; set; }
        public BlocklistScope? Scope { get; set; }
        public string? ScopeName { get; set; }
        public string? ScopePath { get; set; }
        public string? Source { get; set; }
        public string? Type { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Value { get; set; }
    }

    //public class Pagination
    //{
    //    public string NextCursor { get; set; }
    //    public int TotalItems { get; set; }
    //}

    //public class RootObject
    //{
    //    public List<Datum> Data { get; set; }
    //    public Pagination Pagination { get; set; }
    //}

}
