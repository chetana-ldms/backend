namespace LDP_APIs.BL.Models
{
    public class LDPMasterDataModel
    {
		public int DataID { get; set; }
		public string? DataType { get; set; }
		public string? DataName { get; set; }
		//public int active { get; set; }
		public string? DataValue { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string? CreatedUser { get; set; }
		public string? ModifiedUser { get; set; }
	}
}
