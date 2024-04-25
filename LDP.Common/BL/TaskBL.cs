using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using System.Net;


namespace LDP.Common.BL
{
    public class TaskBL : ITaskBL
    {
        ICommonRepository _repo;
        public readonly IMapper _mapper;
        private readonly ILDPSecurityBL _securityBl;
        private readonly ILDPlattformBL _plattformbl;
        public TaskBL(IMapper mapper, ICommonRepository repo, ILDPSecurityBL securityBl = null, ILDPlattformBL plattformbl = null)
        {
            _mapper = mapper;
            _repo = repo;
            _securityBl = securityBl;
            _plattformbl = plattformbl;
        }

        public TaskResponse AddTask(AddTaskRequest request)
        {

            TaskResponse response = new TaskResponse();
            var _mappedRequest = _mapper.Map<AddTaskRequest, Tasks>(request);

            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.created_user = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            //
            var res = _repo.AddTask(_mappedRequest);
            //
            if (res.Result == "")
            {
                //if (_mappedRequest.task_type == "Password_Reset")
                //{
                //    _securityBl.UpdateOpenTaskFlag(_mappedRequest.task_for_user_id);
                //}
                response.Message = "New task data added";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = "Failed to add new task data";
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                response.errors = new List<string>() { res.Result};
            }
            return response;
        }
        public TaskResponse UpdateTask(UpdateTaskRequest request)
        {
            TaskResponse response = new TaskResponse();
            var _mappedRequest = _mapper.Map<UpdateTaskRequest, Tasks>(request);

            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.modified_user = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var res = _repo.UpdateTask(_mappedRequest);

            if (res.Result == "")
            {
                response.Message = "task data updated";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = "Failed to update the task data";
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                response.errors = new List<string>() { res.Result };
            }
            return response;
        }
        public TaskResponse UpdatePasswordResetTaskStatus(UpdateTaskStatusRequest request)
        {
            TaskResponse response = new TaskResponse();

            string _modifiedUserName = string.Empty;

            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata != null)
            {
                _modifiedUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var res = _repo.UpdatePasswordResetTaskStatus(request, _modifiedUserName);

            if (res.Result == "")
            {
                response.Message = "Update task status successful ";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = "Failed to update the task status ";
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                response.errors = new List<string>() { res.Result };
            }
            return response;
        }

        public TaskResponse TaskCancel(TaskCancelRequest request)
        {
            TaskResponse response = new TaskResponse();

            string _cancelledUserName = string.Empty;

            var userdata = _securityBl.GetUserbyID(request.CancelledUserId);
            if (userdata.Userdata != null)
            {
                _cancelledUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var res = _repo.CancelTask(request, _cancelledUserName);

            if (res.Result == "")
            {
                response.Message = "cancel task  successful ";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = "Failed to cancel the task  ";
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                response.errors = new List<string>() { res.Result };
            }
            return response;
        }
        public GetTasksResponse GetOpenTasksByOwner(int ownerUserId)
        {
            GetTasksResponse response = new GetTasksResponse();
            var res = _repo.GetOpenTasksByOwner(ownerUserId);
            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Task data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<Tasks>,List<GetTasksModel>>(res.Result);
                //
                var users = getUserdata(_mappedResponse);
                var orgList = getOrgdata();

                foreach (var task in _mappedResponse)
                {
                    if (task.OrgId > 0)
                    {
                        var Org = orgList.Where(o => o.OrgID == task.OrgId).FirstOrDefault();
                        if (Org != null )
                        {
                            task.OrgName = Org.OrgName;
                        }
                        
                    }
                    if (task.OwnerId > 0)
                    {
                        var owneruser = users.Where(u => u.UserID == task.OwnerId).FirstOrDefault();
                        if(owneruser != null ) 
                        {
                            task.OwnerUserName = owneruser.Name;
                        }
                        
                    }
                    if (task.TaskForUserId > 0)
                    {
                        var taskforuser = users.Where(u => u.UserID == task.TaskForUserId).FirstOrDefault();
                        if (taskforuser != null)
                        {
                            task.TaskForUserName = taskforuser.Name;
                        }
                      
                    }

                }
                //
                response.TaskList = _mappedResponse;
            }
            return response;
        }

        public GetTasksResponse GetOpenTasksByTaskForUser(int taskForUserId)
        {
            GetTasksResponse response = new GetTasksResponse();
            var res = _repo.GetOpenTasksByTaskForUser(taskForUserId);
            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Task data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<Tasks>, List<GetTasksModel>>(res.Result);
                //
                var users = getUserdata(_mappedResponse);
                var orgList = getOrgdata();

                foreach (var task in _mappedResponse)
                {
                    if (task.OrgId > 0)
                    {
                        var Org = orgList.Where(o => o.OrgID == task.OrgId).FirstOrDefault();
                        if (Org != null)
                        {
                            task.OrgName = Org.OrgName;
                        }

                    }
                    if (task.OwnerId > 0)
                    {
                        var owneruser = users.Where(u => u.UserID == task.OwnerId).FirstOrDefault();
                        if (owneruser != null)
                        {
                            task.OwnerUserName = owneruser.Name;
                        }

                    }
                    if (task.TaskForUserId > 0)
                    {
                        var taskforuser = users.Where(u => u.UserID == task.TaskForUserId).FirstOrDefault();
                        if (taskforuser != null)
                        {
                            task.TaskForUserName = taskforuser.Name;
                        }

                    }

                }
                //
                response.TaskList = _mappedResponse;
            }
            return response;
        }
        private List<SelectUserModel>  getUserdata(List<GetTasksModel> tasks)
        {
            HashSet<int>userIds = new HashSet<int>();

            foreach (var task in tasks) 
            {
                userIds.Add(task.OwnerId);
                userIds.Add(task.TaskForUserId);

            }

           var userslist =  _securityBl.GetUserbyIds(userIds.ToList());

           return userslist.UsersList;


        }

        private List<GettOrganizationsModel> getOrgdata()
        {
            var orgList = _plattformbl.GetOrganizationList();

            return orgList.OrganizationList;

        }
        public TaskResponse AddPasswordResetTask(PasswordResetTaskRequest request)
        {
            
            //
            // Get the userid , role id details based on username or email id
            //

            TaskResponse res = new TaskResponse();
            var _userDtl = _securityBl.GetUserbyNameOrEmail(request.UserName);

            if (_userDtl == null) 
            {
                res.Message = "User details not found";
                res.IsSuccess = false;
                res.HttpStatusCode = HttpStatusCode.BadRequest;
                return res;
            }
            // Check already task created for same user 
            var _existingTasks = GetOpenTasksByTaskForUser(_userDtl.UserID);

            if (_existingTasks.IsSuccess)
            {
                var _passwordResetTask = _existingTasks.TaskList.Where(t => t.TaskType == "Password_Reset").FirstOrDefault();
                if ( _passwordResetTask != null ) 
                {
                    res.Message = string.Format("Password Reset task already exist with open status , Task id : {0} , Task title : {1} , Assinged to : {2} ", _passwordResetTask.TaskId , _passwordResetTask.TaskTitle, _passwordResetTask.OwnerUserName);
                    res.IsSuccess = false;
                    res.HttpStatusCode = HttpStatusCode.BadRequest;
                    return res;
                }

            }
            var _adminList = _securityBl.GetAdminUserList(request.OrgId);

            if (_adminList.Count == 0 )
            {
                res.Message = "admin details not found";
                res.IsSuccess = false;
                res.HttpStatusCode = HttpStatusCode.NotFound;
                return res;
            }

            var  _clientAdmins = _adminList.Where(a => a.ClientAdminRole == 1 && a.UserID != _userDtl.UserID && a.OrgId == request.OrgId).ToList();
            int _ownerUserId = 0;
            string _ownerUserName = string.Empty;
            if (_clientAdmins.Count == 0)
            {
                var _globalAdmins = _adminList.Where(a => a.ClientAdminRole == 1 && a.UserID != _userDtl.UserID).ToList();

                if (_globalAdmins.Count == 0 )
                {
                    res.Message = "admin details not found";
                    res.IsSuccess = false;
                    res.HttpStatusCode = HttpStatusCode.NotFound;
                    return res;

                }
                _ownerUserId = _globalAdmins[0].UserID;
                _ownerUserName = _globalAdmins[0].Name;
            }

            if (_ownerUserId == 0)
            {
                _ownerUserId = _clientAdmins[0].UserID;
                _ownerUserName = _clientAdmins[0].Name;
            }
            var Taskrequest = new AddTaskRequest()
            {
                  CreatedDate = request.CreatedDate,
                  OrgId = request.OrgId,
                  TaskTitle = "Password Reset request ",
                  TaskType = "Password_Reset",
                  //Status = "New",
                  CreatedUserId = _userDtl.UserID,
                  OwnerId = _ownerUserId,
                  TaskForUserId = _userDtl.UserID

            };

            res = AddTask(Taskrequest);

            if (res.IsSuccess)
            {
                res.Message = string.Format("New Task created and assigned to {0}", _ownerUserName);
            }

            return res;

        }
       
    }
}
