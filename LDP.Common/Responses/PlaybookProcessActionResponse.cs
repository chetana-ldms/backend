using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class GetAlertPlayBookProcessactionResponse : baseResponse
    {
        public List<GetAlertPlayBookProcessActionModel>? AlertPlayBookProcessActionData { get; set; }
    }
    public class AlertPlaybookProcessActionResponse : baseResponse
    {
    }
}
