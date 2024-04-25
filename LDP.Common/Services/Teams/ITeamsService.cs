using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.Services.Teams
{
    public interface ITeamsService
    {
        Task<TeamsCreateChannelResponse> CreateChannel(TeamsCreatechannelServiceRequest request);
    }
}
