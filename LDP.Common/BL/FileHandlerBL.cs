using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP.Common.Services.FileService;
using LDP_APIs.BL.Interfaces;
using System.Net;

namespace LDP.Common.BL
{
    public class FileHandlerBL : IFileHandlerBL
    {
        IFileHandlerRepository _fileHandlerRepo;
        IFileHandlerService _fileHandlerService;
        ILDCChannelBL _channel;
        
        private readonly ILDPSecurityBL _securityBl;
        public readonly IMapper _mapper;
        public FileHandlerBL(IFileHandlerRepository fileHandlerRepo, IFileHandlerService fileHandlerService, IMapper mapper, ILDCChannelBL channel, ILDPSecurityBL securityBl)
        {
            _fileHandlerRepo = fileHandlerRepo;
            _fileHandlerService = fileHandlerService;
            _mapper = mapper;
            _channel = channel;
            _securityBl = securityBl;
        }

        public DeleteFileResponse Delete(int fileId)
        {
            DeleteFileResponse response = new DeleteFileResponse();
            var fileData = _fileHandlerRepo.GetUploadFileDetails(fileId).Result;
            if (fileData==null)
            {
                response.IsSuccess = false;
                response.Message = "File details not found";
                response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
            //var fileUrl = fileData.file_url;
            var deleteFileResponse = _fileHandlerService.Delete(fileData);
            if (deleteFileResponse.Result.IsSuccess)
            {
                var repoResponse = _fileHandlerRepo.DeleteUploadedFileDetails(fileData.file_id);
                if (string.IsNullOrEmpty(repoResponse.Result))
                {
                    response.IsSuccess = true;
                    response.Message = "File deletion success";
                    response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "File deletion failed";
                    response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = deleteFileResponse.Result.Message;
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
            return response;
        }

        public FileDownloadResponse Download(int fileId)
        {
            FileDownloadResponse response = new FileDownloadResponse();
           
                var fileData = _fileHandlerRepo.GetUploadFileDetails(fileId).Result;
                if (fileData == null)
                {
                    response.IsSuccess = false;
                    response.Message = "File details not found";
                    response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                //var fileUrl = fileData.file_url;
                var deleteFileResponse = _fileHandlerService.Download(fileData);
                if (deleteFileResponse.Result.IsSuccess)
                {
                    return deleteFileResponse.Result;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = deleteFileResponse.Result.Message;//"Error while downloading the file";
                    response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    return response;
                }
           
        
        }

        public GetUploadedFileListResponse GetUploadedFilesListByChannelId(int channelId)
        {
            GetUploadedFileListResponse response = new GetUploadedFileListResponse();
            var res = _fileHandlerRepo.GetUploadFilesListByChannel(channelId);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Channel uploaded files details not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<ChannelUploadFile>, List<GetChannelUploadFileModel>>(res.Result);
                response.UploadedFileList = _mappedResponse;
                response.Message = "Success";

            }
            return response;
        }

        public GetUploadedFileListResponse GetUploadedFilesListByChannelSubItemId(int SubItemId)
        {
            GetUploadedFileListResponse response = new GetUploadedFileListResponse();
            var res = _fileHandlerRepo.GetUploadFilesListByChannelSubItem(SubItemId);

            if (res.Result.Count ==0)
            {
                response.IsSuccess = false;
                response.Message = "Channel uploaded files details not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<ChannelUploadFile>, List<GetChannelUploadFileModel>>(res.Result);
                response.UploadedFileList = _mappedResponse;
                response.Message = "Success";

            }
            return response;
        }

        public FileUploadResponse Upload(FileUploadRequest request)
        {
            FileUploadResponse response = new FileUploadResponse();
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {

                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
           var fileDuplicateCheck = _fileHandlerRepo.GetUploadFileDetailsByChannelAndFileName(request.ChannelId, request.File.FileName).Result;
            if (fileDuplicateCheck != null )
            {
                response.IsSuccess = false;
                response.Message = "File alread uploaded";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            string channelName = string.Empty;
            string channelSubItemName = string.Empty;
            var channelResponse = _channel.GetChannelDetails(request.ChannelId);
            if (!channelResponse.IsSuccess)
            {
                response.IsSuccess = false;
                response.Message = "Invalid channel";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;

            }
            channelName = channelResponse.ChannelsData.ChannelName;
            channelName = channelName.Replace(" ", "_");
            if (request.SubitemId>0)
            {
                var subItemResponse = _channel.GetChannelSubitemDetails(request.SubitemId);
                if (!subItemResponse.IsSuccess) 
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid channel sub item";
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                if (subItemResponse.ChannelSubItem.ChannelId != request.ChannelId)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid channel and subitem ";
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                channelSubItemName = subItemResponse.ChannelSubItem.ChannelSubItemName;
                channelSubItemName = channelSubItemName.Replace(" ", "_");
            }
            var fileserviceRes = _fileHandlerService.Upload(request,channelName, channelSubItemName).Result;
            if (!fileserviceRes.IsSuccess)
            {
                response.IsSuccess = false;
                response.Message = fileserviceRes.Message;//"Failed to upload file";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                return response;
            }
            var _fileUploadMappedData = _mapper.Map<FileUploadRequest, ChannelUploadFile>(request);
            _fileUploadMappedData.created_user = userdata.Userdata.Name;
            _fileUploadMappedData.file_name = fileserviceRes.FileName;
            _fileUploadMappedData.file_url = fileserviceRes.FileURL;
            _fileUploadMappedData.file_physical_path = fileserviceRes.PhysicalFilePath;

            var fileuploadrepoResponse = _fileHandlerRepo.AddUploadFileData(_fileUploadMappedData).Result;

            if (string.IsNullOrEmpty(fileuploadrepoResponse))
            {
                response.IsSuccess = true;
                response.Message =  "File upload success";
                response.HttpStatusCode = HttpStatusCode.OK;
                response.FileName = fileserviceRes.FileName;
                response.FileURL = fileserviceRes.FileURL;
                response.PhysicalFilePath = fileserviceRes.PhysicalFilePath;
                return response;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "File upload failed";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                return response;
            }
            
        }
    }
}
