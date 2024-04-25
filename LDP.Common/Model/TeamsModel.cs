using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace LDP.Common.Model
{
    public class SiteResponse
    {
        public string Id { get; set; }
    }

    public class TeamsHelper
        {
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

                        //return siteId;
                    }
                    else
                    {
                        //Console.WriteLine($"Failed to retrieve site information. Status code: {response.StatusCode}");
                        //return null;
                        siteId = null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine($"An error occurred: {ex.Message}");
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

        
        public async Task<string > SendFileToChannel(string acccessToken, string teamId, string channelId, string filePath)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", acccessToken);

            var requestUri = $"https://graph.microsoft.com/v1.0/drives/";// {driveItem.ParentReference.DriveId}/items/{driveItem.Id}/content?@name='testfile.pdf'";

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

        public async Task<string> GenerateToken(string tenantId, string clientId , string clientSecret)
        {
            string straccessToken = string.Empty;
            string tokenUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
            using var tokenClient = new HttpClient();
            var tokenData = new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}&scope=https://graph.microsoft.com/.default", Encoding.UTF8, "application/x-www-form-urlencoded");
            var tokenResponse = await tokenClient.PostAsync(tokenUrl, tokenData);
            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(tokenContent).access_token;
            straccessToken = accessToken.ToString();
            return straccessToken;

        }

    }

       // var requestUri = $"https://graph.microsoft.com/v1.0/teams/{teamId}/channels/{channelId}/filesFolder";
    
    public class DriveItemResponse
    {
        public string Id { get; set; }
    }

    public class ChannelDrive
    {
        public string id { get; set; }
        public string name { get; set; }
        public ParentReference? parentReference { get; set; }

    }

    public class ParentReference
    {

        public string driveId { get; set; }

    }

    class MeetingRequest
    {
        public string Subject { get; set; }
        public MeetingDateTime Start { get; set; }
        public MeetingDateTime End { get; set; }
        public List<Attendee> Attendees { get; set; }
        public OnlineMeeting OnlineMeeting { get; set; }
    }

    class MeetingDateTime
    {
        public string DateTime { get; set; }
        public string TimeZone { get; set; }
    }

    class Attendee
    {
        public EmailAddress EmailAddress { get; set; }
    }

    class EmailAddress
    {
        public string Address { get; set; }
        public string Name { get; set; }
    }

    class OnlineMeeting
    {
        public string ExternalId { get; set; }
    }

}
