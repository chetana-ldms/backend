using LDP.Common.Model.Common;
using LDP_APIs.Models;

namespace LDP.Common.Requests.Common
{
    public class AddActivityRequest:AddActivityModel
    {
       public Dictionary<string, string>? TemplateData { get ; set; }
       public string? ActivityType { get; set;}

        public string? AdditionalText { get; set; }

        public bool IsSuccess { get; set;}  

    }

    public class GetActivitiesRequest 
    {
        public int OrgId { get; set; }
        public int? UserId { get; set; }
        public List<int?>? ActivityTypeIds { get; set; }
        public List<int?>? AlertIds { get; set; }
        public List<int?>? IncidentIds { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public RequestPaging? paging { get; set; }

    }
}
