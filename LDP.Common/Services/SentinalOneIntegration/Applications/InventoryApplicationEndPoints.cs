using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications.EndPoints
{
    public class ApplicationEndPoints:baseResponse
    {
        public List<Data>? data { get; set; }
        public Pagination? pagination { get; set; }
    }

public class Data
    {
        public string? accountName { get; set; }
        public DateTime? applicationInstallationDate { get; set; }
        public string? applicationInstallationPath { get; set; }
        public string? applicationName { get; set; }
        public int? coreCount { get; set; }
        public string? cpe { get; set; }
        public int? cpuCount { get; set; }
        public DateTime? detectionDate { get; set; }
        public string? endpointId { get; set; }
        public string? endpointName { get; set; }
        public string? endpointType { get; set; }
        public string? endpointUuid { get; set; }
        public int? fileSize { get; set; }
        public string? groupName { get; set; }
        public string? id { get; set; }
        public string? osArch { get; set; }
        public string? osName { get; set; }
        public string? osType { get; set; }
        public string? osVersion { get; set; }
        public string? siteName { get; set; }
        public string? version { get; set; }
    }

    //public class Pagination
    //{
    //    public string? nextCursor { get; set; }
    //    public int totalItems { get; set; }
    //}

    //public class Root
    //{
    //    public List<Data> data { get; set; }
    //    public Pagination pagination { get; set; }
    //}
    public class ApplicationEndPoint
    {
        public string? accountName { get; set; }
        public DateTime? applicationInstallationDate { get; set; }
        public string? applicationInstallationPath { get; set; }
        public string? applicationName { get; set; }
        public int? coreCount { get; set; }
        public string? cpe { get; set; }
        public int? cpuCount { get; set; }
        public DateTime? detectionDate { get; set; }
        public string? endpointId { get; set; }
        public string? endpointName { get; set; }
        public string? endpointType { get; set; }
        public string? endpointUuid { get; set; }
        public int? fileSize { get; set; }
        public string? groupName { get; set; }
        public string? id { get; set; }
        public string? osArch { get; set; }
        public string? osName { get; set; }
        public string? osType { get; set; }
        public string? osVersion { get; set; }
        public string? siteName { get; set; }
        public string? version { get; set; }
    }

    public class ApplicationEndPointsResponse : baseResponse
    {
        public List<ApplicationEndPoint>? EndPoints { get; set; }    
    }

}
