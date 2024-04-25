using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.Services.Notifications.Teams
{
    public interface ITeamsMessageService
    {
        Task<SendTeamsMessageResponse> SendTeamsMessage(SendTeamsMessageRequest request, SendTeamsMessageConfiguration configData);

    }
}
