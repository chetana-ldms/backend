using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class AccountPolicy:baseResponse
    {
        public AgentData? data { get; set; }
    }

    

    public class AgentData
    {
        public bool? agentLoggingOn { get; set; }
        public bool? agentNotification { get; set; }
        public AgentUi? agentUi { get; set; }
        public bool? agentUiOn { get; set; }
        public bool? allowRemoteShell { get; set; }
        public bool? antiTamperingOn { get; set; }
        public int? autoDecommissionDays { get; set; }
        public bool? autoDecommissionOn { get; set; }
        public AutoFileUpload? autoFileUpload { get; set; }
        public bool? autoImmuneOn { get; set; }
        public string? autoMitigationAction { get; set; }
        public bool? cloudValidationOn { get; set; }
        public DateTime? createdAt { get; set; }
        public bool? driverBlocking { get; set; }
        public DvAttributesPerEventType? dvAttributesPerEventType { get; set; }
        public Engines? engines { get; set; }
        public ForensicsAutoTriggering? forensicsAutoTriggering { get; set; }
        public bool? fwForNetworkQuarantineEnabled { get; set; }
        public string? identityEndpointReporting { get; set; }
        public bool? identityOn { get; set; }
        public int? identityReportInterval { get; set; }
        public int? identityThrottlingInterval { get; set; }
        public int? identityUpdateInterval { get; set; }
       // public object? inheritedFrom { get; set; } // arun
        public bool? ioc { get; set; }
        public IocAttributes? iocAttributes { get; set; }
        public bool? isDefault { get; set; }
        public bool? isDvPolicyPerEventType { get; set; }
        public string? mitigationMode { get; set; }
        public string? mitigationModeSuspicious { get; set; }
        public bool? monitorOnExecute { get; set; }
        public bool? monitorOnWrite { get; set; }
        public bool? networkQuarantineOn { get; set; }
        public RemoteScriptOrchestration? remoteScriptOrchestration { get; set; }
        public bool? removeMacros { get; set; }
        public bool? researchOn { get; set; }
        public bool? scanNewAgents { get; set; }
        public bool? signedDriverBlockingOn { get; set; }
        public bool? snapshotsOn { get; set; }
        public bool? unsignedDriverBlockingOn { get; set; }
        public DateTime? updatedAt { get; set; }
    }

    public class AgentUi
    {
        public bool? agentUiOn { get; set; }
        public string? contactCompany { get; set; }
        public string? contactDirectMessage { get; set; }
        public string? contactEmail { get; set; }
        public string? contactFreeText { get; set; }
        public string? contactOther { get; set; }
        public string? contactPhoneNumber { get; set; }
        public string? contactSupportWebsite { get; set; }
        public bool? devicePopUpNotifications { get; set; }
        public double? maxEventAgeDays { get; set; }
        public bool? showAgentWarnings { get; set; }
        public bool? showDeviceTab { get; set; }
        public bool? showQuarantineTab { get; set; }
        public bool? showSupport { get; set; }
        public bool? showSuspicious { get; set; }
        public bool? threatPopUpNotifications { get; set; }
    }

    public class AutoFileUpload
    {
        public bool? enabled { get; set; }
        public bool? includeBenignFiles { get; set; }
        public double? maxDailyFileUpload { get; set; }
        public double? maxDailyFileUploadLimit { get; set; }
        public double? maxFileSize { get; set; }
        public double? maxFileSizeLimit { get; set; }
        public double? maxLocalDiskUsage { get; set; }
        public double? maxLocalDiskUsageLimit { get; set; }
    }

    public class DvAttributesPerEventType
    {
        public AutoInstallBrowserExtensions? autoInstallBrowserExtensions { get; set; }
        public BehavioralIndicators? behavioralIndicators { get; set; }
        public CommandScripts? commandScripts { get; set; }
        public CrossProcess? crossProcess { get; set; }
        public DataMasking? dataMasking { get; set; }
        public DllModuleLoad? dllModuleLoad { get; set; }
        public Dns? dns { get; set; }
        public Driver? driver { get; set; }
        public File? file { get; set; }
        public Ip? ip { get; set; }
       // public Login? login { get; set; }
        public NamedPipe? namedPipe { get; set; }
        public NamedPipeExtended? namedPipeExtended { get; set; }
        public Process? process { get; set; }
        public Registry? registry { get; set; }
        public ScheduledTask? scheduledTask { get; set; }
        public SmartFileMonitoring? smartFileMonitoring { get; set; }
        public Url? url { get; set; }
        public WindowsEventLogs? windowsEventLogs { get; set; }
        public WindowsEventLogsExtended? windowsEventLogsExtended { get; set; }
    }

    public class AutoInstallBrowserExtensions
    {
        public bool? autoInstallBrowserExtensions { get; set; }
    }

    public class BehavioralIndicators
    {
        public bool? dvEventTypeBehavioralIndicators { get; set; }
    }

    public class CommandScripts
    {
        public bool? dvEventTypeCommandScripts { get; set; }
    }

    public class CrossProcess
    {
        public bool? dvEventTypeCrossProcessDuplicateProcess { get; set; }
        public bool? dvEventTypeCrossProcessDuplicateThread { get; set; }
        public bool? dvEventTypeCrossProcessOpenProcess { get; set; }
        public bool? dvEventTypeCrossProcessRemoteThread { get; set; }
    }

    public class DataMasking
    {
        public bool? dataMasking { get; set; }
    }

    public class DllModuleLoad
    {
        public bool? dvEventTypeDllModuleLoad { get; set; }
    }

    public class Dns
    {
        public bool? dvEventTypeDns { get; set; }
    }

    public class Driver
    {
        public bool? dvEventTypeDriverLoad { get; set; }
    }

    public class File
    {
        public bool? dvEventTypeFileCreation { get; set; }
        public bool? dvEventTypeFileDeletion { get; set; }
        public bool? dvEventTypeFileModification { get; set; }
        public bool? dvEventTypeFileRename { get; set; }
        public bool? fullDiskScan { get; set; }
    }

    public class Ip
    {
        public bool? dvEventTypeIpConnect { get; set; }
        public bool? dvEventTypeIpListen { get; set; }
    }

   public class NamedPipe
    {
        public bool? dvEventTypeNamedPipeConnection { get; set; }
        public bool? dvEventTypeNamedPipeCreation { get; set; }
    }

    public class NamedPipeExtended
    {
        public bool? namedPipeExtended { get; set; }
    }

    public class Process
    {
        public bool? dvEventTypeProcessCreation { get; set; }
    }

    public class Registry
    {
        public bool? dvEventTypeRegistryKeyCreated { get; set; }
        public bool? dvEventTypeRegistryKeyDelete { get; set; }
        public bool? dvEventTypeRegistryKeyExport { get; set; }
        public bool? dvEventTypeRegistryKeyImport { get; set; }
        public bool? dvEventTypeRegistryKeyRename { get; set; }
        public bool? dvEventTypeRegistryKeySecurityChanged { get; set; }
        public bool? dvEventTypeRegistryValueCreated { get; set; }
        public bool? dvEventTypeRegistryValueDeleted { get; set; }
        public bool? dvEventTypeRegistryValueModified { get; set; }
    }

    public class ScheduledTask
    {
        public bool? dvEventTypeScheduledTaskDelete { get; set; }
        public bool? dvEventTypeScheduledTaskRegister { get; set; }
        public bool? dvEventTypeScheduledTaskStart { get; set; }
        public bool? dvEventTypeScheduledTaskTrigger { get; set; }
        public bool? dvEventTypeScheduledTaskUpdate { get; set; }
    }

    public class SmartFileMonitoring
    {
        public bool? smartFileMonitoring { get; set; }
    }

    public class Url
    {
        public bool? dvEventTypeUrl { get; set; }
    }

    public class WindowsEventLogs
    {
        public bool? dvEventTypeWindowsEventLogCreation { get; set; }
    }

    public class WindowsEventLogsExtended
    {
        public bool? windowsEventLogsExtended { get; set; }
    }

    public class Engines
    {
        public string? applicationControl { get; set; }
        public string? dataFiles { get; set; }
        public string? executables { get; set; }
        public string? exploits { get; set; }
        public string? lateralMovement { get; set; }
        public string? penetration { get; set; }
        public string? preExecution { get; set; }
        public string? preExecutionSuspicious { get; set; }
        public string? pup { get; set; }
        public string? remoteShell { get; set; }
        public string? reputation { get; set; }
    }

    public class ForensicsAutoTriggering
    {
        public bool? linuxEnabled { get; set; }
        public int? linuxProfileId { get; set; }
        public string? linuxProfileName { get; set; }
        public bool? macosEnabled { get; set; }
        public int? macosProfileId { get; set; }
        public string? macosProfileName { get; set; }
        public bool? windowsEnabled { get; set; }
        public int? windowsProfileId { get; set; }
        public string? windowsProfileName { get; set; }
    }

    public class IocAttributes
    {
        public bool? autoInstallBrowserExtensions { get; set; }
        public bool? behavioralIndicators { get; set; }
        public bool? commandScripts { get; set; }
        public bool? crossProcess { get; set; }
        public bool? dataMasking { get; set; }
        public bool? dllModuleLoad { get; set; }
        public bool? dns { get; set; }
        public bool? driver { get; set; }
        public bool? fds { get; set; }
        public bool? file { get; set; }
        public bool? ip { get; set; }
        public bool? login { get; set; }
        public bool? namedPipe { get; set; }
        public bool? namedPipeExtended { get; set; }
        public bool? process { get; set; }
        public bool? registry { get; set; }
        public bool? scheduledTask { get; set; }
        public bool? smartFileMonitoring { get; set; }
        public bool? url { get; set; }
        public bool? windowsEventLogs { get; set; }
        public bool? windowsEventLogsExtended { get; set; }
    }

    public class RemoteScriptOrchestration
    {
        public bool? alwaysUploadStreamToCloud { get; set; }
        public double? maxDailyFileDownload { get; set; }
        public double? maxDailyFileDownloadLimit { get; set; }
        public double? maxDailyFileUpload { get; set; }
        public double? maxDailyFileUploadLimit { get; set; }
        public double? maxFileSize { get; set; }
        public double? maxFileSizeLimit { get; set; }
        public double? maxLocalPackageDiskUsage { get; set; }
        public double? maxLocalPackageDiskUsageLimit { get; set; }
    }

    //public class Root
    //{
    //    public AgentData? data { get; set; }
    //}

}
