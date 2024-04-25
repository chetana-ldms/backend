namespace LDP_APIs.BL.Models
{
	public class AlertModel
	{
		public int AlertID { get; set; }

		public int AlertDevicePKID { get; set; }
		public int ToolID { get; set; }
		public int OrgID { get; set; }
		public string? Name { get; set; }
		public string? Severity { get; set; }
		public string? Score { get; set; }
		public string? Status { get; set; }
		public int? StatusID { get; set; }
		public DateTime? Detectedtime { get; set; }
		public string? ObservableTagID { get; set; }
		public int ObservableTag { get; set; }
		public int OwnerUserID { get; set; }
		public string? ownerusername { get; set; }
		public string? Source { get; set; }
		public string? AlertData { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime ModifiedDate { get; set; }
		public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		public int Processed { get; set; }
	}
}
