using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Tasks")]
    public class TaskController : ControllerBase
    {
        ITaskBL _bl;

        public TaskController(ITaskBL bl) 
        {
            _bl = bl;
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Add")]
        public TaskResponse AddTask(AddTaskRequest request)
        {
        
            return _bl.AddTask(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("PasswordReset/Add")]
        public TaskResponse AddPasswordResetTask(PasswordResetTaskRequest request)
        {

            return _bl.AddPasswordResetTask(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Update")]
        public TaskResponse UpdateTask(UpdateTaskRequest request)
        {

            return _bl.UpdateTask(request);

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("List")]
        public GetTasksResponse GetOpenTasksByOwner(int ownerUserId)
        {

            return _bl.GetOpenTasksByOwner(ownerUserId);

        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("PasswordReset/Status/Update")]
        public TaskResponse UpdatePasswordResetTaskStatus(UpdateTaskStatusRequest request)
        {
            return _bl.UpdatePasswordResetTaskStatus(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Status/Cancel")]
        public TaskResponse TaskCancel(TaskCancelRequest request)
        {
            return _bl.TaskCancel(request);
        }


    }
}
