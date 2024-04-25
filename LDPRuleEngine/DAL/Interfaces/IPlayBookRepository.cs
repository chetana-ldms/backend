using LDPRuleEngine.BL.Models;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.DAL.Interfaces
{
    public interface IPlayBookRepository
    {
        Task<string> AddPlayBook(PlayBook playbookrequest , List<PlayBookDtl> playbookdtlsrequest);
        Task<string> UpdatePlayBook(PlayBook playbookrequest, List<PlayBookDtl> playbookdtlsrequest);
        Task<List<PlayBook>> GetPlaybooks();
        Task<PlayBook> GetPlayBookByPlayBookID(int PlaybookID);
    }
}
