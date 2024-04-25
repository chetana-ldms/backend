using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.DAL.Entities;

namespace LDP.Common.DAL.Interfaces
{
    public interface IAlertReportsRepository
    {
        public Task<List<Alerts>> GetAlertSummery(AlertReportsBase request);

        public Task<List<AlertExtnField>> GetAlertRulesSummery(AlertRulesSummeryReportRequest request);

        public AlertSLAMeasurementReportResponse GetAlertSLAMeasurementSummery(AlertSLAMeasurementReportRequest request);

    }
}
