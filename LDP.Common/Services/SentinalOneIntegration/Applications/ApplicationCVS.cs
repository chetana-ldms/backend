using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications.CVS
{
    public class ApplicationCVS:baseResponse
    {
        public List<Data> data { get; set; }
        public Pagination pagination { get; set; }

    }

    public class FpFnMark
    {
        public string? markedBy { get; set; }
        public DateTime? markedDate { get; set; }
        public MarkedOnScope? markedOnScope { get; set; }
        public string? reason { get; set; }
        public string? type { get; set; }
    }

    public class MarkedOnScope
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
    }

    public class Data
    {
        public string? cveId { get; set; }
        public string? cvssVersion { get; set; }
        public string? description { get; set; }
        public string? exploitCodeMaturity { get; set; }
        public string? exploitedInTheWild { get; set; }
        public List<FpFnMark>? fpFnMarks { get; set; }
        public string? mitreUrl { get; set; }
        public string? nvdBaseScore { get; set; }
        public string? nvdUrl { get; set; }
        public DateTime? publishedDate { get; set; }
        public string? remediationLevel { get; set; }
        public string? reportConfidence { get; set; }
        public string? riskScore { get; set; }
        public string? severity { get; set; }
    }

    //public class Pagination
    //{
    //    public string? nextCursor { get; set; }
    //    public int totalItems { get; set; }
    //}


    public class ApplicatioCVSData
    {
        public string? cveId { get; set; }
        public string? cvssVersion { get; set; }
        public string? description { get; set; }
        public string? exploitCodeMaturity { get; set; }
        public string? exploitedInTheWild { get; set; }
        public List<FpFnMark>? fpFnMarks { get; set; }
        public string? mitreUrl { get; set; }
        public string? nvdBaseScore { get; set; }
        public string? nvdUrl { get; set; }
        public DateTime? publishedDate { get; set; }
        public string? remediationLevel { get; set; }
        public string? reportConfidence { get; set; }
        public string? riskScore { get; set; }
        public string? severity { get; set; }
    }

    public class ApplicationCVSResponse:baseResponse
    {
        public List<ApplicatioCVSData>? CVSList { get; set; }
    }

}
