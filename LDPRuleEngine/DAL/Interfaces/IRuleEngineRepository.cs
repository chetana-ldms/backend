using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.DAL.Interfaces
{
    public interface IRuleEngineRepository
    {
        Task<string> AddRuleCondition(RuleCondition request);
        Task<string> UpdateRuleCondition(RuleCondition request);
        Task<List<RuleCondition>> GetRuleConditions();
        Task<List<RuleCondition>> GetRuleConditionsByRuleID(int RuleID);

        Task<string> AddRuleConditionValue(RuleConditionValue request);
        Task<string> UpdateRuleConditionValue(RuleConditionValue request);
        Task<List<RuleConditionValue>> GetRuleConditionsValues();
        Task<List<RuleConditionValue>> GetRuleConditionValuesByRuleID(int RuleID);

        Task<string> AddRule(AddRuleRequest request, LDPRule _ruleEntity);
        Task<string> UpdateRule(UpdateRuleRequest request, LDPRule _ruleEntity);
        Task<List<LDPRule>> GetRules();

        Task<LDPRule> GetRuleData(int ruleID);

        Task<string> AddRuleEngineMasterData(RulesEngineMasterData request);
        Task<string> UpdateRuleEngineMasterData(RulesEngineMasterData request);
        Task<List<RulesEngineMasterData>> GetRuleEngineMasterDatas(string request);



    }
}
