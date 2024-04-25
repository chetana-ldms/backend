using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class Exclusions:baseResponse
    {
        public List<ExclusionData>? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }

   // using System;
//using System.Collections.Generic;

public class Scope
    {
        public List<string>? AccountIds { get; set; }
    }

    public class ExclusionData
    {
        public List<string>? Actions { get; set; }
        public string? ApplicationName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Description { get; set; }
        public string? Id { get; set; }
        public bool? Imported { get; set; }
        public bool? InAppInventory { get; set; }
        public bool? IncludeChildren { get; set; }
        public bool? IncludeParents { get; set; }
        public string? Mode { get; set; }
        public string? NotRecommended { get; set; }
        public string? OsType { get; set; }
        public string? PathExclusionType { get; set; }
        public Scope? Scope { get; set; }
        public string? ScopeName { get; set; }
        public string? ScopePath { get; set; }
        public string? Source { get; set; }
        public string? Type { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Value { get; set; }
    }

    //public  Pagination
    //{
    //    public object NextCursor { get; set; }
    //    public int TotalItems { get; set; }
    //}

    public class RootObject
    {
        //public List<DataItem> Data { get; set; }
        //public Pagination Pagination { get; set; }
    }

}
