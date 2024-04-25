using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using LDP_APIs.DAL.DataContext;

namespace LDP.Common.DAL.Repositories
{
    public class AlertExtnFieldRepository : IAlertExtnFieldRepository
    {
        private readonly AlertsDataContext? _context;

        public AlertExtnFieldRepository(AlertsDataContext context)
        {
            _context = context;
        }

        public async Task<int> AddAlertExtnFields(AlertExtnField request)
        {
            _context.vm_alerts_extn_fields.Add(request);
            var res = await _context.SaveChangesAsync();

            return res;
        }

        public async Task<int> AddRangeAlertExtnFields(List<AlertExtnField> request)
        {
            _context.vm_alerts_extn_fields.AddRange(request);
            var res = await _context.SaveChangesAsync();

            return res;
        }

        public Task<List<AlertExtnField>> GetAlertExtnFieldsAsync(GetAlertExtnFieldRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
