using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;

namespace LDP_APIs.BL.Interfaces
{
    public interface ILDPSecurityBL
    {
        AuthicateUserResponse AuthenticateUser(AuthenticateUserRequest request);
        UserResponse AddNewUser(AddUserRequest request);
        UserResponse UpdateUser(UpdateUserRequest request);

        UserResponse DeleteUser(DeleteUserRequest request);

        UserResponse ChangeUserPassword(ChangeUserPasswordRequest request);

        UserResponse ResetPassword(ResetPasswordRequest request);

        GetUsersResponse GetUsers(int OrgId);

        GetUserResponse GetUserbyID(int id);
        GetRolesResponse GetRoles(int orgId);

        GetRoleResponse GetRoleByID(int roleID);
        AddRoleResponse AddRole(AddRoleRequest request);
        UpdateRoleResponse UpdateRole(UpdateRoleRequest request);
        DeleteRoleResponse DeleteRole(DeleteRoleRequest request);
    }
}
