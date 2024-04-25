using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.DAL.Interfaces
{
    public interface IRuleActionRespository
    {
        Task<string> AddRuleAction(RuleAction request);
        Task<string> UpdateRuleAction(RuleAction request);
        Task<List<RuleAction>> GetRuleActions();
        Task<List<RuleAction>> GetRuleActionByRuleActionID(int RuleActionID);
    }
}
