using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications.Inventory
{
    public class SentinalOneApplicationInventory : baseResponse
    {
        public List<LDP.Common.Services.SentinalOneIntegration.Applications.Inventory.Data> data { get; set; }

        public Pagination? pagination { get; set; }
    }


    public class Data
    {
        public string? applicationName { get; set; }
        public string? applicationVendor { get; set; }
        public int applicationVersionsCount { get; set; }
        public int endpointsCount { get; set; }
        public bool estimate { get; set; }
    }

    //public class Pagination
    //{
    //    public string? nextCursor { get; set; }
    //    public int totalItems { get; set; }
    //}

    //public class SentinalOneApplicationInventoryResponse : baseResponse
    //{
    //    public List<LDP.Common.Services.SentinalOneIntegration.Applications.Inventory.Data> ApplicationList { get; set; }

    //}

    public class SentinalOneApplicationInventoryResponse : baseResponse
    {
        public List<ApplicationInventory>  ApplicationList { get; set; }

    }

    public class ApplicationInventory
    {
        public string? applicationName { get; set; }
        public string? applicationVendor { get; set; }
        public int applicationVersionsCount { get; set; }
        public int endpointsCount { get; set; }
        public bool estimate { get; set; }
    }
}
