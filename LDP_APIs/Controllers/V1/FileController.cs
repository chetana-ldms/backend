using FluentValidation;
using FluentValidation.Results;
using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("FileHandler")]
    public class FileController : ControllerBase
    {
        IFileHandlerBL _bl;

        private IValidator<FileUploadRequest> _fileUploadValidator;

        public FileController(IFileHandlerBL bl, IValidator<FileUploadRequest> fileUploadValidator)
        {
            _bl = bl;
            _fileUploadValidator = fileUploadValidator;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Files/Upload")]
        public FileUploadResponse Upload([FromForm]FileUploadRequest request)
        {
            baseResponse response = new FileUploadResponse();
          

            var result = _fileUploadValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as FileUploadResponse;

            }
            response = _bl.Upload(request);
            return response as FileUploadResponse;

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Files/Download")]
        public IActionResult Download(int fileId)
        {
            var res = _bl.Download(fileId);
            if (res.IsSuccess)
            {
                return File(res.FileContent, res.ContentType, res.FileName);
            }

            return new ObjectResult(res) { StatusCode = (int?)HttpStatusCode.NotFound };


        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Files/Delete")]
        public DeleteFileResponse Delete(int fileId)
        {
            var res = _bl.Delete(fileId);
            return res;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Files/GetUploadedFilesListByChannelId")]
        public GetUploadedFileListResponse GetUploadedFilesListByChannelId(int channelId)
        {
            var res = _bl.GetUploadedFilesListByChannelId(channelId);
            return res;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Files/GetUploadedFilesListByChannelSubItemId")]
        public GetUploadedFileListResponse GetUploadedFilesListByChannelSubItemId(int SubItemId)
        {
            var res = _bl.GetUploadedFilesListByChannelSubItemId(SubItemId);
            return res;
        }

        private void BuildValiationMessage(ValidationResult result, ref baseResponse validationresponse)
        {
            validationresponse.IsSuccess = false;
            validationresponse.Message = "Validation Error";
            validationresponse.HttpStatusCode = HttpStatusCode.BadRequest;
            validationresponse.errors = result.Errors.Select(e => e.ErrorMessage.ToString()).ToList();

        }
    }
        //public class ChannelDownloadFileResponse : baseResponse
        //{
        //    public FileContentResult DownloadFile { get; set; }
        //}

        //private string GetFileNameFromUrl(string url)
        //{
        //    var uri = new Uri(url);
        //    return Path.GetFileName(uri.LocalPath);
        //}

        //[HttpPost]
        //[MapToApiVersion("1.0")]
        //[Route("UploadLargeFile")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        //[MultipartFormData]
        //[DisableFormValueModelBinding]
        //private async Task<IActionResult> UploadLargeFile()
        //{
        //    var fileUploadSummary = await UploadFileAsync(HttpContext.Request.Body, Request.ContentType);

        //    return CreatedAtAction(nameof(UploadLargeFile), fileUploadSummary);


        //}

        //[HttpPost]
        //[MapToApiVersion("1.0")]
        //[Route("Download")]
        //public AddChatMessageResponse AddChatMessages(AddChatMessagesRequest request)
        //{
        //    return _bl.AddChatMessages(request);
        //}
        //private async Task<FileUploadSummary> UploadFileAsync(Stream fileStream, string contentType)
        //{
        //    var fileCount = 0;
        //    long totalSizeInBytes = 0;

        //    var boundary = GetBoundary(MediaTypeHeaderValue.Parse(contentType));
        //    var multipartReader = new MultipartReader(boundary, fileStream);
        //    var section = await multipartReader.ReadNextSectionAsync();

        //    var filePaths = new List<string>();
        //    var notUploadedFiles = new List<string>();

        //    while (section != null)
        //    {
        //        var fileSection = section.AsFileSection();
        //        if (fileSection != null)
        //        {
        //            totalSizeInBytes += await SaveFileAsync(fileSection, filePaths, notUploadedFiles);
        //            fileCount++;
        //        }

        //        section = await multipartReader.ReadNextSectionAsync();
        //    }

        //    return new FileUploadSummary
        //    {
        //        TotalFilesUploaded = fileCount,
        //        TotalSizeUploaded = ConvertSizeToString(totalSizeInBytes),
        //        FilePaths = filePaths,
        //        NotUploadedFiles = notUploadedFiles
        //    };
        //}
        //async Task<long> SaveFileAsync(FileMultipartSection fileSection, IList<string> filePaths, IList<string> notUploadedFiles)
        //{
        //    var extension = Path.GetExtension(fileSection.FileName);
        //    if (!allowedExtensions.Contains(extension))
        //    {
        //        notUploadedFiles.Add(fileSection.FileName);
        //        return 0;
        //    }

        //    Directory.CreateDirectory(UploadsSubDirectory);

        //    var filePath = Path.Combine(UploadsSubDirectory, fileSection?.FileName);

        //    await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024);
        //    await fileSection.FileStream?.CopyToAsync(stream);

        //    filePaths.Add(GetFullFilePath(fileSection));

        //    return fileSection.FileStream.Length;
        //}

    //    private string GetFullFilePath(FileMultipartSection fileSection)
    //    {
    //        return !string.IsNullOrEmpty(fileSection.FileName)
    //            ? Path.Combine(Directory.GetCurrentDirectory(), UploadsSubDirectory, fileSection.FileName)
    //            : string.Empty;
    //    }

    //    private string ConvertSizeToString(long bytes)
    //    {
    //        var fileSize = new decimal(bytes);
    //        var kilobyte = new decimal(1024);
    //        var megabyte = new decimal(1024 * 1024);
    //        var gigabyte = new decimal(1024 * 1024 * 1024);

    //        return fileSize switch
    //        {
    //            _ when fileSize < kilobyte => "Less then 1KB",
    //            _ when fileSize < megabyte =>
    //                $"{Math.Round(fileSize / kilobyte, fileSize < 10 * kilobyte ? 2 : 1, MidpointRounding.AwayFromZero):##,###.##}KB",
    //            _ when fileSize < gigabyte =>
    //                $"{Math.Round(fileSize / megabyte, fileSize < 10 * megabyte ? 2 : 1, MidpointRounding.AwayFromZero):##,###.##}MB",
    //            _ when fileSize >= gigabyte =>
    //                $"{Math.Round(fileSize / gigabyte, fileSize < 10 * gigabyte ? 2 : 1, MidpointRounding.AwayFromZero):##,###.##}GB",
    //            _ => "n/a"
    //        };
    //    }

    //    private string GetBoundary(MediaTypeHeaderValue contentType)
    //    {
    //        var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;

    //        if (string.IsNullOrWhiteSpace(boundary))
    //        {
    //            throw new InvalidDataException("Missing content-type boundary.");
    //        }

    //        return boundary;
    //    }
    //}
    //public class FileUploadSummary
    //{
    //    public int TotalFilesUploaded { get; set; }
    //    public string TotalSizeUploaded { get; set; }
    //    public IList<string> FilePaths { get; set; } = new List<string>();
    //    public IList<string> NotUploadedFiles { get; set; } = new List<string>();
    //}
}
