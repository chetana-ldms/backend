using LDP.Common.DAL.Entities.Common;
using LDP.Common.Requests;

namespace LDP.Common.DAL.Interfaces
{
    public interface IUserActionsRepository
    {
        Task<string> AddUserAction(useraction request);

        Task<string> UpdateUserAction(useraction request);

        Task<List<useraction>> GetUserActionsByUser(GetUserActionRequest request);

        Task<useraction> GetUserActionsByActionTypeRefID(GetUserActionByActionTypeRefIDRequest request);

        Task<List<useraction>> GetUserActionsByMultipleActionTypeRefID(GetUserActionByMultipleActionTypeRefIDRequest request);
        Task<string> AssignOwner(AssignUserActionOwnerRequest request);

        Task<string> SetUserActionStatus(SetUserActionStatusRequest request);

        Task<string> SetMultipleUserActionStatus(SetMultipleUserActionStatusRequest request);

        Task<string> SetUserActiontPriority(SetUserActionPriorityRequest request);

        Task<string> SetUserActiontSeviarity(SetUserActionSeviarityRequest request);


        Task<string> AssignUserActionScore(AssignUserActionScoresRequest request);

        
       


    }
}
