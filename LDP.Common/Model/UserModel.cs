namespace LDP_APIs.BL.Models
{
    public class UserModel
	{
		public string? Name { get; set; }

		public int RoleID { get; set; }

		public int SysUser { get; set; }

        public int OrgId { get; set; }

		public string? EmailId { get; set; }

    }

	public class AdduserModel:UserModel
    {
		//public string? CreatedByUserName { get; set; }

        public int CreatedUserId { get; set; }
        public DateTime CreatedDete { get; set; }
	}

	public class UpdateUserModel : UserModel
	{
		//public string? UpdatedByUserName { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public int UserID { get; set; }
	
	}
	public class SelectUserModel : UpdateUserModel
	{
		public string? CreatedByUserName { get; set; }

		public DateTime? CreatedDete { get; set; }

		public int Active { get; set; }

		public string?  RoleName { get; set; }

        public int GlobalAdminRole { get; set; }
        public int ClientAdminRole { get; set; }
        public string? UpdatedByUserName { get; set; }

		public string? OrganizationName { get; set; }

        public int OpenTaskFlag { get; set; }

        public int DefaultPasswordFlag { get; set; }
    }


}
