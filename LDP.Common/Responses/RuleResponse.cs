using LDP_APIs.Models;
using LDPRuleEngine.BL.Models;

namespace LDPRuleEngine.Controllers.Responses
{
    public class GeAlltRuleResponse:baseResponse
    {
        public List<GetLDPRuleModel>? RulesList { get; set; }
    }

    public class RuleResponse : baseResponse
    {
       
    }

    public class GetRuleResponse : baseResponse
    {
        public GetLDPRuleModel RuleData { get; set; }
    }
}
