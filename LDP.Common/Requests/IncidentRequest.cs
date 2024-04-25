using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Requests
{
   

    public class CreateIncidentRequest : AddIncidentModel
    {
     
        public int CreateUserId { get; set; }

        public bool CreateInternalIncident { get; set; }

    }
    public class GetIncidentsRequest : PagingRequest
    {
        public int LoggedInUserId { get; set; }
    }

    public class GetInternalIncidentDataRequest 
    {
        public int IncidentID { get; set; }
    }
    public class GetInternalIncidentsByIncidentIDsRequest : baseRequest
    {
        public List<int>? IncidentIDList { get; set; }
    }

    public class GetUnAttendedIncidentCount : baseRequest
    {
        public int? UserID { get; set; }
        public double? NumberofDays { get; set; }

    }

    public class IncidentUpdateBaseRequest : baseRequest
    {
        public int IncidentID { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int ModifiedUserId { get; set;}
    }
    public class IncidentAssignOwnerRequest: IncidentUpdateBaseRequest
    {
        //public int IncidentID { get; set; }
        public int OwnerUserID { get; set; }
        public string? OwnerUserName { get; set; }
        //public string? ModifiedUserName { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }

    public class SetIncidentPriorityRequest : IncidentUpdateBaseRequest
    {
        //public int IncidentID { get; set; }
        public int PriorityID { get; set; }

        public string? PriorityValue { get; set; }
       // public string? ModifiedUser { get; set; }
       // public DateTime? ModifiedDate { get; set; }
    }

    public class SetIncidentStatusRequest : IncidentUpdateBaseRequest
    {
        //public int IncidentID { get; set; }
        public int StatusID { get; set; }
        public string? StatusName { get; set; }
        //public string? ModifiedUser { get; set; }
        //public DateTime? ModifiedDate { get; set; }
    }

    public class SetIncidentSeviarityRequest : IncidentUpdateBaseRequest
    {
        //public int IncidentID { get; set; }
        public int SeviarityID { get; set; }
        public string? Seviarity { get; set; }
        //public string? ModifiedUser { get; set; }
        //public DateTime? ModifiedDate { get; set; }
    }

    public class SetIncidentTypeRequest : IncidentUpdateBaseRequest
    {
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
    }

    public class SetIncidentScoreRequest : IncidentUpdateBaseRequest
    {
        //public int IncidentID { get; set; }
        public string? Score { get; set; }
        //public string? ModifiedUser { get; set; }
        //public DateTime? ModifiedDate { get; set; }
    }

    public class GetMyInternalIncidentsRequest : baseRequest
    {
        public int UserID { get; set; }

    }

    public class GetIncidentCountByPriorityAndStatusRequest : baseRequest
    {
        public int? UserID { get; set; }
        public double? NumberofDays { get; set; }

        public int? PriorityID { get; set; }

        public int? StatusID { get; set; }

    }

    public class IncidentSeeachRequest : PagingRequest
    {
        public int LoggedInUserId { get; set; }

        public int StatusId { get; set; }
        public string? SearchText { get; set; }

        public int SortOptionId { get; set; }

    }

    public enum SortOptions
    {
        RecentModified,
        RecentCreated
    }

    public class UpdateIncidentRequest 
    {
        public int IncidentId { get; set; }

        public int StatusId { get; set; }

        public int PriorityId { get; set; }

        public int SeverityId { get; set; }

        public string? Score { get; set; }

       // public int ObservableTagId { get; set; }
        public int TypeId { get; set; }

        public int OwnerUserId { get; set; }

        //public string? AlertNote { get; set; }
        public bool SignificantIncident { get; set; }
        public int ModifiedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
