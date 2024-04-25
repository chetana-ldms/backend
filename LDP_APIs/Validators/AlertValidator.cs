using FluentValidation;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.Models;

namespace LDP_APIs.Validators
{
    public class GetAlertsValidator : AbstractValidator<GetAlertsRequest>
    {
        public GetAlertsValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.LoggedInUserId).GreaterThan(0).WithMessage("Please check!.. LoggedInUserId should be greater than zero");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
    public class AlertAssignOwnerValidator : AbstractValidator<AssignOwnerRequest>
    {
        public AlertAssignOwnerValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AlertID).GreaterThan(0).WithMessage("Please check!.. alertID should be greater than zero");
            RuleFor(x => x.UserID).GreaterThan(0).WithMessage("Please check!.. UserID should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("Please check!.. UserName  is null or empty");
           // RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SetAlertStatusValidator : AbstractValidator<SetAlertStatusRequest>
    {
        public SetAlertStatusValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.alertID).GreaterThan(0).WithMessage("Please check!.. alertID should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.StatusID).GreaterThan(0).WithMessage("Please check!.. StatusID should be greater than zero");
            RuleFor(x => x.StatusName).NotNull().NotEmpty().WithMessage("Please check!.. StatusName  is null or empty");
            RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SetAlertSpecificStatusValidator : AbstractValidator<SetAlertSpecificStatusRequest>
    {
        public SetAlertSpecificStatusValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.alertIDs.Count).GreaterThan(0).WithMessage("Please check!.. alertIDs count should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            //RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SetAlertPriorityValidator : AbstractValidator<SetAlertPriorityRequest>
    {
        public SetAlertPriorityValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AlertID).GreaterThan(0).WithMessage("Please check!.. alertID should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.PriorityID).GreaterThan(0).WithMessage("Please check!.. PriorityID should be greater than zero");
            RuleFor(x => x.PriorityValue).NotNull().NotEmpty().WithMessage("Please check!.. PriorityValue  is null or empty");
           // RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SetAlertSevirityValidator : AbstractValidator<SetAlertSevirityRequest>
    {
        public SetAlertSevirityValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AlertID).GreaterThan(0).WithMessage("Please check!.. alertID should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.SevirityId).GreaterThan(0).WithMessage("Please check!.. SevirityID should be greater than zero");
            RuleFor(x => x.Sevirity).NotNull().NotEmpty().WithMessage("Please check!.. Sevirity  is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
    public class AssignAlertTagValidator : AbstractValidator<AssignAlertTagsRequest>
    {
        public AssignAlertTagValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AlertID).GreaterThan(0).WithMessage("Please check!.. alertID should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.TagID).GreaterThan(0).WithMessage("Please check!.. TagID should be greater than zero");
            RuleFor(x => x.TagText).NotNull().NotEmpty().WithMessage("Please check!.. TagText  is null or empty");
           // RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class AssignAlertScoresValidator : AbstractValidator<AssignAlertScoresRequest>
    {
        public AssignAlertScoresValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AlertID).GreaterThan(0).WithMessage("Please check!.. alertID should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            // RuleFor(x => x.ScoreID).GreaterThan(0).WithMessage("Please check!.. ScoreID should be greater than zero");
            RuleFor(x => x.Score).NotNull().NotEmpty().WithMessage("Please check!.. Score  is null or empty");
            //RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SetAlertPositiveAnalysisValidator : AbstractValidator<SetAlertPositiveAnalysisRequest>
    {
        public SetAlertPositiveAnalysisValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AlertID).GreaterThan(0).WithMessage("Please check!.. alertID should be greater than zero");
            RuleFor(x => x.PositiveAnalysisID).GreaterThan(0).WithMessage("Please check!.. PositiveAnalysisID should be greater than zero");
            RuleFor(x => x.PositiveAnalysisValue).NotNull().NotEmpty().WithMessage("Please check!.. PositiveAnalysisValue  is null or empty");
           // RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class UpdateAlertRequestValidator : AbstractValidator<UpdateAlertRequest>
    {
        public UpdateAlertRequestValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AlertId).GreaterThan(0).WithMessage("Please check!.. AlertId should be greater than zero");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId  should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
    

}
