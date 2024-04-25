namespace LDP.Common.Model
{
    public class CommonTasksModel
    {
        public string? TaskType { get; set; }

        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        //public DateTime? StartedDate { get; set; }
        //public DateTime? ClosedDate { get; set; }
        public int OrgId { get; set; }
        public int OwnerId { get; set; }
       
        public int? TaskForUserId { get; set; }
    }

    public class AddTasksModel: CommonTasksModel
    {
        
         public int CreatedUserId { get; set; }
         public DateTime? CreatedDate { get; set; }
        
    }
    public class UpdateTasksModel: CommonTasksModel
    {
        public int TaskId { get; set; }

        public int OwnerId { get; set; }
        public int ModifiedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Status { get; set; }
    }

    public class GetTasksModel: UpdateTasksModel
    {
       // public string? TaskType { get; set; }
        public int OwnerId { get; set; }
        public string? Status { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ClosedDate { get; set; }

        public int TaskForUserId { get; set; }  
        public string? CreadatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? OwnerUserName { get; set; }

        public string? TaskForUserName { get; set; }

        public string? OrgName { get; set; }
    }

    
}
