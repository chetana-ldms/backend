using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using LDP_APIs.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class AlertPlayBookProcessActionRepository : IAlertPlayBookProcessActionRepository
    {
        private readonly AlertsDataContext? _context;
        public AlertPlayBookProcessActionRepository(AlertsDataContext context)
        {
            _context = context;
        }
        public async Task<string> AddAlertPlayBookProcessAction(AlertPlayBookProcessAction request)
        {
            _context.vm_alert_playbooks_process_actions.Add(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<string> AddRangeAlertPlayBookProcessActions(List<AlertPlayBookProcessAction> request)
        {
            _context.vm_alert_playbooks_process_actions.AddRange(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<List<AlertPlayBookProcessAction>> GetAlertPlayBookProcessActionsByStatus(GetAlertPlayBookProcessActionsByStatusRequest request)
        {
             var res = await _context.vm_alert_playbooks_process_actions
                .Where(action => request.Status.Contains(action.tool_action_status)
            && action.tool_id == request.ToolID)
                .OrderBy(action => action.alert_playbooks_process_action_id).Skip(request.paging.RangeStart)
                .Take(request.paging.RangeEnd).ToListAsync();
            return res;

        }

        public async Task<double> GetCountAlertPlayBookProcessActionsByStatus(GetAlertPlayBookProcessActionsByStatusRequest request)
        {
            var res = await _context.vm_alert_playbooks_process_actions
                .Where(action => request.Status.Contains(action.tool_action_status)
                && action.tool_id == request.ToolID)
              .CountAsync();
            return res;

        }

        public async Task<string> UpdateActionStatus(UpdateActionStatusRequest request)
        {
            var res = await _context.vm_alert_playbooks_process_actions.Where(action => action.alert_playbooks_process_action_id == request.alertplaybooksprocessactionid).FirstOrDefaultAsync();
            if (res == null)
                return "alert data not found to update";
            else
            {
                res.tool_action_status = request.Status;
                _context.SaveChangesAsync();

            }
            return "";
        }
    }
}
