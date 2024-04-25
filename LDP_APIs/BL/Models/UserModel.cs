namespace LDP_APIs.BL.Models
{
    public class UserModel
	{
		public string? Name { get; set; }

		public int RoleID { get; set; }

		public int SysUser { get; set; }

        public int OrgId { get; set; }

    }

	public class AdduserModel:UserModel
    {
		//public string? Password { get; set; }
		public string? CreatedByUserName { get; set; }
		public DateTime CreatedDete { get; set; }
	}

	public class UpdateUserModel : UserModel
	{
		public string? UpdatedByUserName { get; set; }
		public DateTime? UpdatedDete { get; set; }
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

    }


}
