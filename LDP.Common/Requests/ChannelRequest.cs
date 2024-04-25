using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class AddChannelRequest:AddChannelModel
    {

    }

    public class UpdateChannelRequest : UpdateChannelModel
    {

    }

    public class GetChannelRequest 
    {
        public int OrgId { get; set; }
    }

    public class DeleteChannelRequest
    {
        public int ChannelId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedUserId { get; set; }
    }

    public class UpdateMsTeamsDataRequest
    {
        public int ChannelId { get; set; }
        public string? MsTeamsChannelId { get; set; }

        public string? MsTeamsId { get; set; }


    }
}
