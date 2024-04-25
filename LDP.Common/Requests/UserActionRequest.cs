using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class UserActionRequest:UserActionModel
    {

    }
    public class GetUserActionRequest
    {
        public int UserId { get; set; }  
    }

    public class GetUserActionByActionTypeRefIDRequest
    {
        public double ActionTypeRefId { get; set; }
        public string? ActionType { get; set; }

    }

    public class GetUserActionByMultipleActionTypeRefIDRequest
    {
        public List<double> ActionTypeRefIds { get; set; }
        public string? ActionType { get; set; }

    }

    public class UserActionCommon
    {
        public string? ActionType { get; set; }

        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public class UserActionBase: UserActionCommon
    {
        public double Id { get; set; }
       
    }

    public class MultipleUserActionBase : UserActionCommon
    {
        public List<double>? Ids { get; set; }

    }
    public class AssignUserActionOwnerRequest: UserActionBase
    {
        
        public int UserId { get; set; }

        public string? UserName { get; set; }
        

    }

    public class SetUserActionStatusRequest: UserActionBase
    {
   
        public int StatusId { get; set; }

        public string? StatusName { get; set; }
        public int OwnerID { get; set; }
        public string? OwnerName { get; set; }

    }

    public class SetMultipleUserActionStatusRequest : MultipleUserActionBase
    {

        public int StatusId { get; set; }

        public string? StatusName { get; set; }
        public int OwnerId { get; set; }
        public string? OwnerName { get; set; }

    }
    public class SetUserActionPriorityRequest: UserActionBase
    {
       
        public int PriorityId { get; set; }

        public string? PriorityValue { get; set; }
       
    }

    public class SetUserActionSeviarityRequest : UserActionBase
    {
        public string? Sevirity { get; set; }
        public int SevirityId { get; set; }
    }

    public class SetUserActionTypeRequest : UserActionBase
    {
        public string? Type { get; set; }
        public int TypeId { get; set; }
    }

    public class AssignUserActionScoresRequest: UserActionBase
    {
        public string? Score { get; set; }
   
    }
}
