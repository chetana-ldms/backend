namespace LDP.Common.Services.SentinalOneIntegration
{
    public class GetThreatTimeline
    {
        public List<ThreatTimeLine>? data { get; set; }
        public Pagination? pagination { get; set; }
    }

   

    public class Pagination
    {
        public string? nextCursor { get; set; }
        public int totalItems { get; set; }
    }

    //public class Root
    //{
    //    public List<Datum> data { get; set; }
    //    public Pagination pagination { get; set; }
    //}


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class TimeLIneData
    {
        public string? accountName { get; set; }
        public object description { get; set; }
        public string? fileContentHash { get; set; }
        public string? fullScopeDetails { get; set; }
        public string? fullScopeDetailsPath { get; set; }
        public object groupName { get; set; }
        public object ipAddress { get; set; }
        public string? osFamily { get; set; }
        public string? scopeLevel { get; set; }
        public string? scopeName { get; set; }
        public string? siteName { get; set; }
        public string? username { get; set; }
    }

    public class ThreatTimeLine
    {
        public string? accountId { get; set; }
        public int activityType { get; set; }
        public object agentId { get; set; }
        public object agentUpdatedVersion { get; set; }
        public DateTime? createdAt { get; set; }
        public TimeLIneData? data { get; set; }
        public object groupId { get; set; }
        public string? hash { get; set; }
        public string? id { get; set; }
        public string? osFamily { get; set; }
        public string? primaryDescription { get; set; }
        public string? secondaryDescription { get; set; }
        public string? siteId { get; set; }
        public object? threatId { get; set; }
        public DateTime? updatedAt { get; set; }
        public object userId { get; set; }
    }


}
