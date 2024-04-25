using FluentValidation;
using FluentValidation.Results;
using LDP_APIs.Models;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDPRuleEngine.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Rules Action Configuration")]
    public class RuleActionController : ControllerBase
    {
        IRuleActionBL _bl;

        private IValidator<AddRuleActionRequest> _addRuleactionValidator;
        private IValidator<UpdateRuleActionRequest> _updateRuleactionValidator;
        private IValidator<DeleteRuleActionRequest> _deleteRuleactionValidator;
        public RuleActionController(IRuleActionBL bl
            , IValidator<AddRuleActionRequest> addRuleactionValidator
            , IValidator<UpdateRuleActionRequest> updateRuleactionValidator
            , IValidator<DeleteRuleActionRequest> deleteRuleactionValidator
            )
        {
            _bl = bl;
            _addRuleactionValidator = addRuleactionValidator;
            _updateRuleactionValidator = updateRuleactionValidator;
            _deleteRuleactionValidator = deleteRuleactionValidator;
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("RuleActions")]
        public GetRuleActionResponse GetRuleActions(int orgId)
        {

            return _bl.GetRuleActions(orgId);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("RuleActionDetails")]
        public GetRuleActionSingleResponse GetRuleActionByRuleActionDetails(int id)
        {

            return _bl.GetRuleActionByRuleActionID(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("RuleAction/Add")]
        public RuleActionResponse AddRuleAction(AddRuleActionRequest request)
        {
            baseResponse response = new RuleActionResponse();

            var result = _addRuleactionValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as RuleActionResponse;

            }
            return _bl.AddRuleAction(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("RuleAction/Update")]
        public RuleActionResponse UpdateRuleAction(UpdateRuleActionRequest request)
        {
            baseResponse response = new RuleActionResponse();

            var result = _updateRuleactionValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as RuleActionResponse;

            }
            return _bl.UpdateRuleAction(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("RuleAction/Delete")]
        public RuleActionResponse DeleteRuleAction(DeleteRuleActionRequest request)
        {
            baseResponse response = new RuleActionResponse();

            var result = _deleteRuleactionValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as RuleActionResponse;

            }
            return _bl.DeleteRuleAction(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("RuleAction/Execute")]
        public RuleActionResponse ExecuteRuleAction(ExecuteRuleActionRequest request)
        {
            return _bl.ExecuteRuleAction(request);
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
