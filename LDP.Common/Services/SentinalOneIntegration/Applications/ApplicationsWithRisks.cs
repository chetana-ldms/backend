using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications.Risks
{
    public class ApplicationsWithRisks:baseResponse
    {
        public List<Application>? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }

    public class Application
    {
        public string? ApplicationId { get; set; }
        public int? CveCount { get; set; }
        public int? DaysDetected { get; set; }
        public DateTime? DetectionDate { get; set; }
        public int? EndpointCount { get; set; }
        public bool? Estimate { get; set; }
        public string? HighestNvdBaseScore { get; set; } // Assuming this can be null or a double value
        public string? HighestSeverity { get; set; }
        public string? Name { get; set; }
        public string? Vendor { get; set; }
    }

    //public class Pagination
    //{
    //    public string? NextCursor { get; set; }
    //    public int TotalItems { get; set; }
    //}

    



}
