using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP.Common.Services.SentinalOneIntegration;
using LDP.Common.Services.SentinalOneIntegration.Sentinel;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.Models;

namespace LDP_APIs.BL.Interfaces
{
    public interface IAlertsBL
    {
       getAlertsResponse GetAlerts(GetAlertsRequest request);

        getAlertsResponse GetalertsByAlertIds(List<double> request);
        baseResponse AssignOwner(AssignOwnerRequest request);

        getAlertResponse GetAlertData(GetAlertRequest request);

        GetThreatDetailsResponse GetAlertDetailsFromTool(double alertId);

        getAlertResponse GetAlertDataFromTool(GetAlertRequest request);

        getAlertsResponse GetAlertsByAssignedUser(GetAlertByAssignedOwnerRequest request);

        getAlertsResponse GetAlertDataByAutomationStatus(GetAlertByAutomationStatusRequest request);

         UpdateAutomationStatusResponse UpdateAlertAutomationStatus(UpdateAutomationStatusRequest request);

        getUnattendedAlertcountResponse GetUnAttendedAlertsCount(GetUnAttendedAlertCount request);

        SetAlertStatusResponse SetAlertStatus(SetAlertStatusRequest request);

        SetAlertStatusResponse UpdateAlertStatus(UpdateAlertStatusRequest request);

        

        SetAlertStatusResponse SetAlertEscalationStatus(SetAlertSpecificStatusRequest request);

        SetAlertStatusResponse SetAlertIrrelavantStatus(SetAlertSpecificStatusRequest request);


        SetAlertPriorityResponse SetAlertPriority(SetAlertPriorityRequest request);

        SetAlertSevirityResponse SetAlertSevirity(SetAlertSevirityRequest request);

        AssignAlertTagResponse AssignAlertTag(AssignAlertTagsRequest request);

        AssignAlertScoresResponse AssignAlertScores(AssignAlertScoresRequest request);

        GetAlertsCountByPriorityAndStatusResponse GetAlertsCountByPriorityAndStatus(GetAlertsCountByPriorityAndStatusRequest request);

        GetFalsePositiveAlertsCountResponse GetFalsePositiveAlertsCount(GetAlertsCountByPositiveAnalysisRequest request);

        SetAlertPositiveAnalysisResponse SetAlertPositiveAnalysis(SetAlertPositiveAnalysisRequest request);

        GetAlertsResolvedMeanTimeResponse GetAlertsResolvedMeanTime(GetAlertsResolvedMeanTimeRequest request);

        GetAlertsMostUsedTagsResponse GetAlertsMostUsedTags(GetAlertsMostUsedTagsRequest request);

        GetAlertsTrendDataResponse GetAlertsTrendData(GetAlertsTrendDatasRequest request);

        GetAlertNoteResponse GetAlertNotesByAlertID(GetAlertNoteRequest request);

        string UpdateAlertIncidentMappingId(List<double> alertIds, int AlertMapId);

        UpdateAlertResponse UpdateAlert(UpdateAlertRequest request);

        SetAnalystVerdictResponse SetAnalystVerdict(SetAnalystVerdictRequest request);

        AlertEscalateActionResponse AlertEscalateAction(AlertEscalateActionRequest request);

        AlertIgnoreORIrrelavantActionRespnse IgnoreORIrrelavantAction(AlertIgnoreORIrrelavantActionRequest request);


        MitigateActionResponse AlertMitigateAction(MitigateActionRequest request);

        ThreatActionResponse AlertAction(ThreatActionRequest request);



        AddToNetworkResponse AddToNetwork(AddToNetworkRequest request);

        DisconnectFromNetworkResponse DisconnectFromNetwork(DisconnectFromNetworkRequest request);

        BlockListResponse GetBlockList(BlockListRequest request);
        AddToBlocklistResponse AddToblockListForThreats(AddToBlocklistForThreatsRequest request);

        AddToBlocklistResponse AddToblockList(AddToBlocklistRequest request);

        AddToBlocklistResponse UpdateAddToblockList(UpdateAddToBlocklistRequest request);

        AddToBlocklistResponse DeleteAddToblockList(DeleteAddToBlocklistRequest request);

        ExclustionsResponse GetExclusions(ExclusionRequest request);
        AddToExclusionlistResponse AddToExclusionList(AddToExclusionRequest request);

        AddToExclusionlistResponse UpdateAddToExclusionList(UpdateAddToExclusionRequest request);

        AddToExclusionlistResponse DeleteAddToExclusionList(DeleteAddToExclusionRequest request);

        AddAlertNotesResponse AddAlertNote(AddAlertNoteRequest request , string userName, string actionType, int actionId, string actionName);

    }
}
