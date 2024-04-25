namespace LDP.Common.Model
{
    public class CommonAlertPlayBookProcessModel
    {
		//public int alertplaybooksprocessid { get; set; }
		public int orgid { get; set; }
		public double alertid { get; set; }
		//public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		//public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}

	public class AddAlertPlayBookProcessModel: CommonAlertPlayBookProcessModel
	{
		public DateTime? CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
		public List<AddAlertPlayBookProcessActionModel>? AlertPlayBookProcessActions { get; set; }

	}

	public class UpdateAlertPlayBookProcessModel : CommonAlertPlayBookProcessModel
	{
		public int alertplaybooksprocessid { get; set; }
		//public int orgid { get; set; }
		//public double alertid { get; set; }
		//public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		//public string? CreatedUser { get; set; }
		public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}

	public class GetAlertPlayBookProcessModel : UpdateAlertPlayBookProcessModel
	{
		//public int alertplaybooksprocessid { get; set; }
		//public int orgid { get; set; }
		//public double alertid { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}

}
