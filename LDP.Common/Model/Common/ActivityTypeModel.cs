namespace LDP.Common.Model.Common
{
    public class CommonActivityModel
    {
        public int? ActivityTypeId { get; set; }
        public int? OrgId { get; set; }
        public double? AlertId { get; set; }
        public int? IncidentId { get; set; }
        public string? PrimaryDescription { get; set; }
        public string? SecondaryDescription { get; set; }

        public string? Source { get; set; }

        public int? ActivityExistToolAndLDC { get; set; }

        public int? ToolId { get; set; }

        public DateTime? ActivityDate { get; set; }
        public int? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
    public class GetActivityModel : CommonActivityModel
    {
        public int? ActivityId { get; set; }
        public string? CreateedUser { get; set; }
    
    }
    public class AddActivityModel : CommonActivityModel
    {
       
    }
    public class ActivityTypeModel
    {
        public int? ActivityTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? Template { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }

    }

}
