using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class GetUsersResponse:baseResponse 
    {
        public List<SelectUserModel>? UsersList { get; set; }
    }

    public class GetUserResponse : baseResponse
    {
        public SelectUserModel? Userdata { get; set; }
    }
}
