namespace LDP.Common.Model
{
    public class UserActionModel
    {
        public int ActionId { get; set; }
        public string? ActionType { get; set; }
        public double ActionTypeRefid { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
        public String? Severity { get; set; }
        public int SeverityId { get; set; }
        public int Owner { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
        public int ActionStatus { get; set; }
        public string? PriorityName { get; set; }
        //public string? SeverityName { get; set; }
        public string? OwnerName { get; set; }
        public string? ActionStatusName { get; set; }
        public string? Score { get; set; }
        public DateTime? ActionDate { get; set; }
    }
}
