//using LDPRuleEngine.BL.Framework.Actions;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.Controllers.Requests;


namespace LDPRuleEngine.BL.Interfaces
{
    public interface IRuleActionBL
    {
        RuleActionResponse AddRuleAction(AddRuleActionRequest request);
        RuleActionResponse UpdateRuleAction(UpdateRuleActionRequest request);
        GetRuleActionResponse GetRuleActions();
        RuleActionResponse ExecuteRuleAction(ExecuteRuleActionRequest request);

        GetRuleActionResponse GetRuleActionByRuleActionID(int RuleActionID);
    }
}
