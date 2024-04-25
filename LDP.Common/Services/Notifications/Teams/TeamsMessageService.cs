using LDP.Common.Requests;
using LDP.Common.Responses;
using Newtonsoft.Json;
using System.Text;

namespace LDP.Common.Services.Notifications.Teams
{
    public class TeamsMessageService : ITeamsMessageService
    {
        public async Task<SendTeamsMessageResponse> SendTeamsMessage(SendTeamsMessageRequest request, SendTeamsMessageConfiguration configData)
        {
            SendTeamsMessageResponse response = new SendTeamsMessageResponse();
            //configData.HookUrl = "https://lancesoftinc.webhook.office.com/webhookb2/17e93b8c-2a9a-4c9f-aca0-69c24ca5bc91@a36dd3dc-9208-44b9-a84e-1b0808cd56eb/IncomingWebhook/e95942a79dcc4efa85a57b0239bc0535/e3653355-8f50-46b4-8eb7-f66de3c5f0c1";
            var webhookUrl = configData.HookUrl;
            string _title = string.Empty;
            if (string.IsNullOrEmpty(request.Title)) 
            {
                _title = configData.Title;
            }
            string _themecolor = string.Empty;
            if (string.IsNullOrEmpty(request.ThemeColor))
            {
                _themecolor = configData.ThemeColor;
            }
            var httpClient = new HttpClient();
            var messageData = new Dictionary<string, string>
        {
            { "text", request.Message },
            { "title", _title },
            { "themeColor", _themecolor }
        };
            var json = JsonConvert.SerializeObject(messageData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var _apiCallResponse = await httpClient.PostAsync(configData.HookUrl, content);

            if (_apiCallResponse.IsSuccessStatusCode == true)
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "some thing went wrong during Teams api call ";
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
            }
            return response;

        }
    }
}
