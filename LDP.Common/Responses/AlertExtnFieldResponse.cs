using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class GetAlertExtnFieldResponse:baseResponse
    {
        public List<AlertExtnFieldModel> Data { get; set; }
    }

    public class AddAlertExtnFieldResponse : baseResponse
    {
       
    }
}
