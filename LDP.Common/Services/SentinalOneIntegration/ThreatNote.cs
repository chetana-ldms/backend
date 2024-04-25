namespace LDP.Common.Services.SentinalOneIntegration
{

    public class GetThreatNote
    {
        public List<ThreatNoteData>? data { get; set; }
        public Pagination? pagination { get; set; }
    }
    
    public class ThreatNoteData
    {
        
        public string? id { get; set; }
        public string? text { get; set; }
        public DateTime? createdAt { get; set; }
        public string? creator { get; set; }
        public DateTime? updatedAt { get; set; }
        public bool edited { get; set; }
        public string? creatorId { get; set; }
        
    }

    //public class ThreatTimeLine
    //{
    //    public string? accountId { get; set; }
    //    public int activityType { get; set; }
    //    public object agentId { get; set; }
    //    public object agentUpdatedVersion { get; set; }
    //    public DateTime? createdAt { get; set; }
    //    public TimeLIneData? data { get; set; }
    //    public object groupId { get; set; }
    //    public string? hash { get; set; }
    //    public string? id { get; set; }
    //    public string? osFamily { get; set; }
    //    public string? primaryDescription { get; set; }
    //    public string? secondaryDescription { get; set; }
    //    public string? siteId { get; set; }
    //    public object? threatId { get; set; }
    //    public DateTime? updatedAt { get; set; }
    //    public object userId { get; set; }
    //}
}
