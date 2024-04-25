using AutoMapper;
using LDP.Common;
using LDP.Common.BL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Responses;
using LDP.Common.Services.FileService;
using LDP_APIs.APIResponse;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.DAL;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Interfaces;

namespace LDP_APIs.BL
{
    public class ChatBL : IChatBL
    {
        IChatHistoryRepository _repo;
        public readonly IMapper _mapper;
        ILDPSecurityBL _securityBl;
        IFileHandlerService _fileService;
        public ChatBL(IChatHistoryRepository repo, IMapper mapper, ILDPSecurityBL securityBl,  IFileHandlerService fileService)
        {
            _repo = repo;
            _mapper = mapper;
            _securityBl = securityBl;
            _fileService = fileService;
        }
        public GetChatHistoryResponse GetChatHistory(GetChatHistoryRequest request)
        {
            GetChatHistoryResponse blResponse = new GetChatHistoryResponse();

            var res = _repo.GetChatHistory(request).Result;

            if (res.Count == 0 )
            {
                blResponse.IsSuccess = false;
                blResponse.Message = "No data found";
                blResponse.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
             }
            else
            {
                blResponse.IsSuccess = true;
                blResponse.Message = "Chat Messages";
                blResponse.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                var _mappedResponse = _mapper.Map<List<ChatHistory>, List<GetChatMessageHistoryModel>>(res);
                blResponse.ChatHistory = _mappedResponse;
            }
            return blResponse;
        }

        public ChatMessageResponse AddChatMessages(AddChatMessagesRequest request)
        {
            ChatMessageResponse blResponse = new ChatMessageResponse();
            //GetUserResponse userresponse = null;
            HashSet<int> userIds = new HashSet<int>();
            foreach(var msg in request.ChatMessages)
            {
                if (msg.FromUserID>0) userIds.Add(msg.FromUserID);
                if (msg.ToUserID > 0) userIds.Add(msg.ToUserID);

            }
 
            var _mappedResponse = _mapper.Map<List<AddChatMessageModel>, List<ChatHistory>>(request.ChatMessages);
            if (userIds.Count > 0)
            {
                 var users = _securityBl.GetUserbyIds(userIds.ToList()) ;
                if (users.UsersList.Count > 0)
                {
                    foreach (var msg in _mappedResponse)
                    {
                        if (msg.from_user_id > 0)
                        {
                            var fromuser = users.UsersList.Where(u => u.UserID == msg.from_user_id).FirstOrDefault();
                            if (fromuser != null)
                            {
                                msg.from_user_name = fromuser.Name;
                                msg.created_user = fromuser.Name;
                            }
                        }
                        if (msg.to_user_id >0 )
                        {
                            var touser = users.UsersList.Where(u => u.UserID == msg.to_user_id).FirstOrDefault();
                            if (touser != null)
                            {
                                msg.to_user_name = touser.Name;
                            }
                        }
                        msg.message_type = Constants.Chat_Message_Type_Chat_Message;             

                    }
                }
            }

           
             var res = _repo.AddChatMessages(_mappedResponse).Result;
            if (!string.IsNullOrEmpty(res))
            {
                blResponse.IsSuccess = false;
                blResponse.Message = "Failed to add chat messages";
                blResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
            }
            else
            {
               // AddToHistory(_mappedResponse);

                blResponse.IsSuccess = true;
                blResponse.Message = "Chat messages added successfully";
                blResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
                
            }

            return blResponse; 
        }

        public ChatMessageResponse AddChatMessage(AddChatMessageRequest request)
        {
            ChatMessageResponse blResponse = new ChatMessageResponse();


            var _mappedResponse = _mapper.Map<AddChatMessageModel, ChatHistory>(request);
            FillUserData(request, _mappedResponse);
            _mappedResponse.message_type = Constants.Chat_Message_Type_Chat_Message;
            var res = _repo.AddChatMessage(_mappedResponse).Result;

            if (!string.IsNullOrEmpty(res))
            {
                blResponse.IsSuccess = false;
                blResponse.Message = "Failed to add chat message";
                blResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
            }
            else
            {

                blResponse.IsSuccess = true;
                blResponse.Message = "Chat message added successfully";
                blResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;

            }
            return blResponse;
        }

        private void FillUserData(AddChatMessageModel request, ChatHistory _mappedResponse)
        {
            HashSet<int> userIds = new HashSet<int>();

            if (request.FromUserID > 0) userIds.Add(request.FromUserID);
            if (request.ToUserID > 0) userIds.Add(request.ToUserID);

            if (userIds.Count > 0)
            {
                var users = _securityBl.GetUserbyIds(userIds.ToList());
                if (users.UsersList.Count > 0)
                {
                    var fromuser = users.UsersList.Where(u => u.UserID == _mappedResponse.from_user_id).FirstOrDefault();
                    if (fromuser != null)
                    {
                        _mappedResponse.from_user_name = fromuser.Name;
                        _mappedResponse.created_user = fromuser.Name;
                    }
                    var touser = users.UsersList.Where(u => u.UserID == _mappedResponse.to_user_id).FirstOrDefault();
                    if (touser != null)
                    {
                        _mappedResponse.to_user_name = touser.Name;
                    }

                }
            }
        }

        public SendChatMessageResponse SendChatMessage(UploadChatAttachmentRequest request)
        {

            SendChatMessageResponse blResponse = new SendChatMessageResponse();

            // Persist the upload document in physical location 
            List<ChatHistory> chatMessages = new List<ChatHistory>();
            FileUploadResponse uploadfiledata = null;
            
           

            if (request.ChatAttachmentFile != null) 
            {
                var _mappedResponse = _mapper.Map<AddChatMessageModel, ChatHistory>(request);
                FillUserData(request as AddChatMessageModel, _mappedResponse);
                uploadfiledata = UploadAttachment(request);
                if (!uploadfiledata.IsSuccess)
                {
                    blResponse.IsSuccess = false;
                    blResponse.Message = "Failed to send message";
                    blResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    return blResponse;
                }
                _mappedResponse.message_type = Constants.Chat_Message_Type_Attachment;
                _mappedResponse.chat_message = uploadfiledata.FileName;
                _mappedResponse.attachment_physical_path = uploadfiledata.PhysicalFilePath;
                _mappedResponse.attachment_url = uploadfiledata.FileURL;
                chatMessages.Add(_mappedResponse);
            }
            if (!string.IsNullOrEmpty(request.ChatMessage))
            {
                var _mappedResponse = _mapper.Map<AddChatMessageModel, ChatHistory>(request);
                FillUserData(request as AddChatMessageModel, _mappedResponse);
                _mappedResponse.message_type = Constants.Chat_Message_Type_Chat_Message;
                _mappedResponse.chat_message = request.ChatMessage;
                chatMessages.Add(_mappedResponse);
            }
           
            var res = _repo.AddChatMessages(chatMessages).Result;

            if (!string.IsNullOrEmpty(res))
            {
                blResponse.IsSuccess = false;
                blResponse.Message = "Failed to send chat message";
                blResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
            }
            else
            {

                blResponse.IsSuccess = true;
                blResponse.Message = "Message successfully sent";
                blResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
                if (request.ChatAttachmentFile != null)
                {
                    blResponse.FileName = uploadfiledata.FileName;
                    blResponse.PhysicalFilePath = uploadfiledata.PhysicalFilePath;
                    blResponse.FileURL = uploadfiledata.FileURL;
                }
            }
            return blResponse;

        }

        private FileUploadResponse UploadAttachment(UploadChatAttachmentRequest request)
        {
            List<string> foldersList = new List<string>();
            foldersList.Add(Constants.LDC_Upload_FolderPath);
            foldersList.Add(Constants.LDC_Upload_ChatFolderName);
            foldersList.Add(request.ChatSubject);
            foldersList.Add(request.SubjectRefID.ToString());
            var folderspath = _fileService.BuildUploadFolderRelativePath(foldersList);

            var uploadfiledata = _fileService.Upload(request.ChatAttachmentFile, folderspath).Result;
            return uploadfiledata;
        }

        public FileDownloadResponse DownloadChatAttachment(DownloadRequest request)
        {
            return _fileService.Download(request.FileUrl, request.FilePhysicalPath).Result;
        }
        public FileDownloadResponse DownloadChatAttachmentByChatId(int chatId)
        {
            FileDownloadResponse response = new FileDownloadResponse();

            var attachmentDtls = _repo.GetChatHistoryDetails(chatId).Result;
            if (attachmentDtls == null || attachmentDtls.message_type != Constants.Chat_Message_Type_Attachment)
            {
                response.IsSuccess = false;
                response.Message = "Attachment details not found";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            return _fileService.Download(attachmentDtls.attachment_url, attachmentDtls.attachment_physical_path).Result;
        }

        //private void AddToHistory(List<ChatHistory> request)
        //{
        //    List<AlertHistoryModel> alertHistList = new List<AlertHistoryModel>();

        //    AlertHistoryModel historyRequest = null ;
        //    foreach (var chatMsg in request) 
        //    {
        //        historyRequest = new AlertHistoryModel();
        //        if (chatMsg.chat_subject == Constants.chat_message_Incident_Subject)
        //        {
        //            historyRequest.IncidentId = chatMsg.subject_refid;
        //        }
        //        if (chatMsg.chat_subject == Constants.chat_message_alert_Subject)
        //        {
        //            historyRequest.AlertId = chatMsg.subject_refid;
        //        }

        //        historyRequest.CreatedUser = chatMsg.created_user;
        //        historyRequest.HistoryDate = chatMsg.created_date;//.Value.ToUniversalTime();
        //        historyRequest.HistoryDescription = "Conversation:  " + chatMsg.chat_message;
        //        historyRequest.CreatedUserId = chatMsg.from_user_id;
        //        historyRequest.OrgId = chatMsg.org_id;
        //        alertHistList.Add(historyRequest);
        //    }

        //    _history.AddRangealertHistory(alertHistList);
        //}
    }
}
