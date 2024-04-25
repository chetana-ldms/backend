using LDPChat_Server.Models;

namespace LDPChat_Server.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);

    }
}
