using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities.Common
{
    public class useraction
    {
        [Key]
        public int action_id { get; set; }
        public string? action_type { get; set; }
        public int action_type_refid { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
        public string? Severity { get; set; }
        public int severity_id { get; set; }
        public int Owner { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public string? Created_User { get; set; }
        public string? Modified_User { get; set; }
        public int action_Status { get; set; }
        public string? Priority_Name { get; set; }
        //public string? Severity_Name { get; set; }
        public string? Owner_Name { get; set; }
        public string? Action_Status_Name { get; set; }
        public string? score { get; set; }
        public DateTime? action_Date { get; set; }
        
    


    }
}
