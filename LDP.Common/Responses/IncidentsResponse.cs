using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    //public class GetInternelIncidentsResponse:baseResponse 
    //{
    //    public List<GetIncidentModel>? IncidentList { get; set; }

    //    public double? TotalIncidentsCount { get; set; }    
    //}

    public class CreateIncidentResponse : baseResponse
    {
        public string? IncidentNumber { get; set; }
    }

    public class CreateIncidentInternalResponse : baseResponse
    {
        public double IncidentID { get; set; }

        public string? IncidentJsonText { get; set; }   
    }

    public class getUnattendedIncidentcountResponse : baseResponse
    {
        public double? UnattendedIncidentCount { get; set; }
    }

    public class IncidentAssignOwnerResponse : baseResponse
    {
        
    }
    public class SetIncidentStatusResponse : baseResponse
    {

    }
    public class SetIncidentPriorityResponse : baseResponse
    {

    }
    public class SetIncidentSeviarityResponse : baseResponse
    {

    }


    public class SetIncidentScoreResponse : baseResponse
    {

    }
    public class SetIncidentTypeResponse : baseResponse
    {

    }
    
    public class GetIncidentsResponse : baseResponse
    {
        public List<GetIncidentModel>? IncidentList { get; set; }
        public double TotalIncidentsCount { get; internal set; }
    }

    public class GetIncidentResponse : baseResponse
    {
        public GetIncidentModel IncidentData { get; set; }
       
    }

    public class GetIncidentCountByPriorityAndStatusResponse : baseResponse
    {
        public double IncidentCount { get; set; }
    }

    public class SearchIncidentsResponse : baseResponse
    {
        public List<GetIncidentModel>? IncidentList { get; set; }
        public double TotalIncidentsCount { get; internal set; }
    }

    public class UpdateIncidentResponse : baseResponse
    {

    }

}
