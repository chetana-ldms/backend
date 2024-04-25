namespace LDPRuleEngine.BL.Models
{
    public class CommonPlayBookModel
    {
		//public int Play_Book_ID { get; set; }
		public string? AlertCatogory { get; set; }
		public string? PlayBookName { get; set; }
		public string? Remarks { get; set; }
		//public int active { get; set; }
		//public DateTime? Created_Date { get; set; }
		//public DateTime? Modified_Date { get; set; }
		//public string? Created_User { get; set; }
		//public string? Modified_User { get; set; }
		//public int Processed { get; set; }
	}

	public class AddPlayBookModel: CommonPlayBookModel
	{
		//public int Play_Book_ID { get; set; }
		//public string? Play_Alert_Catogory { get; set; }
		//public string? Play_Book_Name { get; set; }
		//public string? Remarks { get; set; }
		//public int active { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime? Modified_Date { get; set; }
		public string? CreatedUser { get; set; }
		//public string? Modified_User { get; set; }
		//public int Processed { get; set; }
	}

	public class UpdatePlayBookModel: CommonPlayBookModel
	{
		public int PlayBookID { get; set; }
		//public string? Play_Alert_Catogory { get; set; }
		//public string? Play_Book_Name { get; set; }
		//public string? Remarks { get; set; }
		//public int active { get; set; }
		//public DateTime? Created_Date { get; set; }
		public DateTime? ModifiedDate { get; set; }
		//public string? Created_User { get; set; }
		public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}

	public class GetPlayBookModel : UpdatePlayBookModel
	{
		public int active { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
		public int Processed { get; set; }
        public List<GetPlayBookDtlsModel>? PlaybookDtl { get; set; }
    }
}
