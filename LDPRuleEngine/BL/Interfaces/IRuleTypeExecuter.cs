using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;

namespace LDPRuleEngine.BL.Interfaces
{
    public interface IRuleConditionTypeExecuter
    {
        ExecuteRuleConditionTypeResponse ExecuteRuleConditionType(ExecuteRuleConditionTypeRequest request);
    }
}
