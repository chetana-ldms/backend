using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class AlertHistoryResponse:baseResponse
    {

    }
    public class GetAlertHistoryResponse : baseResponse
    {
        public List<AlertHistoryModel>? AlertHistoryData { get; set; }
    }
}
