namespace LDP.Common.Requests
{
    public class TeamsServiceBaseRequest
    {
        public string?  TenantId { get; set; }
        public string? ClientId { get; set; }

        public string? ClientSecret { get; set; }

        public string? GetTokenGraphUrl { get; set; }

        public string? GraphOperationUrl { get; set; }

      
       
    }
    public class TeamsCreatechannelServiceRequest: TeamsServiceBaseRequest
    {

        public string?  MSTeamsId { get; set; }

        public string? MSTeamsChannelId { get; set; }

        public string? ChannelName { get; set; }

        public string? ChanneDescription { get; set; }

        public string? MembershipType { get; set; }
    }

    public class TeamscreateChannelRequest
    {
        public int TeamsId { get; set; }

        public int ChannelId { get; set; }

       // public string? MembershipType { get; set; }
    }
  

}
