using FluentValidation;
using LDP.Common.Requests;
using LDP.Common.Services.Notifications.SMS;

namespace LDP_APIs.Validators
{

    public class SendSMSRequestValidator : AbstractValidator<SendSMSRequest>
    {
        public SendSMSRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.SMSMessage).NotNull().NotEmpty().WithMessage("Please check!.. SMSMessage  is null or empty");
            RuleFor(x => x.ToPhoneNumber).NotNull().NotEmpty().WithMessage("Please check!.. ToPhoneNumber is null or empty");
           
        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SendTeamsMessageValidator : AbstractValidator<SendTeamsMessageRequest>
    {
        public SendTeamsMessageValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.Message).NotNull().NotEmpty().WithMessage("Please check!.. Message  is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

}
