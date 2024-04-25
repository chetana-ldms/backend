using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IMSTeamsBL
    {
        GetTeamsResponse GetTeamList(int orgId);
        TeamsCreateChannelResponse CreateChannel(TeamscreateChannelRequest request);
    }
}
