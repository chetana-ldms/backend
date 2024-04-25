using Microsoft.Graph.Identity.ApiConnectors.Item.UploadClientCertificate;

namespace LDP_APIs.FileHandler
{
    public interface IFileHandler
    {
        string Upload(FileStream request);
        FileStream Download(string filePath);

        bool CreatFolder(string folderName);

        bool StoreFile(string filePath);

        bool DeleteFile(string filePath);

    }
}
