using LDP_APIs.APIRequests;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.DAL.Entities;

namespace LDP_APIs.DAL
{
    public interface IChatHistoryRepository
    {
        Task<List<ChatHistory>> GetChatHistory(GetChatHistoryRequest request);
        Task<string>  AddChatMessage(ChatHistory request);

        Task<string> AddChatMessages(List<ChatHistory> request);

        Task<ChatHistory> GetChatHistoryDetails(int chatId);



    }

}
