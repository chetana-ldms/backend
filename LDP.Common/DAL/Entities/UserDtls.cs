using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{

	public class User
	{
		[Key]

		public int User_ID { get; set; }
		public string? User_Name { get; set; }
		public string? Salt_Password { get; set; }
		public int Role_ID { get; set; }
		public DateTime? Created_date { get; set; }
		public DateTime? Modified_date { get; set; }
		public string? Created_user { get; set; }
		public string? Modified_user { get; set; }
		public int Processed { get; set; }

		public int active { get; set; }

		public DateTime? deleted_date { get; set; }
		public string? deleted_user { get; set; }

		public int sys_user { get; set; }

        public int org_id { get; set; }

        public string? email_id  { get; set; }

        //public int open_task_flag { get; set; }

        //public int default_password_flag { get; set; }

    }
}
