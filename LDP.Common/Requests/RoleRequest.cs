using LDP_APIs.BL.Models;

namespace LDP_APIs.BL.APIRequests
{
    public class AddRoleRequest:AddRoleModel
    {

    }

    public class UpdateRoleRequest : UpdateRoleModel
    {
        public int ModifiedUserId { get; set; }

    }

    public class DeleteRoleRequest : DeleteRoleModel
    {

    }
}
