using AutoMapper;
using LDP.Common;
using LDP.Common.BL.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly ILDPlattformBL _plattform;
        ICommonBL _commonBL;
       // ITaskBL _task;
        public LDPSecurityBL(ILDPSecurityRepository repo, IMapper mapper , ICommonBL commonBL
            //, ITaskBL taaskBl
            )
        {
            _repo = repo;
            _mapper = mapper;
            _commonBL = commonBL;
          //  _task = taaskBl;

            // _plattform = plattform;
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
            }
            else
            {
                authicateUserResponse.IsSuccess = true;
                authicateUserResponse.UserID = res.Result.User_ID;
                authicateUserResponse.RoleID = res.Result.Role_ID;
                authicateUserResponse.UserName = res.Result.User_Name;
                authicateUserResponse.RoleID = res.Result.Role_ID;
                // authicateUserResponse.OrgId = res.Result.org_id;
                authicateUserResponse.OrgId = request.OrgId;
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
            
            if (res.Result.Count == 0)
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

                //var _orgData = _plattform.GetOrganizationByID(OrgId);
                //string _orgName = string.Empty;
                //if (_orgData.OrganizationData != null)
                //{
                //    _orgName = _orgData.OrganizationData.OrgName;
                //}

                _mappedResponse.ForEach(user =>
                {
                    user.RoleName = getRoleName(user.RoleID, roles);
                   // user.OrganizationName = _orgName;
                }
                );

                response.UsersList = _mappedResponse.ToList();
            }
            return response;
        }

        public GetUserResponse GetUserbyID(int? id)
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
                var _userData = res.Result;
                //var _orgData = _plattform.GetOrganizationByID(res.Result.OrgId);
                //if (_orgData.OrganizationData != null )
                //{
                //    _userData.OrganizationName = _orgData.OrganizationData.OrgName;
                //}
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Userdata = _userData;
            }
            return response;
        }

        public GetUsersResponse GetUserbyIds(List<int> ids)
        {
            GetUsersResponse response = new GetUsersResponse();
            var res = _repo.GetUserbyIds(ids);
            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Users data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                response.UsersList = res.Result;
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

            var userdata = this.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var _mappedRequest = _mapper.Map<LDP_APIs.BL.Models.UpdateUserModel, LDP_APIs.DAL.Entities.User>(request );
            _mappedRequest.Modified_user = userdata.Userdata.Name;
            var res = _repo.UpdateUser(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "User updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
                
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update the user";
                response.errors = new List<string>() { res.Result };
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
               
            }

            //
            //       Activity log creation
            //var _orgData = _plattform.GetOrganizationByID(request.OrgId);
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Created_user);
            _templateData.Add("UpdateUser", _mappedRequest.User_Name);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.ModifiedUserId,
                OrgId = request.OrgId,
                CreatedDate = request.ModifiedDate,
                 ActivityDate = request.ModifiedDate

            }, _templateData, Constants.Activity_Template_User_Update, response.IsSuccess
              );
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

            var userdata = this.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var _mappedRequest = _mapper.Map<LDP_APIs.BL.Models.AdduserModel, LDP_APIs.DAL.Entities.User>(request);
            _mappedRequest.Created_user = userdata.Userdata.Name;

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
                response.Message = "Failed to add user data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            //
            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Created_user);
            _templateData.Add("NewUser", _mappedRequest.User_Name);
            
            

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.CreatedUserId,
                OrgId = request.OrgId,
                CreatedDate = request.CreatedDete,
                 ActivityDate = request.CreatedDete

            }, _templateData, Constants.Activity_Template_User_Add , response.IsSuccess
              );
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
        
        public UserResponse ChangeUserPassword(ChangeUserPasswordRequest request)
        {
            UserResponse response = new UserResponse();

            var userdata = this.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _operationUserData = this.GetUserbyID(request.UserId);
            if (_operationUserData.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.ChangeUserPassword(request,userdata.Userdata.Name);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "User Password updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, userdata.Userdata.Name);
            _templateData.Add("PasswordChangeUser", _operationUserData.Userdata.Name);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.ModifiedUserId,
                OrgId = _operationUserData.Userdata.OrgId,
                CreatedDate = request.ModifiedDate,
                ActivityDate = request.ModifiedDate

            }, _templateData, Constants.Activity_Template_User_Password_Change, response.IsSuccess
              );
            return response;
        }

        public (UserResponse , string) ResetPassword(ResetPasswordRequest request)
        {
            UserResponse response = new UserResponse();

            var userdata = this.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return (response,null);
            }
            var _operationUserdata = this.GetUserbyID(request.UserId);
            if (_operationUserdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return (response, null);
            }
            var res = _repo.ResetPassword(request,userdata.Userdata.Name);

            string _newPassword = Constants.defaultPassword;
            if (res.Result == "")
            {
  
                response.IsSuccess = true;
                response.Message = "User Password reset completed";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, userdata.Userdata.Name);
            _templateData.Add("PasswordResetUser", _operationUserdata.Userdata.Name);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.ModifiedUserId ,
                OrgId = _operationUserdata.Userdata.OrgId,
                CreatedDate = request.ModifiedDate,
                ActivityDate = request.ModifiedDate

            }, _templateData, Constants.Activity_Template_User_Password_Reset, response.IsSuccess
              );
          
            return (response , _newPassword);
        }
        public UserResponse CheckDefaultPasswordSet(int userId)
        {
            UserResponse res = new UserResponse();

            var _repoRes = _repo.CheckDefaultPasswordSet(userId);

            if (_repoRes.Result == true)
            {
                res.IsSuccess = true;
                res.HttpStatusCode = HttpStatusCode.OK;
                res.Message = "Success";
            }
            else
            {
                res.IsSuccess = false;
                res.HttpStatusCode = HttpStatusCode.NotFound;
                res.Message = "Not found";
            }
            return res;
        }
        public UserResponse DeleteUser(DeleteUserRequest request)
        {
            UserResponse response = new UserResponse();

            var userdata = this.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var _deleteUserdata = this.GetUserbyID(request.UserID);
            if (_deleteUserdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.DeleteUser(request,userdata.Userdata.Name);
            
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
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            //
            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, userdata.Userdata.Name);
            _templateData.Add("DeleteUser", _deleteUserdata.Userdata.Name);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.DeletedUserId,
                OrgId = _deleteUserdata.Userdata.OrgId,
                CreatedDate = request.DeletedDate,
                 ActivityDate = request.DeletedDate

            }, _templateData, Constants.Activity_Template_User_Delete, response.IsSuccess
              );
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

            var userdata = this.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _mappedRequest = _mapper.Map<AddRoleRequest, LDP_APIs.DAL.Entities.Role>(request);
            _mappedRequest.Created_user = userdata.Userdata.Name;
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
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;

            }

            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, userdata.Userdata.Name);
            _templateData.Add("NewRole", request.RoleName);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.CreatedUserId,
                OrgId = request.OrgId,
                CreatedDate = request.CreatedDate,
                ActivityDate = request.CreatedDate

            }, _templateData, Constants.Activity_Template_Role_Add, response.IsSuccess
              );

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
            var userdata = this.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _updateRoledata = this.GetRoleByID(request.RoleID);
            if (_updateRoledata.RoleData == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid role";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.UpdateRole(request,userdata.Userdata.Name);
            response.IsSuccess = true;
            if (res.Result == "")
            {
                response.Message = "Role updated";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = res.Result;
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            // Activiy log
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, userdata.Userdata.Name);
            _templateData.Add("UpdateRole", _updateRoledata.RoleData.RoleName);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.ModifiedUserId,
                OrgId = request.OrgId,
                CreatedDate = request.Modifieddate,
                ActivityDate = request.Modifieddate

            }, _templateData, Constants.Activity_Template_Role_Update, response.IsSuccess
              );
            return response;
        }

        public DeleteRoleResponse DeleteRole(DeleteRoleRequest request)
        {
            DeleteRoleResponse response = new DeleteRoleResponse();

            var userdata = this.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var _deleteRoleData = _repo.GetRoleByID(request.RoleID).Result;

            if (_deleteRoleData == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid Role";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var res = _repo.DeleteRole(request,userdata.Userdata.Name);

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

            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, userdata.Userdata.Name);
            _templateData.Add("DeleteRole", _deleteRoleData.Role_Name);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.DeletedUserId,
                OrgId = _deleteRoleData.Role_ID,
                CreatedDate = request.DeletedDate,
                ActivityDate = request.DeletedDate

            }, _templateData, Constants.Activity_Template_Role_Delete, response.IsSuccess
              );

            return response;
        }
        public int GetPasswordResetTaskOwnerId(int userId)
        {
            return 1;
        }

        public SelectUserModel GetUserbyNameOrEmail(string request)
        {
            return _repo.GetUserbyNameOrEmail(request).Result;
        }
        public List<SelectUserModel> GetAdminUserList(int orgId)
        {
            return _repo.GetAdminUserList(orgId).Result;
        }
    }

   
}
