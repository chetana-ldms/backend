using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.DAL.Repositories
{
    public interface IPlayBookDtlsRepository
    {
        Task<string> AddPlayBookDtls(PlayBookDtl request);

        Task<string> AddRangePlayBookDtls(List<PlayBookDtl> request);
        Task<List<PlayBookDtl>> GetPlaybookDtls();
        Task<List<PlayBookDtl>> GetPlaybookDtlsByPlaybookID(int PlaybookID);
        Task<string> UpdatePlaybookDtls(PlayBookDtl request);

        Task<string> UpdateRangePlaybookDtls(List<PlayBookDtl> request);
    }
}