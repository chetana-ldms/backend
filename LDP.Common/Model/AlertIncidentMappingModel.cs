namespace LDP.Common.Model
{
    public class CommonAlertIncidentMappingModel
    {
		public int orgid { get; set; }
		public int tooltypeid { get; set; }
		public int toolid { get; set; }
		//public double alertid { get; set; }
		public double? incidentnumber { get; set; }
		public string? incidentdata { get; set; }
        public int SignificantIncident { get; set; }
        public string? ClientToolIncidentId { get; set; }

    }

	public class AddAlertIncidentMappingModel: CommonAlertIncidentMappingModel
	{
		public DateTime? CreateDate { get; set; }
    	public string? CreateUser { get; set; }
	}

	public class UpdateAlertIncidentMappingModel : CommonAlertIncidentMappingModel
	{
		public double alertincientmappingid { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string? ModifiedUser { get; set; }
	}

	public class GetAlertIncidentMappingModel : CommonAlertIncidentMappingModel
    {
        public double alertincientmappingid { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string? CreatedUser { get; set; }
        //public int Processed { get; set; }
        public List<GetAlertIncidentMappingDtlModel>? AlertIncidentMappingDtl { get; set; }

	}
   

    public class GetAlertsByIncidentIdModel
    {
        public int orgid { get; set; }
        public double alertid { get; set; }
    }
}
