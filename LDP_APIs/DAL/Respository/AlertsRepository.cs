
using LDP_APIs.BL.APIRequests;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Entities;
using LDP_APIs.DAL.Interfaces;
using LDP_APIs.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LDP_APIs.DAL.Respository
{
    public class AlertsRepository: IAlertsRepository
    {
        private readonly AlertsDataContext? _context;
        public AlertsRepository(AlertsDataContext context)
        {
            _context = context;
        }

        public async Task<string> Addalerts(List<Alerts> request)
       {
            try
            {
                _context.vm_alerts.AddRange(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return "success";
        }



        public List<Alerts> Getalerts(GetOffenseRequest request)
        {
            try
            {
                var res = _context.vm_alerts.OrderBy(alrt => alrt.alert_id).Skip(request.paging.RangeStart).Take(request.paging.RangeEnd).ToList();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public double GetAlertsDataCount()
        {
           return _context.vm_alerts.Count();
        }

        public async Task<string> AssignOwner(AssignOwnerRequest request)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.alertID ).AsNoTracking().FirstOrDefaultAsync();
            if (alertdata == null)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.owner_user_id = request.UserID;
            }

            _context.vm_alerts.Update(alertdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the user to alert ";
        }

        public async Task<List<Alerts>> GetAlertData(GetAlertRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.alertID)
                .AsNoTracking().ToListAsync();

            return res;
        }
        
        public async Task<List<Alerts>> GetAlertsByAssignedUser(GetAlertByAssignedOwnerRequest request)
        {
            //var res = await _context.vm_alerts.Where(alrt => alrt.owner_user_id == request.UserID && request.StatusIDs.Contains(alrt.status_ID)  )
            //    .OrderBy(alrt => alrt.alert_id).Skip(request.paging.RangeStart)
            //    .Take(request.paging.RangeEnd).ToListAsync();
            //return res;

            return null;
        }

        public async Task<double> GetAlertsCountByAssignedUser(GetAlertByAssignedOwnerRequest request)
        {
            //var res = await _context.vm_alerts.Where(alrt => alrt.owner_user_id == request.UserID 
            //&& request.StatusIDs.Contains(alrt.status_ID)).CountAsync();

            return 0 ;
        }
    }
}
