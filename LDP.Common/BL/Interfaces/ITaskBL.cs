using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface ITaskBL
    {
        TaskResponse AddTask(AddTaskRequest request );

        TaskResponse UpdateTask(UpdateTaskRequest request);
        TaskResponse UpdatePasswordResetTaskStatus(UpdateTaskStatusRequest request);

        TaskResponse TaskCancel(TaskCancelRequest request);

        TaskResponse AddPasswordResetTask(PasswordResetTaskRequest request);

        //  Task<string> UpdateTask(Tasks request);
        GetTasksResponse GetOpenTasksByOwner(int ownerUserId);

        GetTasksResponse GetOpenTasksByTaskForUser(int taskForUserId);

    }
}
