using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.Interfaces;
using LDPRuleEngine.DAL.Interfaces;
using System.Net;

namespace LDP.Common.BL
{
    public class LDCChannelBL : ILDCChannelBL
    {
        ILDCChannelRespository _channelrepo;
        public readonly IMapper _mapper;
        private readonly ILDPSecurityBL _securityBl;
        private readonly ILDPlattformBL _platformBl;
        public LDCChannelBL(ILDCChannelRespository channelrepo, IMapper mapper, ILDPSecurityBL securityBl, ILDPlattformBL platformBl)
        {
            _channelrepo = channelrepo;
            _mapper = mapper;
            _securityBl = securityBl;
            _platformBl = platformBl;
        }

        public ChannelResponse AddChannel(AddChannelRequest request)
        {
            ChannelResponse response = new ChannelResponse();
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {
               
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            // check the duplicate name validation 
            var duplicateCheckChannel = _channelrepo.GetChannelDetailsByChannelName(request.ChannelName).Result;
            if (duplicateCheckChannel != null )
            {
                response.IsSuccess = false;
                response.Message = "Channel already exists";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
        
            //
            var _mappedRequest = _mapper.Map<AddChannelRequest, LDCChannel>(request);
            _mappedRequest.Created_user = userdata.Userdata.Name;
            _mappedRequest.channel_type_name = Constants.Channel_Type_UnderReview;

            _mappedRequest.channel_type_id = _platformBl.GetMasterDataByDataValue(Constants.Channel_Type, Constants.Channel_Type_UnderReview);
            _mappedRequest.active = 1;

            var res = _channelrepo.AddChannel(_mappedRequest);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "New channel data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }

            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new channel data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
            

            
        }

        public GetChannelsResponse GetAllChannels(int orgId)
        {
            GetChannelsResponse response = new GetChannelsResponse();
            var res = _channelrepo.GetAllChannels(orgId);

            if (res.Result.Count == 0 )
            {
                response.IsSuccess = false;
                response.Message = "LDC channels not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<LDCChannel>, List<GetChannelModel>>(res.Result);
                response.ChannelsData = _mappedResponse;
                response.Message = "Success";

            }
            return response;
        }
        public GetChannelsResponse GetActiveChannels(int orgId)
        {
            GetChannelsResponse response = new GetChannelsResponse();
            var res = _channelrepo.GetActiveChannels(orgId);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "LDC channels not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<LDCChannel>, List<GetChannelModel>>(res.Result);
                response.ChannelsData = _mappedResponse;
                response.Message = "Success";

            }
            return response;
        }
        public ChannelResponse UpdateChannel(UpdateChannelRequest request)
        {
            ChannelResponse response = new ChannelResponse();
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {

                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            // check the duplicate name validation 
            var duplicateCheckChannel = _channelrepo.GetChannelDetailsByUpdateChannelName(request.ChannelName,request.ChannelId).Result;
            if (duplicateCheckChannel != null)
            {
                response.IsSuccess = false;
                response.Message = "Channel already exists";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var channelType = _platformBl.GetMasterDataValueByDataId(Constants.Channel_Type, request.ChannelTypeId);
            if (string.IsNullOrEmpty(channelType))
            {
                response.IsSuccess = false;
                response.Message = "Invalid ChannelType";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _mappedRequest = _mapper.Map<UpdateChannelRequest, LDCChannel>(request);
            _mappedRequest.Modified_user = userdata.Userdata.Name;
            _mappedRequest.channel_type_id = request.ChannelTypeId;
            _mappedRequest.channel_type_name = channelType;

            //if (channelType == Constants.Channel_Type_UnderConstruction || channelType == Constants.Channel_Type_UnderReview
            //    || channelType == Constants.Channel_Type_ReadyToDeployment)
            //{
            //    // if pre active stage --> active = 0 
            //    _mappedRequest.active = 0;
            //}
            //else
            //{
            //    _mappedRequest.active = 1;
            //    // else --> active = 1 
            //    // Teams channel 
            //}

            var res = _channelrepo.UpdateChannel(_mappedRequest);
            
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Channel data updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update the channel data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;

        }
        public GetChannelsResponse GetPreActiveChannels(int orgId)
        {
            GetChannelsResponse response = new GetChannelsResponse();
            var res = _channelrepo.GetPreActiveChannels(orgId);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "LDC channels not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<LDCChannel>, List<GetChannelModel>>(res.Result);
                response.ChannelsData = _mappedResponse;
                response.Message = "Success";

            }
            return response;
        }
       
        public ChannelResponse DeleteChannel(DeleteChannelRequest request)
        {
            ChannelResponse response = new ChannelResponse();
            string _deletedUserName = string.Empty;
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata != null)
            {
                _deletedUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _channelrepo.DeleteChannel(request, _deletedUserName);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Channel deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }
        public GetChannelResponse GetChannelDetails(int channelId)
        {
            GetChannelResponse response = new GetChannelResponse();
            var res = _channelrepo.GetChannelDetails(channelId);

            if (res.Result == null )
            {
                response.IsSuccess = false;
                response.Message = "LDC channel not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<LDCChannel, GetChannelModel>(res.Result);
                response.ChannelsData = _mappedResponse;
                response.Message = "Success";
            }
            return response;
        }

        public ChannelSubItemsResponse AddChannelSubItem(AddChannelSubItemRequest request)
        {
            ChannelSubItemsResponse response = new ChannelSubItemsResponse();
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {

                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var duplicateCheck = _channelrepo.GetChannelSubItemDetailsByName(request.ChannelSubItemName).Result;
             if (duplicateCheck != null)
            {

                response.IsSuccess = false;
                response.Message = "Channel sub item already exist";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _mappedRequest = _mapper.Map<AddChannelSubItemRequest, ChannelSubItem>(request);
            _mappedRequest.Created_user = userdata.Userdata.Name;
         

            var res = _channelrepo.AddChannelSubItem(_mappedRequest);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "New channel sub item data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }

            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new channel sub item data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }

        public ChannelSubItemsResponse UpdateChannelSubItem(UpdateChannelSubItemRequest request)
        {
            ChannelSubItemsResponse response = new ChannelSubItemsResponse();
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {

                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var duplicateCheck = _channelrepo.GetChannelSubItemDetailsByUpdateName(request.ChannelSubItemName,request.ChannelSubItemId).Result;
            if (duplicateCheck != null)
            {

                response.IsSuccess = false;
                response.Message = "Channel sub item already exist";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _mappedRequest = _mapper.Map<UpdateChannelSubItemRequest, ChannelSubItem>(request);
            _mappedRequest.Modified_user = userdata.Userdata.Name;
          

            var res = _channelrepo.UpdateChannelSubItem(_mappedRequest);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Channel sub item data updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update the channel sub item data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;

        }

        
        public GetChannelSubItemsResponse GetChannelsubitemsByOrgChannel(GetChannelSubItemsRequest request)
        {
            GetChannelSubItemsResponse response = new GetChannelSubItemsResponse();
            var res = _channelrepo.GetChannelsubitemsByOrgChannel(request);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "LDC channel sub items not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<ChannelSubItem>, List<GetChannelSubItemModel>>(res.Result);
                response.ChannelSubItems = _mappedResponse;
                response.Message = "Success";

            }
            return response;
        }

        public GetChannelSubItemResponse GetChannelSubitemDetails(int subItemId)
        {
            GetChannelSubItemResponse response = new GetChannelSubItemResponse();
            var res = _channelrepo.GetChannelSubItemDetails(subItemId);

            if (res.Result == null )
            {
                response.IsSuccess = false;
                response.Message = "LDC channel sub item details not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<ChannelSubItem, GetChannelSubItemModel>(res.Result);
                response.ChannelSubItem = _mappedResponse;
                response.Message = "Success";

            }
            return response;
        }

        public ChannelSubItemsResponse DeleteChannelSubItem(DeleteChannelSubItemRequest request)
        {
            ChannelSubItemsResponse response = new ChannelSubItemsResponse();
            string _deletedUserName = string.Empty;
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata != null)
            {
                _deletedUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _channelrepo.DeleteChannelSubItem(request, _deletedUserName);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Channel sub item deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }

        public ChannelQAResponse AddChannelQuestion(AddChannelQuestionRequest request)
        {
            ChannelQAResponse response = new ChannelQAResponse();
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {

                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _mappedRequest = _mapper.Map<AddChannelQuestionRequest, ChannelQA>(request);
            _mappedRequest.created_user = userdata.Userdata.Name;


            var res = _channelrepo.AddChannelQA(_mappedRequest);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "New channel question added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }

            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new channel question";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }

        public ChannelQAResponse AddChannelAnswer(AddChannelAnswerRequest request)
        {
            ChannelQAResponse response = new ChannelQAResponse();
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {

                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _mappedRequest = _mapper.Map<AddChannelAnswerRequest, ChannelQA>(request);
            _mappedRequest.created_user = userdata.Userdata.Name;


            var res = _channelrepo.AddChannelQA(_mappedRequest);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "New channel question added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }

            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new channel question";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }

        public ChannelQAResponse UpdateChannelQuestion(UpdateChannelQuestionRequest request)
        {
            ChannelQAResponse response = new ChannelQAResponse();
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {

                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
           
            var _mappedRequest = _mapper.Map<UpdateChannelQuestionRequest, ChannelQA>(request);
            _mappedRequest.modified_user = userdata.Userdata.Name;


            var res = _channelrepo.UpdateChannelQA(_mappedRequest);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Channel question updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update the channel question";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }
       public  ChannelQAResponse UpdateChannelAnswer(UpdateChannelAnswerRequest request)
        {
            ChannelQAResponse response = new ChannelQAResponse();
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {

                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var _mappedRequest = _mapper.Map<UpdateChannelAnswerRequest, ChannelQA>(request);
            _mappedRequest.modified_user = userdata.Userdata.Name;


            var res = _channelrepo.UpdateChannelQA(_mappedRequest);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Channel answer updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update the channel answer";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }
        public GetChannelsQAResponse GetChannelQuestions(GetChannelQuestionsRequest request)
        {
            GetChannelsQAResponse response = new GetChannelsQAResponse();
            var res = _channelrepo.GetChannelQuestions(request);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "LDC channel sub items not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.ChannelQAList = res.Result;
                response.Message = "Success";

            }
            return response;
        }


        public GetChannelQAResponse GetChannelQuestionDetails(int QuestionId)
        {
            GetChannelQAResponse response = new GetChannelQAResponse();
            var res = _channelrepo.GetChannelQADetails(QuestionId);

            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "LDC channel questions details not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.ChannelQAData = res.Result;
                response.Message = "Success";

            }
            return response;
        }

        public GetChannelAnswerResponse GetChannelAnswerDetails(int AnswerId)
        {
            GetChannelAnswerResponse response = new GetChannelAnswerResponse();
            var res = _channelrepo.GetChannelAnswerDetails(AnswerId);

            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "LDC channel answer details not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.ChannelAnswerData = res.Result;
                response.Message = "Success";

            }
            return response;
        }

        public ChannelQAResponse DeleteChannelQuestion(DeleteChannelQuestionRequest request)
        {
            ChannelQAResponse response = new ChannelQAResponse();
            string _deletedUserName = string.Empty;
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata != null)
            {
                _deletedUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _channelrepo.DeleteChannelQuestion(request, _deletedUserName);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Channel question deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }

        public ChannelQAResponse DeleteChannelAnswer(DeleteChannelAnswerRequest request)
        {
            ChannelQAResponse response = new ChannelQAResponse();
            string _deletedUserName = string.Empty;
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata != null)
            {
                _deletedUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _channelrepo.DeleteChannelAnswer(request, _deletedUserName);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Channel answer deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }

       public GetTeamsResponse GetTeamList(int orgId)
        {
            GetTeamsResponse response = new GetTeamsResponse();
            var res = _channelrepo.GetTeams(orgId);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Team data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedRequest = _mapper.Map< List<MSTeam>, List<MsTeamModel>>(res.Result);

                response.Teams = _mappedRequest;
                response.Message = "Success";

            }
            return response;
        }
        public GetTeamResponse GetTeamDetails(int teamId)
        {
            GetTeamResponse response = new GetTeamResponse();
            var res = _channelrepo.GetTeamDetails(teamId);
            if (res.Result == null )
            {
                response.IsSuccess = false;
                response.Message = "Team data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedRequest = _mapper.Map<MSTeam, MsTeamModel>(res.Result);
                response.TeamData = _mappedRequest;
                response.Message = "Success";
            }
            return response;
        }
        public TeamResponse UpdateMsTeamsData(UpdateMsTeamsDataRequest request)
        {
            TeamResponse response = new TeamResponse();
           

            var res = _channelrepo.UpdateMsTeamsData(request);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Teams data updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update the teams data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }
    }
}
