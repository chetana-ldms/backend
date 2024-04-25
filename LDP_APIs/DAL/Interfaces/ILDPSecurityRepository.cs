using LDP_APIs.APIRequests;
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
        Task<List<User>> GetUsers(int OrgId);
        Task<User> GetUserbyName(string name);

        Task<SelectUserModel> GetUserbyID(int id);
        Task<User> GetUserbyNameOnUpdate(string name , int id);
        Task<string> DeleteUser(DeleteUserRequest request);
        Task<List<Role>> GetRoles(int orgId);
        Task<Role> GetRole(int roleID);

        Task<Role> GetRoleByID(int roleID);

        Task<Role> GetRolebyName(string name);

        Task<Role> GetRolebyNameOnUpdate(string name, int id);
        Task<string> AddRole(Role request);
        Task<string> UpdateRole(UpdateRoleRequest request);
        Task<string> DeleteRole(DeleteRoleRequest request);
        Task<string> ChangeUserPassword(ChangeUserPasswordRequest request);
        Task<string> ResetPassword(ResetPasswordRequest request);
    }
  
}
