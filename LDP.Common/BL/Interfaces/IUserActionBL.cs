using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IUserActionBL
    {
        UserActionResponse AddUserAction(UserActionModel request);

        UserActionResponse UpdateUserAction(UserActionModel request);

        GetUserActionsResponse GetUserActionsByUser(GetUserActionRequest request);

        UserActionResponse AssignOwner(UserActionRequest request);

        UserActionResponse SetUserActionStatus(SetUserActionStatusRequest request);

        UserActionResponse SetMultipleUserActionStatus(SetMultipleUserActionStatusRequest request);
        UserActionResponse SetUserActiontPriority(SetUserActionPriorityRequest request);

        UserActionResponse SetUserActiontSeviarity(SetUserActionSeviarityRequest request);

        UserActionResponse SetUserActiontType(SetUserActionTypeRequest request);


        UserActionResponse AssignUserActionScore(AssignUserActionScoresRequest request);

        GetUserActionResponse GetUserActionsByActionTypeRefID(GetUserActionByActionTypeRefIDRequest request);

    }
}
