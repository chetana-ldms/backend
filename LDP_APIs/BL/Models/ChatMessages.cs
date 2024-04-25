namespace LDP_APIs.APIResponse
{
    

    public class ChatMessagesCommon
    {
        public int OrgId { get; set; }
        public int FromUserID { get; set; }
        public int ToUserID { get; set; }
        public string? ChatMessage { get; set; }
        public string? ChatSubject { get; set; }
        public int SubjectRefID { get; set; }

        //public int ChannelId { get; set; }


    }

    public class AddChatMessageModel: ChatMessagesCommon
    {

       public DateTime? MesssageDate { get; set; }

    }
    public class GetChatMessageHistoryModel : ChatMessagesCommon
    {
        public double ChatId { get; set; }

        public DateTime? MesssageDate { get; set; }

        public string? FromUserName { get; set; }

        public string? ToUserName { get; set; }

        public string? MessageType { get; set; }

        public string? AttachmentUrl { get; set; }

        public string? AttachmentPhysicalPath { get; set; }


    }

}
    
