using AutoMapper;
using LDPRuleEngine.BL.Framework;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;

namespace LDPRuleEngine.BL
{
    public class RulesConfigurationBL : IRulesConfigurationBL
    {

        IRuleEngineRepository _repo;
        private readonly IMapper _mapper;

        public RulesConfigurationBL(IRuleEngineRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }
        public RuleResponse AddRule(AddRuleRequest request)
        {
            RuleResponse response = new RuleResponse();

            var _mappedRuleRequest = _mapper.Map<AddRuleRequest, LDPRule>(request);
            //var _mappedRuleConditonRequest = _mapper.Map<List<AddRuleConditionModel>, List<RuleCondition>>(request.RuleConditions);
            //var _mappedRuleConditonValueRequest = _mapper.Map<List<AddRuleConditionValueModel>, List<RuleConditionValue>>(request.RuleConditions);

            var res = _repo.AddRule(request, _mappedRuleRequest);
            response.IsSuccess = true;
            //if (res.Result == "")
            response.Message = "New rule data added";
            //else
            //{
            //    response.Message = "Failed to add new rule data";
            //}
            return response;
        }

        public GeAlltRuleResponse GetRules()
        {
            GeAlltRuleResponse response = new GeAlltRuleResponse();
            var DBrules = _repo.GetRules();
            var DBruleConditions = _repo.GetRuleConditions();
            var DBruleConditionValues = _repo.GetRuleConditionsValues();
            List<GetLDPRuleModel> rulesmodel = null;
            List<GetRuleConditionModel> conditionsmodel = null;
            List<GetRuleConditionValueModel> conditionValuesmodel = null;

            rulesmodel = _mapper.Map<List<LDPRule>, List<GetLDPRuleModel>>(DBrules.Result);

            //Loop all DB rule entites 
            foreach (var rulemodel in rulesmodel)
            {
                var DBruleConditonsForeachRule = DBruleConditions.Result.Where(rulecnd => rulecnd.Rule_ID == rulemodel.RuleID);

                var ruleConditionForeachRule = _mapper.Map<IEnumerable<RuleCondition>, List<GetRuleConditionModel>>(DBruleConditonsForeachRule);


                foreach (var ruleConditionModel in ruleConditionForeachRule)
                {
                    var ruleConditionValuesForEachRuleCondtions = DBruleConditionValues.Result
                        .Where(rulecondVal => rulecondVal.Rules_Condition_ID == ruleConditionModel.RulesConditionID).ToList();

                    var ruleConditionValueModel = _mapper.Map<List<RuleConditionValue>, List<GetRuleConditionValueModel>>(ruleConditionValuesForEachRuleCondtions.ToList());

                    ruleConditionModel.RuleConditionValues = ruleConditionValueModel;

                }
                rulemodel.RuleConditions = ruleConditionForeachRule;

            }

            response.RulesList = rulesmodel;
            response.IsSuccess = true;
            //if (res == null)
            //response.Message = "rules data not found";
            //else
            //{
            response.Message = "Success";

            //   var _mappedResponse = _mapper.Map<List<Organization>, List<GettOrganizationsModel>>(res.Result);
            // response.OrganizationList = _mappedResponse.ToList();
            //}
            return response;
        }

        public RuleResponse UpdateRule(UpdateRuleRequest request)
        {
            RuleResponse response = new RuleResponse();
            // var _mappedRequest = _mapper.Map<UpdateOrganizationModel, Organization>(request);

            //var res = _repo.UpdateRule(_mappedRequest);
            response.IsSuccess = true;
            //if (res.Result == "")
            response.Message = "New rule data added";
            //else
            //{
            //    response.Message = "Failed to add new rule data";
            //}
            return response;
        }

        public ExecuteRuleResponse ExecuteRule(ExecuteRuleRequest request)
        {
            ExecuteRuleResponse ruleResponse = new ExecuteRuleResponse();
            //ruleResponse.RuleExecuterMessage = new System.Text.StringBuilder();
            IRuleConditionTypeExecuter? _ruleConditionTypeExecuter;
            ExecuteRuleResponse response = new ExecuteRuleResponse();

            //1. GEt the Rule data from DB

            var ruledata = GetRuleByRuleID(request.RuleID);

            //2. loop through the rule conditions 

            ExecuteRuleConditionTypeRequest? ruleConditionTypeRequest = null;
            foreach (var getrulecondition in ruledata.RuleData.RuleConditions)
            {

                _ruleConditionTypeExecuter = RuleConditionTypeFactory.GetInstance((Constants.RuleConditionTypes)getrulecondition.RulesConditionTypeID);
                if (_ruleConditionTypeExecuter == null)
                {
                    ruleResponse.IsSuccess = false;
                    // ruleResponse.RuleExecuterMessage.Append("Rule condition executer object not fount".ToString()).ToString();
                }
                else
                {

                    ruleConditionTypeRequest = new ExecuteRuleConditionTypeRequest();
                    ruleConditionTypeRequest.InputDataToValidate = request.InputTextToRuleExecute;

                    var ruleconditonvaluesList = getrulecondition.RuleConditionValues
                         .Select(ruleConditionsValues => new { ruleConditionsValues.RulesConditionValue });

                    ruleConditionTypeRequest.DataToPerformValidationOnInputData = ruleconditonvaluesList;
                    //
                    var RuleConditionexecuterResult = _ruleConditionTypeExecuter.ExecuteRuleConditionType(ruleConditionTypeRequest);
                    ruleResponse.IsSuccess = RuleConditionexecuterResult.IsSuccess;
                    ruleResponse.RuleExecuterMessage = RuleConditionexecuterResult.RuleConditionExecutionMessages.ToString();
                }
            };
            //3. Execute the rule condition executer 


            return ruleResponse;
        }

        public GetRuleResponse GetRuleByRuleID(int ruleID)
        {
            GetRuleResponse rule = new GetRuleResponse();

            var DBRule = _repo.GetRuleData(ruleID);
            var DBRuleConditions = _repo.GetRuleConditionsByRuleID(ruleID);
            var DBRuleConditionsValues = _repo.GetRuleConditionValuesByRuleID(ruleID);
            var _RuleMapperResponse = _mapper.Map<LDPRule, GetLDPRuleModel>(DBRule.Result);
            var _RuleConditionsMapperResponse = _mapper.Map<List<RuleCondition>, List<GetRuleConditionModel>>(DBRuleConditions.Result);
            _RuleMapperResponse.RuleConditions = _RuleConditionsMapperResponse;

            foreach (var ruleConditionModel in _RuleMapperResponse.RuleConditions)
            {
                var ruleConditionValuesForEachRuleCondtions = DBRuleConditionsValues.Result
                    .Where(rulecondVal => rulecondVal.Rules_Condition_ID == ruleConditionModel.RulesConditionID).ToList();

                var ruleConditionValueModel = _mapper.Map<List<RuleConditionValue>, List<GetRuleConditionValueModel>>(ruleConditionValuesForEachRuleCondtions.ToList());
                ruleConditionModel.RuleConditionValues = ruleConditionValueModel;

            }
            rule.RuleData = _RuleMapperResponse;


            return rule;
        }

        #region Master data
        
        public GetRuleEngineMasterDataResponse GetRuleEngineMasterData(string masterdataType)
        {
            GetRuleEngineMasterDataResponse response = new GetRuleEngineMasterDataResponse();
            var res = _repo.GetRuleEngineMasterDatas(masterdataType);
            response.IsSuccess = true;
            if (res == null)
                response.Message = "Rule engine master data not found";
            else
            {
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<List<RulesEngineMasterData>, List<GetRulesEngineMasterDataModel>>(res.Result).ToList();
                response.MasterDataList = _mappedResponse;
            }
            return response;
        }
        #endregion
    }
}