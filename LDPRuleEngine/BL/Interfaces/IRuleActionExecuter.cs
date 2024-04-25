using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;

namespace LDPRuleEngine.BL.Interfaces
{
    public interface IRuleActionExecuter
    {
        ExecuteRuleResponse ExecuteeRuleAction(ExecuteRuleRequest request);
    }
}
