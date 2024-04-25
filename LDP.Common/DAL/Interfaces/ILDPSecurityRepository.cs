using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.DAL
{
    public interface ILDPSecurityRepository
    {
        Task<User> AuthenticateUser(AuthenticateUserRequest request);
        Task<string> AddNewUser(User request);
        Task<string> UpdateUser(User request);

       // Task<string> UpdateOpenTaskFlag(int userId);
        Task<List<User>> GetUsers(int OrgId);

        Task<User> GetUserbyName(string name);

        Task<SelectUserModel> GetUserbyID(int? id);

        Task<SelectUserModel> GetUserbyNameOrEmail(string request);
        Task<List<SelectUserModel>> GetAdminUserList(int orgId);
        Task<List<SelectUserModel>> GetUserbyIds(List<int> id);
              

        Task<User> GetUserbyNameOnUpdate(string name , int id);
        Task<string> DeleteUser(DeleteUserRequest request,string deletedUserName);
        Task<List<Role>> GetRoles(int orgId);
        Task<Role> GetRole(int roleID);

        Task<Role> GetRoleByID(int roleID);

        Task<Role> GetRolebyName(string name);

        Task<Role> GetRolebyNameOnUpdate(string name, int id);
        Task<string> AddRole(Role request);
        Task<string> UpdateRole(UpdateRoleRequest request,string updatedUserName);
        Task<string> DeleteRole(DeleteRoleRequest request, string updatedUserName);
        Task<string> ChangeUserPassword(ChangeUserPasswordRequest request,string modifiedUserName);
        Task<string> ResetPassword(ResetPasswordRequest request, string modifiedUserName);

        Task<bool> CheckDefaultPasswordSet(int userId);

    }
  
}
