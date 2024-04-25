using LDP_APIs.APIResponse;

namespace LDP_APIs.BL.APIRequests
{
    public class GetChatHistoryRequest 
    {
        public int OrgId { get; set; }
        public string? Subject { get; set; }

        public int SubjectRefId { get; set; }


    }
    public class AddChatMessageRequest : AddChatMessageModel
    {
        
    }

    public class AddChatMessagesRequest
    {
        public List<AddChatMessageModel>? ChatMessages { get; set; }
    }
    public class UploadChatAttachmentRequest : AddChatMessageModel
    {
        public IFormFile? ChatAttachmentFile {get ; set; }   

    }
    public class DownloadRequest
    {
        public string?  FileUrl { get; set; }
        public string? FilePhysicalPath { get; set; }

    }
}
