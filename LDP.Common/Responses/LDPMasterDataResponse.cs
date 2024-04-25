using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class LDPMasterDataResponse:baseResponse 
    {
        public List<LDPMasterDataModel>? MasterData { get; set; }
    }

    public class LDPMasterDataByIDResponse : baseResponse
    {
        public LDPMasterDataModel? MasterData { get; set; }
    }
}
