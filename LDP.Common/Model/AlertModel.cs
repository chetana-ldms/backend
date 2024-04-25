namespace LDP_APIs.BL.Models
{
	public class AlertModel
	{
		public int AlertID { get; set; }

		public Double AlertDevicePKID { get; set; }
		public int ToolID { get; set; }
		public int OrgID { get; set; }
		public string? Name { get; set; }
		//public string? Severity { get; set; }
		public string? Score { get; set; }
		public string? Status { get; set; }
		public int StatusID { get; set; }
		public DateTime? Detectedtime { get; set; }

        public string? DetectedtimeString { get; set; }
        public int ObservableTagID { get; set; }
		public string? ObservableTag { get; set; }
		public int OwnerUserID { get; set; }
		public string? ownerusername { get; set; }
		public string? Source { get; set; }
		public string? AlertData { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string? CreatedUser { get; set; }
		public string? ModifiedUser { get; set; }
		public int Processed { get; set; }

		public string? AutomationStatus { get; set; }

		public int priorityid { get; set; }

		public string? priorityname { get; set; }

		public int PositiveAnalysisId { get; set; }

		public string? PositiveAnalysis { get; set; }

		public DateTime? resolvedtime { get; set; }

		public double resolvedtimeDuration { get; set; }

        public int AlertIncidentMappingId { get; set; }

        public int FalsePositive { get; set; }

        public string? SLA { get; set; }

        public string? SeverityName { get; set; }

        public int OrgToolSeverity { get; set; }

        public string? SeverityId { get; set; }

        public string? EventId { get; set; }

        public string? DestinationUser { get; set; }

        public string? SourceIp { get; set; }

        public string? Vendor { get; set; }
    }
}
