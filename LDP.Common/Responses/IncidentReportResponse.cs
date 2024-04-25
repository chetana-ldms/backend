using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class IncidentReportsResponseBase : baseResponse
    {
        public double TotalAlertsCount { get; set; }
    }
    public class AllCreatedIncidentsStatusReportResponse : IncidentReportsResponseBase
    {
        public List<IncidenttStatusSummery>? Data { get; set; }
    }

    public class ClosedIncidentsReportResponse : IncidentReportsResponseBase
    {
        public List<IncidenttStatusSummery>? Data { get; set; }

    }

    public class OpenIncidentsReportResponse : IncidentReportsResponseBase
    {
        public List<IncidenttStatusSummery>? Data { get; set; }

    }

    public class SignificantIncidentsReportResponse : IncidentReportsResponseBase
    {
        public List<IncidenttStatusSummery>? Data { get; set; }
    }

    public class IncidenttStatusSummery
    {
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public double AlertCount { get; set; }
        public double PercentageValue { get; set; }
    }

    
}
