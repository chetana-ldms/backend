using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Services.SentinalOneIntegration.Sentinel;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Models;

namespace LDP_APIs.DAL.Interfaces
{
    public interface IAlertsRepository
    {
        Task<string> Addalerts(List<Alerts> request);

        Task<string> UpdateAlert(Alerts request);

        List<Alerts> Getalerts(GetAlertsRequest request , bool isAdmin );

        List<Alerts> GetalertsByAlertIds(List<double> request);
        double GetAlertsDataCount();
        Task<Double> GetAlertsDataCount(GetAlertsRequest request, bool isAdmin);

        Task<string> AssignOwner(AssignOwnerRequest request, string modifiedUserName);
        Task<List<Alerts>> GetAlertData(GetAlertRequest request);

        Task<List<Alerts>> GetAlertsByDevicePKIds(List<double> request, int orgId);


        Task<List<Alerts>> GetAlertsByAssignedUser(GetAlertByAssignedOwnerRequest request);
        Task<double> GetAlertsCountByAssignedUser(GetAlertByAssignedOwnerRequest request);

        Task<List<Alerts>> GetAlertDataByAutomationStatus(GetAlertByAutomationStatusRequest request);

        Task<double> GetAlertsCountByAutomationStatus(GetAlertByAutomationStatusRequest request);
        Task<string> UpdateAlertAutomationStatus(UpdateAutomationStatusRequest request);

        Task<double> GetUnAttendedAlertsCount(GetUnAttendedAlertCount request, int masterdataID);

        Task<string> SetAlertStatus(SetAlertStatusRequest request, string modifiedUserName);

       // Task<string> SetMultipleAlertStatus(SetMultipleAlertStatusRequest request, string modifiedUserName);

        Task<string> SetMultipleAlertsStatus(SetMultipleAlertStatusRequest request, string modifiedUserName);

        Task<string> UpdateAlertStatus(UpdateAlertStatusRequest request, string modifiedUserName,string statusName);

        Task<string> SetAlertPriority(SetAlertPriorityRequest request, string modifiedUserName);

        Task<string> SetAlertSevirity(SetAlertSevirityRequest request, string modifiedUserName);


        Task<string> AssignAlertTag(AssignAlertTagsRequest request, string modifiedUserName);

        Task<string> AssignAlertScores(AssignAlertScoresRequest request, string modifiedUserName);

        Task<double> GetAlertsCountByPriorityAndStatus(GetAlertsCountByPriorityAndStatusRequest request);

        Task<double> GetAlertsCountByPositiveAnalysis(GetAlertsCountByPositiveAnalysisRequest request);

        Task<string> SetAlertPositiveAnalysis(SetAlertPositiveAnalysisRequest request);

        Task<string> SetAnalystVerdict(SetAnalystVerdictDTO request);

        Task<double> GetAlertsResolvedMeanTime(GetAlertsResolvedMeanTimeRequest request);

        Task<List<Alerts>> GetAlertsMostUsedTages(GetAlertsMostUsedTagsRequest request);

        Task<List<Alerts>> GetAlertsTrendData(GetAlertsTrendDatasRequest request, int LastNumberofHours);

        Task<string> AddAlertNote(alert_note request);
        Task<string> AddRangeAlertNote(List<alert_note> request);

        Task<List<alert_note>> GetAlertNotesByAlertID(GetAlertNoteRequest request);

        Task<string> UpdateAlertIncidentMappingId(List<double> alertIds, int alertIncidentMapId);

        Task<string> AlertEscalateAction(AlertEscalateActionRequest request,string ownerUserName ,string modifiedUserName);

        Task<string> IgnoreORIrrelavantAction(AlertIgnoreORIrrelavantActionRequest request, int analystVerdictId, string modifiedUserName);

        Task<string> AddAlertAccountStructureRange(List<AlertsAccountStructure> alertAccountStructureList);

    }
}

