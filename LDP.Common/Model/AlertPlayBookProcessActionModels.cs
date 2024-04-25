namespace LDP.Common.Model
{
    public class CommonAlertPlayBookProcessActionModel
    {
		//public int alertplaybooksprocessactionid { get; set; }
		public int alertplaybooksprocessid { get; set; }
		public int tooltypeid { get; set; }
		public int toolid { get; set; }
		public int playbookid { get; set; }
		public int toolactionid { get; set; }
		//public string? toolactionstatus { get; set; }
		//public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		//public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}

	public class AddAlertPlayBookProcessActionModel: CommonAlertPlayBookProcessActionModel
	{
		//public int alertplaybooksprocessactionid { get; set; }
		//public int alertplaybooksprocessid { get; set; }
		//public int tooltypeid { get; set; }
		//public int toolid { get; set; }
		//public int playbookid { get; set; }
		//public int toolactionid { get; set; }
		//public string? toolactionstatus { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}

	public class UpdateAlertPlayBookProcessActionModel : CommonAlertPlayBookProcessActionModel
	{
		public int alertplaybooksprocessactionid { get; set; }
		//public int alertplaybooksprocessid { get; set; }
		//public int tooltypeid { get; set; }
		//public int toolid { get; set; }
		//public int playbookid { get; set; }
		//public int toolactionid { get; set; }
		//public string? toolactionstatus { get; set; }
		//public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		//public string? CreatedUser { get; set; }
		public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}

	public class GetAlertPlayBookProcessActionModel : UpdateAlertPlayBookProcessActionModel
	{
		//public int alertplaybooksprocessactionid { get; set; }
		//public int alertplaybooksprocessid { get; set; }
		//public int tooltypeid { get; set; }
		//public int toolid { get; set; }
		//public int playbookid { get; set; }
		//public int toolactionid { get; set; }
		//public string? toolactionstatus { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}
}
