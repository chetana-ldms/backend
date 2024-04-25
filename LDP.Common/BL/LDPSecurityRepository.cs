using LDP.Common;
using LDP.Common.Services.DrataIntegration;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.Respository
{
    public class LDPSecurityRepository : ILDPSecurityRepository
    {
        private readonly LDPSecurityDataContext? _context;
        public LDPSecurityRepository(LDPSecurityDataContext context)
        {
            _context = context;
        }
       

        public async Task<User> AuthenticateUser(AuthenticateUserRequest request)
        {
            // user.Role_ID == 1 --- Global admin
            var res = await _context.vm_users.Where(
                                    user => (user.User_Name.ToLower() == request.UserName.ToLower() || user.email_id.ToLower() == request.UserName.ToLower())
                                    && user.Salt_Password.ToLower() == request.Password.ToLower()
                                    && user.active==1
                                    && (user.org_id ==  request.OrgId || user.Role_ID == 1 ))
                                   .AsNoTracking().FirstOrDefaultAsync();

           return res;
            //
        }
        public async Task<string> UpdateUser(User request)
        {
            var user = await _context.vm_users.Where(usr => usr.User_ID == request.User_ID).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
            {
                request.Salt_Password = Constants.defaultPassword;
            }
            else
            {
                request.Salt_Password = user.Salt_Password;
            }
            request.Created_user = user.Created_user;
            request.Created_date = user.Created_date;
            request.active= user.active;
            request.sys_user = user.sys_user;
            request.deleted_date = user.deleted_date;
            request.deleted_user = user.deleted_user;
           // request.org_id = user.org_id;
            _context.vm_users.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to upate the user ";
        }
        //public async Task<string> UpdateOpenTaskFlag(int userId)
        //{
        //   var user = await _context.vm_users.Where(usr => usr.User_ID == userId).AsNoTracking().FirstOrDefaultAsync();
        //    user.open_task_flag = 1;
        //   // request.org_id = user.org_id;
        //    _context.vm_users.Update(user);

        //    var status = await _context.SaveChangesAsync();

        //    if (status > 0)
        //        return "";
        //    else
        //        return "Failed to update user Open task flag ";
        //}
        public async Task<string> AddNewUser(User request)
        {
            request.Salt_Password = Constants.defaultPassword;
            _context.vm_users.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }
        public async Task<List<User>> GetUsers(int OrgId)
        {
            List<int> userIds = new List<int>() { 1, OrgId };
            return await _context.vm_users.Where(user => user.active == 1 && userIds.Contains( user.org_id))
            .AsNoTracking().ToListAsync(); 
        }

        public async Task<bool> CheckDefaultPasswordSet(int userId)
        {
            var res = await _context.vm_users.Where(user => user.User_ID == userId
             && user.Salt_Password == Constants.defaultPassword).CountAsync();

            bool _boolRes = false;

            if (res >=1)
            {
                _boolRes = true;
            }
            return _boolRes;
        }
        public async Task<User> GetUserbyName(string name)
        {
            var res = await _context.vm_users.Where(user => user.User_Name.ToLower() == name.ToLower()).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }

        public  async Task<SelectUserModel> GetUserbyID(int? id)
        {


            var userdtl = await (
                        from u in _context.vm_users
                        join r in _context.vm_roles on u.Role_ID equals r.Role_ID
                        where u.User_ID == id && u.active == 1
                        select new SelectUserModel
                        {
                            Active = u.active,
                            CreatedByUserName = u.Created_user,
                            CreatedDete = u.Created_date,
                            Name = u.User_Name,
                            RoleID = r.Role_ID,
                            RoleName = r.Role_Name,
                            SysUser = u.sys_user,
                            UpdatedByUserName = u.Modified_user,
                            ModifiedDate = u.Modified_date,
                            UserID = u.User_ID,
                            OrgId = u.org_id,
                            ClientAdminRole = r.client_admin_role,
                            GlobalAdminRole = r.global_admin_role,
                             EmailId = u.email_id
                        }
                    ).FirstOrDefaultAsync();


            return userdtl;
        }

        public async Task<SelectUserModel> GetUserbyNameOrEmail(string request)
        {
            var userdtl = await (
                        from u in _context.vm_users
                        join r in _context.vm_roles on u.Role_ID equals r.Role_ID
                        where u.User_Name.ToLower() == request.ToLower() || u.email_id.ToLower() == request.ToLower()
                        && u.active == 1
                        select new SelectUserModel
                        {
                            Active = u.active,
                            CreatedByUserName = u.Created_user,
                            CreatedDete = u.Created_date,
                            Name = u.User_Name,
                            RoleID = r.Role_ID,
                            RoleName = r.Role_Name,
                            SysUser = u.sys_user,
                            UpdatedByUserName = u.Modified_user,
                            ModifiedDate = u.Modified_date,
                            UserID = u.User_ID,
                            OrgId = u.org_id,
                            ClientAdminRole = r.client_admin_role,
                            GlobalAdminRole = r.global_admin_role,
                            EmailId = u.email_id
                        }
                    ).FirstOrDefaultAsync();


            return userdtl;
        }
        public async Task<List<SelectUserModel>> GetAdminUserList(int orgId)
        {
            var userdtl = await (
                        from u in _context.vm_users
                        join r in _context.vm_roles on u.Role_ID equals r.Role_ID
                        where ( u.org_id == orgId || u.org_id ==1 )
                        && u.active == 1
                         && (r.global_admin_role == 1 || r.client_admin_role == 1 )
                        select new SelectUserModel
                        {
                            Active = u.active,
                            CreatedByUserName = u.Created_user,
                            CreatedDete = u.Created_date,
                            Name = u.User_Name,
                            RoleID = r.Role_ID,
                            RoleName = r.Role_Name,
                            SysUser = u.sys_user,
                            UpdatedByUserName = u.Modified_user,
                            ModifiedDate = u.Modified_date,
                            UserID = u.User_ID,
                            OrgId = u.org_id,
                            ClientAdminRole = r.client_admin_role,
                            GlobalAdminRole = r.global_admin_role,
                            EmailId = u.email_id
                        }
                    ).ToListAsync();


            return userdtl;
        }
        public async Task<List<SelectUserModel>> GetUserbyIds(List<int> ids)
        {

            var userdtl = _context.vm_users
             .Join(_context.vm_roles,
                 u => u.User_ID,
                 r => r.Role_ID,
                 (u, r) => new SelectUserModel
                 {
                     Active = u.active,
                     CreatedByUserName = u.Created_user,
                     CreatedDete = u.Created_date,
                     Name = u.User_Name,
                     RoleID = r.Role_ID,
                     RoleName = r.Role_Name,
                     SysUser = u.sys_user,
                     UpdatedByUserName = u.Modified_user,
                     ModifiedDate = u.Modified_date,
                     UserID = u.User_ID,
                     OrgId = u.org_id,
                     ClientAdminRole = r.client_admin_role,
                     GlobalAdminRole = r.global_admin_role

                 }).
             Where(user => ids.Contains(user.UserID)).AsNoTracking().ToListAsync();

            return userdtl.Result;
        }

        public async Task<User> GetUserbyNameOnUpdate(string name, int id)
        {
            var res = await _context.vm_users
                .Where(user => user.User_Name.ToLower() == name.ToLower() && user.User_ID != id )
                .AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        public Task<List<Role>> GetRoles(int orgId)
        {
            List<int> orgIds = new List<int>()
            {
                orgId , 1
            };
            return _context.vm_roles.Where(role => role.active == 1 && orgIds.Contains(role.org_id)).AsNoTracking().ToListAsync();
        }

        public async  Task<Role> GetRole(int roleID)
        {
            var res = await _context.vm_roles.Where(role => role.Role_ID == roleID && role.active == 1
            ).AsNoTracking().FirstOrDefaultAsync();
            return  res;
        }
        public async Task<Role> GetRoleByID(int roleID)
        {
           
                var res = await _context.vm_roles.Where(role => role.Role_ID == roleID && role.active == 1
          ).AsNoTracking().FirstOrDefaultAsync();
                return res;
        }
        public  async Task<string> ChangeUserPassword(ChangeUserPasswordRequest request, string modifiedUserName)
        {
            var user = await _context.vm_users.Where(usr => usr.User_ID == request.UserId && usr.active == 1
            && usr.Salt_Password == request.OldPassword).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
            {
                return "Invalid user ";
            }
            else
            {
                user.Salt_Password = request.NewPassword;
                user.Modified_date = request.ModifiedDate;
                user.Modified_user = modifiedUserName;
            }

            _context.vm_users.Update(user);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the password";
        }

        public async Task<string> ResetPassword(ResetPasswordRequest request , string ModifiedUserName)
        {
            var user = await _context.vm_users.Where(usr => usr.User_ID == request.UserId && usr.active == 1 ).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
            {
                return "Invalid user";
            }
            else
            {
                user.Salt_Password = Constants.defaultPassword;
                user.Modified_date = request.ModifiedDate;
                user.Modified_user = ModifiedUserName;
            }

            _context.vm_users.Update(user);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed to reset the password";
        }

        public async Task<string> DeleteUser(DeleteUserRequest request,string deletedUserName)
        {
            var user = await _context.vm_users.Where(usr => usr.User_ID == request.UserID
            ).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
            {
                return "Invalid user";
            }
            else
            {
                if (user.sys_user==1)
                {
                    return "You cannot delete the system users";
                }
                user.active = 0;
                user.deleted_date = request.DeletedDate;
                user.deleted_user = deletedUserName;
                user.Modified_date = request.DeletedDate;
                user.Modified_user = deletedUserName;

            }

            _context.vm_users.Update(user);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the user";
        }

        public async Task<string> AddRole(Role request)
        {
             _context.vm_roles.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the role";
        }
        public  async Task<string> DeleteRole(DeleteRoleRequest request,string deletedUserName)
        {
            var role = await _context.vm_roles.Where(role => role.Role_ID == request.RoleID
             ).AsNoTracking().FirstOrDefaultAsync();
            if (role == null)
            {
                return "Invalid role";
            }
            if (role.sys_role == 1)
            {
                return "You cannot delete the system roles";
            }
            role.active = 0;
            role.deleted_date = request.DeletedDate;
            role.deleted_user = deletedUserName;
            role.Modified_date = request.DeletedDate;
            role.Modified_user = deletedUserName;

        _context.vm_roles.Update(role);

        var status = await _context.SaveChangesAsync();

        if (status > 0)
            return "";
        else
            return "Failed to delete the role";
    }

        public async Task<string> UpdateRole(UpdateRoleRequest request, string updatedUserName)
        {
            var role = await _context.vm_roles.Where(usr => usr.Role_ID == request.RoleID && usr.active == 1).AsNoTracking().FirstOrDefaultAsync();
            if (role == null)
            {
                return "Invalid role";
            }

            role.Role_Name = request.RoleName;
            role.sys_role = request.Sysrole;
            role.Modified_date = request.Modifieddate;
            role.Modified_user = updatedUserName;
            role.org_id = request.OrgId;
            _context.vm_roles.Update(role);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the role";
        }

        public async Task<Role> GetRolebyName(string name)
        {
            var res = await _context.vm_roles
              .Where(role => role.Role_Name.ToLower() == name.ToLower() )
              .AsNoTracking().FirstOrDefaultAsync();

            return res;
        }

        public async Task<Role> GetRolebyNameOnUpdate(string name, int id)
        {
            var res = await _context.vm_roles
               .Where(role => role.Role_Name.ToLower() == name.ToLower() && role.Role_ID != id)
               .AsNoTracking().FirstOrDefaultAsync();

            return res;

        }

       
    }
}
