using LDP_APIs.Models;
using LDPRuleEngine.BL.Models;

namespace LDPRuleEngine.Controllers.Responses
{
    public class GetRuleEngineMasterDataResponse:baseResponse
    {
        public List<GetRulesEngineMasterDataModel>? MasterDataList { get; set; }
    }

    public class GetRuleEngineMasterDataSingleResponse : baseResponse
    {
        public GetRulesEngineMasterDataModel? MasterData { get; set; }
    }
}
