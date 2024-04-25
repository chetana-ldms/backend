using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP.Common.Services.Teams;

namespace LDP.Common.BL
{
    public class TeamsBL : IMSTeamsBL
    {
        readonly ILDCChannelBL _channelbl;
        ITeamsService _serviceBl;
        readonly IMapper _mapper;
        IConfigurationDataBL _configDataBL;
        IAPIUrlBL _apiUrlBL;
        public TeamsBL(ILDCChannelBL bl, ITeamsService serviceBl, IMapper mapper, IConfigurationDataBL configDataBL, IAPIUrlBL apiUrlBL)
        {
            _channelbl = bl;
            _serviceBl = serviceBl;
            _mapper = mapper;
            _configDataBL = configDataBL;
            _apiUrlBL = apiUrlBL;
        }

        public GetTeamsResponse GetTeamList(int orgId)
        {
           return _channelbl.GetTeamList(orgId);
        }
        public TeamsCreateChannelResponse CreateChannel(TeamscreateChannelRequest request)
        {
            TeamsCreateChannelResponse response = new TeamsCreateChannelResponse();

            //
            //var serviceRequest = _mapper.Map<TeamscreateChannelRequest, TeamsCreatechannelServiceRequest>(request);

            TeamsCreatechannelServiceRequest serviceRequest = new TeamsCreatechannelServiceRequest();
            //if (!string.IsNullOrEmpty(request.MembershipType))
            //{
            //    serviceRequest.MembershipType = request.MembershipType;
            //}
            //else
            //{
            //    serviceRequest.MembershipType = "standard";
            //}
            // get the Azure configuration.
            var azureAppConfig = _configDataBL.GetConfigurationData("AZURE").Data;
            if (azureAppConfig != null) 
            {
                var tenantdata = azureAppConfig.Where(c => c.DataName == Constants.Azure_TenantId).FirstOrDefault();
                if (tenantdata!= null) 
                {
                    serviceRequest.TenantId = tenantdata.DataValue;
                }
                var clientId = azureAppConfig.Where(c => c.DataName == Constants.Azure_ClientId).FirstOrDefault();
                if (clientId != null)
                {
                    serviceRequest.ClientId = clientId.DataValue;
                }
                var clientSecret = azureAppConfig.Where(c => c.DataName == Constants.Azure_ClientSecret).FirstOrDefault();
                if (clientSecret != null)
                {
                    serviceRequest.ClientSecret = clientSecret.DataValue;
                }
            }

            if(serviceRequest.TenantId == null || serviceRequest.ClientId == null || serviceRequest.ClientSecret == null )
            {
                response.IsSuccess = false;
                response.Message = "Azure app configuaration is missing";
                response.HttpStatusCode = System.Net.HttpStatusCode.PreconditionFailed;
                return response;
            }
            //
            var channelData = _channelbl.GetChannelDetails(request.ChannelId);
            if (channelData.ChannelsData == null)
            {
                response.IsSuccess = false;
                response.Message = "Channels data not found";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            if (channelData.ChannelsData.ChannelTypeName == Constants.Channel_Type_UnderReview ||
                channelData.ChannelsData.ChannelTypeName == Constants.Channel_Type_UnderConstruction ||
                channelData.ChannelsData.ChannelTypeName == Constants.Channel_Type_ReadyToDeployment )
            {
                response.IsSuccess = false;
                response.Message = "Teams channels creation not allowed during pre production stage.";
                response.HttpStatusCode = System.Net.HttpStatusCode.PreconditionFailed;
                return response;
            }
            serviceRequest.ChannelName = channelData.ChannelsData.ChannelName;
            serviceRequest.ChanneDescription = channelData.ChannelsData.ChannelDescription;
            // Get TEams Id from db
            var teamsData = _channelbl.GetTeamDetails(request.TeamsId);
            if (teamsData.TeamData == null) 
            {
                response.IsSuccess = false;
                response.Message = "Teams data not found";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            serviceRequest.MSTeamsId = teamsData.TeamData.MsTeamId;
            // Get Graph API configuration data 
            var urlConfig = _apiUrlBL.GetLDCUrlsByGroup(Constants.Api_Url_group_Teams_Graph);

            if (urlConfig.UrlList != null)
            {
                var tokenUrldata = urlConfig.UrlList.Where(u => u.ActionName == Constants.Generate_Token_Api_Url).FirstOrDefault();
                if (tokenUrldata != null)
                {
                    serviceRequest.GetTokenGraphUrl = string.Format(tokenUrldata.ActionUrl, serviceRequest.TenantId);
                }
                var createchanneldata = urlConfig.UrlList.Where(u => u.ActionName == Constants.Create_Teams_Channel_Api_Url).FirstOrDefault();
                if (createchanneldata != null)
                {
                    serviceRequest.GraphOperationUrl = string.Format(createchanneldata.ActionUrl, serviceRequest.MSTeamsId);
                }
            }
            if (serviceRequest.GetTokenGraphUrl == null || serviceRequest.GraphOperationUrl == null)
            {
                response.IsSuccess = false;
                response.Message = "Url configuaration is missing";
                response.HttpStatusCode = System.Net.HttpStatusCode.PreconditionFailed;
                return response;
            }

            var serviceResponse = _serviceBl.CreateChannel(serviceRequest).Result;
            if (!serviceResponse.IsSuccess) 
            {
                response.IsSuccess = false;
                response.Message = "Failed to create channel in Teams";
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                return response;
            }
            UpdateMsTeamsDataRequest updateMsTeamsDataRequest = new UpdateMsTeamsDataRequest();
            updateMsTeamsDataRequest.ChannelId = request.ChannelId;
            updateMsTeamsDataRequest.MsTeamsId = teamsData.TeamData.MsTeamId;
            updateMsTeamsDataRequest.MsTeamsChannelId = serviceResponse.MsTeamsChannelId;
            var updateTeamsDataResponse = _channelbl.UpdateMsTeamsData(updateMsTeamsDataRequest);
            if(!updateTeamsDataResponse.IsSuccess)
            {
                response.IsSuccess = false;
                response.Message = "Failed to update the teams data in DB";
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Teams Channel Creation successfull ";
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            // storing data in DB

            return response;
        }
    }
}
