
using FluentValidation;
using LDP.Common.Requests;
using LDP_APIs.Models;

namespace LDP_APIs.Validators
{
    public class CreateIncidentValidator : AbstractValidator<CreateIncidentRequest>
    {
        public CreateIncidentValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.CreateUserId).GreaterThan(0).WithMessage("Please check!.. CreateUserId should be greater than zero");
            RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Please check!.. Description is null or empty when alert Ids are not associated")
            .When(x => x.AlertIDs.Count == 1 && x.AlertIDs[0] == 0);

            RuleFor(x => x.Subject)
           .NotEmpty().WithMessage("Please check!.. Subject is null or empty when alert Ids are not associated")
           .When(x => x.AlertIDs.Count == 1 && x.AlertIDs[0] == 0);
        }

        private bool BeAValidPostcode(string postcode) 
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class UpdateIncidentValidator : AbstractValidator<UpdateIncidentRequest>
    {
        public UpdateIncidentValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.IncidentId).GreaterThan(0).WithMessage("Please check!.. IncidentId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is empty");
        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class GetIncidentValidator : AbstractValidator<GetIncidentsRequest>
    {
        public GetIncidentValidator()
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
    
    public class IncidentValidator : AbstractValidator<Incidentdtls>
    {
        public IncidentValidator()
        {
            RuleFor(x => x ).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.incidentTitle).NotNull().WithMessage("Please check!.. Title is null or empty");
            RuleFor(x => x.description).NotNull().WithMessage("Please check!.. description is null or empty");
            RuleFor(x => x.short_description).NotNull().WithMessage("Please check!.. short_description is null or empty");
            RuleFor(x => x.category).NotNull().WithMessage("Please check!.. category is null or empty");
        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class GetUnAttendedIncidentsCountValidator : AbstractValidator<GetUnAttendedIncidentCount>
    {
        public GetUnAttendedIncidentsCountValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.NumberofDays).GreaterThan(0).WithMessage("Please check!.. NumberofDays should be greater than zero");
        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class AssignOwnerValidator : AbstractValidator<IncidentAssignOwnerRequest>
    {
        public AssignOwnerValidator()
        {
            
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.IncidentID).GreaterThan(0).WithMessage("Please check!.. IncidentID should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.OwnerUserID).GreaterThan(0).WithMessage("Please check!.. OwnerUserID should be greater than zero");
            RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser data is empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate data is empty");
            RuleFor(x => x.OwnerUserName).NotNull().NotEmpty().WithMessage("Please check!.. OwnerUserName data is empty");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SetIncidentStatusValidator : AbstractValidator<SetIncidentStatusRequest>
    {
        public SetIncidentStatusValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.IncidentID).GreaterThan(0).WithMessage("Please check!.. IncidentID should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");

            RuleFor(x => x.StatusID).GreaterThan(0).WithMessage("Please check!.. StatusID should be greater than zero");
            RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser data is empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate data is empty");
            RuleFor(x => x.StatusName).NotNull().NotEmpty().WithMessage("Please check!.. StatusName data is empty");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SetIncidentPriorityValidator : AbstractValidator<SetIncidentPriorityRequest>
    {
        public SetIncidentPriorityValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.IncidentID).GreaterThan(0).WithMessage("Please check!.. IncidentID should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");

            RuleFor(x => x.PriorityID).GreaterThan(0).WithMessage("Please check!.. PriorityID should be greater than zero");
            RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser data is empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate data is empty");
            RuleFor(x => x.PriorityValue).NotNull().NotEmpty().WithMessage("Please check!.. StatusName data is empty");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SetIncidentSeviarityValidator : AbstractValidator<SetIncidentSeviarityRequest>
    {
        public SetIncidentSeviarityValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.IncidentID).GreaterThan(0).WithMessage("Please check!.. IncidentID should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.SeviarityID).GreaterThan(0).WithMessage("Please check!.. SeviarityID should be greater than zero");
            RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser data is empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate data is empty");
            RuleFor(x => x.Seviarity).NotNull().NotEmpty().WithMessage("Please check!.. SeviarityValue data is empty");
        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class SetIncidentScoreValidator : AbstractValidator<SetIncidentScoreRequest>
    {
        public SetIncidentScoreValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is null or empty");
            RuleFor(x => x.IncidentID).GreaterThan(0).WithMessage("Please check!.. IncidentID should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedUser).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedUser data is null or empty");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate data is null or empty");
            RuleFor(x => x.Score).NotNull().NotEmpty().WithMessage("Please check!.. Score data is null or empty");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class GetMyInternalIncidentsValidator : AbstractValidator<GetMyInternalIncidentsRequest>
    {
        public GetMyInternalIncidentsValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is null or empty");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.UserID).GreaterThan(0).WithMessage("Please check!.. UserID should be greater than zero");
        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
    //GetIncidentCountByPriorityAndStatusRequest

    public class GetIncidentCountByPriorityAndStatusValidator : AbstractValidator<GetIncidentCountByPriorityAndStatusRequest>
    {
        public GetIncidentCountByPriorityAndStatusValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is null or empty");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.PriorityID).GreaterThan(0).WithMessage("Please check!.. UserID should be greater than zero");
            RuleFor(x => x.StatusID).GreaterThan(0).WithMessage("Please check!.. StatusID should be greater than zero");
            RuleFor(x => x.NumberofDays).GreaterThan(0).WithMessage("Please check!.. NumberofDays should be greater than zero");
        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }

    public class IncidentSearchValidator : AbstractValidator<IncidentSeeachRequest>
    {
        public IncidentSearchValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.OrgID).GreaterThan(0).WithMessage("Please check!.. OrgID should be greater than zero");
            RuleFor(x => x.LoggedInUserId).GreaterThan(0).WithMessage("Please check!.. LoggedInUserId should be greater than zero");
            RuleFor(x => x.paging.RangeStart).GreaterThan(0).WithMessage("Please check!.. RangeStart should be greater than zero");
            RuleFor(x => x.paging.RangeEnd).GreaterThan(0).WithMessage("Please check!.. RangeEnd should be greater than zero");

        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
}
