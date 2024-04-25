using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;

namespace LDPRuleEngine.BL.Interfaces
{
    public interface IRuleExecuter
    {
        ExecuteRuleResponse ExecuteeRule(ExecuteRuleRequest request);
    }
}
