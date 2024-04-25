namespace LDP_APIs.Models
{
    public class GetOffenseRequest:baseRequest
    {
        public RequestPaging? paging { get; set; } 
       
    }

    public class PagingRequest : baseRequest
    {
        public RequestPaging? paging { get; set; }

    }

    public class GetAlertsRequest : baseRequest
    {
        public RequestPaging? paging { get; set; }

        public int LoggedInUserId { get; set; }

       
    }
}
