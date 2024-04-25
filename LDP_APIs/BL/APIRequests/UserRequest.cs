using LDP_APIs.BL.Models;

namespace LDP_APIs.BL.APIRequests
{
    public class AddUserRequest: AdduserModel
    {
    }

    public class UpdateUserRequest : UpdateUserModel
    {
    }

    public class ChangePasswordCommon
    {
        
        public string? UpdatedByUserName { get; set; }
        public DateTime UpdatedDete { get; set; }

        public int ? UserId { get; set; }   
    }
    public class ChangeUserPasswordRequest: ChangePasswordCommon
    {
        public string NewPassword { get; set; }
       // public string UserName { get; set; }
        public string OldPassword { get; set; } 
       

    }
    public class ResetPasswordRequest : ChangePasswordCommon
    {

    }

}
