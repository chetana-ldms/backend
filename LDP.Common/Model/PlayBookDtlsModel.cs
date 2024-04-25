namespace LDPRuleEngine.BL.Models
{
    public class CommonPlayBookDtlsModel
    {
		
		public string? PlayBookItemType { get; set; }
		public int PlayBookItemTypeRefID { get; set; }
		public int ExecutionSequenceNumber { get; set; }
	
	}

	public class AddPlayBookDtlsModel: CommonPlayBookDtlsModel
	{
		//public DateTime? CreatedDate { get; set; }
		//public string? CreatedUser { get; set; }
	}

	public class UpdatePlayBookDtlsModel: CommonPlayBookDtlsModel
	{
		public int PlayBookID { get; set; }
		public int PlayBookDtlsID { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		//public string? ModifiedUser { get; set; }

	}

	public class GetPlayBookDtlsModel: UpdatePlayBookDtlsModel
	{
		////public int active { get; set; }
		//public DateTime? CreatedDate { get; set; }
		//public string? CreatedUser { get; set; }
		//public int Processed { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		//public string? ModifiedUser { get; set; }

	}
}
