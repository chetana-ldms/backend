using LDP.Common.DAL.Entities.Common;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
            public interface INotificationDetailBL
        {
            NoficationDetailResponse AddNotificationDetails(NoficationDetailRequest request);

            NoficationDetailResponse UpdateNotificationDetails(NoficationDetailRequest request);
            GetNoficationDetailResponse GetNotificationDetails(GetNoficationDetailRequest request);
            SendTeamsMessageResponse SendTeamsMessage(SendTeamsMessageRequest request);

        }

    
}
