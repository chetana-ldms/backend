using FluentValidation;
using LDP.Common.Requests;
using LDP_APIs.BL.APIRequests;

namespace LDP_APIs.Validators
{
    public class AddLDPToolValidator : AbstractValidator<LDPToolRequest>
    {
        public AddLDPToolValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");

            RuleFor(x => x.ToolName).NotNull().NotEmpty().WithMessage("Please check!.. ToolName  is null or empty");
            RuleFor(x => x.ToolTypeId).GreaterThan(0).WithMessage("Please check!.. ToolTypeID should be greater than zero");
           // RuleFor(x => x.ToolType).NotNull().NotEmpty().WithMessage("Please check!.. ToolType  is null or empty");
           // RuleFor(x => x.CreatedByUser).NotNull().NotEmpty().WithMessage("Please check!.. CreatedByUser is null or empty");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");

        }
    }

    public class UpdateLDPToolValidator : AbstractValidator<UpdateLDPToolRequest>
    {
        public UpdateLDPToolValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ToolId).GreaterThan(0).WithMessage("Please check!.. ToolID should be greater than zero");
            RuleFor(x => x.ToolName).NotNull().NotEmpty().WithMessage("Please check!.. ToolName  is null or empty");
           // RuleFor(x => x.ToolType).NotNull().NotEmpty().WithMessage("Please check!.. ToolType  is null or empty");

            RuleFor(x => x.ToolTypeId).NotNull().NotEmpty().WithMessage("Please check!.. ToolTypeID should be greater than zero");
           // RuleFor(x => x.UpdatedByUser).NotNull().NotEmpty().WithMessage("Please check!.. UpdatedByUser is null or empty");
            RuleFor(x => x.UpdatedDate).NotNull().NotEmpty().WithMessage("Please check!.. UpdatedDate is null or empty");

        }
    }


    public class DeleteLDPToolValidator : AbstractValidator<DeleteLDPToolRequest>
    {
        public DeleteLDPToolValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ToolId).GreaterThan(0).WithMessage("Please check!.. ToolID should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");

        }
    }
    public class AddOrganizationValidator : AbstractValidator<AddOrganizationRequest>
    {
        public AddOrganizationValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");

            RuleFor(x => x.OrgName).NotNull().NotEmpty().WithMessage("Please check!.. OrgName  is null or empty");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. UpdatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");

        }
    }

    public class UpdateOrganizationValidator : AbstractValidator<UpdateOrganizationRequest>
    {
        public UpdateOrganizationValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.OrgName).NotNull().NotEmpty().WithMessage("Please check!.. OrgName  is null or empty");
            RuleFor(x => x.UpdatedUserId).GreaterThan(0).WithMessage("Please check!.. UpdatedUserId should be greater than zero"); ;
            RuleFor(x => x.UpdatedDate).NotNull().NotEmpty().WithMessage("Please check!.. UpdatedDate is null or empty");
        }
    }

    public class DeleteOrganizationValidator : AbstractValidator<DeleteOrganizationRequest>
    {
        public DeleteOrganizationValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");

        }
    }

    public class AddOrganizationToolValidator : AbstractValidator<AddOrganizationToolsRequest>
    {
        public AddOrganizationToolValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.ToolID).GreaterThan(0).WithMessage("Please check!.. ToolID should be greater than zero");
            RuleFor(x => x.AuthKey).NotNull().NotEmpty().WithMessage("Please check!.. AuthKey  is null or empty");
           // RuleFor(x => x.ApiUrl).NotNull().NotEmpty().WithMessage("Please check!.. ApiUrl  is null or empty");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");

        }
    }

    public class UpdateOrganizationToolValidator : AbstractValidator<UpdateOrganizationToolsRequest>
    {
        public UpdateOrganizationToolValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.OrgToolID).GreaterThan(0).WithMessage("Please check!.. OrgToolID should be greater than zero");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.ToolID).GreaterThan(0).WithMessage("Please check!.. ToolID should be greater than zero");
            RuleFor(x => x.AuthKey).NotNull().NotEmpty().WithMessage("Please check!.. AuthKey  is null or empty");
           // RuleFor(x => x.ApiUrl).NotNull().NotEmpty().WithMessage("Please check!.. ApiUrl  is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");

        }
    }

    public class DeleteOrganizationToolValidator : AbstractValidator<DeleteOrganizationToolsRequest>
    {
        public DeleteOrganizationToolValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.OrgToolID).GreaterThan(0).WithMessage("Please check!.. OrgToolID should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");

        }
    }
    public class AddAddToolTypeActionn : AbstractValidator<AddToolTypeActionRequest>
    {
        public AddAddToolTypeActionn()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ToolTypeID).GreaterThan(0).WithMessage("Please check!.. ToolTypeID should be greater than zero");
            RuleFor(x => x.ToolAction).NotNull().NotEmpty().WithMessage("Please check!.. ToolAction  is null or empty");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");

        }
    }

    public class UpdateToolTypeAction : AbstractValidator<UpdateToolTypeActionRequest>
    {
        public UpdateToolTypeAction()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ToolTypeActionID).GreaterThan(0).WithMessage("Please check!.. ToolTypeActionID should be greater than zero");
            RuleFor(x => x.ToolTypeID).GreaterThan(0).WithMessage("Please check!.. ToolTypeID should be greater than zero");
            RuleFor(x => x.ToolAction).NotNull().NotEmpty().WithMessage("Please check!.. ToolAction  is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");

        }
    }

    public class DeleteToolTypeAction : AbstractValidator<DeleteToolTypeActionRequest>
    {
        public DeleteToolTypeAction()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ToolTypeActionID).GreaterThan(0).WithMessage("Please check!.. ToolTypeActionID should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");

        }
    }

    public class AddToolActionValidator : AbstractValidator<AddToolActionRequest>
    {
        public AddToolActionValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ToolTypeActionID).GreaterThan(0).WithMessage("Please check!.. ToolTypeActionID should be greater than zero");
            RuleFor(x => x.ToolID).GreaterThan(0).WithMessage("Please check!.. ToolID  is null or empty");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");

        }
    }

    public class UpdateToolActionValidator : AbstractValidator<UpdateToolActionRequest>
    {
        public UpdateToolActionValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ToolActionID).GreaterThan(0).WithMessage("Please check!.. ToolActionID should be greater than zero");
            RuleFor(x => x.ToolTypeActionID).GreaterThan(0).WithMessage("Please check!.. ToolTypeActionID should be greater than zero");
            RuleFor(x => x.ToolID).GreaterThan(0).WithMessage("Please check!.. ToolID  is null or empty");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
    }

    public class DeleteToolActionValidator : AbstractValidator<DeleteToolActionRequest>
    {
        public DeleteToolActionValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ToolActionId).GreaterThan(0).WithMessage("Please check!.. ToolActionID should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");
            RuleFor(x => x.DeletedUserId).NotNull().NotEmpty().WithMessage("Please check!.. DeletedUserId should be greater than zero");
        }
    }
}
