using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class ApplicationLogResponse:baseResponse
    {
    }
    public class GetApplicationLogResponse : baseResponse
    {
        public List<GetApplicationLogModel>? getApplicationLogs { get; set; }
    }
}
