using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class GetClientDataResponse:baseResponse 
    {
        public List<ClientModel> ClientList { get; set; }
    }
}
