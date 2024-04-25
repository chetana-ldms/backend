using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class AlertReportsRepository : IAlertReportsRepository
    {

        private readonly AlertsDataContext? _context;
        public AlertReportsRepository(AlertsDataContext? context)
        {
            _context = context;
        }
        public AlertSLAMeasurementReportResponse GetAlertSLAMeasurementSummery(AlertSLAMeasurementReportRequest request)
        {
            AlertSLAMeasurementReportResponse res = new AlertSLAMeasurementReportResponse ();
            

            return res; //_repo.GetAlertSLAMeasurementSummery(request);

        }

        public async Task<List<AlertExtnField>> GetAlertRulesSummery(AlertRulesSummeryReportRequest request)
        {
           //
            var dbRes = await _context.vm_alerts_extn_fields
                        .Where(ext => _context.vm_alerts
                        .Where(alert => alert.org_id == request.OrgId
                                && alert.detected_time >= request.AlertFromDate
                                && alert.detected_time <= request.AlertToDate)
                                .Select(alert => alert.alert_id)
                                .Contains(ext.alert_id)
                        && ext.data_type == Constants.AlertAlertRuleType
                        ).AsNoTracking()
                        .ToListAsync();
            //
            return dbRes;
            //
        }

        public async Task<List<Alerts>> GetAlertSummery(AlertReportsBase request)
        {
            List<Alerts> res = null ;

            res = await _context.vm_alerts.Where(
            alert => alert.org_id == request.OrgId
            && alert.detected_time >= request.AlertFromDate
            && alert.detected_time <= request.AlertToDate
            ).AsNoTracking().ToListAsync();

            return res;
        }
    }
}
