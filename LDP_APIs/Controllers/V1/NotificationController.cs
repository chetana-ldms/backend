using FluentValidation;
using FluentValidation.Results;
using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP.Common.Services.Notifications.SMS;
using LDP_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Notifications")]
    public class NotificationController : ControllerBase
    {
        ISMSSenderBL _bl;
        IValidator<SendSMSRequest> _sendSMSValidator;
        IValidator<SendTeamsMessageRequest> _sendTeamsMessageValidator;
        INotificationDetailBL _notificationDetailBL;
        public NotificationController(IValidator<SendSMSRequest> sendSMSValidator, ISMSSenderBL bl
            , IValidator<SendTeamsMessageRequest> sendTeamsMessageValidator
            , INotificationDetailBL notificationDetailBL)
        {
            _sendSMSValidator = sendSMSValidator;
            _bl = bl;
            _sendTeamsMessageValidator = sendTeamsMessageValidator;
            _notificationDetailBL = notificationDetailBL;
        }



        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SMS/Send")]
        public SendSMSResponse SendSMS(SendSMSRequest request)
        {
            baseResponse response = new SendSMSResponse();

            var result = _sendSMSValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SendSMSResponse;

            }
            return _bl.SendSMS(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SMS/Reply")]
        public ResplySMSResponse ReplySMS(ResplySMSRequest request)
        {
            //baseResponse response = new GetSMSReplyResponse();

            //var result = _sendSMSValidator.Validate(request);

            //if (!result.IsValid)
            //{
            //    BuildValiationMessage(result, ref response);
            //    return response as SendSMSResponse;

            //}
            return _bl.ResplySMS(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SMS/GetReplyMessage")]
        public GetSMSDataResponse GetReplyMessage(GetReplyMessageRequest request)
        {
            return _bl.GetReplyMessage(request);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SMS/SMSMessageDetails")]
        public GetSMSDataResponse GetSMSMessageDetails(GetSMSDetailsRequest request)
        {
            //baseResponse response = new GetSMSReplyResponse();

            //var result = _sendSMSValidator.Validate(request);

            //if (!result.IsValid)
            //{
            //    BuildValiationMessage(result, ref response);
            //    return response as SendSMSResponse;

            //}
            return _bl.GetSMSMessages(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("TeamsMessage/Send")]
        public SendTeamsMessageResponse SendTeamsMessage(SendTeamsMessageRequest request)
        {
            baseResponse response = new SendTeamsMessageResponse();

            var result = _sendTeamsMessageValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SendTeamsMessageResponse;

            }
            return _notificationDetailBL.SendTeamsMessage(request);

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
