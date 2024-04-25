using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{
    public class Role
    {
        [Key]
        public int Role_ID { get; set; }
        public string? Role_Name { get; set; }
        public int active { get; set; }
        public DateTime? Created_date { get; set; }

        public string? Created_user { get; set; }
        public DateTime? Modified_date { get; set; }
        public string? Modified_user { get; set; }
        public DateTime? deleted_date { get; set; }
        public string? deleted_user { get; set; }
        public int sys_role { get; set; }
        public int org_id { get; set; }
        public int global_admin_role { get; set; }
        public int client_admin_role { get; set; }

    }
}
