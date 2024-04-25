using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Responses;
using Microsoft.AspNetCore.Http;

namespace LDP.Common.Services.FileService
{
    public interface IFileHandlerService
    {
        Task<FileUploadResponse> Upload(FileUploadRequest request, string channelName, string channelSubItemName);
        Task<FileDownloadResponse> Download(ChannelUploadFile request);
        Task<FileDownloadResponse> Download(string fileUrl, string physicalPath);
        Task<DeleteFileResponse> Delete(ChannelUploadFile request);

        string BuildUploadFolderRelativePath(List<string> folderNamesList);

        Task<FileUploadResponse> Upload(IFormFile uploadFile, string uploadFolderRelativePath);


    }
}
