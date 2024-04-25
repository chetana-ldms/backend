using LDP.Common.DAL.Entities.Common;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.DAL.Interfaces
{
    public interface INotificationDetailsRepository
    {
        Task<int> AddNotificationDetails(NotificaionDetail request);

        Task<int> UpdateNotificationDetails(NotificaionDetail request);
        Task<List<NotificaionDetail>> GetNotificationDetails(GetNoficationDetailRequest request);

    }
}
