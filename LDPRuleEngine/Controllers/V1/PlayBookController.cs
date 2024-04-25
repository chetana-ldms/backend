using FluentValidation;
using FluentValidation.Results;
using LDP_APIs.Models;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDPRuleEngine.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Play Book")]
    public class PlayBookController : ControllerBase
    {
        IPlaybookBL _bl;

        private IValidator<AddPlayBookRequest> _addPlaybookValidator;
        private IValidator<UpdatePlayBookRequest> _updatePlaybookValidator;
        private IValidator<DeletePlayBookRequest> _deletePlaybookValidator;
        public PlayBookController(IPlaybookBL bl
                , IValidator<AddPlayBookRequest> addPlaybookValidator
                , IValidator<UpdatePlayBookRequest> updatePlaybookValidator
                , IValidator<DeletePlayBookRequest> deletePlaybookValidator
            
            )
        {
            _bl = bl;
            _addPlaybookValidator = addPlaybookValidator;
            _updatePlaybookValidator = updatePlaybookValidator;
            _deletePlaybookValidator = deletePlaybookValidator;
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("PlayBooks")]
        public GetPlayBookResponse GetPlayBooks(int orgId)
        {
            return _bl.GetPlayBooks(orgId);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Playbook/Add")]
        public PlayBookResponse AddPlayBook(AddPlayBookRequest request)
        {
            baseResponse response = new PlayBookResponse();

            var result = _addPlaybookValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as PlayBookResponse;

            }
            return _bl.AddPlayBook(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Playbook/Update")]
        public PlayBookResponse UpdatePlaybook(UpdatePlayBookRequest request)
        {
            baseResponse response = new PlayBookResponse();

            var result = _updatePlaybookValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as PlayBookResponse;

            }
            return _bl.UpdatePlaybook(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Playbook/Delete")]
        public DeletePlayBookResponse DeletePlaybook(DeletePlayBookRequest request)
        {
            baseResponse response = new DeletePlayBookResponse();

            var result = _deletePlaybookValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as DeletePlayBookResponse;

            }
            return _bl.DeletePlaybook(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Playbook/Execute")]
        public PlayBookResponse ExecutePlaybook(ExecutePlayBookRequest request)
        {
            return _bl.ExecutePlaybook(request);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("PlaybookByID")]
        public GetPlayBookResponse GetPlayBookPlayBookID(int  PlaybookID)
        {
            return _bl.GetPlayBookbyPlaybookID(PlaybookID);
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
