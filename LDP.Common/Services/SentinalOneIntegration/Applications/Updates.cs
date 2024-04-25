using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    public class EndPointUpdates:baseResponse
    {
        public List<DataItem> Data { get; set; }
        public Pagination Pagination { get; set; }
    }
   

public class DataItem
    {
        public DateTime AppliedAt { get; set; }
        public string AssetFamilyType { get; set; }
        public string DisplayName { get; set; }
        public string LiveUpdateId { get; set; }
    }


    public class EndPointUpdatesResponse : baseResponse
    {
        public List<DataItem> EndPointUpdateList { get; set; }
       
    }


}
