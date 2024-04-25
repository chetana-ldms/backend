namespace LDP_APIs.BL.Models
{
    public class CommonRoleModel
    {
       public string? RoleName { get; set; }
       public int Sysrole { get; set; }

        public int OrgId { get; set; }
        public int GlobalAdminRole { get; set; }
        public int ClientAdminRole { get; set; }

    }

    public class AddRoleModel: CommonRoleModel
    {
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
    }
    public class UpdateRoleModel: CommonRoleModel
    {
         public int RoleID { get; set; }
        public DateTime? Modifieddate { get; set; }
        public string? Modifieduser { get; set; }
    }
    public class GetRoleModel: UpdateRoleModel
    {
        public int active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedUser { get; set; }
    }
    public class DeleteRoleModel
    {
        public int RoleID { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedUser { get; set; }
    }
}
