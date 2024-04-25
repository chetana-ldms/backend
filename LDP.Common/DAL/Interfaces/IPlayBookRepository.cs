using LDPRuleEngine.BL.Models;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.DAL.Interfaces
{
    public interface IPlayBookRepository
    {
        Task<string> AddPlayBook(PlayBook playbookrequest , List<PlayBookDtl> playbookdtlsrequest);
        Task<string> UpdatePlayBook(PlayBook playbookrequest, List<PlayBookDtl> playbookdtlsrequest);

        Task<string> DeletePlayBook(DeletePlayBookRequest request, string deletedUser);
        Task<List<PlayBook>> GetPlaybooks();

        Task<List<PlayBook>> GetPlaybooks(int orgId);
        Task<PlayBook> GetPlayBookByPlayBookID(int PlaybookID);

        Task<PlayBook> GetPlayBookbyName(string name);

        Task<PlayBook> GetPlayBookbyNameOnUpdate(string name, int id);

    }
}
