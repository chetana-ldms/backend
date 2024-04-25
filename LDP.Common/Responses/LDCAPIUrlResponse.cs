using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class GetLDCAPIUrlResponse:baseResponse
    {
        public List<LDCUrlModel> UrlList { get; set; }
    }
}
