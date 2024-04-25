using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IFileHandlerBL
    {
        FileUploadResponse Upload(FileUploadRequest request);
        FileDownloadResponse Download(int fileId);
        DeleteFileResponse Delete(int fileId);

        GetUploadedFileListResponse GetUploadedFilesListByChannelId(int channelId);

        GetUploadedFileListResponse GetUploadedFilesListByChannelSubItemId(int SubItemId);

    }
}
