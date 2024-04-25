using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class AlertHistoryRepository : IAlertHistoryRepository
    {
        private readonly CommonDataContext? _context;
        public AlertHistoryRepository(CommonDataContext context)
        {
            _context = context;
        }

        public async Task<string> AddalertHistory(AlertHistory request)
        {
            _context.vm_alerts_history.Add(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<string> AddRangealertHistory(List<AlertHistory> request)
        {
            _context.vm_alerts_history.AddRange(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<List<AlertHistory>> GetalertHistory(GetAlertHistoryRequest request)
        {
            var res = await _context.vm_alerts_history.Where(ah => ah.alert_id == request.AlertId)
                .OrderByDescending(ah => ah.history_date)
                .AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<List<AlertHistory>> GetIncidentHistory(GetIncidentHistoryRequest request)
        {
            var res = await _context.vm_alerts_history.Where(ah => ah.incident_id == request.IncidentId)
                .OrderByDescending (ah => ah.history_date)
                .AsNoTracking().ToListAsync();
            return res;
        }
    }
}
