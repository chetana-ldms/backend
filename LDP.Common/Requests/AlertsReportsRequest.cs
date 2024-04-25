namespace LDP.Common.Requests
{
    public class AlertReportsBase
    {
        public int OrgId { get; set; }
        public DateTime AlertFromDate { get; set; }
        public DateTime AlertToDate { get; set; }
    }
    public class AlertSummeryReportRequest: AlertReportsBase
    {
        
    }

    public class AlertRulesSummeryReportRequest : AlertReportsBase
    {
     
    }

    public class AlertSLAMeasurementReportRequest : AlertReportsBase
    {

    }
}
