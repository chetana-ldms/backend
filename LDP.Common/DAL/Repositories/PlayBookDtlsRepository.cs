using LDPRuleEngine.DAL.DataContexts;
using LDPRuleEngine.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDPRuleEngine.DAL.Repositories
{
    public class PlayBookDtlsRepository : IPlayBookDtlsRepository
    {
 
        private readonly RuleEngineDataContext? _context;
        public PlayBookDtlsRepository(RuleEngineDataContext context)
        {
            _context = context;
        }

        public async Task<string> AddPlayBookDtls(PlayBookDtl request)
        {
            _context.vm_Play_Book_dtls.Add(request);
            await _context.SaveChangesAsync();
            return "success"; ;
        }

        public async Task<string> AddRangePlayBookDtls(List<PlayBookDtl> request)
        {
            _context.vm_Play_Book_dtls.AddRange(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<List<PlayBookDtl>> GetPlaybookDtls()
        {
            var res = await _context.vm_Play_Book_dtls.AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<List<PlayBookDtl>> GetPlaybookDtlsByPlaybookID(int PlaybookID)
        {
            var res = await _context.vm_Play_Book_dtls.Where(d => d.Play_Book_ID == PlaybookID).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<string> UpdatePlaybookDtls(PlayBookDtl request)
        {
            _context.vm_Play_Book_dtls.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }

        public async Task<string> UpdateRangePlaybookDtls(List<PlayBookDtl> request)
        {
            var playbookid = request[0].Play_Book_ID;
            


            _context.vm_Play_Book_dtls.UpdateRange(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to upate the play book dtls ";
        }
    }
}
