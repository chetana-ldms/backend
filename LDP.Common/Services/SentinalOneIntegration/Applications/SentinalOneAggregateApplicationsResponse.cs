using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    public class SentinalOneAggregateApplicationsResponse : baseResponse
    {
        public List<SentinalOneAggregatedApplicationData> ApplicationDatas { get; set; }
    }
}
