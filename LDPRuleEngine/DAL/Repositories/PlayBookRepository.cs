using LDPRuleEngine.DAL.DataContexts;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LDPRuleEngine.DAL.Repositories
{
    public class PlayBookRepository : IPlayBookRepository
    {
        IPlayBookDtlsRepository _playbookdtlsRepo;

        private readonly RuleEngineDataContext? _context;
        public PlayBookRepository(RuleEngineDataContext context, IPlayBookDtlsRepository playbookdtlsRepo)
        {
            _context = context;
            _playbookdtlsRepo = playbookdtlsRepo;
        }
        public async Task<string> AddPlayBook(PlayBook playbookrequest, List<PlayBookDtl> playbookdtlsrequest)
        {
            using (IDbContextTransaction? transaction = _context?.Database.BeginTransaction())
            {
                try
                {
                    _context.vm_Play_Books.Add(playbookrequest);

                    await _context.SaveChangesAsync();

                    playbookdtlsrequest.ForEach(pbdtl => pbdtl.Play_Book_ID = playbookrequest.Play_Book_ID);

                    _playbookdtlsRepo.AddRangePlayBookDtls(playbookdtlsrequest);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
                return "success";

        }

        public async Task<List<PlayBook>> GetPlaybooks()
        {
            var res = await _context.vm_Play_Books.AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<PlayBook> GetPlayBookByPlayBookID(int PlaybookID)
        {
           var res = await _context.vm_Play_Books.Where(pb => pb.Play_Book_ID == PlaybookID).AsNoTracking().FirstOrDefaultAsync();
           return res;
        }

        public async Task<string> UpdatePlayBook(PlayBook playbookrequest, List<PlayBookDtl> playbookdtlsrequest)
        {
            using (IDbContextTransaction? transaction = _context?.Database.BeginTransaction())
            {
                try
                {
                    _context.vm_Play_Books.Update(playbookrequest);

                    await _context.SaveChangesAsync();

                   // playbookdtlsrequest.ForEach(pbdtl => pbdtl.Play_Book_ID = playbookrequest.Play_Book_ID);

                    _playbookdtlsRepo.UpdateRangePlaybookDtls(playbookdtlsrequest);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return "success";
        }
    }
}
