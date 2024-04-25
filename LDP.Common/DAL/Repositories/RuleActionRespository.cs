using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.DAL.DataContexts;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;

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

        public async Task<string> DeleteRuleAction(DeleteRuleActionRequest request,string deletedUser)
        {
            var ruleaction = await _context.vm_rule_Actions.Where(rc => rc.rule_Action_ID == request.RuleActionID
         ).AsNoTracking().FirstOrDefaultAsync();
            if (ruleaction == null)
            {
                return "rule action details not found";
            }

            ruleaction.active = 0;
            ruleaction.deleted_date = request.DeletedDate;
            ruleaction.deleted_user = deletedUser;
            ruleaction.Modified_date = request.DeletedDate;
            ruleaction.Modified_user = deletedUser;

            _context.vm_rule_Actions.Update(ruleaction);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the rule action";

        }

        public async Task<RuleAction> GetRuleActionByName(string name)
        {
            var res = await _context.vm_rule_Actions.Where(ra => ra.rule_Action_Name.ToLower() == name.ToLower()).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }

        public async Task<RuleAction> GetRuleActionByNameOnUpdate(string name, int id)
        {
            var res = await _context.vm_rule_Actions
                .Where(ra => ra.rule_Action_Name.ToLower() == name.ToLower()
                && ra.rule_Action_ID != id ).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }

        public async Task<RuleAction> GetRuleActionByRuleActionID(int RuleActionID)
        {
            var res = await _context.vm_rule_Actions.Where(ruleaction => ruleaction.rule_Action_ID == RuleActionID)
                .AsNoTracking().FirstOrDefaultAsync();
            return res;
        }

        public async Task<List<RuleAction>> GetRuleActions()
        {
            
            var res = await _context.vm_rule_Actions.Where(ra => ra.active == 1 ).AsNoTracking().ToListAsync();
            
            return res;
        }

        public async Task<List<RuleAction>> GetRuleActions(int orgId)
        {
            var res = await _context.vm_rule_Actions.Where(ra => ra.active == 1 && ra.org_id == orgId).AsNoTracking().ToListAsync();
            return res;
        }
        public async Task<List<RuleAction>> GetRuleActionByID(int RuleActionID)
        {
            var res = await _context.vm_rule_Actions.Where(ruleaction => ruleaction.rule_Action_ID == RuleActionID).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<string> UpdateRuleAction(RuleAction request)
        {
            var ruleaction = await _context.vm_rule_Actions.Where(rc => rc.rule_Action_ID == request.Tool_Action_ID
         ).AsNoTracking().FirstOrDefaultAsync();
            if (ruleaction == null)
            {
                return "Rule action details not  found for update the rule action data";
            }
            request.active = ruleaction.active;
            request.Created_user = ruleaction.Created_user;
            request.Created_date = ruleaction.Created_date;
            request.deleted_date = ruleaction.deleted_date;
            request.deleted_user = ruleaction.deleted_user;


            _context.vm_rule_Actions.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the rule action data";
        }
    }
}
