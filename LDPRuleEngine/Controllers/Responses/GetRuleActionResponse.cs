using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.BL.Framework.Actions;

namespace LDPRuleEngine.Controllers.Responses
{
    public class GetRuleActionResponse:BaseRuleActionResponse
    {
        public List<GetRuleActionModel>? RuleActionList { get; set; }
    }

    public class RuleActionResponse : BaseRuleActionResponse
    {
    }
}
