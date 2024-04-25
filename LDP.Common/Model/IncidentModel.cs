namespace LDP.Common.Model
{
    public class CommonIncidentModel
    {
        public CommonIncidentModel()
		{
			
    }
        public string? Description { get; set; }
        public string? Subject { get; set; }=null;

        public int Priority { get; set; }
		public int Severity { get; set; }
		//public string? Type { get; set; }
		public string? EventID { get; set; } = null;
		public string? DestinationUser { get; set; } = null;
		public string? SourceIP { get; set; } = null;
		public string? Vendor { get; set; } = null;
		public int Owner { get; set; }	

        public int TypeId { get; set; }

        public int OrgId { get; set; }

       // public int ToolId { get; set; }

        //public string? PriorityName { get; set; }

        //public string? SeverityName { get; set; }

        //public int SeverityId { get; set; }
        //public string? OwnerName { get; set; }

        public int IncidentStatus { get; set; }
        //public string? IncidentStatusName { get; set; }

        public string? Score { get; set; } = null;

		public DateTime? IncidentDate { get; set; }

		public int SignificantIncident { get; set;}

       

    }
	public class AddIncidentModel: CommonIncidentModel
	{
		public DateTime? CreateDate { get; set; }

		//public string? CreatedUser { get; set; }
		public List<double>? AlertIDs { get; set; } = new List<double>();
	}

	public class UpdateIncidentModel:CommonIncidentModel
	{
		public double IncidentID { get; set; }

		
		public DateTime? ModifiedDate { get; set; }
		public string? ModifiedUser { get; set; }
	}

	public class GetIncidentModel: UpdateIncidentModel
	{
		public DateTime? CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
		//public int Processed { get; set; }


        public string? PriorityName { get; set; }

        public string? SeverityName { get; set; }

        public string? OwnerName { get; set; }

        public string? IncidentStatusName { get; set; }

        public string? Type { get; set; }

        public int InternalIncident { get; set; }
        public GetAlertIncidentMappingModel? AlertIncidentMapping { get; set; }	

	}

	
}
