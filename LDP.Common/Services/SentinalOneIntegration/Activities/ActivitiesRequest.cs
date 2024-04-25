namespace LDP.Common.Services.SentinalOneIntegration.Activities
{
    public class ActivitiesRequest
    {
        public int  OrgId { get; set; }
        public int? ActivityTypeId { get; set; }
        public string? ActivityEmail { get; set; }
        public string? EndPoint { get; set; }
        public DateTime? ActivityFromDate { get; set; }
        public DateTime? ActivityToDate { get; set; }
    }

    public class ActivityTypesRequest
    {
        public int OrgId { get; set; }
       
    }
}
