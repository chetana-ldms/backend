using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class GetChannelsResponse:baseResponse
    {
        public List<GetChannelModel> ChannelsData { get; set; }
    }
    public class GetChannelResponse : baseResponse
    {
        public GetChannelModel ChannelsData { get; set; }
    }
    public class ChannelResponse : baseResponse
    {
    }
}
