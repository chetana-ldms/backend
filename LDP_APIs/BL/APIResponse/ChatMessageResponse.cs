using LDP_APIs.APIResponse;
using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class ChatMessageResponse : baseResponse
    {
    }

    public class GetChatHistoryResponse : baseResponse
    {
        public List<GetChatMessageHistoryModel>? ChatHistory { get; set; }
    }

    public class SendChatMessageResponse : baseResponse
    {

        public string? FileURL { get; set; }
        public string? FileName { get; set; }

        public string? PhysicalFilePath { get; set; }
    }
}