using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class AddChannelSubItemRequest:AddChannelSubItemModel
    {
    }

    public class UpdateChannelSubItemRequest : UpdateChannelSubItemModel
    {
    }

    public class GetChannelSubItemRequest 
    {
    }

    public class DeleteChannelSubItemRequest
    {
        public int ChannelSubItemId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedUserId { get; set; }
    }

    public class GetChannelSubItemsRequest
    {
        public int OrgId { get; set; }
        public int ChannelId { get; set; }
    }
}
