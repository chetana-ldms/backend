using AutoMapper;
using LDP_APIs.BL.Interfaces;
using LDPRuleEngine.BL.Framework;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;
using System.Net;

namespace LDPRuleEngine.BL
{
    public class RulesConfigurationBL : IRulesConfigurationBL
    {

        IRuleEngineRepository _repo;
        private readonly IMapper _mapper;
        ILDPSecurityBL _securityBl;
        ILDPlattformBL _plattformBL;
        public RulesConfigurationBL(IRuleEngineRepository repo, IMapper mapper, ILDPSecurityBL securityBl, ILDPlattformBL plattformBL)
        {
            _repo = repo;
            _mapper = mapper;
            _securityBl = securityBl;
            _plattformBL = plattformBL;
        }
        public RuleResponse AddRule(AddRuleRequest request)
        {
            RuleResponse response = new RuleResponse();

            var rule = _repo.GetRuleByName(request.RuleName);
            if (rule.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "Rule name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _mappedRuleRequest = _mapper.Map<AddRuleRequest, LDPRule>(request);
            _mappedRuleRequest.Created_User = userdata.Userdata.Name;

            var res = _repo.AddRule(request, _mappedRuleRequest);
            response.IsSuccess = true;
            //if (res.Result == "")
            response.Message = "New rule data added";
            response.HttpStatusCode = HttpStatusCode.OK;
            //else
            //{
            //    response.Message = "Failed to add new rule data";
            //}
            return response;
        }

        public GeAlltRuleResponse GetRules(int orgId)
        {
            GeAlltRuleResponse response = new GeAlltRuleResponse();
            var DBrules = _repo.GetRules(orgId);
            if (DBrules.Result.Count ==0 )
            {
                response.IsSuccess = false;
                response.Message = "Rules data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            var DBruleConditions = _repo.GetRuleConditions(orgId);
            var DBruleConditionValues = _repo.GetRuleConditionsValues(orgId);
            List<GetLDPRuleModel> rulesmodel = null;
            List<GetRuleConditionModel> conditionsmodel = null;
            List<GetRuleConditionValueModel> conditionValuesmodel = null;

            rulesmodel = _mapper.Map<List<LDPRule>, List<GetLDPRuleModel>>(DBrules.Result);

            var cats = _repo.GetRuleEngineMasterDatas("Rule_Catagories");
            // Get Organizations
            var _orgList = _plattformBL.GetOrganizationList();

            //Loop all DB rule entites 
            foreach (var rulemodel in rulesmodel)
            {
                // setting the catagory name
               // var cat = _repo.GetRuleEngineMasterDataByID(rulemodel.RuleCatagoryID);
                if (cats != null)
                {
                    rulemodel.RuleCatagoryName = cats.Result.Where(c => c.master_data_id == rulemodel.RuleCatagoryID).SingleOrDefault().master_data_value;
                }
                if (_orgList.OrganizationList != null)
                {
                    rulemodel.OrgName = _orgList.OrganizationList.Where(o => o.OrgID == rulemodel.OrgId).FirstOrDefault().OrgName;
                }
                var DBruleConditonsForeachRule = DBruleConditions.Result.Where(rulecnd => rulecnd.Rule_ID == rulemodel.RuleID);

                var ruleConditionForeachRule = _mapper.Map<IEnumerable<RuleCondition>, List<GetRuleConditionModel>>(DBruleConditonsForeachRule);


                foreach (var ruleConditionModel in ruleConditionForeachRule)
                {
                    //setting the Rule condition type name 
                    var ruleconditionType = _repo.GetRuleEngineMasterDataByID(ruleConditionModel.RulesConditionTypeID);
                    if (ruleconditionType.Result != null)
                    {
                        ruleConditionModel.RulesConditionTypeName = ruleconditionType.Result.master_data_value;
                    }
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
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var rule = _repo.GetRuleByNameOnUpdate(request.RuleName,request.RuleID);
            if (rule.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "Rule name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }
             var _mappedRequest = _mapper.Map<UpdateRuleRequest, LDPRule>(request);

            var res = _repo.UpdateRule(request,_mappedRequest);
            response.IsSuccess = true;
            //if (res.Result == "")
            response.Message = "Rule data updated";
            response.HttpStatusCode = HttpStatusCode.OK;
            //else
            //{
            //    response.Message = "Failed to add new rule data";
            //}
            return response;
        }
        public RuleResponse DeleteRule(DeleteRuleRequest request)
        {
            RuleResponse response = new RuleResponse();
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.DeleteRule(request,userdata.Userdata.Name);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Rule deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
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
            GetRuleResponse response = new GetRuleResponse();

            var DBRule = _repo.GetRuleData(ruleID);
            if (DBRule.Result==null)
            {
                response.IsSuccess = false;
                response.Message = "Rule data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;

            }
            var DBRuleConditions = _repo.GetRuleConditionsByRuleID(ruleID);

            var DBRuleConditionsValues = _repo.GetRuleConditionValuesByRuleID(ruleID);
            var _RuleMapperResponse = _mapper.Map<LDPRule, GetLDPRuleModel>(DBRule.Result);
            //setting catagory name 
            var cat = _repo.GetRuleEngineMasterDataByID(_RuleMapperResponse.RuleCatagoryID);
            if (cat != null)
            {
                _RuleMapperResponse.RuleCatagoryName = cat.Result.master_data_value;
            }

            // Set the Organization name 

            _RuleMapperResponse.OrgName = _plattformBL.GetOrganizationByID(DBRule.Result.org_id).OrganizationData.OrgName; ;

            var _RuleConditionsMapperResponse = _mapper.Map<List<RuleCondition>, List<GetRuleConditionModel>>(DBRuleConditions.Result);
            _RuleMapperResponse.RuleConditions = _RuleConditionsMapperResponse;

            foreach (var ruleConditionModel in _RuleMapperResponse.RuleConditions)
            {
                // setting the rule condition type name 
                var ruleconditiontype = _repo.GetRuleEngineMasterDataByID(ruleConditionModel.RulesConditionTypeID);
                if (cat != null)
                {
                    ruleConditionModel.RulesConditionTypeName = ruleconditiontype.Result.master_data_value;
                }
                var ruleConditionValuesForEachRuleCondtions = DBRuleConditionsValues.Result
                    .Where(rulecondVal => rulecondVal.Rules_Condition_ID == ruleConditionModel.RulesConditionID).ToList();

                var ruleConditionValueModel = _mapper.Map<List<RuleConditionValue>, List<GetRuleConditionValueModel>>(ruleConditionValuesForEachRuleCondtions.ToList());
                ruleConditionModel.RuleConditionValues = ruleConditionValueModel;

            }
            response.IsSuccess = true;
            response.HttpStatusCode = HttpStatusCode.OK;
            response.RuleData = _RuleMapperResponse;


            return response;
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
        public  GetRuleEngineMasterDataSingleResponse GetRuleEngineMasterDataByID(int id)
        {
            GetRuleEngineMasterDataSingleResponse response = new GetRuleEngineMasterDataSingleResponse();
            var res = _repo.GetRuleEngineMasterDataByID(id);
            response.IsSuccess = true;
            if (res == null)
                response.Message = "Rule engine master data not found";
            else
            {
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<RulesEngineMasterData, GetRulesEngineMasterDataModel>(res.Result);
                     response.MasterData = _mappedResponse;
            }
            return response;
        }
        #endregion
    }
}