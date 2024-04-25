using FluentValidation;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentValidation.Results;
using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Helpers.Interfaces;
using LDP.Common;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("LDP Security")]
    public class LDPSecurityController : ControllerBase
    {
        ILDPSecurityBL _bl;
        ITaskBL _taskBl;
        IEmailHelper _emailhelper;
        ICommonBL _commonBL;
        private IValidator<AuthenticateUserRequest> _validator;
        private IValidator<AddUserRequest> _addusrvalidator;
        private IValidator<UpdateUserRequest> _updateusrvalidator;
        private IValidator<DeleteUserRequest> _Deltusrvalidator;
        private IValidator<ChangeUserPasswordRequest> _ChangePWDusrvalidator;
        private IValidator<ResetPasswordRequest> _ResetPWDusrvalidator;

        private IValidator<AddRoleRequest> _addrolervalidator;
        private IValidator<UpdateRoleRequest> _updaterolevalidator;
        private IValidator<DeleteRoleRequest> _Deltrolevalidator;
        public LDPSecurityController(ILDPSecurityBL bl,
            ITaskBL taskBl,
            IValidator<AuthenticateUserRequest> validator
            , IValidator<AddUserRequest> addusrvalidator
            , IValidator<UpdateUserRequest> updateusrvalidator
            , IValidator<DeleteUserRequest> Deltusrvalidator
            , IValidator<ChangeUserPasswordRequest> ChangePWDusrvalidator
            , IValidator<ResetPasswordRequest> ResetPWDusrvalidator
            , IValidator<AddRoleRequest> addrolervalidator
            , IValidator<UpdateRoleRequest> updaterolevalidator
            , IValidator<DeleteRoleRequest> Deltrolevalidator,
            IEmailHelper emailhelper,
            ICommonBL commonBL)
        {
            _bl = bl;
            _taskBl = taskBl;
            _validator = validator; _addusrvalidator = addusrvalidator;
            _updateusrvalidator = updateusrvalidator;
            _Deltusrvalidator = Deltusrvalidator;
            _ChangePWDusrvalidator = ChangePWDusrvalidator;
            _ResetPWDusrvalidator = ResetPWDusrvalidator;
            _addrolervalidator = addrolervalidator;
            _updaterolevalidator = updaterolevalidator;
            _Deltrolevalidator = Deltrolevalidator;
            _emailhelper = emailhelper;
            _commonBL = commonBL;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("User/Authenticate")]
        public AuthicateUserResponse AuthenticateUserAsync(AuthenticateUserRequest request)
        {
            baseResponse response = new AuthicateUserResponse();
            var result = _validator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as AuthicateUserResponse;

            }
            response = _bl.AuthenticateUser(request);

            if (response.IsSuccess)
            {
                // getting any open tasks count 
               var _opentasks =  _taskBl.GetOpenTasksByOwner((response as AuthicateUserResponse).UserID);
                if (_opentasks.IsSuccess)
                    (response as AuthicateUserResponse).OpenTaskCount = _opentasks.TaskList.Count;
                else
                    (response as AuthicateUserResponse).OpenTaskCount = 0;
                // Get default password flag

                (response as AuthicateUserResponse).DefaultPassword = _bl.CheckDefaultPasswordSet((response as AuthicateUserResponse).UserID).IsSuccess;

               
            }
            //else
            //{
            //    //       Activity log creation
            //    Dictionary<string, string> _templateData = new Dictionary<string, string>();
            //    _templateData.Add(Constants.Acvities_Template_username, request.UserName);
            //    _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            //    {
            //        CreatedUserId = 0,
            //        OrgId = request.OrgId

            //    }, _templateData, Constants.Activity_Template_User_logIn_Failed
            //    );
            //}

            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, (response as AuthicateUserResponse).UserName);
            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = (response as AuthicateUserResponse).UserID,
                OrgId = (response as AuthicateUserResponse).OrgId

            }, _templateData, Constants.Activity_Template_User_logged_in
            );

            return response as AuthicateUserResponse;

        }

       

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("User/Add")]
        public UserResponse AddNewUser(AddUserRequest request)
        {
            baseResponse response = new UserResponse();

            var result = _addusrvalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as UserResponse;

            }
            response = _bl.AddNewUser(request);
   
            return response as UserResponse;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("User/Update")]
        public UserResponse UpdateUser(UpdateUserRequest request)
        {
            baseResponse response = new UserResponse();

            var result = _updateusrvalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as UserResponse;

            }

            return _bl.UpdateUser(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("User/Delete")]
        public UserResponse DeleteUser(DeleteUserRequest request)
        {
            baseResponse response = new UserResponse();

            var result = _Deltusrvalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as UserResponse;

            }
            return _bl.DeleteUser(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Users")]
        public GetUsersResponse GetUsers(int OrgId)
        {
            return _bl.GetUsers(OrgId           );
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("UserDetails")]
        public GetUserResponse GetUserDetails(int id)
        {
            return _bl.GetUserbyID(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("User/ChangePassword")]
        public UserResponse ChangeUserPassword(ChangeUserPasswordRequest request)
        {
            baseResponse response = new UserResponse();

            var result = _ChangePWDusrvalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as UserResponse;

            }
            return _bl.ChangeUserPassword(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("User/ResetPassword")]
        public UserResponse ResetPassword(ResetPasswordRequest request)
        {
            baseResponse response = new UserResponse();

            var result = _ResetPWDusrvalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as UserResponse;

            }
            (  response, string newPassword) =  _bl.ResetPassword(request);

            // closing the Password_Reset Task if any created already.
            if (response.IsSuccess)
            {
              var taskRs =  _taskBl.UpdatePasswordResetTaskStatus(
                    new UpdateTaskStatusRequest()
                    {
                         UserId = request.UserId,
                         ModifiedUserId = request.ModifiedUserId,
                         ModifiedDate = request.ModifiedDate,
                         Status = "Closed"
                    });
                // Send email on new password generated

                //

                var emailData = new Dictionary<string, string>()
                {
                     { "NewPassword", newPassword }
                };
                
                var _emailRes = _emailhelper.PrepareAndSendEmail(
                    new LDP.Common.Helpers.Email.EmailPrepRequest()
                    {
                        EmailType = LDP.Common.Constants.Email_types.Password_Reset,
                        EmailSubject = "Password Reset",
                        UseId = request.UserId,
                        Data = emailData
                    }
                );
            }

            return response as UserResponse;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Roles")]
        public GetRolesResponse GetRoles(int orgId)
        {
            return _bl.GetRoles(orgId);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("RoleDetails")]
        public GetRoleResponse RoleDetails(int id)
        {
            return _bl.GetRoleByID(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Roles/Add")]
        public AddRoleResponse AddRole(AddRoleRequest request)
        {
            baseResponse response = new AddRoleResponse();

            var result = _addrolervalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as AddRoleResponse;

            }
            return _bl.AddRole(request);
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Roles/Update")]
        public UpdateRoleResponse UpdateRole(UpdateRoleRequest request)
        {
            baseResponse response = new UpdateRoleResponse();

            var result = _updaterolevalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as UpdateRoleResponse;

            }
            return _bl.UpdateRole(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Roles/Delete")]
        public DeleteRoleResponse DeleteRole(DeleteRoleRequest request)
        {
            baseResponse response = new DeleteRoleResponse();

            var result = _Deltrolevalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as DeleteRoleResponse;

            }

            return _bl.DeleteRole(request);
        }

        private void BuildValiationMessage(ValidationResult result, ref baseResponse validationresponse)
        {
            validationresponse.IsSuccess = false;
            validationresponse.Message = "Validation Error";
            validationresponse.HttpStatusCode = HttpStatusCode.BadRequest;
            validationresponse.errors = result.Errors.Select(e => e.ErrorMessage.ToString()).ToList();

        }

    }
}

