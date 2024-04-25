using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class UserResponse:baseResponse
    {

    }

    public class UserPasswordResponse : baseResponse
    {
        public string Password { get; set; }
    }
}
