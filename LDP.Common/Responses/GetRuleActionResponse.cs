using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.BL.Framework.Actions;
using LDP_APIs.Models;

namespace LDPRuleEngine.Controllers.Responses
{
    public class GetRuleActionResponse:baseResponse
    {
        public List<GetRuleActionModel>? RuleActionList { get; set; }
    }

    public class GetRuleActionSingleResponse : baseResponse
    {
        public GetRuleActionModel? RuleActionData { get; set; }
    }
    public class RuleActionResponse : baseResponse
    {
    }
}
