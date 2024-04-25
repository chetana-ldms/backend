using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ChatController : ControllerBase
    {
        IChatBL _bl;
        public ChatController(IChatBL bl)
        {
            _bl = bl;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetChatHistory")]
        public GetChatHistoryResponse GetChatHistory(GetChatHistoryRequest request)
        {
           return _bl.GetChatHistory(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddChatMessages")]
        public ChatMessageResponse AddChatMessages(AddChatMessagesRequest request)
        {
            return _bl.AddChatMessages(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddChatMessage")]
        public ChatMessageResponse AddChatMessage(AddChatMessageRequest request)
        {
            return _bl.AddChatMessage(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Chat/SendMessage")]

        public SendChatMessageResponse SendChatMessage([FromForm]UploadChatAttachmentRequest request)
        {
            return _bl.SendChatMessage(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("DownloadAttachment")]
        public IActionResult DownloadChatAttachment(DownloadRequest request)
        {

            var res = _bl.DownloadChatAttachment(request);
            if (res.IsSuccess)
            {
                return File(res.FileContent, res.ContentType, res.FileName);
            }

            return new ObjectResult(res) { StatusCode = (int?)HttpStatusCode.NotFound };
         
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("DownloadAttachmentByChatId")]
        public IActionResult DownloadChatAttachmentByChatId(int chatId)
        {

            var res = _bl.DownloadChatAttachmentByChatId(chatId);
            if (res.IsSuccess)
            {
                return File(res.FileContent, res.ContentType, res.FileName);
            }

            return new ObjectResult(res) { StatusCode = (int?)HttpStatusCode.NotFound };

        }
    }

}
