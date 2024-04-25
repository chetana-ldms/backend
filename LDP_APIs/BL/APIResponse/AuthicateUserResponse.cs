using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class AuthicateUserResponse:baseResponse 
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public string? UserName { get; set; }

        public string? RoleName { get; set; }

        public int OrgId { get; set; }
        public int GlobalAdminRole { get; set; }
        public int ClientAdminRole { get; set; }

    }

    
}
