using FluentValidation;
using FluentValidation.Results;
using LDP_APIs.Models;
using LDPRuleEngine.BL;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDPRuleEngine.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Rules Configuration")]
    public class RulesConfiguratonController : ControllerBase
    {
        IRulesConfigurationBL _bl;

        private IValidator<AddRuleRequest> _addRuleValidator;
        private IValidator<UpdateRuleRequest> _updateRuleValidator;
        private IValidator<DeleteRuleRequest> _deleteRuleValidator;
        public RulesConfiguratonController(IRulesConfigurationBL bl
            , IValidator<AddRuleRequest> addRuleValidator
            , IValidator<UpdateRuleRequest> updateRuleValidator
            , IValidator<DeleteRuleRequest> deleteRuleValidator
            )
        {
            _bl = bl;
            _addRuleValidator = addRuleValidator;
            _updateRuleValidator = updateRuleValidator;
            _deleteRuleValidator = deleteRuleValidator;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Rules")]
        public GeAlltRuleResponse GetRules(int orgId)
        {
            return _bl.GetRules(orgId);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Rules/Add")]
        public RuleResponse AddRule(AddRuleRequest request)
        {
            baseResponse response = new RuleResponse();

            var result = _addRuleValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as RuleResponse;

            }
            return _bl.AddRule(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Rules/Update")]
        public RuleResponse UpdateRule(UpdateRuleRequest request)
        {
            baseResponse response = new RuleResponse();

            var result = _updateRuleValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as RuleResponse;

            }
            return _bl.UpdateRule(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Rules/Delete")]
        public RuleResponse DeleteRule(DeleteRuleRequest request)
        {
            baseResponse response = new RuleResponse();

            var result = _deleteRuleValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as RuleResponse;

            }
            return _bl.DeleteRule(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Rule/Execute")]
        public ExecuteRuleResponse ExecuteeRule(ExecuteRuleRequest request)
        {
            return _bl.ExecuteRule(request);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Rules/RuleDetails")]
        public GetRuleResponse GetRuleDetails(int ruleID)
        {
            return _bl.GetRuleByRuleID(ruleID);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("RuleEngine/MasterData")]
        public GetRuleEngineMasterDataResponse GetRuleEngineMasterData(string  MasterDataType)
        {
            return _bl.GetRuleEngineMasterData(MasterDataType);
        }

        private void BuildValiationMessage(ValidationResult result, ref baseResponse validationresponse)
        {
            validationresponse.IsSuccess = false;
            validationresponse.Message = "Validation Error";
            validationresponse.HttpStatusCode = HttpStatusCode.BadRequest;
            validationresponse.errors = result.Errors.Select(e => e.ErrorMessage.ToString()).ToList();

        }
    }
}
