using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.Services.Notifications.Teams
{
    public interface ITeamsMessageBL
    {
        SendTeamsMessageResponse SendTeamsMessage(SendTeamsMessageRequest request);
    }
}
