using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.DAL.Interfaces
{
    public interface IRuleEngineRepository
    {
        Task<string> AddRuleCondition(RuleCondition request);
        Task<string> UpdateRuleCondition(RuleCondition request);
        Task<List<RuleCondition>> GetRuleConditions();

        Task<List<RuleCondition>> GetRuleConditions(int orgId);
        Task<List<RuleCondition>> GetRuleConditionsByRuleID(int RuleID);

        Task<string> AddRuleConditionValue(RuleConditionValue request);
        Task<string> UpdateRuleConditionValue(RuleConditionValue request);


        Task<List<RuleConditionValue>> GetRuleConditionsValues();

        Task<List<RuleConditionValue>> GetRuleConditionsValues(int orgId);

        Task<List<RuleConditionValue>> GetRuleConditionValuesByRuleID(int RuleID);

        Task<string> AddRule(AddRuleRequest request, LDPRule _ruleEntity);

        Task<string> UpdateRule(UpdateRuleRequest request, LDPRule _ruleEntity);

        Task<string> DeleteRule(DeleteRuleRequest request,string deletedUser);

        Task<List<LDPRule>> GetRules(int orgId);

        Task<LDPRule> GetRuleData(int ruleID);

        Task<LDPRule> GetRuleByName(string name);

        Task<LDPRule> GetRuleByNameOnUpdate(string name, int id);

        Task<string> AddRuleEngineMasterData(RulesEngineMasterData request);
        Task<string> UpdateRuleEngineMasterData(RulesEngineMasterData request);
        Task<List<RulesEngineMasterData>> GetRuleEngineMasterDatas(string request);

        Task<RulesEngineMasterData> GetRuleEngineMasterDataByID(int  id);


    }
}
