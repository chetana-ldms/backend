using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IAlertReportsBL
    {
        public AlertSummeryReportResponse GetAlertSummery(AlertSummeryReportRequest request);

        public AlertRulesSummeryReportResponse GetAlertRulesSummery(AlertRulesSummeryReportRequest request);

        public AlertSLAMeasurementReportResponse GetAlertSLAMeasurementSummery(AlertSLAMeasurementReportRequest request);
    
    }
}
