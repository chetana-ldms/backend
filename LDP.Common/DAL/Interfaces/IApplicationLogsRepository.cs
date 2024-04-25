using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Entities.Common;

namespace LDP.Common.DAL.Interfaces
{
    public interface IApplicationlogsRepository
    {
        Task<string> AddApplicatinLog(ApplicationLog request);

       // List<useraction> GetUserActions(int UserID);

        
    }
}
