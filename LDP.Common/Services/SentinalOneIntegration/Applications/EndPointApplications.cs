using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    public class EndPointApplications : baseResponse
    {
        public List<Software>? Data { get; set; }
    }

    
    public class Software
    {
        public DateTime? InstalledDate { get; set; }
        public string? Name { get; set; }
        public string? Publisher { get; set; }
        public double? Size { get; set; }
        public string? Version { get; set; }
    }

    

}
