using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;

namespace LDPRuleEngine.BL.Framework
{
    public class EqualsRuleTypeExecuter : IRuleConditionTypeExecuter
    {
        public ExecuteRuleConditionTypeResponse ExecuteRuleConditionType(ExecuteRuleConditionTypeRequest request)
        {
            ExecuteRuleConditionTypeResponse response = new ExecuteRuleConditionTypeResponse();
            response.IsSuccess = false;
            response.RuleConditionExecutionMessages.AppendLine("EqualsRuleTypeExecuterr logic not implemented");
            return response;
        }
    }
}
