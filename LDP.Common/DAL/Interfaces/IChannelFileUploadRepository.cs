using LDP.Common.DAL.Entities;

namespace LDP.Common.DAL.Interfaces
{
    public interface IFileHandlerRepository
    {
        Task<string> AddUploadFileData(ChannelUploadFile requests);
        Task<ChannelUploadFile> GetUploadFileDetails(int fileId);
        Task<ChannelUploadFile> GetUploadFileDetailsByChannelAndFileName(int channelId, string fileName);
        Task<string> DeleteUploadedFileDetails(int fileId);
        Task<List<ChannelUploadFile>> GetUploadFilesListByChannel(int channelId);
        Task<List<ChannelUploadFile>> GetUploadFilesListByChannelSubItem(int subitemId);

    }
}
