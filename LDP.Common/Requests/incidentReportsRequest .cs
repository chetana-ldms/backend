namespace LDP.Common.Requests
{
    public class IncidentReportsBase
    {
        public int OrgId { get; set; }
        public DateTime IncidentFromDate { get; set; }
        public DateTime IncidentToDate { get; set; }
    }
    public class AllCreatedIncidentsStatusReportRequest : IncidentReportsBase
    {

    }

    public class ClosedIncidentsReportRequest : IncidentReportsBase
    {

    }

    public class OpenIncidentsReportRequest : IncidentReportsBase
    {

    }

    public class SignificantIncidentsReportRequest : IncidentReportsBase
    {

    }
}
