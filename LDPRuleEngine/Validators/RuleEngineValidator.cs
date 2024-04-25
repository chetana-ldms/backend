using FluentValidation;
using LDPRuleEngine.Controllers.Requests;

namespace LDPRuleEngine.Validators
{
    public class AddRuleRequestValidator : AbstractValidator<AddRuleRequest>
    {
        public AddRuleRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.RuleName).NotNull().NotEmpty().WithMessage("Please check!.. RuleName is null or empty");
            RuleFor(x => x.RuleCatagoryID).GreaterThan(0).WithMessage("Please check!.. RuleCatagoryID should be greater than zero");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");
            RuleFor(x => x.RuleConditions).NotNull().NotEmpty().WithMessage("Please check!.. RuleConditions is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    //UpdateRuleRequest
    public class UpdateRuleRequestValidator : AbstractValidator<UpdateRuleRequest>
    {
        public UpdateRuleRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.RuleID).GreaterThan(0).WithMessage("Please check!.. RuleID should be greater than zero");
            RuleFor(x => x.RuleName).NotNull().NotEmpty().WithMessage("Please check!.. RuleName is null or empty");
            RuleFor(x => x.RuleCatagoryID).GreaterThan(0).WithMessage("Please check!.. RuleCatagoryID  should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");
            RuleFor(x => x.RuleConditions).NotNull().NotEmpty().WithMessage("Please check!.. RuleConditions is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class DeleteRuleRequestValidator : AbstractValidator<DeleteRuleRequest>
    {
        public DeleteRuleRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.RuleID).GreaterThan(0).WithMessage("Please check!.. RuleID should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!..DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
}
