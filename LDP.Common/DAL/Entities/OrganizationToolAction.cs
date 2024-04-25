using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class OrganizationToolAction
    {
        [Key]
        public int org_tool_action_id { get; set; }
        public int org_tool_id { get; set; }
        public int tool_action_id { get; set; }
        public int tool_id { get; set; }
        public int org_id { get; set; }
        public string? auth_key { get; set; }
        public string? api_url { get; set; }
        public int active { get; set; }
        public string? api_verson { get; set; }
        public int GetData_BatchSize { get; set; }
        public string? Last_Read_AlertDate { get; set; }
    }
}
