//using LDPRuleEngine.BL.Framework.Actions;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.Controllers.Requests;


namespace LDPRuleEngine.BL.Interfaces
{
    public interface IRuleActionBL
    {
        RuleActionResponse AddRuleAction(AddRuleActionRequest request);
        RuleActionResponse UpdateRuleAction(UpdateRuleActionRequest request);

        RuleActionResponse DeleteRuleAction(DeleteRuleActionRequest request);
        GetRuleActionResponse GetRuleActions();

        GetRuleActionResponse GetRuleActions(int orgId);
        RuleActionResponse ExecuteRuleAction(ExecuteRuleActionRequest request);

        GetRuleActionSingleResponse GetRuleActionByRuleActionID(int RuleActionID);
    }
}
