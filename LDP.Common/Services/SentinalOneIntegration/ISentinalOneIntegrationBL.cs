using LDP.Common.Services.SentinalOneIntegration;

namespace LDP.Common.Services
{
    public interface ISentinalOneIntegrationBL
    {
        GetSentinalThreatAlertsResponse GetThreats(GetSentinalThreatsRequest request);

        //List<SentinalThreatDetails> GetThreatsData(GetSentinalThreatsRequest request);

        MitigateActionResponse ThreatMitigateAction(MitigateActionRequest request);

        ThreatActionResponse ThreatAction(ThreatActionRequest request);

        ThreatActionResponse ThreatAction(int orgId, List<double> alertIds, string actionType);

        GetAlertTimelineResponse GetThreatTimeline(GetThreatTimelineRequest request);

        UpdateThreatAnalysisVerdictResponse UpdateThreatAnalysisVerdict(SentinalOneUpdateAnalystVerdictRequest request);

        UpdateThreatAnalysisVerdictResponse UpdateThreatAnalysisVerdict(int orgId, List<double> alertIds, string analystVerdict);

        UpdateThreatDetailsResponse UpdateThreatDetails(SentinalOneUpdateThreatDetailsequest request);

        UpdateThreatDetailsResponse UpdateThreatDetails(int orgId, List<double> alertIds, string threatStatus, string threatAnalystVerdict);

        GetThreatNoteResponse GetThreatNotes(GetThreatTimelineRequest request);

        AddThreatNoteResponse AddThreatNotes(AddThreatNoteRequest request);

        GetThreatDetailsResponse GetThreatDetails(double alertId);

        GetThreatDetailsResponse GetThreatDetailsFromTool(double alertId);

        AddToNetworkResponse AddToNetwork(AddToNetworkRequest request);

        DisconnectFromNetworkResponse DisconnectFromNetwork(DisconnectFromNetworkRequest request);
        AddToBlocklistResponse AddToblockListForThreats(AddToBlocklistForThreatsRequest request);
        AddToBlocklistResponse AddToblockList(AddToBlocklistRequest request);

        AddToBlocklistResponse UpdateAddToblockList(UpdateAddToBlocklistRequest request);
        AddToBlocklistResponse DeleteAddToblockList(DeleteAddToBlocklistRequest request);
        AddToExclusionlistResponse AddToExclusionList(AddToExclusionRequest request);

        AddToExclusionlistResponse UpdateAddToExclusionList(UpdateAddToExclusionRequest request);

        AddToExclusionlistResponse DeleteAddToExclusionList(DeleteAddToExclusionRequest request);

    }
}
