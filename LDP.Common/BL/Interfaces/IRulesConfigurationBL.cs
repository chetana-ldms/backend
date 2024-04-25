using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;

namespace LDPRuleEngine.BL
{
    public interface IRulesConfigurationBL
    {
        RuleResponse AddRule(AddRuleRequest request);
        RuleResponse UpdateRule(UpdateRuleRequest request);

        RuleResponse DeleteRule(DeleteRuleRequest request);
        GeAlltRuleResponse GetRules(int orgId);

        GetRuleResponse GetRuleByRuleID(int ruleID);
        ExecuteRuleResponse ExecuteRule(ExecuteRuleRequest request);

        //RuleResponse AddMasterData(string masterdataType);
        //RuleResponse UpdateRule(UpdateRuleRequest request);

        GetRuleEngineMasterDataResponse GetRuleEngineMasterData(string masterdataType);

        GetRuleEngineMasterDataSingleResponse GetRuleEngineMasterDataByID(int id);


    }
}
