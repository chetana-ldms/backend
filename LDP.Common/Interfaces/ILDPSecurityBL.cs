using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Models;

namespace LDP_APIs.BL.Interfaces
{
    public interface ILDPSecurityBL
    {
        AuthicateUserResponse AuthenticateUser(AuthenticateUserRequest request);
        UserResponse AddNewUser(AddUserRequest request);
        UserResponse UpdateUser(UpdateUserRequest request);

        UserResponse DeleteUser(DeleteUserRequest request);

        UserResponse ChangeUserPassword(ChangeUserPasswordRequest request);

        (UserResponse,string) ResetPassword(ResetPasswordRequest request);

        GetUsersResponse GetUsers(int OrgId);

        GetUserResponse GetUserbyID(int? id);

        GetUsersResponse GetUserbyIds(List<int> ids);

        GetRolesResponse GetRoles(int orgId);

        GetRoleResponse GetRoleByID(int roleID);
        AddRoleResponse AddRole(AddRoleRequest request);
        UpdateRoleResponse UpdateRole(UpdateRoleRequest request);
        DeleteRoleResponse DeleteRole(DeleteRoleRequest request);

        int GetPasswordResetTaskOwnerId(int userId);

        SelectUserModel GetUserbyNameOrEmail(string request);
        List<SelectUserModel> GetAdminUserList(int orgId);

        // UserResponse UpdateOpenTaskFlag(int  userId);

        UserResponse CheckDefaultPasswordSet(int userId);
    }
}
