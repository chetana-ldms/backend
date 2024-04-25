using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.Requests;
using LDP.Common.Requests.Common;
using LDP.Common.Responses;

namespace LDP.Common.DAL.Interfaces
{
    public interface ICommonRepository
    {
        Task<string> AddUserAction(useraction request);

        List<useraction> GetUserActions(int UserID);

        Task<string>  AddTask(Tasks request);

        Task<string> UpdateTask(Tasks request);

        Task<List<Tasks>> GetOpenTasksByOwner(int ownerUserId);

        Task<List<Tasks>> GetOpenTasksByTaskForUser(int taskForUserId);

        Task<string> UpdatePasswordResetTaskStatus(UpdateTaskStatusRequest request, string modifiedUser);

        Task<string> CancelTask(TaskCancelRequest request, string cancelledUser);

        Task<ActivityType> GetActivityTypeByTypeName(string activityTypeName);

        Task<List<ActivityType>> GetActivityTypeByTypeNames(List<string> activityTypeNames);

        Task<string> AddActivity(LdcActivity request);

        Task<string> AddActivityRange(List<LdcActivity> request);

        Task<List<ActivityType>> GetActivityTypes();

        (List<LdcActivity>, double count) GetActivities(GetActivitiesRequest request);


    }
}
