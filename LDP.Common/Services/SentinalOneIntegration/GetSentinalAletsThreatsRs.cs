using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration
{
    public class GetSentinalThreatAlertsResponse:baseResponse
    {
        public List<AlertModel>? Alerts { get; set; }

        public int OrgId { get; set; }

        public int ToolId { get; set; }
    }

    public class MitigateActionResponse : baseResponse
    {
       
    }
    public class ThreatActionResponse : baseResponse
    {

    }
    public class GetThreatTimelineResponse : baseResponse
    {
        public  GetThreatTimeline ThreatTimeline { get; set; }  
    }

    public class GetAlertTimelineResponse : baseResponse
    {
        public List<ThreatTimeLine> ThreatTimeLines { get; set; }
    }
    public class UpdateThreatAnalysisVerdictResponse : baseResponse
    {

    }
    public class UpdateThreatDetailsResponse : baseResponse
    {

    }

    public class GetThreatNoteServiceResponse : baseResponse
    {
        public GetThreatNote ThreatNotes { get; set; }
    }

    public class GetThreatNoteResponse : baseResponse
    {
        public List<AlertNoteModel> ThreatNotes { get; set; }
    }

    public class AddThreatNoteResponse : baseResponse
    {

    }

    public class AddToNetworkResponse : baseResponse
    {

    }

    public class DisconnectFromNetworkResponse : baseResponse
    {

    }

    public class AddToBlocklistResponse : baseResponse
    {

    }

    public class AddToExclusionlistResponse : baseResponse
    {

    }


}
