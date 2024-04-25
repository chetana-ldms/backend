using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IAlertExtnFieldBL
    {
        AddAlertExtnFieldResponse AddAlertExtnFields(AlertExtnFieldModel request);
        AddAlertExtnFieldResponse AddRangeAlertExtnFields(List<AlertExtnFieldModel> request);
        GetAlertExtnFieldResponse GetAlertExtnFields(GetAlertExtnFieldRequest request);
    }
}
