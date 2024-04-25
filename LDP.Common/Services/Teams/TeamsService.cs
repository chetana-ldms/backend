using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace LDP.Common.Services.Teams
{
    public class TeamsService : ITeamsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TeamsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        

        public async Task<TeamsCreateChannelResponse> CreateChannel(TeamsCreatechannelServiceRequest request)
        {
            TeamsCreateChannelResponse serviceResponse = new TeamsCreateChannelResponse ();

         
            string straccessToken = GenerateToken( request.GetTokenGraphUrl,  request.ClientId,  request.ClientSecret,request.TenantId).Result;

            using var httpClient = _httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", straccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestBody = new
            {
                displayName = request.ChannelName,
                description = request.ChanneDescription,
                membershipType = "standard"//request.MembershipType //"standard"
            };
            var requestContent = JsonContent.Create(requestBody);

            var response = await httpClient.PostAsync(request.GraphOperationUrl, requestContent);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JsonDocument.Parse(responseBody);

                if (responseObject.RootElement.TryGetProperty("id", out JsonElement channelIdElement))
                {
                    serviceResponse.MsTeamsChannelId = channelIdElement.GetString();
                }
                else
                {
                }
                serviceResponse.IsSuccess = true;
                serviceResponse.Message = "Channel creation success";
            }
            else
            {
                serviceResponse.Message = $"Failed to create channel. Api response : {await response.Content.ReadAsStringAsync()}";
                serviceResponse.IsSuccess = false;
            }

            return serviceResponse;
        }

        public async Task<string> GenerateToken(string tokenUrl,string clientId , string clientSecret, string tenantId)
        {
        
            using var httpClient = _httpClientFactory.CreateClient();
            var tokenData = new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}&scope=https://graph.microsoft.com/.default", Encoding.UTF8, "application/x-www-form-urlencoded");
            var tokenResponse = await httpClient.PostAsync(tokenUrl, tokenData);
            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(tokenContent).access_token;
            return accessToken.ToString();  
        }
        public async Task<string> GetSiteId(string accessToken, string teamId)
        {
            string siteId = string.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var requestUrl = $"https://graph.microsoft.com/v1.0/groups/{teamId}/sites/root";

                    var response = await httpClient.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var siteData = await response.Content.ReadAsStringAsync();
                        siteId = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteResponse>(siteData)?.Id;

                    }
                    else
                    {
                        siteId = null;
                    }
                }
            }
            catch (Exception ex)
            {
                siteId = null;
            }

            return siteId;
        }

        public async Task<string> GetParentItemId(string accessToken, string siteId)
        {
            string parentItemId = string.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var requestUrl = $"https://graph.microsoft.com/v1.0/sites/{siteId}/drive/root";

                    var response = await httpClient.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var driveItemData = await response.Content.ReadAsStringAsync();
                        parentItemId = Newtonsoft.Json.JsonConvert.DeserializeObject<DriveItemResponse>(driveItemData)?.Id;

                        //return siteId;
                    }
                    else
                    {
                        //Console.WriteLine($"Failed to retrieve site information. Status code: {response.StatusCode}");
                        //return null;
                        parentItemId = null;
                    }
                }
            }
            catch (Exception ex)
            {
                parentItemId = null;
            }

            return parentItemId;
        }
        public async Task<ChannelDrive> GetChannelFileFolder(string acccessToken, string teamId, string channelId)
        {

            ChannelDrive drive = null;
            ChannelDrive channelDrive = new ChannelDrive();
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", acccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = $"https://graph.microsoft.com/v1.0/teams/{teamId}/channels/{channelId}/filesFolder";
            var response = await httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                drive = JsonSerializer.Deserialize<ChannelDrive>(responseContent);
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var strStatusCode = response.StatusCode;
                var strresponsecontent = responseContent;
            }
            return drive;
        }


        public async Task<string> SendFileToChannel(string acccessToken, string teamId, string channelId, string filePath)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", acccessToken);

            var requestUri = $"https://graph.microsoft.com/v1.0/drives/";
            using var fileStream = System.IO.File.OpenRead(filePath);
            using var streamContent = new StreamContent(fileStream);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));

            var response = await httpClient.PutAsync(requestUri, streamContent);



            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var strStatusCode = response.StatusCode;
                var strresponsecontent = responseContent;
            }
            return "";
        }


    }
}
