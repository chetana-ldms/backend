using LDP_APIs.Models;
using System.ComponentModel;

namespace LDP_APIs.BL.APIRequests
{
    public class AlertBaseCommonRequest
    {
        //public int alertID { get; set; }
      
       // public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }

        public int OrgId { get; set; }


    }

    public class AlertBaseRequest: AlertBaseCommonRequest
    {
        public int AlertID { get; set; }

        //public string? ModifiedUser { get; set; }
        //public DateTime? ModifiedDate { get; set; }

        //public int ModifiedUserId { get; set; }

        //public int OrgId { get; set; }


    }
    public class AssignOwnerRequest :AlertBaseRequest
    {
        public int UserID { get; set; }

        public string? UserName { get; set; }
    }

    public class GetAlertRequest
    {
        public double alertID { get; set; }
  
    }

    public class GetAlertByAssignedOwnerRequest: GetOffenseRequest
    {
        public int UserID { get; set; }
        public List<int>? StatusIDs { get; set; }

   }

    public class GetAlertByAutomationStatusRequest : GetOffenseRequest
    {
        public List<string>? AutomationStatusList { get; set; }

    }

    public class UpdateAutomationStatusRequest: AlertBaseRequest
    {
        public string? Status { get; set; }
        //public int AlertID { get; set; }
        //public int ModifiedUserId { get; set; }
        //public int OrgId { get; set; }
    }

    public class AnalyzeAlertsForAutomationRequest : PagingRequest
    {
        public List<string>? AutomationStatusList { get; set; }

    }
    

    public class PlayBookProcessActionRequest : PagingRequest
    {
        public List<string>? AutomationStatusList { get; set; }

    }

    public class GetUnAttendedAlertCount : baseRequest
    {
        public int? UserID { get; set; }
        public double? NumberofDays { get; set; }
    }

    public class SetAlertStatusCommonRequest: AlertBaseCommonRequest
    {
        public int StatusID { get; set; }

        public string? StatusName { get; set; }
        //public string? ModifiedUser { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        public int OwnerID { get; set; }
        public string? OwnerName { get; set; }
        public string? Notes { get; set; }

    }
    public class SetAlertStatusRequest: SetAlertStatusCommonRequest
    {
        public string? ModifiedUser { get; set; }
        public int alertID { get; set; }
    }

    public class SetMultipleAlertStatusRequest: SetAlertStatusCommonRequest
    {
        public string? ModifiedUser { get; set; }
        public List<double>? alertIDs { get; set; }
    }

    public class SetAlertSpecificStatusRequest: AlertBaseCommonRequest
    {
        public List<double>? alertIDs { get; set; }
       // public string? ModifiedUser { get; set; }
        //public DateTime? ModifiedDate { get; set; }

        public int OwnerID { get; set; }
        //public string? OwnerName { get; set; }
        public string? Notes { get; set; }

    }
    public class SetAlertPriorityRequest:AlertBaseRequest
    {
        public int PriorityID { get; set; }

        public string? PriorityValue { get; set; }
  
    }

    public class AssignAlertTagsRequest: AlertBaseRequest
    {
        public int TagID   { get; set; }
        public string? TagText { get; set; }
    }

    public class AssignAlertScoresRequest: AlertBaseRequest
    {
        //public int ScoreID { get; set; }
        public string? Score { get; set; }
    }

    public class SetAlertSevirityRequest: AlertBaseRequest
    {
        public int SevirityId { get; set; }
        public string? Sevirity { get; set; }
    }

    public class GetAlertsCountByPriorityAndStatusRequest : baseRequest
    {
        public int? UserID { get; set; }
        public double? NumberofDays { get; set; }

        public int? PriorityID { get; set; }

        public int? StatusID { get; set; }

    }

    

    public class GetAlertsCountByPositiveAnalysisRequest : baseRequest
    {
        public int? UserID { get; set; }
        public double? NumberofDays { get; set; }

        public int? PositiveAnalysisID { get; set; }

    }

    public class SetAlertPositiveAnalysisRequest:AlertBaseRequest
    {
        public int PositiveAnalysisID { get; set; }
        public string? PositiveAnalysisValue { get; set; }
    }

    public class GetAlertsResolvedMeanTimeRequest : baseRequest
    {
        public int? UserID { get; set; }
        public double? NumberofDays { get; set; }


    }

    public class GetAlertsMostUsedTagsRequest : baseRequest
    {
        public int? UserID { get; set; }
        public double? NumberofDays { get; set; }


    }

    public class GetAlertsTrendDatasRequest : baseRequest
    {
        public int? UserID { get; set; }

    }

    public class GetAlertsByAlertIds : baseRequest
    {
        public List<double> AlertIds { get; set; }

    }

    public class UpdateAlertRequest : baseRequest
    {
        public double AlertId { get; set; }

        public int StatusId { get; set; }

        public int PriorityId { get; set; }

        public int SeverityId { get; set; }

        public string?  Score { get; set; }

        public int ObservableTagId { get; set; }

        public int OwnerUserId { get; set; }

        public string?  AlertNote { get; set; }

        public int ModifiedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int AnalystVerdictId { get; set; }

    }

    public class SetAnalystVerdictRequest
    {
        public int OrgID { get; set; }
        public List<double>? AlertIds { get; set; }

        public int AnalystVerdictId { get; set; } 
        
        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }
    }

    public class SetAnalystVerdictDTO: SetAnalystVerdictRequest
    {
        public string? AnalystVerdictValue { get; set; }

        public String? ModifiedUser { get; set; }
    }

    public class UpdateAlertStatusRequest
    {
        public int OrgID { get; set; }
        public List<double>? AlertIds { get; set; }

        public int StatusId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }
    }

    public class SetMultipleAlertsStatusDTO : UpdateAlertStatusRequest
    {
        public string? StatusName { get; set; }

        public String? ModifiedUser { get; set; }
    }

    public class AlertEscalateActionRequest : AlertBaseCommonRequest
    {
        public List<double>? alertIDs { get; set; }
        public int OwnerUserId { get; set; }
        public string? Notes { get; set; }

    }

    public class AlertIgnoreORIrrelavantActionRequest : AlertBaseCommonRequest
    {
        public List<double>? alertIDs { get; set; }
        // public int OwnerUserId { get; set; }
        [DefaultValue("")]
        public string? Notes { get; set; }

    }

    public class AddAlertNoteRequest : baseRequest
    {
        public List<double>? alertIDs { get; set; }
        // public int OwnerUserId { get; set; }
        [DefaultValue("")]
        public string? Notes { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }
    }
}
