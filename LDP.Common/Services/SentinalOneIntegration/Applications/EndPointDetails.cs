using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications.Agent
{
    public class EndPointDetails : baseResponse
    {
        public List<Datum> data { get; set; }
        public Pagination? pagination { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ActiveDirectory
    {
        public string? computerDistinguishedName { get; set; }
        public List<string>? computerMemberOf { get; set; }
        public string? lastUserDistinguishedName { get; set; }
        public List<string>? lastUserMemberOf { get; set; }
    }

    public class CloudProviders
    {
    }

    public class Datum
    {
        public string? accountId { get; set; }
        public string? accountName { get; set; }
        public ActiveDirectory? activeDirectory { get; set; }
        public int? activeThreats { get; set; }
        public string? agentVersion { get; set; }
        public bool? allowRemoteShell { get; set; }
        public string? appsVulnerabilityStatus { get; set; }
       // public CloudProviders cloudProviders { get; set; }
        public string? computerName { get; set; }
        public string? consoleMigrationStatus { get; set; }
       // public object containerizedWorkloadCounts { get; set; }
        public int? coreCount { get; set; }
        public int? cpuCount { get; set; }
        public string? cpuId { get; set; }
        public DateTime? createdAt { get; set; }
        public string? detectionState { get; set; }
        public string? domain { get; set; }
        public bool? encryptedApplications { get; set; }
        public string? externalId { get; set; }
        public string? externalIp { get; set; }
        public bool? firewallEnabled { get; set; }
        public DateTime? firstFullModeTime { get; set; }
        public DateTime? fullDiskScanLastUpdatedAt { get; set; }
        public string groupId { get; set; }
        public string? groupIp { get; set; }
        public string? groupName { get; set; }
        public bool? hasContainerizedWorkload { get; set; }
        public string? id { get; set; }
        public bool? inRemoteShellSession { get; set; }
        public bool? infected { get; set; }
        public string? installerType { get; set; }
        public bool? isActive { get; set; }
        public bool? isDecommissioned { get; set; }
        public bool? isPendingUninstall { get; set; }
        public bool? isUninstalled { get; set; }
        public bool? isUpToDate { get; set; }
        public DateTime? lastActiveDate { get; set; }
        public string? lastIpToMgmt { get; set; }
        public string? lastLoggedInUserName { get; set; }
        public DateTime? lastSuccessfulScanDate { get; set; }
        public string? licenseKey { get; set; }
        public bool? locationEnabled { get; set; }
        public string? locationType { get; set; }
        public List<Location>? locations { get; set; }
        public string? machineType { get; set; }
        //public List<object> missingPermissions { get; set; }
        public string? mitigationMode { get; set; }
        public string? mitigationModeSuspicious { get; set; }
        public string? modelName { get; set; }
        public List<NetworkInterface>? networkInterfaces { get; set; }
        public bool? networkQuarantineEnabled { get; set; }
        public string? networkStatus { get; set; }
        public string? operationalState { get; set; }
        public object? operationalStateExpiration { get; set; }
        public string? osArch { get; set; }
        public string? osName { get; set; }
        public string? osRevision { get; set; }
        public DateTime? osStartTime { get; set; }
        public string? osType { get; set; }
        public string? osUsername { get; set; }
        public object proxyStates { get; set; }
        public string? rangerStatus { get; set; }
        public string? rangerVersion { get; set; }
        public DateTime? registeredAt { get; set; }
        public string remoteProfilingState { get; set; }
        public string? remoteProfilingStateExpiration { get; set; }
        public DateTime? scanAbortedAt { get; set; }
        public DateTime? scanFinishedAt { get; set; }
        public DateTime? scanStartedAt { get; set; }
        public string? scanStatus { get; set; }
        public string? serialNumber { get; set; }
        public bool? showAlertIcon { get; set; }
        public string? siteId { get; set; }
        public string? siteName { get; set; }
        public string? storageName { get; set; }
        public string? storageType { get; set; }
        public Tags? tags { get; set; }
        public bool? threatRebootRequired { get; set; }
        public int? totalMemory { get; set; }
        public DateTime? updatedAt { get; set; }
       // public List<object> userActionsNeeded { get; set; }
        public string? uuid { get; set; }
    }

    public class Location
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? scope { get; set; }
    }

    public class NetworkInterface
    {
        public string? gatewayIp { get; set; }
        public string? gatewayMacAddress { get; set; }
        public string? id { get; set; }
        public List<string>? inet { get; set; }
        public List<string>? inet6 { get; set; }
        public string? name { get; set; }
        public string? physical { get; set; }
    }

    //public class Pagination
    //{
    //    public string? nextCursor { get; set; }
    //    public int? totalItems { get; set; }
    //}

    public class Root
    {
      
    }

    public class Tags
    {
        public List<tagdata>? sentinelone { get; set; }
    }

    public class tagdata
    {
        public string? key { get; set; }
        public string? value { get; set; }

        public DateTime? assignedAt { get; set; }
        public string? assignedBy { get; set; }
        public string? assignedById { get; set; }
        public string? id { get; set; }
        
    }

    public class EndPointAgentDetails
    {
        public string? accountId { get; set; }
        public string? accountName { get; set; }
        public ActiveDirectory? activeDirectory { get; set; }
        public int? activeThreats { get; set; }
        public string? agentVersion { get; set; }
        public bool? allowRemoteShell { get; set; }
        public string? appsVulnerabilityStatus { get; set; }
        // public CloudProviders cloudProviders { get; set; }
        public string? computerName { get; set; }
        public string? consoleMigrationStatus { get; set; }
        // public object containerizedWorkloadCounts { get; set; }
        public int? coreCount { get; set; }
        public int? cpuCount { get; set; }
        public string? cpuId { get; set; }
        public DateTime? createdAt { get; set; }
        public string? detectionState { get; set; }
        public string? domain { get; set; }
        public bool? encryptedApplications { get; set; }
        public string? externalId { get; set; }
        public string? externalIp { get; set; }
        public bool? firewallEnabled { get; set; }
        public DateTime? firstFullModeTime { get; set; }
        public DateTime? fullDiskScanLastUpdatedAt { get; set; }
        public string groupId { get; set; }
        public string? groupIp { get; set; }
        public string? groupName { get; set; }
        public bool? hasContainerizedWorkload { get; set; }
        public string? id { get; set; }
        public bool? inRemoteShellSession { get; set; }
        public bool? infected { get; set; }
        public string? installerType { get; set; }
        public bool? isActive { get; set; }
        public bool? isDecommissioned { get; set; }
        public bool? isPendingUninstall { get; set; }
        public bool? isUninstalled { get; set; }
        public bool? isUpToDate { get; set; }
        public DateTime? lastActiveDate { get; set; }
        public string? lastIpToMgmt { get; set; }
        public string? lastLoggedInUserName { get; set; }
        public DateTime? lastSuccessfulScanDate { get; set; }
        public string? licenseKey { get; set; }
        public bool? locationEnabled { get; set; }
        public string? locationType { get; set; }
        public List<Location>? locations { get; set; }
        public string? machineType { get; set; }
        //public List<object> missingPermissions { get; set; }
        public string? mitigationMode { get; set; }
        public string? mitigationModeSuspicious { get; set; }
        public string? modelName { get; set; }
        public List<NetworkInterface>? networkInterfaces { get; set; }
        public bool? networkQuarantineEnabled { get; set; }
        public string? networkStatus { get; set; }
        public string? operationalState { get; set; }
        public object? operationalStateExpiration { get; set; }
        public string? osArch { get; set; }
        public string? osName { get; set; }
        public string? osRevision { get; set; }
        public DateTime? osStartTime { get; set; }
        public string? osType { get; set; }
        public string? osUsername { get; set; }
        public object proxyStates { get; set; }
        public string? rangerStatus { get; set; }
        public string? rangerVersion { get; set; }
        public DateTime? registeredAt { get; set; }
        public string remoteProfilingState { get; set; }
        public string? remoteProfilingStateExpiration { get; set; }
        public DateTime? scanAbortedAt { get; set; }
        public DateTime? scanFinishedAt { get; set; }
        public DateTime? scanStartedAt { get; set; }
        public string? scanStatus { get; set; }
        public string? serialNumber { get; set; }
        public bool? showAlertIcon { get; set; }
        public string? siteId { get; set; }
        public string? siteName { get; set; }
        public string? storageName { get; set; }
        public string? storageType { get; set; }
        public Tags? tags { get; set; }
        public bool? threatRebootRequired { get; set; }
        public int? totalMemory { get; set; }
        public DateTime? updatedAt { get; set; }
        // public List<object> userActionsNeeded { get; set; }
        public string? uuid { get; set; }
    }

    public class EndPointAgentDetailsResponse : baseResponse
    {
        public List<EndPointAgentDetails> EndPoints { get; set; }
    }
}
