using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    public class GetAggragatedApplicationsWithRiskResponse : baseResponse
    {
        public List<Data> data { get; set; }
        public Pagination? pagination { get; set; }
    }
   
public class Status
    {
        public int count { get; set; }
        public string? key { get; set; }
        public string? label { get; set; }
        public string? ticketCategory { get; set; }
    }

    public class Data
    {
        public string? applicationType { get; set; }
        public int cveCount { get; set; }
        public int daysDetected { get; set; }
        public DateTime? detectionDate { get; set; }
        public int endpointCount { get; set; }
        public int endpointsWithoutTicket { get; set; }
        public bool estimate { get; set; }
        //public object? exploitCodeMaturity { get; set; }
        //public object? exploitedInTheWild { get; set; }
        public string? highestNvdBaseScore { get; set; }
        public string? highestRiskScore { get; set; }
        public string? highestSeverity { get; set; }
        public string? name { get; set; }
        public object? remediationLevel { get; set; }
        public List<Status>? statuses { get; set; }
        public string? vendor { get; set; }
        public int versionCount { get; set; }
    }

    //public class Pagination
    //{
    //    public string? nextCursor { get; set; }
    //    public int totalItems { get; set; }
    //}

    public class SentinalOneAggregatedApplicationData : baseResponse
    {
        public List<Data> ApplicationList { get; set; }
    }

}
