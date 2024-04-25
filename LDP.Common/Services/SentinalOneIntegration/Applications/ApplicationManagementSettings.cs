using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications.ManagementSettings
{
    public class AppManagementSettings:baseResponse
    {
        public Data data { get; set; }
    }

        public class Data
        {
            public bool ExtensiveLinuxScanEnabled { get; set; }
            public bool ExtensiveScanEnabled { get; set; }
            public string? InheritedFrom { get; set; } // Assuming it can be null or another type
            public bool IsDefaultPolicy { get; set; }
            public bool VulnerabilitiesScanEnabled { get; set; }
        }

    public class ApplicationManagementSettings
    {
        public bool ExtensiveLinuxScanEnabled { get; set; }
        public bool ExtensiveScanEnabled { get; set; }
        public string? InheritedFrom { get; set; } // Assuming it can be null or another type
        public bool IsDefaultPolicy { get; set; }
        public bool VulnerabilitiesScanEnabled { get; set; }
    }
    public class ApplicationManagementSettingsResponse : baseResponse
    {
        public ApplicationManagementSettings AppSettings { get; set; }
    }

}
