
using FluentValidation;
using LDP_APIs.BL.APIRequests;

namespace LDP_APIs.Validators
{
    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserRequest>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(x => x ).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("Please check!.. UserName is null or empty");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Please check!.. Password is null or empty");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Please check!.. user name is null or empty");
            RuleFor(x => x.EmailId).NotNull().NotEmpty().WithMessage("Please check!.. user EmailId is null or empty");
            RuleFor(x => x.RoleID).GreaterThan(0).WithMessage("Please check!.. RoleID should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDete).NotNull().NotEmpty().WithMessage("Please check!.. created datee is null or empty");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.UserID).GreaterThan(0).WithMessage("Please check!.. UserID should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Please check!.. user name is null or empty");
            RuleFor(x => x.EmailId).NotNull().NotEmpty().WithMessage("Please check!.. user EmailId is null or empty");
            RuleFor(x => x.RoleID).GreaterThan(0).WithMessage("Please check!.. RoleID should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. updated date is null or empty");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator()
        {
           
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.UserID).GreaterThan(0).WithMessage("Please check!.. UserID should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. deleted date is null or empty");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
    public class ChangeUserPasswordRequestRequestValidator : AbstractValidator<ChangeUserPasswordRequest>
    {
        public ChangeUserPasswordRequestRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Please check!.. UserID should be greater than zero");
            RuleFor(x => x.NewPassword).NotNull().NotEmpty().WithMessage("Please check!.. new password is null or empty");
            RuleFor(x => x.OldPassword).NotNull().NotEmpty().WithMessage("Please check!.. old password is null or empty");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate  is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
    public class ResetPasswordRequestRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Please check!.. UserID should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate  is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
}
