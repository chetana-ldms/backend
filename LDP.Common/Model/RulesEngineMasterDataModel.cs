using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.BL.Models
{
    public class CommonRulesEngineMasterDataModel
    {
		//public int masterdataid { get; set; }
		public string? masterdatatype { get; set; }
		public string? masterdataname { get; set; }
		public int active { get; set; }
		public string? masterdatavalue { get; set; }
		public string? MapRefIdentifier { get; set; }
		//public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		//public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		public int Processed { get; set; }
	}

	public class AddRulesEngineMasterDataModel : CommonRulesEngineMasterDataModel
	{
		//public int masterdataid { get; set; }
		//public string? masterdatatype { get; set; }
		//public string? masterdataname { get; set; }
		//public int active { get; set; }
		//public string? masterdatavalue { get; set; }
		//public string? MapRefIdentifier { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}


	public class UpdateRulesEngineMasterDataModel: CommonRulesEngineMasterDataModel
	{
		public int masterdataid { get; set; }
		//public string? masterdatatype { get; set; }
		//public string? masterdataname { get; set; }
		//public int active { get; set; }
		//public string? masterdatavalue { get; set; }
		//public string? MapRefIdentifier { get; set; }
		//public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		//public string? CreatedUser { get; set; }
		public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}

	public class GetRulesEngineMasterDataModel : UpdateRulesEngineMasterDataModel
	{
		//public int masterdataid { get; set; }
		//public string? masterdatatype { get; set; }
		//public string? masterdataname { get; set; }
		//public int active { get; set; }
		//public string? masterdatavalue { get; set; }
		//public string? MapRefIdentifier { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}
}
