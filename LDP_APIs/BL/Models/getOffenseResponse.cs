using LDP_APIs.BL.Models;

namespace LDP_APIs.Models
{
    public class getOffenseResponse:baseResponse
    {
        
        public IEnumerable<QRadaroffense>? offensesList { get; set; }
        public int OrgId { get; set; }
        public int  ToolId { get; set; }
    }

    public class getAlertsResponse : baseResponse
    {
        public double TotalOffenseCount { get; set; }
        public IEnumerable<AlertModel>? AlertsList { get; set; }
    }

    public class getAlertResponse : baseResponse
    {
           public IEnumerable<AlertModel>? AlertsList { get; set; }
    }
}
