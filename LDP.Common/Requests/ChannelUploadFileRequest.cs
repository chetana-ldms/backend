using LDP.Common.Model;
using Microsoft.AspNetCore.Http;

namespace LDP.Common.Requests
{
    public class FileUploadRequest: AddFileUploadFileModel
    {
        public IFormFile? File { get; set; }
    }
}
