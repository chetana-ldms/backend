//using LDP_APIs.Models;

//namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
//{
//    public class EndPoints :baseResponse
//    {
//        public List<EndPointData> Data { get; set; }
//        public Pagination Pagination { get; set; }

//    }

   
//public class ActiveDirectory
//    {
//        public object? ComputerDistinguishedName { get; set; }
//        public List<object>? ComputerMemberOf { get; set; }
//        public object? LastUserDistinguishedName { get; set; }
//        public List<object>? LastUserMemberOf { get; set; }
//    }

//    public class Location
//    {
//        public string? Id { get; set; }
//        public string? Name { get; set; }
//        public string? Scope { get; set; }
//    }

//    public class NetworkInterface
//    {
//        public string? GatewayIp { get; set; }
//        public string? GatewayMacAddress { get; set; }
//        public string? Id { get; set; }
//        public List<string>? Inet { get; set; }
//        public List<string>? Inet6 { get; set; }
//        public string? Name { get; set; }
//        public string? Physical { get; set; }
//    }

//    public class Tag
//    {
//        public List<object>? Sentinelone { get; set; }
//    }

//    public class EndPointData
//    {
//        public string? AccountId { get; set; }
//        public string? AccountName { get; set; }
//        public ActiveDirectory? ActiveDirectory { get; set; }
//        public int ActiveThreats { get; set; }
//        public string? AgentVersion { get; set; }
//        public bool AllowRemoteShell { get; set; }
//        public string? AppsVulnerabilityStatus { get; set; }
//        public Dictionary<string, object>? CloudProviders { get; set; }
//        public string? ComputerName { get; set; }
//        public string? ConsoleMigrationStatus { get; set; }
//        public object? ContainerizedWorkloadCounts { get; set; }
//        public int CoreCount { get; set; }
//        public int CpuCount { get; set; }
//        public string? CpuId { get; set; }
//        public DateTime CreatedAt { get; set; }
//        public string? DetectionState { get; set; }
//        public string? Domain { get; set; }
//        public bool EncryptedApplications { get; set; }
//        public string? ExternalId { get; set; }
//        public string? ExternalIp { get; set; }
//        public bool FirewallEnabled { get; set; }
//        public DateTime FirstFullModeTime { get; set; }
//        public DateTime FullDiskScanLastUpdatedAt { get; set; }
//        public string? GroupId { get; set; }
//        public string? GroupIp { get; set; }
//        public string? GroupName { get; set; }
//        public bool HasContainerizedWorkload { get; set; }
//        public string? Id { get; set; }
//        public bool InRemoteShellSession { get; set; }
//        public bool Infected { get; set; }
//        public string? InstallerType { get; set; }
//        public bool IsActive { get; set; }
//        public bool IsDecommissioned { get; set; }
//        public bool IsPendingUninstall { get; set; }
//        public bool IsUninstalled { get; set; }
//        public bool IsUpToDate { get; set; }
//        public DateTime LastActiveDate { get; set; }
//        public string? LastIpToMgmt { get; set; }
//        public string? LastLoggedInUserName { get; set; }
//        public DateTime LastSuccessfulScanDate { get; set; }
//        public string? LicenseKey { get; set; }
//        public bool LocationEnabled { get; set; }
//        public string? LocationType { get; set; }
//        public List<Location>? Locations { get; set; }
//        public string? MachineType { get; set; }
//        public List<object>? MissingPermissions { get; set; }
//        public string? MitigationMode { get; set; }
//        public string? MitigationModeSuspicious { get; set; }
//        public string? ModelName { get; set; }
//        public List<NetworkInterface>? NetworkInterfaces { get; set; }
//        public bool NetworkQuarantineEnabled { get; set; }
//        public string? NetworkStatus { get; set; }
//        public string? OperationalState { get; set; }
//        public object? OperationalStateExpiration { get; set; }
//        public string? OsArch { get; set; }
//        public string? OsName { get; set; }
//        public string? OsRevision { get; set; }
//        public DateTime OsStartTime { get; set; }
//        public string? OsType { get; set; }
//        public object? OsUsername { get; set; }
//        public object? ProxyStates { get; set; }
//        public string? RangerStatus { get; set; }
//        public string? RangerVersion { get; set; }
//        public DateTime RegisteredAt { get; set; }
//        public string? RemoteProfilingState { get; set; }
//        public object? RemoteProfilingStateExpiration { get; set; }
//        public object? ScanAbortedAt { get; set; }
//        public DateTime ScanFinishedAt { get; set; }
//        public DateTime ScanStartedAt { get; set; }
//        public string? ScanStatus { get; set; }
//        public string? SerialNumber { get; set; }
//        public bool ShowAlertIcon { get; set; }
//        public string? SiteId { get; set; }
//        public string? SiteName { get; set; }
//        public object? StorageName { get; set; }
//        public object? StorageType { get; set; }
//        public Tag? Tags { get; set; }
//        public bool ThreatRebootRequired { get; set; }
//        public int TotalMemory { get; set; }
//        public DateTime UpdatedAt { get; set; }
//        public List<object>? UserActionsNeeded { get; set; }
//        public string? Uuid { get; set; }
//    }

//    //public class Pagination
//    //{
//    //    public string? NextCursor { get; set; }
//    //    public int TotalItems { get; set; }
//    //}

//    //public class RootObject
//    //{
//    //    public List<Data>? Data { get; set; }
//    //    public Pagination? Pagination { get; set; }
//    //}

//    public class EndPointsRespoinse : baseResponse
//    {
//        public List<EndPointData>? EndPointList { get; set; }

//    }

//}
