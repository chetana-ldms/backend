using LDP.Common.DAL.Entities;
using LDP.Common.Requests;

namespace LDP.Common.DAL.Interfaces
{
    public interface IAlertPlayBookProcessRepository
    {
        Task<string> AddAlertPlayBookProcess(AddAlertPlayBookProcessRequest request);
        Task<string> AddRangeAlertPlayBookProcess(List<AlertPlayBookProcess> request);
        Task< List<AlertPlayBookProcess>> GetAlertPlayBookProcessByStatus(string status);
        Task<string> UpdateStatus(string stsatus, string user, DateTime updateDate);

        Task<List<AlertPlayBookProcess>> GetAlertPlayBookProcessByIDs(List<int> ProcessIDs);
    }
}
