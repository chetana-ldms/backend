using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP_APIs.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class FileHandlerRepository : IFileHandlerRepository
    {
        
        private readonly ChannelsDataContext? _context;
        public FileHandlerRepository(ChannelsDataContext? context)
        {
          //  _fileuploadrepo = fileuploadrepo;
            _context = context;
        }

        public async Task<string> AddUploadFileData(ChannelUploadFile request)
        {
            _context.vm_channel_upload_files.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the file details";
        }

        public async Task<string> DeleteUploadedFileDetails(int fileId)
        {
            var file = await _context.vm_channel_upload_files.Where(c => c.file_id == fileId).AsNoTracking().FirstOrDefaultAsync();
            if (file == null)
            {
                return "File details not found ";
            }
            _context.vm_channel_upload_files.Remove(file);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to remove the file";
        }

        public async  Task<ChannelUploadFile> GetUploadFileDetails(int fileId)
        {
            var file = await _context.vm_channel_upload_files.Where(c => c.file_id == fileId).AsNoTracking().FirstOrDefaultAsync();
            return file;

        }

        public async Task<ChannelUploadFile> GetUploadFileDetailsByChannelAndFileName(int channelId,string fileName)
        {
            var file = await _context.vm_channel_upload_files.Where(c => c.channel_id == channelId
            && c.file_name.ToLower() == fileName.ToLower()).AsNoTracking().FirstOrDefaultAsync();
            return file;

        }
        public async Task<List<ChannelUploadFile>> GetUploadFilesListByChannel(int channelId)
        {
            var files = await _context.vm_channel_upload_files.Where(c => c.channel_id == channelId).AsNoTracking().ToListAsync();
            return files;
        }

        public async Task<List<ChannelUploadFile>> GetUploadFilesListByChannelSubItem(int subitemId)
        {
            var files = await _context.vm_channel_upload_files.Where(c => c.subitem_id == subitemId).AsNoTracking().ToListAsync();
            return files;
        }
    }
}
