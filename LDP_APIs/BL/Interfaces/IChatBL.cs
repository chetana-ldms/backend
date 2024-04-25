using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;

namespace LDP_APIs.Interfaces
{
    public interface IChatBL
    {
        GetChatHistoryResponse GetChatHistory(GetChatHistoryRequest request);
        ChatMessageResponse AddChatMessages(AddChatMessagesRequest request);

        ChatMessageResponse AddChatMessage(AddChatMessageRequest request);

        SendChatMessageResponse SendChatMessage(UploadChatAttachmentRequest request);

        FileDownloadResponse DownloadChatAttachment(DownloadRequest request);
        FileDownloadResponse DownloadChatAttachmentByChatId(int chatId);


    }
}
