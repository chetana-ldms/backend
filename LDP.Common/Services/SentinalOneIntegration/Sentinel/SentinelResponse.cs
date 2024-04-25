using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class ExclustionsResponse :baseResponse
    {
        public List<ExclusionData>? ExclusionList { get ; set; }    
    }

    public class BlockListResponse : baseResponse
    {
        public List<BlockListData>? BlockedItemList { get; set; }
    }

}
