using LDP.Common.BL;
using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class GetNoficationDetailResponse:baseResponse
    {
        public List<NotificationDetailModel>? Data { get; set; }
    }

    public class NoficationDetailResponse : baseResponse
    {
     
    }
}
