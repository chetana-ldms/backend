using AutoMapper;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL;
using System.Net;

namespace LDP_APIs.BL
{
    public class LDPSecurityBL : ILDPSecurityBL
    {
        ILDPSecurityRepository _repo;
        public readonly IMapper _mapper;

        public LDPSecurityBL(ILDPSecurityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        public AuthicateUserResponse AuthenticateUser(AuthenticateUserRequest request)
        {
            AuthicateUserResponse authicateUserResponse = new AuthicateUserResponse();
            var res =  _repo.AuthenticateUser(request);

            if (res.Result == null)
            {
                authicateUserResponse.Message = "Authentication failed";
                authicateUserResponse.IsSuccess = false;
                authicateUserResponse.HttpStatusCode = HttpStatusCode.Unauthorized;
               // authicateUserResponse.ErrorMessage = "User name / Password is invalid";
            }
            else
            {
                authicateUserResponse.IsSuccess = true;
               // authicateUserResponse.ClientID = res.Result.client_id;
                authicateUserResponse.UserID = res.Result.User_ID;
                authicateUserResponse.RoleID = res.Result.Role_ID;
                authicateUserResponse.UserName = res.Result.User_Name;
                authicateUserResponse.RoleID = res.Result.Role_ID;
                authicateUserResponse.OrgId = res.Result.org_id;
                //Get role data
                var roleData = _repo.GetRoleByID(authicateUserResponse.RoleID);
                if (roleData != null) 
                {
                    authicateUserResponse.RoleName = roleData.Result.Role_Name;
                    authicateUserResponse.GlobalAdminRole = roleData.Result.global_admin_role;
                    authicateUserResponse.ClientAdminRole = roleData.Result.client_admin_role;
                }
                authicateUserResponse.HttpStatusCode = HttpStatusCode.OK;
            }

            return authicateUserResponse;
        }

        public GetUsersResponse GetUsers(int OrgId)
        {
            GetUsersResponse response = new GetUsersResponse();
            var res = _repo.GetUsers(OrgId);
            
            if (res == null)
            {
                response.IsSuccess = false;
                response.Message = "user data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<LDP_APIs.DAL.Entities.User>, List<LDP_APIs.BL.Models.SelectUserModel>>(res.Result);
                var roles = _repo.GetRoles(OrgId);

                _mappedResponse.ForEach(user => user.RoleName = getRoleName(user.RoleID, roles));

                response.UsersList = _mappedResponse.ToList();
            }
            return response;
        }

        public GetUserResponse GetUserbyID(int id)
        {
            GetUserResponse response = new GetUserResponse();
            var res = _repo.GetUserbyID(id);
            if (res.Result == null )
            {
                response.IsSuccess = false;
                response.Message = "User data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Userdata = res.Result;
            }
            return response;
        }

        private string  getRoleName(int roleID, Task<List<DAL.Entities.Role>> roles)
        {
           var rolename =  roles.Result.Where(role => role.Role_ID == roleID).First().Role_Name;

           return rolename;
        }

 
        public UserResponse UpdateUser(UpdateUserRequest request)
        {
            UserResponse response = new UserResponse();

            var user = _repo.GetUserbyNameOnUpdate(request.Name, request.UserID);

            if (user.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "User name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }
            var _mappedRequest = _mapper.Map<LDP_APIs.BL.Models.UpdateUserModel, LDP_APIs.DAL.Entities.User>(request );

            var res = _repo.UpdateUser(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
            {
                response.IsSuccess = false;
                response.Message = "User updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
                
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update the user";
                response.errors = new List<string>() { res.Result };
                response.HttpStatusCode = HttpStatusCode.BadRequest;
               
            }
            return response;
        }

        public UserResponse AddNewUser(AddUserRequest request)
        {

            UserResponse response = new UserResponse();
            var user = _repo.GetUserbyName(request.Name);
            if (user.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "User name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }
            var _mappedRequest = _mapper.Map<LDP_APIs.BL.Models.AdduserModel, LDP_APIs.DAL.Entities.User>(request);
           // _mappedRequest.Salt_Password = request.Password;
            var res = _repo.AddNewUser(_mappedRequest);
 
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "User added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public GetRolesResponse GetRoles(int orgId)
        {
            GetRolesResponse response = new GetRolesResponse();
            var res = _repo.GetRoles(orgId);
            response.IsSuccess = true;
            if (res == null)
                response.Message = "Roles data not found";
            else
            {
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<List<LDP_APIs.DAL.Entities.Role>, List<LDP_APIs.BL.Models.GetRoleModel>>(res.Result);
                response.RolesList = _mappedResponse.ToList();
            }
            return response;
        }

        public GetRoleResponse GetRoleByID(int roleID)
        {
            GetRoleResponse response = new GetRoleResponse();
            var res = _repo.GetRoleByID(roleID);
            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Role data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<LDP_APIs.DAL.Entities.Role, LDP_APIs.BL.Models.GetRoleModel>(res.Result);
                response.RoleData = _mappedResponse;
            }
            return response;
        }

        //private static void BuildResponse(bool isSuccess , string message , HttpStatusCode httpcode)
        //{
        //    response.IsSuccess = isSuccess;
        //    response.Message = message
        //    response.HttpStatusCode = httpcode;
        //}

        public UserResponse ChangeUserPassword(ChangeUserPasswordRequest request)
        {
            UserResponse response = new UserResponse();

            var res = _repo.ChangeUserPassword(request);
            response.IsSuccess = true;
            if (res.Result == "")
            {
                response.Message = "User Password updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = "Failed to update  the user password";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        public UserResponse ResetPassword(ResetPasswordRequest request)
        {
            UserResponse response = new UserResponse();
  
            var res = _repo.ResetPassword(request);
            response.IsSuccess = true;
            if (res.Result == "")
            {
                response.Message = "User Password reset completed";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = "Failed to reset the user password";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        public UserResponse DeleteUser(DeleteUserRequest request)
        {
            UserResponse response = new UserResponse();

            var res = _repo.DeleteUser(request);
            
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "User deleted";
                response.HttpStatusCode= HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        public AddRoleResponse AddRole(AddRoleRequest request)
        {
            AddRoleResponse response = new AddRoleResponse();
            var role = _repo.GetRolebyName(request.RoleName);

            if (role.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "Role name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }
            var _mappedRequest = _mapper.Map<AddRoleRequest, LDP_APIs.DAL.Entities.Role>(request);
            var res = _repo.AddRole(_mappedRequest);
            
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Role added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.BadRequest;

            }
            return response;
        }

        public UpdateRoleResponse UpdateRole(UpdateRoleRequest request)
        {
            UpdateRoleResponse response = new UpdateRoleResponse();
            var role = _repo.GetRolebyNameOnUpdate(request.RoleName, request.RoleID);

            if (role.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "User name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }
            var res = _repo.UpdateRole(request);
            response.IsSuccess = true;
            if (res.Result == "")
            {
                response.Message = "role updated";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = res.Result;
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        public DeleteRoleResponse DeleteRole(DeleteRoleRequest request)
        {
            DeleteRoleResponse response = new DeleteRoleResponse();

            var res = _repo.DeleteRole(request);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "role deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }
    }
}
