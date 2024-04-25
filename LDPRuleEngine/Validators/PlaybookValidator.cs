using FluentValidation;
using LDPRuleEngine.Controllers.Requests;

namespace LDPRuleEngine.Validators
{
    public class AddPlaybooktValidator : AbstractValidator<AddPlayBookRequest>
    {
        public AddPlaybooktValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.PlayBookName).NotNull().NotEmpty().WithMessage("Please check!.. PlayBookName is null or empty");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. Createduser is null or empty");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. Createddate is null or empty");
        }
    }

    public class UpdatePlaybooktValidator : AbstractValidator<UpdatePlayBookRequest>
    {
        public UpdatePlaybooktValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.PlayBookID).GreaterThan(0).WithMessage("Please check!.. PlayBookID  should be greater than zero");
            RuleFor(x => x.PlayBookName).NotNull().NotEmpty().WithMessage("Please check!.. PlayBookName is null or empty");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId  should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
    }
    public class DeletePlaybooktValidator : AbstractValidator<DeletePlayBookRequest>
    {
        public DeletePlaybooktValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.PlayBookID).GreaterThan(0).WithMessage("Please check!.. PlayBookID  should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId  should be greater than zero");

        }
    }
}