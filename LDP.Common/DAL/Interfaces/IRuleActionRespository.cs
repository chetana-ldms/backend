using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.DAL.Interfaces
{
    public interface IRuleActionRespository
    {
        Task<string> AddRuleAction(RuleAction request);
        Task<string> UpdateRuleAction(RuleAction request);

        Task<RuleAction> GetRuleActionByName(string name);

        Task<RuleAction> GetRuleActionByNameOnUpdate(string name, int id);

        Task<string> DeleteRuleAction(DeleteRuleActionRequest request,string deletedUser);
        Task<List<RuleAction>> GetRuleActions();
         
        Task<List<RuleAction>> GetRuleActions(int orgId);
        Task<RuleAction> GetRuleActionByRuleActionID(int RuleActionID);
    }
}
