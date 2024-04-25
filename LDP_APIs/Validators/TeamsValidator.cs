using FluentValidation;
using LDP.Common.Requests;
using LDP_APIs.Models;

namespace LDP_APIs.Validators
{
    public class CreateTeamsChannelValidator: AbstractValidator<TeamscreateChannelRequest>
    {
        public CreateTeamsChannelValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.TeamsId).GreaterThan(0).WithMessage("Please check!.. TeamsId should be greater than zero");
            RuleFor(x => x.ChannelId).GreaterThan(0).WithMessage("Please check!.. ChannelId should be greater than zero");

        }
    }
}
