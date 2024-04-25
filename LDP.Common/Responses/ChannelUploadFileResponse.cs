using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class FileUploadResponse : baseResponse
    {
        public string? FileURL { get; set; }
        public string? FileName { get; set; }

        public string? PhysicalFilePath { get; set; }

    }

    public class DBChannelUploadFileResponse : baseResponse
    {
        public int FileId { get; set; }
    }

    public class DeleteFileResponse : baseResponse
    {
        
    }

    public class FileDownloadResponse : baseResponse
    {
        public byte[]? FileContent { get; set; }
        public string? ContentType { get; set; }
        public string? FileName { get; set; }
    }
    
    public class GetUploadedFileListResponse:baseResponse
    {
        public List<GetChannelUploadFileModel> UploadedFileList { get; set; }
    }

}
