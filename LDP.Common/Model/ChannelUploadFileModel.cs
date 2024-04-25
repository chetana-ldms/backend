namespace LDP.Common.Model
{
    public class FileCommon
    {
        public int OrgId { get; set; }
        public int ChannelId { get; set; }
        public int SubitemId { get; set; }
    }
    public class AddFileUploadFileModel : FileCommon
    {
        public int CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
        public class GetChannelUploadFileModel: FileCommon
    {
        
            public int FileId { get; set; }
    
            public string? FileName { get; set; }
            public string? FileUrl { get; set; }
            public string? FilePhysicalPath { get; set; }
        public int Active { get; set; }
            public DateTime? CreatedDate { get; set; }
           // public DateTime? ModifiedDate { get; set; }
            public string? CreatedUser { get; set; }
           // public string? ModifiedUser { get; set; }
        

    }
}
