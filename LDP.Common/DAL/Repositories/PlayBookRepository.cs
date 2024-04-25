using LDPRuleEngine.Controllers.Requests;
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
            var res = await _context.vm_Play_Books.Where(pb => pb.active == 1 ).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<List<PlayBook>> GetPlaybooks(int orgId)
        {
            var res = await _context.vm_Play_Books.Where(pb => pb.active == 1 && pb.org_id == orgId).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<PlayBook> GetPlayBookByPlayBookID(int PlaybookID)
        {
           var res = await _context.vm_Play_Books.Where(pb => pb.Play_Book_ID == PlaybookID).AsNoTracking().FirstOrDefaultAsync();
           return res;
        }

        public async Task<PlayBook> GetPlayBookbyName(string name)
        {
            var res = await _context.vm_Play_Books.Where(pb => pb.Play_Book_Name.ToLower() == name.ToLower()).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }

        public async Task<PlayBook> GetPlayBookbyNameOnUpdate(string name, int id)
        {
            var res = await _context.vm_Play_Books
                .Where(pb => pb.Play_Book_Name.ToLower() == name.ToLower() && pb.Play_Book_ID != id)
                .AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        public async Task<string> UpdatePlayBook(PlayBook playbookrequest, List<PlayBookDtl> playbookdtlsrequest)
        {
            var playbook = await _context.vm_Play_Books.Where(pb => pb.Play_Book_ID == playbookrequest.Play_Book_ID
      ).AsNoTracking().FirstOrDefaultAsync();
            if (playbook == null)
            {
                return "Play book details not found for update the play book";
            }
            playbookrequest.Created_Date = playbook.Created_Date;
            playbookrequest.Created_User = playbook.Created_User;
            playbookrequest.active = playbook.active;
            playbookrequest.deleted_user = playbook.deleted_user;
            playbookrequest.deleted_date = playbook.deleted_date;

            playbookdtlsrequest.ForEach(dtl =>
            {
                dtl.Created_User = playbook.Created_User;
                dtl.Created_Date = playbook.Created_Date;
            });
                
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
                    return "Failed to update the play book data";
                }
            }

            return "success";
        }

        public async Task<string> DeletePlayBook(DeletePlayBookRequest request,string deletedUser)
        {
            var playbook = await _context.vm_Play_Books.Where(pb => pb.Play_Book_ID == request.PlayBookID
       ).                   AsNoTracking().FirstOrDefaultAsync();
            if (playbook == null)
            {
                return "Play book details not found for delete the play book";
            }

            playbook.active = 0;
            playbook.deleted_date = request.DeletedDate;
            playbook.deleted_user = deletedUser;
            playbook.Modified_Date = request.DeletedDate;
            playbook.Modified_User = deletedUser;

            _context.vm_Play_Books.Update(playbook);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the play book";

        }
    }
}
