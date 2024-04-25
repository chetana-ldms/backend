using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications.RiskApplicaionsEndPoint
{
    public class RiskApplicationsEndPoints:baseResponse
    {
        public List<Data>? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }


    public class ExternalTicketSystem
    {
        public bool? Available { get; set; }
        public string? Type { get; set; }
    }

    public class Data
    {
        public string? AccountName { get; set; }
        public int? ApplicationDaysDetected { get; set; }
        public DateTime? ApplicationDetectionDate { get; set; }
        public string? ApplicationVersion { get; set; }
        public string? Domain { get; set; }
        public string? EndpointId { get; set; }
        public string? EndpointName { get; set; }
        public string? EndpointType { get; set; }
        public string? EndpointUuid { get; set; }
        public ExternalTicketSystem? ExternalTicketSystem { get; set; }
        public string? GroupName { get; set; }
        public DateTime? LastScanDate { get; set; }
        public string? LastScanResult { get; set; }
        public string? OsType { get; set; }
        public string? OsVersion { get; set; }
        public string? SiteName { get; set; }
      //  public object Ticket { get; set; }
    }

    //public class Pagination
    //{
    //    public string? NextCursor { get; set; }
    //    public int? TotalItems { get; set; }
    //}

    //public class RootObject
    //{
    //    public List<Data> Data { get; set; }
    //    public Pagination Pagination { get; set; }
    //}

    public class RiskApplicationEndpointData
    {
        public string? AccountName { get; set; }
        public int? ApplicationDaysDetected { get; set; }
        public DateTime? ApplicationDetectionDate { get; set; }
        public string? ApplicationVersion { get; set; }
        public string? Domain { get; set; }
        public string? EndpointId { get; set; }
        public string? EndpointName { get; set; }
        public string? EndpointType { get; set; }
        public string? EndpointUuid { get; set; }
        public ExternalTicketSystem? ExternalTicketSystem { get; set; }
        public string? GroupName { get; set; }
        public DateTime? LastScanDate { get; set; }
        public string? LastScanResult { get; set; }
        public string? OsType { get; set; }
        public string? OsVersion { get; set; }
        public string? SiteName { get; set; }
        //  public object Ticket { get; set; }
    }

    public class RiskApplicationsEndPointsResponse : baseResponse
    {
        public List<RiskApplicationEndpointData>? EndPoints { get; set; }
       public Pagination? Pagination { get; set; }
    }
}
