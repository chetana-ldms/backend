using LDP.Common.DAL.Entities;
using LDP.Common.Requests;

namespace LDP.Common.DAL.Interfaces
{
    public interface IAlertPlayBookProcessActionRepository
    {

        Task<string> AddAlertPlayBookProcessAction(AlertPlayBookProcessAction request);
        Task<string> AddRangeAlertPlayBookProcessActions(List<AlertPlayBookProcessAction> request);

        Task<List<AlertPlayBookProcessAction>> GetAlertPlayBookProcessActionsByStatus(GetAlertPlayBookProcessActionsByStatusRequest request);
        Task<double> GetCountAlertPlayBookProcessActionsByStatus(GetAlertPlayBookProcessActionsByStatusRequest request);

        Task<string> UpdateActionStatus(UpdateActionStatusRequest request);
       
        }
}
