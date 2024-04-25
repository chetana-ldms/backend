using LDPRuleEngine.DAL.DataContexts;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LDPRuleEngine.DAL.Repositories
{
    public class RuleActionRespository : IRuleActionRespository
    {
        private readonly RuleEngineDataContext? _context;

        public RuleActionRespository(RuleEngineDataContext context)
        {
            _context = context; 
        }
        public async Task<string> AddRuleAction(RuleAction request)
        {
            _context.vm_rule_Actions.Add(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<List<RuleAction>> GetRuleActionByRuleActionID(int RuleActionID)
        {
            var res = await _context.vm_rule_Actions.Where(ruleaction => ruleaction.rule_Action_ID == RuleActionID).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<List<RuleAction>> GetRuleActions()
        {
            try
            {
                var res = await _context.vm_rule_Actions.AsNoTracking().ToListAsync();
                return res;
            }
            catch (Exception ex)
            {
                return new List<RuleAction>();
            }
            
        }

        //public async Task<List<RuleAction>> GetRuleActions(int RuleActionID)
        //{
        //    var res = await _context.vm_rule_Actions.Where(ruleaction => ruleaction.rule_Action_ID == RuleActionID).AsNoTracking().ToListAsync();
        //    return res;
        //}

        public async Task<string> UpdateRuleAction(RuleAction request)
        {
            _context.vm_rule_Actions.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }
    }
}
