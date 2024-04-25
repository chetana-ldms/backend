using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class GetRolesResponse:baseResponse 
    {
        public List<GetRoleModel> RolesList { get; set; }
    }

    public class AddRoleResponse : baseResponse
    {
        
    }

    public class UpdateRoleResponse : baseResponse
    {

    }

    public class DeleteRoleResponse : baseResponse
    {

    }

    public class GetRoleResponse : baseResponse
    {
        public GetRoleModel RoleData { get; set; }
    }
}
