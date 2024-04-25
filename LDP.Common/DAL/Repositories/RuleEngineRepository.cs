using AutoMapper;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.DAL.DataContexts;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LDPRuleEngine.DAL.Repositories
{
    public class RuleEngineRepository : IRuleEngineRepository
    {
        private readonly RuleEngineDataContext? _context;
        private readonly IMapper? _mapper;
        public RuleEngineRepository(RuleEngineDataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region rules 
        public async Task<string> AddRule(AddRuleRequest request , LDPRule _ruleEntity)
        {
            using (IDbContextTransaction? transaction = _context?.Database.BeginTransaction())
            {
                try
                {
                    // Inserting Rules
                    _context.vm_Rules.Add(_ruleEntity);
                    await _context.SaveChangesAsync();

                    RuleCondition _ruleConditonEntity = null;
                    RuleConditionValue _ruleConditionValueEntity = null;

                    //Inserting Rule condition related above
                    foreach (var _ruleconditionrequest in request.RuleConditions)
                    {
                        _ruleConditonEntity = new RuleCondition();
                        _ruleConditonEntity.Rules_Condition_Type_ID = _ruleconditionrequest.RulesConditionTypeID;
                        _ruleConditonEntity.Rule_ID = _ruleEntity.Rule_ID;
                        _ruleConditonEntity.Created_Date = _ruleEntity.Created_Date;
                        _ruleConditonEntity.Created_User = _ruleEntity.Created_User;
                        _ruleConditonEntity.Processed = 0;
                        _ruleConditonEntity.active = 1;
                        //var _mappedRuleRequest = _mapper.Map<AddRuleConditionModel, RuleCondition>(_ruleconditionrequest);
                        _context.vm_Rule_Conditions.Add(_ruleConditonEntity);
                        await _context.SaveChangesAsync();

                        //Inserting Values related to conditions 
                        foreach (var _ruleConditionValueRequest in _ruleconditionrequest.RuleConditionValues)
                        {
                            _ruleConditionValueEntity = new RuleConditionValue();
                            _ruleConditionValueEntity.Rules_Condition_ID = _ruleConditonEntity.Rules_Condition_ID;
                            _ruleConditionValueEntity.Rules_Condition_Value = _ruleConditionValueRequest.RulesConditionValue;
                            _ruleConditionValueEntity.Created_Date = request.CreatedDate;
                            _ruleConditionValueEntity.Created_User = _ruleEntity.Created_User;
                            _ruleConditionValueEntity.Processed = 0;
                            _ruleConditionValueEntity.active = 1;
                            _context.vm_Rules_Condition_Valuess.Add(_ruleConditionValueEntity);
                        }

                    }
                    _context.SaveChangesAsync();
                    transaction.Commit();

                }
                catch (Exception ex )
                {
                    transaction.Rollback();
                    throw ;
                }
            }
            return "success";
        }
        public async Task<LDPRule> GetRuleData(int ruleID)
        {
            return await _context.vm_Rules.Where(rule => rule.Rule_ID == ruleID).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<LDPRule> GetRuleByName(string name)
        {
            var res = await _context.vm_Rules.Where(rl => rl.Rule_Name.ToLower() == name.ToLower()).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }

        public async Task<LDPRule> GetRuleByNameOnUpdate(string name, int id)
        {
            var res = await _context.vm_Rules
                .Where(rl => rl.Rule_Name.ToLower() == name.ToLower() && rl.Rule_ID != id)
                .AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        public async Task<string> UpdateRule(UpdateRuleRequest request, LDPRule _ruleEntity)
        {
            var ruledata = await _context.vm_Rules.Where(rule => rule.Rule_ID == _ruleEntity.Rule_ID
                          ).AsNoTracking().FirstOrDefaultAsync();
            if (ruledata == null)
            {
                return " Rule details not found for update the rule data";
            }
            _ruleEntity.Created_Date = ruledata.Created_Date;
            _ruleEntity.Created_User = ruledata.Created_User;
            _ruleEntity.active = ruledata.active;
            _ruleEntity.deleted_date = ruledata.deleted_date;
            _ruleEntity.deleted_user = ruledata.deleted_user;
            _context.vm_Rules.Update(_ruleEntity);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the rule data";
        }
        public async Task<string> DeleteRule(DeleteRuleRequest request,string deletedUser)
        {
            var ttaction = await _context.vm_Rules.Where(rule => rule.Rule_ID == request.RuleID
                          ).AsNoTracking().FirstOrDefaultAsync();
            if (ttaction == null)
            {
                return " Rule details not found for delete the rule";
            }

            ttaction.active = 0;
            ttaction.deleted_date = request.DeletedDate;
            ttaction.deleted_user = deletedUser;
            ttaction.Modified_Date = request.DeletedDate;
            ttaction.Modified_User = deletedUser;

            _context.vm_Rules.Update(ttaction);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed to delete the rule";

        }
        public async Task<List<LDPRule>> GetRules(int orgId)
        {
            var res = await _context.vm_Rules.Where(rl => rl.active == 1 && rl.org_id == orgId).AsNoTracking().ToListAsync();
            return res;
        }

        #endregion


        #region rule conditions 
        public async Task<string> AddRuleCondition(RuleCondition request)
        {
            _context.vm_Rule_Conditions.Add(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<string> UpdateRuleCondition(RuleCondition request)
        {
            _context.vm_Rule_Conditions.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }
        public async Task<List<RuleCondition>> GetRuleConditions()
        {
            var res = await _context.vm_Rule_Conditions.AsNoTracking().ToListAsync();
            return res;
        }
        public async Task<List<RuleCondition>> GetRuleConditions(int orgId)
        {
            var res = await (
                from condition in _context.vm_Rule_Conditions
                join rule in _context.vm_Rules on condition.Rule_ID equals rule.Rule_ID
                where rule.org_id == orgId
                select condition
            ).AsNoTracking().ToListAsync();

            return res;
        }

        public async Task<List<RuleCondition>> GetRuleConditionsByRuleID(int RuleID)
        {
            return await _context.vm_Rule_Conditions.Where(rulecnd => rulecnd.Rule_ID == RuleID).AsNoTracking().ToListAsync();  
        }

        #endregion

        #region rule condition value 
        public async Task<string> AddRuleConditionValue(RuleConditionValue request)
        {
            _context.vm_Rules_Condition_Valuess.Add(request);
            await _context.SaveChangesAsync();
            return "success";
        }
        public async Task<string> UpdateRuleConditionValue(RuleConditionValue request)
        {
            _context.vm_Rules_Condition_Valuess.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }
        public async Task<List<RuleConditionValue>> GetRuleConditionsValues()
        {
            var res = await _context.vm_Rules_Condition_Valuess.AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<List<RuleConditionValue>> GetRuleConditionsValues(int orgId)
        {
                var res = await (

                    from conditionvalues in _context.vm_Rules_Condition_Valuess
                    join condition in _context.vm_Rule_Conditions on conditionvalues.Rules_Condition_ID equals condition.Rules_Condition_ID
                    join rule in _context.vm_Rules on condition.Rule_ID equals rule.Rule_ID
                    where rule.org_id == orgId
                    select conditionvalues
                ).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<List<RuleConditionValue>> GetRuleConditionValuesByRuleID(int RuleID)
        {

            var ruleConditionValues = await (from rulecond in _context.vm_Rule_Conditions
                                             join rulecondValues in _context.vm_Rules_Condition_Valuess
                                             on rulecond.Rules_Condition_ID equals rulecondValues.Rules_Condition_ID
                                             where rulecond.Rule_ID == RuleID
                                             select rulecondValues).AsNoTracking().ToListAsync();
  
            return  ruleConditionValues;
                          

        }
        #endregion


        #region Master data 
        public async Task<string> AddRuleEngineMasterData(RulesEngineMasterData request)
        {
            _context.vm_RulesEngine_Masterdata.AddRange(request);
            await _context.SaveChangesAsync();
            return "success";
        }


        public async Task<string> UpdateRuleEngineMasterData(RulesEngineMasterData request)
        {
            _context.vm_RulesEngine_Masterdata.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }

        //public async Task<List<RulesEngineMasterData>> GetRuleEngineMasterDatas()
        //{
        //    var res = await _context.vm_RulesEngine_Masterdata.AsNoTracking().ToListAsync();
        //    return res;
        //}

        public async Task<List<RulesEngineMasterData>> GetRuleEngineMasterDatas(string request)
        {
            var res = await _context.vm_RulesEngine_Masterdata.Where(masterdata => masterdata.master_data_type.ToLower()==request.ToLower()).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<RulesEngineMasterData> GetRuleEngineMasterDataByID(int id)
        {
            var res = await _context.vm_RulesEngine_Masterdata.Where(masterdata => masterdata.master_data_id==id).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }







        #endregion

    }
}
