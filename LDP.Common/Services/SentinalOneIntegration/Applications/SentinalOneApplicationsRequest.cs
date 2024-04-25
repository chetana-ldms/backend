using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    public class SentinelOneAccountStructure
    {
        public string? AccountId { get; set; }
        public string? SiteId { get; set; }
        public string? GroupId { get; set; }

    }
    public class SentinalOneApplicationsRequest
    {
        public int OrgID { get; set; }
    }

    public class SentinalOneApplicationsInventoryRequest
    {
        public int OrgID { get; set; }
        // public string? SiteId { get; set; }
        public List<AccountStructureLevel>? OrgAccountStructureLevel { get; set; }

    }

    public class SentinalOneApplicationsEndPointsRequest
    {
        public int OrgID { get; set; }
        public string? ApplicationName { get; set; }

        public string? ApplicationVendor { get; set;}
    }

    public class SentinalOneApplicationsCVSRequest
    {
        public int OrgID { get; set; }
        // public string? ApplicationName { get; set; }

        //public string? ApplicationVendor { get; set; }

        public string? ApplicationId { get; set; }

     
    }

    public class SentinalOneApplicationsAgentRequest
    {
        public int OrgID { get; set; }
        public string? EndPiontId { get; set; }

        public List<AccountStructureLevel>? OrgAccountStructureLevel { get; set; }



    }

    //public class SentinalOneEndPointApplicationsRequest
    //{
    //    public int OrgID { get; set; }
    //    public string? EndPiontId { get; set; }


    //}


    public class GetSentinalOneEndPointApplicationsRequest
    {
        public int OrgID { get; set; }
        public string? EndPointId { get; set; }


    }
    public class GetSentinalOneEndPointApplicationsRisksRequest
    {
        public int OrgID { get; set; }
        // public string? EndPointId { get; set; }
        public List<AccountStructureLevel>? OrgAccountStructureLevel { get; set; }
    }

    public class GetRiskApplicationEndpointRequest
    {
        public int OrgID { get; set; }
        public string? ApplicationId { get; set; }

   }

    public class GetApplicationSettingsRequest
    {
        public int OrgID { get; set; }
        // public string? EndPointId { get; set; }
    }
    public class GetEndPointUpdatesRequest
    {
        public int OrgID { get; set; }
        public string? EndPointId { get; set; }
    }

    public class GetEndPointsRequest
    {
        public int OrgID { get; set; }
        //public string? EndPointId { get; set; }
    }
}
