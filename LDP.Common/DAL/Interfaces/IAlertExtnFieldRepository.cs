using LDP.Common.DAL.Entities;
using LDP.Common.Requests;

namespace LDP.Common.DAL.Interfaces
{
    public interface IAlertExtnFieldRepository
    {
        Task<int> AddAlertExtnFields(AlertExtnField request);

        Task<int> AddRangeAlertExtnFields(List<AlertExtnField> request);

        Task<List<AlertExtnField>> GetAlertExtnFieldsAsync(GetAlertExtnFieldRequest request);

    }
}
