using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{

    public class ChannelSubItemsResponse : baseResponse
    {
       
    }
    public class GetChannelSubItemsResponse:baseResponse
    {
        public List<GetChannelSubItemModel>? ChannelSubItems { get; set; }
    }

    public class GetChannelSubItemResponse : baseResponse
    {
        public GetChannelSubItemModel? ChannelSubItem { get; set; }
    }
}
