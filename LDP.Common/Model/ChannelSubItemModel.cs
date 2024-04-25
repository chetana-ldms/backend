namespace LDP.Common.Model
{
    public class CommonChannelSubItemModel
    {
        public int ChannelId { get; set; }
        public string? ChannelSubItemName { get; set; }
        public string? ApiUrl { get; set; }
        public string? DocumentUrl { get; set; }
        public string? ChannelSubItemDescription { get; set; }
    }

    public class AddChannelSubItemModel : CommonChannelSubItemModel
    {
        public DateTime? CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
    }

    public class UpdateChannelSubItemModel : CommonChannelSubItemModel
    {
        public int ChannelSubItemId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ModifiedUserId { get; set; }
    }
    public class GetChannelSubItemModel: CommonChannelSubItemModel
    {
        public int ChannelSubItemId { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
    }

}
