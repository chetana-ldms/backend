using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Requests
{
    public class AddAlertPlayBookProcessRequest
    {
        public List<AddAlertPlayBookProcessModel>? AlertPlayBookProcessData { get; set; }

    }
    public class GetAlertPlayBookProcessActionsByStatusRequest: PagingRequest
    {
        public List<string>? Status { get; set; }  

    }
    public class UpdateActionStatusRequest
    {
        public string Status { get; set; }
        public int alertplaybooksprocessactionid { get; set; }
    }
}
