using LDP_APIs.BL.Models;

namespace LDP_APIs.Models
{
    public class getOffenseResponse:baseResponse
    {
        
        public List<QRadaroffense>? offensesList { get; set; }
        public int OrgId { get; set; }
        public int  ToolId { get; set; }
    }

    public class getAlertsResponse : baseResponse
    {
        public double TotalOffenseCount { get; set; }
        public List<AlertModel>? AlertsList { get; set; }

        public List<string>? Source { get; set; }
    }

    public class getAlertResponse : baseResponse
    {
       public IEnumerable<AlertModel>? AlertsList { get; set; }
    }

    public class UpdateAutomationStatusResponse : baseResponse
    {
       
    }

    public class getUnattendedAlertcountResponse : baseResponse
    {
        public double? UnattendedAlertsCount { get; set; }
    }

    public class SetAlertStatusResponse : baseResponse
    {

    }

    public class SetAlertPriorityResponse : baseResponse
    {

    }

    public class SetAlertSevirityResponse : baseResponse
    {

    }

    public class AssignAlertTagResponse : baseResponse
    {

    }

    public class AssignAlertScoresResponse : baseResponse
    {

    }

    public class GetAlertsCountByPriorityAndStatusResponse : baseResponse
    {
        public double? AlertsCount { get; set; }
    }

    public class GetFalsePositiveAlertsCountResponse : baseResponse
    {
        public double? AlertsCount { get; set; }
    }

    public class SetAlertPositiveAnalysisResponse : baseResponse
    {
        
    }

    public class GetAlertsResolvedMeanTimeResponse : baseResponse
    {
        public string? AlertsResolvedMeanTime { get; set; }
    }

    public class GetAlertsMostUsedTagsResponse : baseResponse
    {
        public List<string> MostUsedTags { get; set; }
    }


    public class GetAlertsTrendDataResponse : baseResponse
    {
        public List<AlertTrendData>? AlertsTrendDatas { get; set; }
    }

    public class AlertTrendData
    {
        public int TrendHours { get; set; }
        public int AlertsCount { get; set; }

    }

    public class UpdateAlertResponse : baseResponse
    {
        
    }
    public class SetAnalystVerdictResponse : baseResponse
    {

    }

    public class AlertEscalateActionResponse : baseResponse
    {

    }

    public class AlertIgnoreORIrrelavantActionRespnse : baseResponse
    {

    }

    public class AddAlertNotesResponse : baseResponse
    {

    }


}
