using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Helpers;
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
            var res = await _context.vm_users.Where(user => user.User_Name.ToLower() == request.UserName.ToLower()
                                    && user.Salt_Password.ToLower() == request.Password.ToLower()
                                    && user.active==1).AsNoTracking().FirstOrDefaultAsync();

           return res;
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
            request.org_id = user.org_id;
            _context.vm_users.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }
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
  
            return await _context.vm_users.Where(user => user.active == 1 && user.org_id == OrgId)
            .AsNoTracking().ToListAsync(); ;
        }

        public async Task<User> GetUserbyName(string name)
        {
            var res = await _context.vm_users.Where(user => user.User_Name.ToLower() == name.ToLower()).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }

        public  async Task<SelectUserModel> GetUserbyID(int id)
        {
            
           var userdtl = _context.vm_users
            .Join(_context.vm_roles,
                u => u.User_ID,
                r => r.Role_ID,
                (u,r) => new SelectUserModel
                {
                    Active = u.active,
                    CreatedByUserName = u.Created_user,
                    CreatedDete= u.Created_date,
                    Name = u.User_Name,
                    RoleID = r.Role_ID,
                    RoleName = r.Role_Name,
                    SysUser = u.sys_user,
                    UpdatedByUserName = u.Modified_user,
                    UpdatedDete = u.Modified_date,
                    UserID = u.User_ID,
                    OrgId = u.org_id,
                    ClientAdminRole = r.client_admin_role,
                    GlobalAdminRole = r.global_admin_role

                }).
            Where(user => user.UserID == id).AsNoTracking().FirstOrDefaultAsync();

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
            return _context.vm_roles.Where(role => role.active ==1 && role.org_id == orgId).AsNoTracking().ToListAsync();
        }

        public async  Task<Role> GetRole(int roleID)
        {
            var res = await _context.vm_roles.Where(role => role.Role_ID == roleID
            ).AsNoTracking().FirstOrDefaultAsync();
            return  res;
        }
        public async Task<Role> GetRoleByID(int roleID)
        {
           
                var res = await _context.vm_roles.Where(role => role.Role_ID == roleID
          ).AsNoTracking().FirstOrDefaultAsync();
                return res;
        }
        public  async Task<string> ChangeUserPassword(ChangeUserPasswordRequest request)
        {
            var user = await _context.vm_users.Where(usr => usr.User_ID == request.UserId
            && usr.Salt_Password == request.OldPassword).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
            {
                return "Invalid login details";
            }
            else
            {
                user.Salt_Password = request.NewPassword;
            }

            _context.vm_users.Update(user);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed to update the password";
        }

        public async Task<string> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _context.vm_users.Where(usr => usr.User_ID == request.UserId).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
            {
                return "User Not found";
            }
            else
            {
                user.Salt_Password = Constants.defaultPassword;
            }

            _context.vm_users.Update(user);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed to update the change the password";
        }

        public async Task<string> DeleteUser(DeleteUserRequest request)
        {
            var user = await _context.vm_users.Where(usr => usr.User_ID == request.UserID
            ).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
            {
                return "User details not for delete the user";
            }
            else
            {
                if (user.sys_user==1)
                {
                    return "You cannot delete the system users";
                }
                user.active = 0;
                user.deleted_date = request.DeletedDate.Value.ToUniversalTime();
                user.deleted_user = request.DeletedUser;
                user.Modified_date = request.DeletedDate.Value.ToUniversalTime();
                user.Modified_user = request.DeletedUser;

            }

            _context.vm_users.Update(user);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed to delete the user";
        }

        public async Task<string> AddRole(Role request)
        {
             _context.vm_roles.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed to add the role";
        }
        public  async Task<string> DeleteRole(DeleteRoleRequest request)
        {
            var role = await _context.vm_roles.Where(role => role.Role_ID == request.RoleID
             ).AsNoTracking().FirstOrDefaultAsync();
            if (role == null)
            {
                return "role details not for delete the role";
            }
            if (role.sys_role == 1)
            {
                return "You cannot delete the system roles";
            }
            role.active = 0;
            role.deleted_date = request.DeletedDate.Value.ToUniversalTime();
            role.deleted_user = request.DeletedUser;
            role.Modified_date = request.DeletedDate.Value.ToUniversalTime();
            role.Modified_user = request.DeletedUser;

        _context.vm_roles.Update(role);

        var status = await _context.SaveChangesAsync();

        if (status > 0)
            return "";
        else
            return "failed to delete the role";
    }

        public async Task<string> UpdateRole(UpdateRoleRequest request)
        {
            var role = await _context.vm_roles.Where(usr => usr.Role_ID == request.RoleID).AsNoTracking().FirstOrDefaultAsync();
            if (role == null)
            {
                return "Role data not found to update";
            }

            role.Role_Name = request.RoleName;
            role.sys_role = request.Sysrole;
            role.Modified_date = request.Modifieddate;
            role.Modified_user = request.Modifieduser;
            _context.vm_roles.Update(role);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
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
