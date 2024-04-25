using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration
{
    public class GetThreatDetailsResponse:baseResponse
    {
        public ThreatHeaderDtls? ThreatHeaderDtls { get; set; } 
        public NetworkHistory? NetworkHistory { get; set; }
        public Threat_Info? ThreatInfo { get; set; }
        public Endpoint_Info? Endpoint_Info { get; set; }

    }

    public class Threat_Info
    {
        public string? Name { get; set; }
        public string? URL { get; set; } //URL: https://apse1-2001.sentinelone.net/incidents/threats/1813164543545709284/overview
        public string? Path { get; set; } //: \Device\HarddiskVolume3\Users\Administrator\Downloads\eicarcom2.zip
        public string? ProcessUser { get; set; } //: XMAN\Administrator
        public string? SignatureVerification { get; set; } //: NotSigned
        public string? OriginatingProcess { get; set; } //: chrome.exe
        public string? SHA1 { get; set; } //: bec1b52d350d721c7e22a6d4bb0a92909893a3ae
        public string? InitiatedBy { get; set; } //: Agent Policy
        public string? Engine { get; set; } //: SentinelOne Cloud
        public string? DetectionType { get; set; } //: Static
        public string? Classification { get; set; } //: Malware
        public string? FileSize { get; set; } //: 308.00 B
        public string? Storyline { get; set; } //: 9F55B7978ACB81D9
        public string? ThreatId { get; set; } //: 1813164543545709284
    }
    public class Endpoint_Info
    {
        public string? ComputerName { get; set; } //: Xman
        public string? ConsoleConnectivity { get; set; } //: Online
        public string? FullDiskScan { get; set; } //: Completed at Nov 08, 2023 20:41:57
        public string? PendinRreboot { get; set; } //: No
        public string? NetworkStatus { get; set; } //: Connected
        public string? Scope { get; set; } //: LANCESOFT INDIA PRIVATE LIMITED / Lancesoft SOC / Default Group
        public string? OSVersion { get; set; } // Version: Windows 11 Pro 22621
        public string? AgentVersion { get; set; } // Version: 23.3.1.114
        public string? Policy { get; set; } //: protect
        public string? LoggedInUser { get; set; } //: Administrator
        public string? UUID { get; set; } //: 58057ab3f5cf4ce89b6f2e535609f659
        public string? Domain { get; set; } //: WORKGROUP
        public string? IPV4Address { get; set; } //: 10.41.3.232
        public string? IPV6Address { get; set; } //: fe80::594e:8081:e47d:1a28
        public string? ConsoleVisibleIPAddress { get; set; } //: 115.110.192.133
        public DateTime? SubscriptionTime { get; set; } //: Sep 08, 2023 07:58:21
    }
    public class NetworkHistory
    {
        public DateTime FirstSeen { get; set; }

        public DateTime LastSeen { get; set; }

        public string? EndPointOccurances { get; set; }

        public string? ScopeOccurances { get; set; }
    }

    public class ThreatHeaderDtls 
    {
        public string? ThreatStatus { get; set; }
        public string? AIConfidenceLevel { get; set; }

        public string? AnalysisVerdict { get; set; }

        public string? IncidentStatus { get; set; }

        public List<string>? MiticationActions { get; set; }

        public DateTime? IdentifiedTime { get; set; }

        public DateTime? ReportingTime { get; set; }

    }

}