using FluentValidation;
using LDPRuleEngine.Controllers.Requests;

namespace LDPRuleEngine.Validators
{
    public class AddRuleActionRequestValidator : AbstractValidator<AddRuleActionRequest>
    {
        public AddRuleActionRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.RuleActionName).NotNull().NotEmpty().WithMessage("Please check!.. ruleActionName is null or empty");
            RuleFor(x => x.ToolTypeID).GreaterThan(0).WithMessage("Please check!.. ToolTypeID  should be greater than zero");
            RuleFor(x => x.ToolID).GreaterThan(0).WithMessage("Please check!.. ToolTypeID  should be greater than zero");
            RuleFor(x => x.ToolActionID).GreaterThan(0).WithMessage("Please check!.. ToolActionID  should be greater than zero");
            RuleFor(x => x.CreateduserId).GreaterThan(0).WithMessage("Please check!.. CreateduserId  should be greater than zero");
            RuleFor(x => x.Createddate).NotNull().NotEmpty().WithMessage("Please check!.. Createddate is null or empty");
           // RuleFor(x => x.rul).NotNull().NotEmpty().WithMessage("Please check!.. RuleConditions is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class UpdateRuleActionRequestValidator : AbstractValidator<UpdateRuleActionRequest>
    {
        public UpdateRuleActionRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.RuleActionID).GreaterThan(0).WithMessage("Please check!.. RuleActionID  should be greater than zero");
            RuleFor(x => x.RuleActionName).NotNull().NotEmpty().WithMessage("Please check!.. ruleActionName is null or empty");
            RuleFor(x => x.ToolTypeID).GreaterThan(0).WithMessage("Please check!.. ToolTypeID  should be greater than zero");
            RuleFor(x => x.ToolID).GreaterThan(0).WithMessage("Please check!.. ToolTypeID  should be greater than zero");
            RuleFor(x => x.ToolActionID).GreaterThan(0).WithMessage("Please check!.. ToolActionID  should be greater than zero");
            RuleFor(x => x.Modifieduserid).GreaterThan(0).WithMessage("Please check!..  Modifieduserid  should be greater than zero");
            RuleFor(x => x.Modifieddate).NotNull().NotEmpty().WithMessage("Please check!.. Modifieddate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class DelteRuleActionRequestValidator : AbstractValidator<DeleteRuleActionRequest>
    {
        public DelteRuleActionRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.RuleActionID).GreaterThan(0).WithMessage("Please check!.. RuleActionID  should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!..  DeletedUserId  should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
}
