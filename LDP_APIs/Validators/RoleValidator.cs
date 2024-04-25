using FluentValidation;
using LDP_APIs.BL.APIRequests;

namespace LDP_APIs.BL.Validators
{
    public class AddRoleRequesttValidator : AbstractValidator<AddRoleRequest>
    {
        public AddRoleRequesttValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.RoleName).NotNull().NotEmpty().WithMessage("Please check!.. Role name is null or empty");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. created date is null or empty");
        
        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.RoleID).GreaterThan(0).WithMessage("Please check!.. RoleID should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.RoleName).NotNull().NotEmpty().WithMessage("Please check!.. Role name should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.Modifieddate).NotNull().NotEmpty().WithMessage("Please check!.. modified date is null or empty");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class DeleteRoleRequestValidator : AbstractValidator<DeleteRoleRequest>
    {
        public DeleteRoleRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.RoleID).GreaterThan(0).WithMessage("Please check!.. RoleID should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

}
