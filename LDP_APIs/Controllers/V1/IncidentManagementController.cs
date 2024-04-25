using FluentValidation;
using FluentValidation.Results;
using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Incident Management")]
      public class IncidentManagementController : ControllerBase
    {
        IIncidentsBL _incidentbl;
        private IValidator<GetUnAttendedIncidentCount> _unAttendedIncidentCountvalidator;
        private IValidator<IncidentAssignOwnerRequest> _assignOwnerRequestvalidator;
        private IValidator<SetIncidentStatusRequest> _setIncidentStatusValidator;
        private IValidator<SetIncidentPriorityRequest> _setIncidentPriorityValidator;
        private IValidator<SetIncidentSeviarityRequest> _setIncidentSeviarityValidator;
        private IValidator<GetMyInternalIncidentsRequest> _getMyInternalIncidentsValidator;
        private IValidator<SetIncidentScoreRequest> _setIncidentScoreValidator;
        private IValidator<GetIncidentsRequest> _getIncidentsValidator;
        private IValidator<CreateIncidentRequest> _createIncidentValidator;
        private IValidator<UpdateIncidentRequest> _updateIncidentValidator;
        private IValidator<GetIncidentCountByPriorityAndStatusRequest> _getIncidentCountByPriorityAndStatusValidator;
        private IValidator<IncidentSeeachRequest> _incidentSeeachRequestValidator;


        public IncidentManagementController(//IIncidentManagementBL bl,
            IIncidentsBL incidentbl
            , IValidator<GetUnAttendedIncidentCount> unAttendedIncidentCountvalidator
            , IValidator<IncidentAssignOwnerRequest> assignOwnerRequestvalidator
            , IValidator<SetIncidentStatusRequest> setIncidentStatusValidator
            , IValidator<SetIncidentPriorityRequest> setIncidentPriorityValidator
            , IValidator<SetIncidentSeviarityRequest> setIncidentSeviarityValidator
            , IValidator<GetMyInternalIncidentsRequest> getMyInternalIncidentsValidator
            , IValidator<SetIncidentScoreRequest> setIncidentScoreValidator
            , IValidator<GetIncidentsRequest> getInternalIncidentsValidator
            , IValidator<CreateIncidentRequest> createIncidentValidator
            , IValidator<GetIncidentCountByPriorityAndStatusRequest> getIncidentCountByPriorityAndStatusValidator,
IValidator<UpdateIncidentRequest> updateIncidentValidator,
IValidator<IncidentSeeachRequest> incidentSeeachRequestValidator)
        {
            //_bl = bl;
            _incidentbl = incidentbl;
            _unAttendedIncidentCountvalidator = unAttendedIncidentCountvalidator;
            _assignOwnerRequestvalidator = assignOwnerRequestvalidator;
            _setIncidentStatusValidator = setIncidentStatusValidator;
            _setIncidentPriorityValidator = setIncidentPriorityValidator;
            _setIncidentSeviarityValidator = setIncidentSeviarityValidator;
            _getMyInternalIncidentsValidator = getMyInternalIncidentsValidator;
            _setIncidentScoreValidator = setIncidentScoreValidator;
            _getIncidentsValidator = getInternalIncidentsValidator;
            _createIncidentValidator = createIncidentValidator;
            _getIncidentCountByPriorityAndStatusValidator = getIncidentCountByPriorityAndStatusValidator;
            _updateIncidentValidator = updateIncidentValidator;
            _incidentSeeachRequestValidator = incidentSeeachRequestValidator;
        }



        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("CreateIncident")]
        public CreateIncidentResponse CreateIncident(CreateIncidentRequest request)
        {
            baseResponse response = new CreateIncidentResponse();

            var result = _createIncidentValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as CreateIncidentResponse;

            }
            return _incidentbl.CreateIncident(request);
            
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("UpdateIncident")]
        public UpdateIncidentResponse UpdateIncident(UpdateIncidentRequest request)
        {
            baseResponse response = new UpdateIncidentResponse();

            var result = _updateIncidentValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as UpdateIncidentResponse;

            }
            return _incidentbl.UpdateIncident(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Incidents")]
        public GetIncidentsResponse GetIncidents(GetIncidentsRequest request)
        {
            
             baseResponse response = new GetIncidentsResponse();

            var result = _getIncidentsValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as GetIncidentsResponse;

            }
            return _incidentbl.GetIncidents(request);

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("IncidentDetails")]
        public GetIncidentResponse GetIncidentDetails(int incidentId)
        {
           
            return _incidentbl.GetIncidentDetails(incidentId);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetUnAttendedIncidentsCount")]
        public getUnattendedIncidentcountResponse GetUnAttendedIncidentsCount(GetUnAttendedIncidentCount request)
        {
            baseResponse response = new getUnattendedIncidentcountResponse();

            var result = _unAttendedIncidentCountvalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as getUnattendedIncidentcountResponse;

            }
            return _incidentbl.GetUnAttendedIncidentsCount(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AssignOwner")]
        public IncidentAssignOwnerResponse AssignOwner(IncidentAssignOwnerRequest request)
        {

            baseResponse response = new IncidentAssignOwnerResponse();

            var result = _assignOwnerRequestvalidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as IncidentAssignOwnerResponse;

            }
            return _incidentbl.AssignOwner(request);

        }

     
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SetIncidentStatus")]
        public SetIncidentStatusResponse SetIncidentStatus(SetIncidentStatusRequest request)
        {
            baseResponse response = new SetIncidentStatusResponse();

            var result = _setIncidentStatusValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SetIncidentStatusResponse;

            }
            return _incidentbl.SetIncidentStatus(request);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SetIncidentPriority")]
        public SetIncidentPriorityResponse SetIncidentPriority(SetIncidentPriorityRequest request)
        {
            baseResponse response = new SetIncidentPriorityResponse();

            var result = _setIncidentPriorityValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SetIncidentPriorityResponse;

            }
            return _incidentbl.SetIncidentPriority(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SetIncidentSeviarity")]
        public SetIncidentSeviarityResponse SetIncidentSeviarity(SetIncidentSeviarityRequest request)
        {
            baseResponse response = new SetIncidentSeviarityResponse();

            var result = _setIncidentSeviarityValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SetIncidentSeviarityResponse;

            }
            return _incidentbl.SetIncidentSeviarity(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SetIncidentScore")]
        public SetIncidentScoreResponse SetIncidentScore(SetIncidentScoreRequest request)
        {
            baseResponse response = new SetIncidentScoreResponse();

            var result = _setIncidentScoreValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SetIncidentScoreResponse;

            }
            return _incidentbl.SetIncidentScore(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetMyInternalIncidents")]
        public GetIncidentsResponse GetMyInternalIncidents(GetMyInternalIncidentsRequest request)
        {
            baseResponse response = new LDP.Common.Responses.GetIncidentsResponse();

            var result = _getMyInternalIncidentsValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as LDP.Common.Responses.GetIncidentsResponse;

            }

            return _incidentbl.GetMyInternalIncidents(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetIncidentCountByPriorityAndStatus")]
        public GetIncidentCountByPriorityAndStatusResponse GetIncidentCountByPriorityAndStatus(GetIncidentCountByPriorityAndStatusRequest request)
        {
            baseResponse response = new GetIncidentCountByPriorityAndStatusResponse();

            var result = _getIncidentCountByPriorityAndStatusValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as GetIncidentCountByPriorityAndStatusResponse;

            }
            return _incidentbl.GetIncidentCountByPriorityAndStatus(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetIncidentSearchResult")]
        public SearchIncidentsResponse GetIncidentSearchResult(IncidentSeeachRequest request)
        {
            baseResponse response = new SearchIncidentsResponse();

            var result = _incidentSeeachRequestValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SearchIncidentsResponse;

            }
            return _incidentbl.GetIncidentSearchResult(request);
        }

        private void BuildValiationMessage(ValidationResult result, ref baseResponse validationresponse)
        {
            validationresponse.IsSuccess = false;
            validationresponse.Message = "Validation Error";
            validationresponse.HttpStatusCode = HttpStatusCode.BadRequest;
            validationresponse.errors = result.Errors.Select(e => e.ErrorMessage.ToString()).ToList();

        }
    }
}
