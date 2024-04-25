using LDP.Common.BL;
using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class NoficationDetailRequest : NotificationDetailModel
    {
    }

    public class GetNoficationDetailRequest 
    {
        public string MessageId { get; set; }
    }

}
