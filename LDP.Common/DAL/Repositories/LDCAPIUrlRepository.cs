
using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class LDCAPIUrlRepository:ILDCAPIUrlRepository
    {
        private readonly CommonDataContext? _context;
           public LDCAPIUrlRepository(CommonDataContext context)
        {
            _context = context;
            
        }
        

        public async Task<List<LDCApiUrls>> GetLDCUrls(int orgId)
        {
            var res = await _context.vm_ldc_api_urls.Where(u => u.url_id == orgId || u.org_id == 0)
                .AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<List<LDCApiUrls>> GetLDCUrlsByGroup(string groupName)
        {
            var res = await _context.vm_ldc_api_urls.Where(u => u.url_group == groupName)
                 .AsNoTracking().ToListAsync();
            return res;
        }
        

        public async  Task<List<LDCApiUrls>> GetLDCUrlsByGroupAndAction(string groupName, string actionName)
        {
            var res = await _context.vm_ldc_api_urls.Where(u => u.url_group == groupName && u.action_name == actionName)
              .AsNoTracking().ToListAsync();
            return res;
        }
    }
}
