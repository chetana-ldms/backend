using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class AlertReportsResponseBase:baseResponse
    {
        public double TotalAlertsCount { get; set; }   
    }
    public class AlertSummeryReportResponse : AlertReportsResponseBase
    {
        public List<AlertStatusSummery>? Data { get; set; }
    }

    public class AlertRulesSummeryReportResponse : AlertReportsResponseBase
    {
       public List<AlertRuleSummery>? Data { get; set; }

    }

    public class AlertSLAMeasurementReportResponse : AlertReportsResponseBase
    {
        public List<AlertSLASummery>? Data { get; set; }

    }
    public class AlertStatusSummery
    {
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public double AlertCount { get; set; }
        public double PercentageValue { get; set; }
    }

    public class AlertRuleSummery
    {
        public string? AlertRule { get; set; }
        public int AlertCount { get; set; }
    }

    public class AlertSLASummery
    {
        public string? SummeryPeriod { get; set; }

        public string? SummeryPeriodType { get; set; }
        public double SummeryPeriodAlertCount { get; set; }

        public double SummeryPeriodPercentageValue { get; set; }
        public int SevirityId { get; set; }
        public string? SevirityName { get; set; }
        public double SevirityWisePercentageValue { get; set; }

        public double SevirityWiseAlertCount { get; set; }
        public double SLAMetPercentageValue { get; set; }
    }
}
