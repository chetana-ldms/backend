namespace LDP.Common.Model
{
    public class ChannelModelcommon
    {

        public string? ChannelName { get; set; }
        public string? ChannelDescription { get; set; }
        public int DisplayOrder { get; set; }
        public int OrgId { get; set; }

        public string? MsTeamsTeamsId { get; set; }

        public string? MsTeamsChannelId { get; set; }

    }

    public class AddChannelModel : ChannelModelcommon
    {

        public DateTime? CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
     
    }

    public class UpdateChannelModel : ChannelModelcommon
    {

        public int ChannelId { get; set; }

        public int ChannelTypeId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ModifiedUserId { get; set; }
    }

    public class GetChannelModel : ChannelModelcommon
    {

        public int ChannelId { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
        public int Active { get; set; }
        public int ChannelTypeId { get; set; }
        public string? ChannelTypeName { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string? DeletedUser { get; set; }
        
    }

}


