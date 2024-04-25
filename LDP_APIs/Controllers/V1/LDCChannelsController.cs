using FluentValidation;
using FluentValidation.Results;
using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Channels")]
    public class LDCChannelsController : ControllerBase
    {
        ILDCChannelBL _channelBL;
        private IValidator<AddChannelRequest> _addChannelValidator;
        private IValidator<UpdateChannelRequest> _updateChannelValidator;
        private IValidator<DeleteChannelRequest> _deleteChannelValidator;

        private IValidator<AddChannelSubItemRequest> _addChannelSubitemValidator;
        private IValidator<UpdateChannelSubItemRequest> _updateChannelSubitemValidator;
        private IValidator<DeleteChannelSubItemRequest> _deleteChannelSubitemValidator;

        private IValidator<AddChannelQuestionRequest> _addChannelQuestionValidator;
        private IValidator<UpdateChannelQuestionRequest> _updateChannelQuestionValidator;
        private IValidator<DeleteChannelQuestionRequest> _deleteChannelQuestionValidator;

        private IValidator<AddChannelAnswerRequest> _addChannelAnswerValidator;
        private IValidator<UpdateChannelAnswerRequest> _updateChannelAnswerValidator;
        private IValidator<DeleteChannelAnswerRequest> _deleteChannelAnswerValidator;

        public LDCChannelsController(
                ILDCChannelBL channelBL, IValidator<AddChannelRequest> addChannelValidator, IValidator<UpdateChannelRequest> updateChannelValidator, IValidator<DeleteChannelRequest> deleteChannelValidator, IValidator<AddChannelSubItemRequest> addChannelSubitemValidator, IValidator<UpdateChannelSubItemRequest> updateChannelSubitemValidator, IValidator<DeleteChannelSubItemRequest> deleteChannelSubitemValidator, IValidator<AddChannelQuestionRequest> addChannelQuestionValidator, IValidator<UpdateChannelQuestionRequest> updateChannelQuestionValidator, IValidator<DeleteChannelQuestionRequest> deleteChannelQuestionValidator, IValidator<AddChannelAnswerRequest> addChannelAnswerValidator, IValidator<UpdateChannelAnswerRequest> updateChannelAnswerValidator, IValidator<DeleteChannelAnswerRequest> deleteChannelAnswerValidator)
        {
            _channelBL = channelBL;
            _addChannelValidator = addChannelValidator;
            _updateChannelValidator = updateChannelValidator;
            _deleteChannelValidator = deleteChannelValidator;
            _addChannelSubitemValidator = addChannelSubitemValidator;
            _updateChannelSubitemValidator = updateChannelSubitemValidator;
            _deleteChannelSubitemValidator = deleteChannelSubitemValidator;
            _addChannelQuestionValidator = addChannelQuestionValidator;
            _updateChannelQuestionValidator = updateChannelQuestionValidator;
            _deleteChannelQuestionValidator = deleteChannelQuestionValidator;
            _addChannelAnswerValidator = addChannelAnswerValidator;
            _updateChannelAnswerValidator = updateChannelAnswerValidator;
            _deleteChannelAnswerValidator = deleteChannelAnswerValidator;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/Add")]
        public ChannelResponse AddChannel(AddChannelRequest request)
        {
            baseResponse response = new ChannelResponse();

            var result = _addChannelValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelResponse;

            }
            return _channelBL.AddChannel(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/Update")]
        public ChannelResponse UpdateChannel(UpdateChannelRequest request)
        {
            baseResponse response = new ChannelResponse();

            var result = _updateChannelValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelResponse;

            }
            return _channelBL.UpdateChannel(request);

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Channels")]
        public GetChannelsResponse GetAllChannels(int orgId)
        {

            return _channelBL.GetAllChannels(orgId);

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Channels/Active")]
        public GetChannelsResponse GetActiveChannels(int orgId)
        {

            return _channelBL.GetActiveChannels(orgId);

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Channels/PreActive")]
        public GetChannelsResponse GetPreActiveChannels(int orgId)
        {
            return _channelBL.GetPreActiveChannels(orgId);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/Delete")]
        public ChannelResponse DeleteChannel(DeleteChannelRequest request)
        {
            baseResponse response = new ChannelResponse();

            var result = _deleteChannelValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelResponse;

            }
            return _channelBL.DeleteChannel(request);

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("ChannelDetails")]
        public GetChannelResponse GetChannelDetails(int channelId)
        {
            return _channelBL.GetChannelDetails(channelId);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/SubItem/Add")]
        public ChannelSubItemsResponse AddChannelSubItem(AddChannelSubItemRequest request)
        {
            baseResponse response = new ChannelSubItemsResponse();

            var result = _addChannelSubitemValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelSubItemsResponse;

            }
            return _channelBL.AddChannelSubItem(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/SubItem/Update")]
        public ChannelSubItemsResponse UpdateChannelSubItem(UpdateChannelSubItemRequest request)
        {
            baseResponse response = new ChannelSubItemsResponse();

            var result = _updateChannelSubitemValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelSubItemsResponse;

            }
            return _channelBL.UpdateChannelSubItem(request);
        }
        //[HttpGet]
        //[MapToApiVersion("1.0")]
        //[Route("Channels/SubItemsbyChannelId")]
        //public GetChannelSubItemsResponse GetChannelsubitemsByChannelId(int channelId)
        //{
        //    return _channelBL.GetChannelsubitemsByChannelId(channelId);
        //}
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/SubItemsByOrgChannel")]
        public GetChannelSubItemsResponse GetChannelsubitemsByOrgChannel(GetChannelSubItemsRequest request)
        {
            return _channelBL.GetChannelsubitemsByOrgChannel(request);
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Channels/SubItemDetails")]
        public GetChannelSubItemResponse GetChannelSubitemDetails(int subItemId)
        {
            return _channelBL.GetChannelSubitemDetails(subItemId);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/SubItem/Delete")]
        public ChannelSubItemsResponse DeleteChannelSubItem(DeleteChannelSubItemRequest request)
        {
            baseResponse response = new ChannelSubItemsResponse();

            var result = _deleteChannelSubitemValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelSubItemsResponse;

            }
            return _channelBL.DeleteChannelSubItem(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/Questions/Add")]
        public ChannelQAResponse AddChannelQuestion(AddChannelQuestionRequest request)
        {
            baseResponse response = new ChannelQAResponse();

            var result = _addChannelQuestionValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelQAResponse;

            }
            return _channelBL.AddChannelQuestion(request);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/Questions/Answer/Add")]
        public ChannelQAResponse AddChannelAnswer(AddChannelAnswerRequest request)
        {
            baseResponse response = new ChannelQAResponse();

            var result = _addChannelAnswerValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelQAResponse;

            }
            return _channelBL.AddChannelAnswer(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/Questions/Update")]
        public  ChannelQAResponse UpdateChannelQuestion(UpdateChannelQuestionRequest request)
        {
            baseResponse response = new ChannelQAResponse();

            var result = _updateChannelQuestionValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelQAResponse;

            }
            return _channelBL.UpdateChannelQuestion(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/Questions/Answere/Update")]
        public ChannelQAResponse UpdateChannelAnswer(UpdateChannelAnswerRequest request)
        {
            baseResponse response = new ChannelQAResponse();

            var result = _updateChannelAnswerValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelQAResponse;

            }
            return _channelBL.UpdateChannelAnswer(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channel/Questions")]
        public GetChannelsQAResponse GetChannelQuestions(GetChannelQuestionsRequest request)
        {
            return _channelBL.GetChannelQuestions(request);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Channels/QuestionDetails")]
        public GetChannelQAResponse GetChannelQuestionDetails(int QuestionId)
        {
            return _channelBL.GetChannelQuestionDetails(QuestionId);
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Channels/AnswerDetails")]
        public GetChannelAnswerResponse GetChannelAnswerDetails(int AnswerId)
        {
            return _channelBL.GetChannelAnswerDetails(AnswerId);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channel/Questions/Delete")]

        public ChannelQAResponse DeleteChannelQuestion(DeleteChannelQuestionRequest request)
        {
            baseResponse response = new ChannelQAResponse();

            var result = _deleteChannelQuestionValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelQAResponse;

            }
            return _channelBL.DeleteChannelQuestion(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channel/Questions/Answer/Delete")]
        public ChannelQAResponse DeleteChannelAnswer(DeleteChannelAnswerRequest request)
        {
            baseResponse response = new ChannelQAResponse();

            var result = _deleteChannelAnswerValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ChannelQAResponse;

            }
            return _channelBL.DeleteChannelAnswer(request);
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
