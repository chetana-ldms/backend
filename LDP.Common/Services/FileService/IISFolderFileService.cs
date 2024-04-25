using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.APIResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Twilio.Http;

namespace LDP.Common.Services.FileService
{
    public class IISFolderFileService : IFileHandlerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IISFolderFileService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DeleteFileResponse> Delete(ChannelUploadFile request)
        {
            DeleteFileResponse response = new DeleteFileResponse();
            try
            {
                System.IO.File.Delete(request.file_physical_path);
                //using (HttpClient client = new HttpClient())
                //{
                //    Uri requestUri = new Uri(fileUrl, UriKind.Absolute);

                //    HttpResponseMessage apiCalRresponse = await client.DeleteAsync(requestUri);
                //    if (apiCalRresponse.IsSuccessStatusCode) 
                //    {
                response.IsSuccess = true;
                response.Message = "File delete success";
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                //    }
                //    else
                //    {
                //        response.IsSuccess = false;
                //        string errorContent = await apiCalRresponse.Content.ReadAsStringAsync();
                //        ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                //        response.Message = errorResponse.ErrorMessage;
                //        response.HttpStatusCode = apiCalRresponse.StatusCode;

                //    }

                //}
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;

            }

            return response;
        }

        public async Task<FileDownloadResponse> Download(ChannelUploadFile request )
        {
           
            string tracepoint = string.Empty;
            FileDownloadResponse fileControllerResponse = new FileDownloadResponse();
            try
            {
                
                string physicalPath = request.file_physical_path;
                if (System.IO.File.Exists(physicalPath))
                {
                    var fileName = Path.GetFileName(physicalPath);
                    var fileBytes = System.IO.File.ReadAllBytes(physicalPath);
                    var mimeType = "application/octet-stream"; // Set the appropriate content type for your file
                    fileControllerResponse.FileContent = fileBytes;
           
                    fileControllerResponse.ContentType = mimeType;
                    fileControllerResponse.FileName = fileName;
                    fileControllerResponse.IsSuccess = true;

                }

                    else
                    {
                        fileControllerResponse.IsSuccess = false;
                    }
            }
            catch (Exception ex )
            {
                fileControllerResponse.errors = new List<string> { ex.Message };
            }
            finally
            {
            }
            return fileControllerResponse;
        }
        public async Task<FileDownloadResponse> Download(string fileUrl  , string physicalPath)
        {

            string tracepoint = string.Empty;
            FileDownloadResponse fileControllerResponse = new FileDownloadResponse();
            if (System.IO.File.Exists(physicalPath))
            {
                var fileName = Path.GetFileName(physicalPath);
                var fileBytes = System.IO.File.ReadAllBytes(physicalPath);
                var mimeType = "application/octet-stream"; // Set the appropriate content type for your file
                fileControllerResponse.FileContent = fileBytes;

                fileControllerResponse.ContentType = mimeType;
                fileControllerResponse.FileName = fileName;
                fileControllerResponse.IsSuccess = true;
            }
            else
            {
                fileControllerResponse.IsSuccess = false;
                fileControllerResponse.Message = "File not found";
            }
            return fileControllerResponse;
        }

        private string GetFileNameFromUrl(string url)
        {
            var uri = new Uri(url);
            return Path.GetFileName(uri.LocalPath);
        }

        public async Task<FileUploadResponse> Upload(FileUploadRequest request, string channelName , string channelSubItemName)
        {
            FileUploadResponse response = new FileUploadResponse();
            var file = request.File;
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, Constants.LDC_Upload_FolderPath));
                    if (!string.IsNullOrEmpty(channelName))
                    {
                        path = path + "\\" + channelName;
                    }
                    if (!string.IsNullOrEmpty(channelSubItemName))
                    {
                        path = path + "\\" + channelSubItemName;
                    }
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}";
                    string fileUrl = string.Empty;
                    if (!string.IsNullOrEmpty(channelName))
                    {
                        fileUrl = $"{baseUrl}/{Constants.LDC_Upload_FolderPath}/{channelName}/{file.FileName}";
                    }
                    if (!string.IsNullOrEmpty(channelSubItemName))
                    {
                        fileUrl = $"{baseUrl}/{Constants.LDC_Upload_FolderPath}/{channelName}/{channelSubItemName}/{file.FileName}";
                    }
                    response.IsSuccess = true;
                    response.FileURL = fileUrl;
                    response.PhysicalFilePath = Path.Combine(path, file.FileName);
                    response.FileName = file.FileName;
                    response.Message = "File upload success";
                    response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "File not found in request";
                    response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Application error";
                response.errors = new List<string>() { ex.Message };
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }
        public string BuildUploadFolderRelativePath(List<string> folderNamesList)
        {
            string _relativePath = string.Empty;
            if (folderNamesList.Count > 0)
            {
                foreach(string folderName in folderNamesList)
                {
                    if (string.IsNullOrEmpty(folderName)) {  continue; }    
                    _relativePath = _relativePath + "\\" + folderName;
                }
                _relativePath = _relativePath.Substring(1);
            }

            return _relativePath;
        }
        public async Task<FileUploadResponse> Upload(IFormFile uploadFile, string uploadFolderRelativePath)
        {
            FileUploadResponse response = new FileUploadResponse();
            var file = uploadFile;
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, uploadFolderRelativePath));
               
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}";
                    var  fileUrl = $"{baseUrl}/{uploadFolderRelativePath}/{file.FileName}";
                    fileUrl=fileUrl.Replace("\\", "/");
                    response.IsSuccess = true;
                    response.FileURL = fileUrl;
                    response.PhysicalFilePath = Path.Combine(path, file.FileName);
                    response.FileName = file.FileName;
                    response.Message = "File upload success";
                    response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "File not found in request";
                    response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Application error";
                response.errors = new List<string>() { ex.Message };
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }
    }
}
