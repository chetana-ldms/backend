using FluentValidation;
using FluentValidation.Results;
using LDP.Common;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP.Common.Services.SentinalOneIntegration;
using LDP.Common.Services.SentinalOneIntegration.Sentinel;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Alerts")]
    public class AlertsController : CommonController
    {
        IAlertsBL _bl;
        IValidator<AssignOwnerRequest> _alertAssignOwnerValidator;
        IValidator<SetAlertStatusRequest> _setAlertStatusValidator;
        IValidator<SetAlertSpecificStatusRequest> _setAlertSpecificStatusValidator;
        IValidator<SetAlertPriorityRequest> _setAlertPriorityValidator;
        IValidator<AssignAlertTagsRequest> _assignAlertTagsValidator;
        IValidator<AssignAlertScoresRequest> _assignAlertScoresValidator;
        IValidator<SetAlertPositiveAnalysisRequest> _setAlertPositiveAnalysisValidator;
        IValidator<GetAlertsRequest> _getAlertsValidator;
        IValidator<SetAlertSevirityRequest> _setSevirityValidator;
        IValidator<UpdateAlertRequest> _updateAlertRequestValidator;

        public AlertsController(IAlertsBL bl, ILogger<AlertsController> logger,
                IValidator<AssignOwnerRequest> alertAssignOwnerValidator,
                IValidator<SetAlertStatusRequest> setAlertStatusValidator,
                IValidator<SetAlertSpecificStatusRequest> setAlertSpecificStatusValidator,
                IValidator<SetAlertPriorityRequest> setAlertPriorityValidator,
                IValidator<AssignAlertTagsRequest> assignAlertTagsValidator,
                IValidator<AssignAlertScoresRequest> assignAlertScoresValidator,
                IValidator<SetAlertPositiveAnalysisRequest> setAlertPositiveAnalysisValidator,
                IValidator<GetAlertsRequest> getAlertsValidator,
                IValidator<SetAlertSevirityRequest> setSevirityValidator
            ,   IValidator<UpdateAlertRequest> updateAlertRequestValidator) : base(logger)
                    {
                        _bl = bl;
            _alertAssignOwnerValidator = alertAssignOwnerValidator;
            _setAlertStatusValidator = setAlertStatusValidator;
            _setAlertSpecificStatusValidator = setAlertSpecificStatusValidator;
            _setAlertPriorityValidator = setAlertPriorityValidator;
            _assignAlertTagsValidator = assignAlertTagsValidator;
            _assignAlertScoresValidator = assignAlertScoresValidator;
            _setAlertPositiveAnalysisValidator = setAlertPositiveAnalysisValidator;
            _getAlertsValidator = getAlertsValidator;
            _setSevirityValidator = setSevirityValidator;
            _updateAlertRequestValidator = updateAlertRequestValidator;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Alerts")]
        public getAlertsResponse GetAlerts(GetAlertsRequest request)
        {
            baseResponse response = new getAlertsResponse();

            var result = _getAlertsValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as getAlertsResponse;

            }
            return _bl.GetAlerts(request);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AlertsByAlertIds")]
        public getAlertsResponse GetalertsByAlertIds(List<double> alertIds)
        {
            return _bl.GetalertsByAlertIds(alertIds);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Alert")]
        public getAlertResponse GetAlertData(GetAlertRequest request)
        {

            return _bl.GetAlertData(request);
           
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("DetailsFromTool")]
        public GetThreatDetailsResponse GetAlertDetailsFromTool(double alertId)
        {
            GetThreatDetailsResponse response = new GetThreatDetailsResponse();
            if (alertId == 0)
            {
                response.IsSuccess = false;
                response.Message = "Alert Id should be greater than zero ";
                response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
            return _bl.GetAlertDetailsFromTool(alertId);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AlertsByAssignedOwner")]
        public getAlertsResponse GetAlertsByAssignedUser(GetAlertByAssignedOwnerRequest request)
        {
             return _bl.GetAlertsByAssignedUser(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AlertsByAutomationStatus")]
        public getAlertsResponse AlertsByAutomationStatus(GetAlertByAutomationStatusRequest request)
        {
            return _bl.GetAlertDataByAutomationStatus(request);

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetUnAttendedAletsCount")]
        public getUnattendedAlertcountResponse GetUnAttendedAletsCount(GetUnAttendedAlertCount request)
        {
            return _bl.GetUnAttendedAlertsCount(request);

        }



        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetAlertsCountByPriorityAndStatus")]
        public GetAlertsCountByPriorityAndStatusResponse GetAlertsCountByPriorityAndStatus(GetAlertsCountByPriorityAndStatusRequest request)
        {
             return _bl.GetAlertsCountByPriorityAndStatus(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetFalsePositiveAlertsCount")]
        public GetFalsePositiveAlertsCountResponse GetFalsePositiveAlertsCount(GetAlertsCountByPositiveAnalysisRequest request)
        {
            return _bl.GetFalsePositiveAlertsCount(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetAlertsResolvedMeanTime")]
        public GetAlertsResolvedMeanTimeResponse GetAlertsResolvedMeanTime(GetAlertsResolvedMeanTimeRequest request)
        {
            return _bl.GetAlertsResolvedMeanTime(request);
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetAlertsMostUsedTages")]
        public GetAlertsMostUsedTagsResponse GetAlertsMostUsedTages(GetAlertsMostUsedTagsRequest request)
        {
            return _bl.GetAlertsMostUsedTags(request);
        }

        
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetAlertsTrendData")]
        public GetAlertsTrendDataResponse GetAlertsTrendData(GetAlertsTrendDatasRequest request)
        {
            return _bl.GetAlertsTrendData(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetAlertNotesByAlertID")]
        public GetAlertNoteResponse GetAlertNotesByAlertID(GetAlertNoteRequest request)
        {
            return _bl.GetAlertNotesByAlertID(request);
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Update")]
        public UpdateAlertResponse UpdateAlert(UpdateAlertRequest request)
        {
            baseResponse response = new UpdateAlertResponse();

            var result = _updateAlertRequestValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as UpdateAlertResponse;

            }

            return _bl.UpdateAlert(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AnalystVerdict/Update")]
        public SetAnalystVerdictResponse SetAnalystVerdict(SetAnalystVerdictRequest request)
        {
     
            //var result = _updateAlertRequestValidator.Validate(request);

            //if (!result.IsValid)
            //{
            //    BuildValiationMessage(result, ref response);
            //    return response as UpdateAlertResponse;

            //}

            return _bl.SetAnalystVerdict(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Status/Update")]
        public SetAlertStatusResponse UpdateAlertStatus(UpdateAlertStatusRequest request)
        {
            
            return _bl.UpdateAlertStatus(request);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Actions/Escalate")]
        public AlertEscalateActionResponse AlertEscalateAction(AlertEscalateActionRequest request)
        {
            //var result = _setAlertSpecificStatusValidator.Validate(request);

            //if (!result.IsValid)
            //{
            //    BuildValiationMessage(result, ref response);
            //    return response as SetAlertStatusResponse;

            //}
            return _bl.AlertEscalateAction(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Actions/IgnoreORIrrelavant")]
        public AlertIgnoreORIrrelavantActionRespnse IgnoreORIrrelavantAction(AlertIgnoreORIrrelavantActionRequest request)
        { 
            //var result = _setAlertSpecificStatusValidator.Validate(request);

            //if (!result.IsValid)
            //{
            //    BuildValiationMessage(result, ref response);
            //    return response as SetAlertStatusResponse;

            //}
            return _bl.IgnoreORIrrelavantAction(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Action")]
        public ThreatActionResponse AlertAction(ThreatActionRequest request)
        {
            return _bl.AlertAction(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("MitigateAction")]
        public MitigateActionResponse MitigateAction(MitigateActionRequest request)
        {
            return _bl.AlertMitigateAction(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Agent/ConnectToNetwork")]

        public AddToNetworkResponse ConnectToNetwork(AddToNetworkRequest request)
        {
    
            return _bl.AddToNetwork(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Agent/DisConnectFromNetwork")]
        public DisconnectFromNetworkResponse DisconnectFromNetwork(DisconnectFromNetworkRequest request)
        {
            return _bl.DisconnectFromNetwork(request);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("BlokckedList")]
        public BlockListResponse GetBlockList(BlockListRequest request)
        {
            return _bl.GetBlockList(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threats/AddToblockList")]
        public AddToBlocklistResponse AddToblockListForThreats(AddToBlocklistForThreatsRequest request)
        {
             return _bl.AddToblockListForThreats(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddToblockList")]

        public AddToBlocklistResponse AddToblockList(AddToBlocklistRequest request)
        {
            return _bl.AddToblockList(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("blockedListItem/Update")]

        public AddToBlocklistResponse UpdateAddToblockList(UpdateAddToBlocklistRequest request)
        {
            return _bl.UpdateAddToblockList(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("blockedListItem/Delete")]

        public AddToBlocklistResponse DeleteAddToblockList(DeleteAddToBlocklistRequest request)
        {
            return _bl.DeleteAddToblockList(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Exclusion/List")]
        public ExclustionsResponse GetExclusions(ExclusionRequest request)
        {
            return _bl.GetExclusions(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddToExclusionList")]
        public AddToExclusionlistResponse AddToExclusionList(AddToExclusionRequest request)
        {
  
            return _bl.AddToExclusionList(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ExcludedListItem/Update")]
        public AddToExclusionlistResponse UpdateAddToExclusionList(UpdateAddToExclusionRequest request)
        {

            return _bl.UpdateAddToExclusionList(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ExcludedListItem/Delete")]
        public AddToExclusionlistResponse DeleteAddToExclusionList(DeleteAddToExclusionRequest request)
        {

            return _bl.DeleteAddToExclusionList(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Notes/Add")]

        public AddAlertNotesResponse AddAlertNote( AddAlertNoteRequest request)
        {
             return _bl.AddAlertNote(request  , null ,Constants.AlertActionType , Constants.AlertAddNoteActionId , Constants.AlertAddNoteActionName);
        }

        private void BuildValiationMessage(ValidationResult result, ref baseResponse validationresponse)
        {
            validationresponse.IsSuccess = false;
            validationresponse.Message = "Validation Error";
            validationresponse.HttpStatusCode = HttpStatusCode.BadRequest;
            validationresponse.errors = result.Errors.Select(e => e.ErrorMessage.ToString()).ToList();

        }

        #region unUsedMethods
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SetAlertStatus")]
        [NonAction]
        public SetAlertStatusResponse SetAlertStatus(SetAlertStatusRequest request)
        {
            baseResponse response = new SetAlertStatusResponse();

            var result = _setAlertStatusValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SetAlertStatusResponse;

            }
            return _bl.SetAlertStatus(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SetAlertEscalationStatus")]
        public SetAlertStatusResponse SetAlertEscalationStatus(SetAlertSpecificStatusRequest request)
        {
            baseResponse response = new SetAlertStatusResponse();

            var result = _setAlertSpecificStatusValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SetAlertStatusResponse;

            }
            return _bl.SetAlertEscalationStatus(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SetAlertIrrelavantStatus")]
        public SetAlertStatusResponse SetAlertIrrelavantStatus(SetAlertSpecificStatusRequest request)
        {
            baseResponse response = new SetAlertStatusResponse();

            var result = _setAlertSpecificStatusValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SetAlertStatusResponse;

            }
            return _bl.SetAlertIrrelavantStatus(request);

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SetAlertPriority")]
        public SetAlertPriorityResponse SetAlertPriority(SetAlertPriorityRequest request)
        {
            return _bl.SetAlertPriority(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SetAlertSevirity")]
        public SetAlertSevirityResponse SetAlertSevirity(SetAlertSevirityRequest request)
        {
            baseResponse response = new SetAlertSevirityResponse();

            var result = _setSevirityValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as SetAlertSevirityResponse;

            }
            return _bl.SetAlertSevirity(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AssignAlertTag")]
        public AssignAlertTagResponse AssignAlertTag(AssignAlertTagsRequest request)
        {
            baseResponse response = new AssignAlertTagResponse();

            var result = _assignAlertTagsValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as AssignAlertTagResponse;

            }

            return _bl.AssignAlertTag(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AssignAlertScores")]
        public AssignAlertScoresResponse AssignAlertScores(AssignAlertScoresRequest request)
        {
            baseResponse response = new AssignAlertScoresResponse();

            var result = _assignAlertScoresValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as AssignAlertScoresResponse;

            }
            return _bl.AssignAlertScores(request);

        }

        //[HttpPost]
        //[MapToApiVersion("1.0")]
        //[Route("SetAlertPositiveAnalysis")]
        //public SetAlertPositiveAnalysisResponse SetAlertPositiveAnalysis(SetAlertPositiveAnalysisRequest request)
        //{
        //    baseResponse response = new SetAlertPositiveAnalysisResponse();

        //    var result = _setAlertPositiveAnalysisValidator.Validate(request);

        //    if (!result.IsValid)
        //    {
        //        BuildValiationMessage(result, ref response);
        //        return response as SetAlertPositiveAnalysisResponse;

        //    }
        //    return _bl.SetAlertPositiveAnalysis(request);
        //}


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Alerts/AssignOwner")]
        public baseResponse AssignOwner(AssignOwnerRequest request)
        {
            baseResponse response = new baseResponse();

            var result = _alertAssignOwnerValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as baseResponse;

            }
            return _bl.AssignOwner(request);
        }


        #endregion unUsedMethods
    }
}
