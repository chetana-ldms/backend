using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class AddTaskRequest:AddTasksModel 
    {

    }

    public class UpdateTaskRequest : UpdateTasksModel
    {

    }

    public class GetTaskRequest : GetTasksModel
    {

    }

    public class PasswordResetTaskRequest
    {
        public string? UserName { get; set; }

        public int OrgId { get; set; }

        public DateTime? CreatedDate { get; set; }

    }

    public class UpdateTaskStatusRequest
    {
        public int UserId { get; set; }
        public string?  Status { get; set; }
        public int ModifiedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }

    public class TaskCancelRequest
    {
        public int TaskId { get; set; }
       // public string? Status { get; set; }
        public int CancelledUserId { get; set; }
        public DateTime? CancelledDate { get; set; }

    }

}
