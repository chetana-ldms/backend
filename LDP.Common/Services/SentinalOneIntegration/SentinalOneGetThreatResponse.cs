using LDP_APIs.Models;

namespace LDP.Common.Services
{


    public class SentinalOneGetThreatResponse:baseResponse 
    {
        public SentinalThreatData? Alerts { get; set; }

        public int OrgId { get; set; }

        public int ToolId { get; set; }

        public string ResponseJson { get; set; }

    }

    public class SentinalThreatData
    {
        public List<SentinalThreatDetails>? data { get; set; }
        public pagination? Pagination { get; set; }
    }
    public class SentinalThreatDetails
    {
        public AgentDetectionInfo? AgentDetectionInfo { get; set; }
        public AgentRealtimeInfo? AgentRealtimeInfo { get; set; }
        public containerinfo? ContainerInfo { get; set; }
        public string? Id { get; set; }
        public List<Indicator>? Indicators { get; set; }
       
        public kubernetesInfo? KubernetesInfo { get; set; }
       
        public List<MitigationStatus>? MitigationStatus { get; set; }
        public ThreatInfo? threatInfo { get; set; }
        public List<string>? WhiteningOptions { get; set; }
        
    }

   
   
        public class Indicator
    {
        public string category { get; set; }
        public string description { get; set; }
        public List<int> ids { get; set; }
        public List<object> tactics { get; set; }
    }
    public class networkInterface
    {
        public string? id { get; set; }
        public List<string>? inet { get; set; }
        public List<string>? inet6 { get; set; }
        public string? name { get; set; }
        public string? physical { get; set; }
    }

    public class mitigationstatusActionCounters
    {
        public int Failed { get; set; }
        public int NotFound { get; set; }
        public int PendingReboot { get; set; }
        public int Success { get; set; }
        public int Total { get; set; }
    }

    public class MitigationStatus
    {
        public string? Action { get; set; }
        //public mitigationstatusActioncounters? ActionsCounters { get; set; }
        public bool AgentSupportsReport { get; set; }
        public bool GroupNotFound { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? LatestReport { get; set; }
        public DateTime? MitigationEndedAt { get; set; }
        public DateTime? MitigationStartedAt { get; set; }
        public string? Status { get; set; }
    }

    public class ThreatInfo
    {
        public string? AnalystVerdict { get; set; }
        public string? AnalystVerdictDescription { get; set; }
        public bool? AutomaticallyResolved { get; set; }
        public string? BrowserType { get; set; }
        public string? CertificateId { get; set; }
        public string? Classification { get; set; }
        public string? ClassificationSource { get; set; }
        public string? CloudFilesHashVerdict { get; set; }
        public string? CollectionId { get; set; }
        public string? ConfidenceLevel { get; set; }
        public DateTime? createdAt { get; set; }
        public List<Dictionary<string?, string?>>? DetectionEngines { get; set; }
        public string? DetectionType { get; set; }
        //public List<string>? Engines { get; set; }
        public bool ExternalTicketExists { get; set; }
        public string? ExternalTicketId { get; set; }
        public bool? FailedActions { get; set; }
        public string? FileExtension { get; set; }
        public string? FileExtensionType { get; set; }
        public string? FilePath { get; set; }
        public int FileSize { get; set; }
        public string? FileVerificationType { get; set; }
        public DateTime? IdentifiedAt { get; set; }
        public string? IncidentStatus { get; set; }
        public string? IncidentStatusDescription { get; set; }
        public string? InitiatedBy { get; set; }
        public string? InitiatedByDescription { get; set; }
        public string? InitiatingUserId { get; set; }
        public string? InitiatingUsername { get; set; }
        public bool? IsFileless { get; set; }
        public bool? IsValidCertificate { get; set; }
        public string? MaliciousProcessArguments { get; set; }
        public string? Md5 { get; set; }
        public bool? MitigatedPreemptively { get; set; }
        public string? MitigationStatus { get; set; }
        public string? MitigationStatusDescription { get; set; }
        public string? OriginatorProcess { get; set; }
        public bool? PendingActions { get; set; }
        public string? ProcessUser { get; set; }
        public string? PublisherName { get; set; }
        public bool? ReachedEventsLimit { get; set; }
        public bool? RebootRequired { get; set; }
        public string? Sha1 { get; set; }
        public string? Sha256 { get; set; }
        public string? Storyline { get; set; }
        public string? threatId { get; set; }
        public string? threatName { get; set; }
        public DateTime? updatedAt { get; set; }
    }

    

    public class AgentDetectionInfo
    {
        public string? AccountId { get; set; }
        public string? AccountName { get; set; }
        public string? AgentDetectionState { get; set; }
        public string?  AgentDomain { get; set; }
        public string? AgentIpV4 { get; set; }
        public string? AgentIpV6 { get; set; }
        public string? AgentLastLoggedInUpn { get; set; }
        public string? AgentLastLoggedInUserMail { get; set; }
        public string? AgentLastLoggedInUserName { get; set; }
        public string AgentMitigationMode { get; set; }
        public string? AgentOsName { get; set; }
        public string? AgentOsRevision { get; set; }
        public DateTime? AgentRegisteredAt { get; set; }
        public string? AgentUuid { get; set; }
        public string? AgentVersion { get; set; }
        //public object? CloudProviders { get; set; }
        public string? ExternalIp { get; set; }
        public string? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? SiteId { get; set; }
        public string? SiteName { get; set; }
    }

    public class AgentRealtimeInfo
    {
        public string? AccountId { get; set; }
        public string? AccountName { get; set; }
        public int ActiveThreats { get; set; }
        public string? AgentComputerName { get; set; }
        public bool? agentDecommissionedAt { get; set; }
        public string? agentDomain { get; set; }
        public string? agentId { get; set; }
        public bool agentinfected { get; set; }
        public bool agentisactive { get; set; }
        public bool agentisdecommissioned { get; set; }
        public string? agentmachinetype { get; set; }
        public string? agentmitigationmode { get; set; }
        public string? agentnetworkstatus { get; set; }
        public string? agentosname { get; set; }
        public string? agentosrevision { get; set; }
        public string? agentostype { get; set; }
        public string? agentuuid { get; set; }
        public string? agentversion { get; set; }
        //public List<NetworkInterface>? NetworkInterfaces { get; set; }
        public string? operationalstate { get; set; }
        public bool rebootrequired { get; set; }
        public DateTime? scanabortedat { get; set; }
        public DateTime? scanfinishedat { get; set; }
        public DateTime? scanstartedat { get; set; }
        public string? scanstatus { get; set; }
        public string? siteid { get; set; }
        public string? sitename { get; set; }
        //public object? storagename { get; set; }
        //public object? storagetype { get; set; }
       // public List<object>? useractionsneeded { get; set; }
    }

    public class containerinfo
    {
        //public object? id { get; set; }
        //public object? image { get; set; }
        public bool? iscontainerquarantine { get; set; }
        //public object? labels { get; set; }
        //public object? name { get; set; }
    }

    public class kubernetesInfo
    {
        //public object? cluster { get; set; }
        //public object? controllerkind { get; set; }
        //public object? controllerlabels { get; set; }
        //public object? controllerName { get; set; }
        public bool? iscontainerquarantine { get; set; }
        //public object? namespace { get; set; }
        //public object? namespacelabels { get; set; }
        //public object? node { get; set; }
        //public object? nodelabels { get; set; }
        //public object? pod { get; set; }
        //public object? podLabels { get; set; }
    }

    public class pagination
    {
        public string? nextcursor { get; set; }
        public int totalitems { get; set; }
    }


    // Below used to get the createdAt as string for further process purpose.
    public class SentinalThreatDetailsTemp
    {

       public ThreatInfoTemp? threatInfo { get; set; }


    }
    public class ThreatInfoTemp
    {

        public string? createdAt { get; set; }
    }

}
