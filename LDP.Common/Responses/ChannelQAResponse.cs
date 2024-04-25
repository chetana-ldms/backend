using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class ChannelQAResponse:baseResponse
    {
    }

    public class GetChannelsQAResponse : baseResponse
    {
        public List<GetChannelQACombinedModel>? ChannelQAList { get; set; }
    }
    
    public class GetChannelQAResponse : baseResponse
    {
        public GetChannelQACombinedModel? ChannelQAData { get; set; }
    }

    public class GetChannelAnswerResponse : baseResponse
    {
        public GetChannelAnswerDetailsModel? ChannelAnswerData { get; set; }
    }

    public class GetTeamsResponse : baseResponse 
    {
        public List<MsTeamModel>? Teams { get; set; }
    }
    public class GetTeamResponse : baseResponse
    {
        public MsTeamModel? TeamData { get; set; }
    }
    public class TeamResponse : baseResponse
    {
    }
}
