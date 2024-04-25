using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using System.Net;

namespace LDP.Common.BL
{
    public class UserActionBL : IUserActionBL
    {
        IUserActionsRepository _repo;
        public readonly IMapper _mapper;
 
        public UserActionBL(IUserActionsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
   
        }

        public UserActionResponse AddUserAction(UserActionModel request)
        {
            UserActionResponse response = new UserActionResponse();

            
                var _mappedRequest = _mapper.Map<UserActionModel, useraction>(request);

                var res = _repo.AddUserAction(_mappedRequest);
                if (res.Result == "")
                {
                    response.IsSuccess = true;
                    response.Message = "User action added";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Failed to add user action";
                }
                return response;
            
        }

        public UserActionResponse UpdateUserAction(UserActionModel request)
        {
            UserActionResponse response = new UserActionResponse();


            var _mappedRequest = _mapper.Map<UserActionModel, useraction>(request);

            var res = _repo.UpdateUserAction(_mappedRequest);
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "User action updated";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update user action";
            }
            return response;

        }

        public GetUserActionResponse GetUserActionsByActionTypeRefID(GetUserActionByActionTypeRefIDRequest request)
        {
            GetUserActionResponse returnobj = new GetUserActionResponse();

            var _useractiondata = _repo.GetUserActionsByActionTypeRefID(
                new GetUserActionByActionTypeRefIDRequest()
                {
                    ActionType = request.ActionType,
                    ActionTypeRefId = request.ActionTypeRefId
                });
            if (_useractiondata.Result == null)
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "user action not found ";
                return returnobj;
            }
            var _mappedResponse = _mapper.Map<useraction, UserActionModel>(_useractiondata.Result);
            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.HttpStatusCode = HttpStatusCode.OK;
            returnobj.UserActionData = _mappedResponse;
            return returnobj;
        }


        public UserActionResponse AssignUserActionScore(AssignUserActionScoresRequest request)
        {
            UserActionResponse returnobj = new UserActionResponse();

            var _useractiondata = _repo.GetUserActionsByActionTypeRefID(
                new GetUserActionByActionTypeRefIDRequest()
                {
                    ActionType = request.ActionType,
                    ActionTypeRefId = request.Id
                });
            if (_useractiondata.Result == null)
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "user action not found , so no action needed";
                return returnobj;
            }

            var repoResponse = _repo.AssignUserActionScore(request);

            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed to assign the owner to user action";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return returnobj;

        }

        public UserActionResponse AssignOwner(UserActionRequest request)
        {
            UserActionResponse response = new UserActionResponse();

            var _useractiondata = _repo.GetUserActionsByActionTypeRefID(
                new GetUserActionByActionTypeRefIDRequest()
                {
                    ActionType = request.ActionType,
                    ActionTypeRefId = request.ActionTypeRefid
                });
            if (_useractiondata.Result == null)
            {
                var _mappedRequest = _mapper.Map<UserActionRequest, useraction>(request);

                var res = _repo.AddUserAction(_mappedRequest);
                
                if (res.Result == "")
                {
                    response.IsSuccess = true;
                    response.Message = "User action added";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Failed to add user action";
                }
                return response;
            }
            else
            {
                AssignUserActionOwnerRequest ownerRequest = new AssignUserActionOwnerRequest();
                ownerRequest.ActionType = request.ActionType;
                ownerRequest.Id = request.ActionTypeRefid;
                ownerRequest.ModifiedUser = request.ModifiedUser;
                ownerRequest.ModifiedDate = request.ModifiedDate;
                ownerRequest.UserId = request.Owner;
                ownerRequest.UserName = request.OwnerName;
                var res = _repo.AssignOwner(ownerRequest);
                if (res.Result == "")
                {
                    response.IsSuccess = true;
                    response.Message = "User action added";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Failed to add user action";
                }
                return response;
            }
        }
        public GetUserActionsResponse GetUserActionsByUser(GetUserActionRequest request)
        {
            var userActionData = _repo.GetUserActionsByUser(request);
            GetUserActionsResponse returnobj = new GetUserActionsResponse();
            if (userActionData.Result !=null)
            {
                var _mappedResponse = _mapper.Map<List<useraction>, List<UserActionModel>>(userActionData.Result);

                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
                returnobj.UserActionsData = _mappedResponse;
            }
            else
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "No user action data found";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            }
            return returnobj;
        }

        public UserActionResponse SetUserActiontPriority(SetUserActionPriorityRequest request)
        {
            UserActionResponse returnobj = new UserActionResponse();

            var _useractiondata = _repo.GetUserActionsByActionTypeRefID(
                new GetUserActionByActionTypeRefIDRequest()
                {
                    ActionType = request.ActionType,
                    ActionTypeRefId = request.Id
                });
            if (_useractiondata.Result == null)
            { 
                returnobj.IsSuccess = true;
                returnobj.Message = "user action not found , so no action needed";
                return returnobj;
            }
             
            var repoResponse = _repo.SetUserActiontPriority(request);
            
            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed to set  the priority to user action";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return returnobj;
        }

        public UserActionResponse SetUserActiontSeviarity(SetUserActionSeviarityRequest request)
        {
            UserActionResponse returnobj = new UserActionResponse();

            var _useractiondata = _repo.GetUserActionsByActionTypeRefID(
                new GetUserActionByActionTypeRefIDRequest()
                {
                    ActionType = request.ActionType,
                    ActionTypeRefId = request.Id
                });
            if (_useractiondata.Result == null)
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "user action not found , so no action needed";
                return returnobj;
            }

            var repoResponse = _repo.SetUserActiontSeviarity(request);

            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed to set  the severity to user action";
                returnobj.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }

            return returnobj;
        }

        public UserActionResponse SetUserActionStatus(SetUserActionStatusRequest request)
        {
            UserActionResponse returnobj = new UserActionResponse();

            var _useractiondata = _repo.GetUserActionsByActionTypeRefID(
                new GetUserActionByActionTypeRefIDRequest()
                {
                    ActionType = request.ActionType,
                    ActionTypeRefId = request.Id
                });
            if (_useractiondata.Result == null)
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "user action not found , so no action needed";
                return returnobj;
            }
            var repoResponse = _repo.SetUserActionStatus(request);

            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed to set  the status to user action";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return returnobj;
        }

        public UserActionResponse SetMultipleUserActionStatus(SetMultipleUserActionStatusRequest request)
        {
            UserActionResponse returnobj = new UserActionResponse();

            var _useractiondata = _repo.GetUserActionsByMultipleActionTypeRefID(
                new GetUserActionByMultipleActionTypeRefIDRequest()
                {
                    ActionType = request.ActionType,
                    ActionTypeRefIds = request.Ids
                });
            if (_useractiondata.Result == null)
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "user action not found , so no action needed";
                return returnobj;
            }
            var repoResponse = _repo.SetMultipleUserActionStatus(request);

            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed to set  the status to user action";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return returnobj;
        }

        public UserActionResponse SetUserActiontType(SetUserActionTypeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
