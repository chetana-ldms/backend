namespace LDPRuleEngine.BL.Models
{
    public class CommonPlayBookModel
    {
		public string? AlertCatogory { get; set; }
		public string? PlayBookName { get; set; }
		public string? Remarks { get; set; }

        public int OrgId { get; set; }
    }

	public class AddPlayBookModel: CommonPlayBookModel
	{
		public DateTime? CreatedDate { get; set; }
		public int CreatedUserId { get; set; }
	}

	public class UpdatePlayBookModel: CommonPlayBookModel
	{
		public int PlayBookID { get; set; }
		public DateTime? ModifiedDate { get; set; }
		
	}

	public class GetPlayBookModel : UpdatePlayBookModel
	{
		public int active { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
	//	public int Processed { get; set; }
        public List<GetPlayBookDtlsModel>? PlaybookDtl { get; set; }

		public DateTime? DeletedDate { get; set; }
		public string? DeletedUser { get; set; }

        public string? ModifiedUser { get; set; }
        public int OrgId { get; set; }
        public string? OrgName { get; set; }

    }

	public class DeletePlayBookModel
	{
		public int PlayBookID { get; set; }
		public DateTime? DeletedDate { get; set; }
		public int DeletedUserId { get; set; }

	}
}
