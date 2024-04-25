using LDPChat_Server.Hubs.Clients;
using LDPChat_Server.Models;
using Microsoft.AspNetCore.SignalR;

namespace LDPChat_Server.Hubs
{
    public class ChatHub:Hub<IChatClient>
    {
        public async Task SendMessage(ChatMessage message)
        {
           await Clients.All.ReceiveMessage(message);

        }

    }
}
