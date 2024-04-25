using FluentValidation;
using FluentValidation.Results;
using LDP.Common.BL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.Models;
using LDP_APIs.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace LDP_APIs.Controllers.V1
{



    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Teams")]

    public class TeamsController : ControllerBase
    {
        IMSTeamsBL _bl;
        private IValidator<TeamscreateChannelRequest> _teamsCreateChannelValidator;
        public TeamsController(IMSTeamsBL bl, IValidator<TeamscreateChannelRequest> teamsCreateChannelValidator)
        {
            _bl = bl;
            _teamsCreateChannelValidator = teamsCreateChannelValidator;
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("List")]
        public  GetTeamsResponse GetTeamlist(int orgId)
        {
            return _bl.GetTeamList(orgId);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channels/Create")]
        public TeamsCreateChannelResponse CreateChannel(TeamscreateChannelRequest request)
        {
            baseResponse response = new TeamsCreateChannelResponse();
            var result = _teamsCreateChannelValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as TeamsCreateChannelResponse;

            }
            return _bl.CreateChannel(request);   
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("CreateChannelTab")]
        public async Task<baseResponse> CreateChannelTab()
        {
            baseResponse res = new baseResponse();

            try
            {
                string clientId = "c1f139cf-c98e-4c49-aff3-b2945fdc368c";
                string clientSecret = "mzi8Q~Gma-7KjMspVuAjoqtl.j5.MhDrnAHGgbVo";
                string tenantId = "a36dd3dc-9208-44b9-a84e-1b0808cd56eb";
                string teamId = "17e93b8c-2a9a-4c9f-aca0-69c24ca5bc91";
                string channelId = "19%3abe544de8b2824967b39d33c120e0f7b9%40thread.tacv2";

                string tokenUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
                using var tokenClient = new HttpClient();
                var tokenData = new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}&scope=https://graph.microsoft.com/.default", Encoding.UTF8, "application/x-www-form-urlencoded");
                var tokenResponse = await tokenClient.PostAsync(tokenUrl, tokenData);
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(tokenContent).access_token;
                //
                string straccessToken = accessToken.ToString();
                
                string encodedChannelId = Uri.EscapeDataString(channelId);
                
                HttpClient client = new HttpClient();
                
                
                    var fileUrl = $"https://lancesoftinc.sharepoint.com/sites/DotNetTeam504/Shared%20Documents/8a67ef36-fcab-44c4-994e-ca8c9b47bf0f";

                    var tabConfig = new TeamsTabConfiguration
                    {
                        ContentUrl = fileUrl,
                        WebsiteUrl = fileUrl
                    };
                    string navbindurl = "https://graph.microsoft.com/v1.0/appCatalogs/teamsApps/com.microsoft.teamspace.tab.files.sharepoint";
                var tab = new TeamsTab
                {
                    DisplayName = "LDC code channel",
                    Configuration = tabConfig,

               
                AdditionalData = new Dictionary<string, object>()
                {
                    { "teamsAppId", "com.microsoft.teamspace.tab.web" }
                }
                    };
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", straccessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonBody = JsonConvert.SerializeObject(tab);
                    var content1 = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var url = "https://graph.microsoft.com/v1.0/appCatalogs/teamsApps/c1f139cf-c98e-4c49-aff3-b2945fdc368c/appDefinitions";
                
                var getresponse = await client.GetAsync(url);

                var requestUri = $"https://graph.microsoft.com/v1.0/teams/{teamId}/channels/{channelId}/tabs";
                    
                  var response = await client.PostAsync(requestUri, content1);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                    }
  
            }
            catch (Exception ex)
            {
                res.Message = "Exception on channel creation ";
            }
            res.Message = "Channel created";
            return res;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SendMessageToChannel")]
        public async Task<baseResponse> SendMessageToChannel()
        {
            baseResponse res = new baseResponse();

            try
            {
                string clientId = "c1f139cf-c98e-4c49-aff3-b2945fdc368c";
                string clientSecret = "mzi8Q~Gma-7KjMspVuAjoqtl.j5.MhDrnAHGgbVo";
                string tenantId = "a36dd3dc-9208-44b9-a84e-1b0808cd56eb";
                string teamId = "17e93b8c-2a9a-4c9f-aca0-69c24ca5bc91";
                string channelId = "19%3abe544de8b2824967b39d33c120e0f7b9%40thread.tacv2";

  
                string straccessToken = "eyJ0eXAiOiJKV1QiLCJub25jZSI6Ik1PZ2VpOGRJbkZyNWwwY3VPSUtGVlBZZERmOVYyMHZNTm5NbDJ2Znk5ajgiLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9hMzZkZDNkYy05MjA4LTQ0YjktYTg0ZS0xYjA4MDhjZDU2ZWIvIiwiaWF0IjoxNjg0Mzk0MDUxLCJuYmYiOjE2ODQzOTQwNTEsImV4cCI6MTY4NDM5ODM0MiwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhUQUFBQTRHa0MwUTAyTWFaVFIvRXBmczZVWFEvWXNoL3FnSW1tbWVoWE84TzFlRzNIOHdHQ2NjTzFucFNCVWZna3EyNGNyYThQampOcmJPdUxHUFdyUDVmRktrRlBIZWwxQWcrQzJMbGh0YUxkUkdnPSIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiVGVhbXNfTERDIiwiYXBwaWQiOiI3MjU3NjAwMi02Y2EyLTQ1ZDEtOGZmMy1hOTIyMGJkYTY3MmYiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6IlNhbmdhbWFkIiwiZ2l2ZW5fbmFtZSI6IkNoZXRhbmEiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIyNDAxOjQ5MDA6NGJiNDplNjQzOjNjMDc6YmQwOTozNTZkOjNjNiIsIm5hbWUiOiJDaGV0YW5hIFNhbmdhbWFkIiwib2lkIjoiNDU1NmRjNjgtM2RiZi00MzRlLWE4MmUtOTE4Yjk2Y2JmODlkIiwicGxhdGYiOiIzIiwicHVpZCI6IjEwMDMyMDAxREZCRkQyNDUiLCJyaCI6IjAuQVNvQTNOTnRvd2lTdVVTb1Roc0lDTTFXNndNQUFBQUFBQUFBd0FBQUFBQUFBQUFxQU93LiIsInNjcCI6IkNoYW5uZWxNZXNzYWdlLlNlbmQgR3JvdXAuUmVhZC5BbGwgR3JvdXAuUmVhZFdyaXRlLkFsbCBvcGVuaWQgcHJvZmlsZSBVc2VyLlJlYWQgZW1haWwiLCJzaWduaW5fc3RhdGUiOlsia21zaSJdLCJzdWIiOiJxN0d0LWdHcjFyTEcxRjRFNW8wZVg2UUgwVG80MFRGX29NVEpkR29tbzVJIiwidGVuYW50X3JlZ2lvbl9zY29wZSI6IkFTIiwidGlkIjoiYTM2ZGQzZGMtOTIwOC00NGI5LWE4NGUtMWIwODA4Y2Q1NmViIiwidW5pcXVlX25hbWUiOiJDaGV0YW5hLlNAbGFuY2Vzb2Z0LmNvbSIsInVwbiI6IkNoZXRhbmEuU0BsYW5jZXNvZnQuY29tIiwidXRpIjoibXoyX1Foa2xQMC1SblpOcWtZWVZBQSIsInZlciI6IjEuMCIsIndpZHMiOlsiYjc5ZmJmNGQtM2VmOS00Njg5LTgxNDMtNzZiMTk0ZTg1NTA5Il0sInhtc19zdCI6eyJzdWIiOiJ4RUlpa0YzdDNvT2YxVktscVFJVmZCbUljUVZMNUZxaUY2Rm5YTFFFNExnIn0sInhtc190Y2R0IjoxNTEwNzU4MTQzfQ.qQgli8Xto6AMs192MjtqkUi9CG5KqkWQS3Ty6sumtXaAVo14Gg1jMbLtth9tg1x7GXCu1i91GwFPXFsXOcPzoUPFmS1k8PXO7o8sadQsXS8rGoLLd2bjKqPKAIr1ZyoLmnhpo0eGy-AuVlUzRqTP1Bx_OOsp3t1wWix8FmmYV9UHPOeLuHeGpSMu64NH0PDH-KG6iKjUAN2-eu47iNKTtnCpEde5jpxHkYYojBcXXGHtc2KxE9z6PBi5_SwKvBv1duOlYKx90exKi5L8DCUQ3yjoxt-n8R9ClVXPk-uLgaCW-80zxCPbTSfVWXUJ5GvqSmftTuSNIkeK6fB37CNzwg";
                var graphApiUrl = "https://graph.microsoft.com/beta";
                var messageText = "Code Tesing : Hello from the dotnet api code ";

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", straccessToken);

                var requestUrl = $"{graphApiUrl}/teams/{teamId}/channels/{channelId}/messages";
                var payload = new
                {
                    body = new
                    {
                        content = messageText
                    }
                };

                var payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var requestContent = new StringContent(payloadJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(requestUrl, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Message sent successfully!");
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var strStatusCode = response.StatusCode;
                    var strresponsecontent = responseContent;
                }


                if (!response.IsSuccessStatusCode)
                {
                    res.Message = "Failed to send message to channel.";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Message = "Exception on sending message to channel  ";
                return res;
            }

            res.Message = "Message send message to Channel ";
            return res;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channel/File/MessagewithFileAttachment")]
        public async Task<IActionResult> UploadFileToTeamsChannelAsMessage(IFormFile file)
        {

            TeamsHelper _teamsHelper = new TeamsHelper();


            string clientId = "c1f139cf-c98e-4c49-aff3-b2945fdc368c";
            string clientSecret = "mzi8Q~Gma-7KjMspVuAjoqtl.j5.MhDrnAHGgbVo";
            string tenantId = "a36dd3dc-9208-44b9-a84e-1b0808cd56eb";
            string teamId = "17e93b8c-2a9a-4c9f-aca0-69c24ca5bc91";
            string channelId = "19%3abe544de8b2824967b39d33c120e0f7b9%40thread.tacv2";
            string straccessToken;

            string fileMimeType = "text/plain";
            string fileName = "FileToChannel.txt";
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file was uploaded.");
                }


                string tokenUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
                using var tokenClient = new HttpClient();
                var tokenData = new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}&scope=https://graph.microsoft.com/.default", Encoding.UTF8, "application/x-www-form-urlencoded");
                var tokenResponse = await tokenClient.PostAsync(tokenUrl, tokenData);
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(tokenContent).access_token;
                straccessToken = accessToken.ToString();
                
                using var httpClient = new HttpClient();


                
                straccessToken = "eyJ0eXAiOiJKV1QiLCJub25jZSI6InNtY3VsbzBtbjM0RVIxS084bWYwRmtDWTlEWl9XcG1jaElDZEJOX3Fhb1kiLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9hMzZkZDNkYy05MjA4LTQ0YjktYTg0ZS0xYjA4MDhjZDU2ZWIvIiwiaWF0IjoxNjg0MTUwOTYyLCJuYmYiOjE2ODQxNTA5NjIsImV4cCI6MTY4NDIzNzY2MiwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFUUUF5LzhUQUFBQThHNXFPSGlVMklPK1VENXl4ZTVmMnh5YTBvRW51QVZ6ditFczVXaGxEQnFXUVd2eHRIYVBKb3gvdnQ0L2lhWjgiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkdyYXBoIEV4cGxvcmVyIiwiYXBwaWQiOiJkZThiYzhiNS1kOWY5LTQ4YjEtYThhZC1iNzQ4ZGE3MjUwNjQiLCJhcHBpZGFjciI6IjAiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIxMDYuNzkuMTk1Ljc4IiwibmFtZSI6IkxEQyBVc2VyIiwib2lkIjoiNWJmNmEzNjItMDhmOS00Mzk2LTllOTItMDM1MWM5NDc4OWFlIiwicGxhdGYiOiIzIiwicHVpZCI6IjEwMDMyMDAyOUY0MEIxNkQiLCJyaCI6IjAuQVNvQTNOTnRvd2lTdVVTb1Roc0lDTTFXNndNQUFBQUFBQUFBd0FBQUFBQUFBQUFxQUIwLiIsInNjcCI6IkFQSUNvbm5lY3RvcnMuUmVhZC5BbGwgQVBJQ29ubmVjdG9ycy5SZWFkV3JpdGUuQWxsIENoYW5uZWwuQ3JlYXRlIENoYW5uZWwuUmVhZEJhc2ljLkFsbCBDaGFubmVsTWVzc2FnZS5SZWFkLkFsbCBDaGF0LkNyZWF0ZSBDaGF0LlJlYWRXcml0ZSBEaXJlY3RvcnkuUmVhZFdyaXRlLkFsbCBHcm91cC5SZWFkLkFsbCBHcm91cC5SZWFkV3JpdGUuQWxsIG9wZW5pZCBwcm9maWxlIFVzZXIuUmVhZCBlbWFpbCIsInNpZ25pbl9zdGF0ZSI6WyJrbXNpIl0sInN1YiI6IlJDUk9nQ2FMeGNtdWlaNzRsMnE5ellydWxwYUFhX2xlVHhHYmNleGNkMGMiLCJ0ZW5hbnRfcmVnaW9uX3Njb3BlIjoiQVMiLCJ0aWQiOiJhMzZkZDNkYy05MjA4LTQ0YjktYTg0ZS0xYjA4MDhjZDU2ZWIiLCJ1bmlxdWVfbmFtZSI6ImxkY3VzZXJAbGFuY2Vzb2Z0LmNvbSIsInVwbiI6ImxkY3VzZXJAbGFuY2Vzb2Z0LmNvbSIsInV0aSI6IlJiU1pST3pJNmtpbm9hMWlUcTBfQUEiLCJ2ZXIiOiIxLjAiLCJ3aWRzIjpbIjYyZTkwMzk0LTY5ZjUtNDIzNy05MTkwLTAxMjE3NzE0NWUxMCIsIjE1OGMwNDdhLWM5MDctNDU1Ni1iN2VmLTQ0NjU1MWE2YjVmNyIsIjliODk1ZDkyLTJjZDMtNDRjNy05ZDAyLWE2YWMyZDVlYTVjMyIsImI3OWZiZjRkLTNlZjktNDY4OS04MTQzLTc2YjE5NGU4NTUwOSJdLCJ4bXNfY2MiOlsiQ1AxIl0sInhtc19zc20iOiIxIiwieG1zX3N0Ijp7InN1YiI6IlA0M2M0UkprVTE1bGtCUHlDS3hxMjFEcHItSHg3Tm03M1dPLUlVVVp6amsifSwieG1zX3RjZHQiOjE1MTA3NTgxNDN9.BUoqVhfIsRcFu6RHiOVrQB80lSAWzzW_6DaBzSPSr8lXVW_DBFGlZCmkq2wr9Gcy9BaOCSB8EB0USHGJm3vTWMEpAcZnZLsnbPOBtl1dclnjJX212yfhhZ5Z14iDwePXeY-xFPkrCgCR3mXD866rgTXpiKap2_1946CqVXSCT2jO-bxXDnKX7umXt_zA5op1-2WOgE-fkfSi7pmThxd1JfO8HaaJR4bDlvMBE1KoBErYHkXxPDG7p4iHAJDmPwul0aoBAfJj-a2WEhAk4Y4IOGj9F9izca58F1H7oWxJiArAWXnKf7Yn1RyDEjurdvIYXpDj1XDfwwF-Mr_wQ7_D3Q";
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", straccessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                
                var messageContent = new
                {
                    contentType = "html",
                    content = "Here's test file attached. <attachment id=\"8A67EF36-FCAB-44C4-994E-CA8C9B47BF0F\"></attachment>"
                };

                var attachmentContent = new
                {
                    id = "8A67EF36-FCAB-44C4-994E-CA8C9B47BF0F",
                    contentType = "reference",
                    contentUrl = "https://lancesoftinc.sharepoint.com/sites/DotNetTeam504/Shared%20Documents/FileToChannel.txt",
                    name = "FileToChannel.txt"
                };

                var payload = new
                {
                    body = messageContent,
                    attachments = new[] { attachmentContent }
                };
                var payloadJson = System.Text.Json.JsonSerializer.Serialize(payload);
                var requestContent = new StringContent(payloadJson, Encoding.UTF8, "application/json");
                var requestUri = "https://graph.microsoft.com/v1.0/teams/17e93b8c-2a9a-4c9f-aca0-69c24ca5bc91/channels/19%3abe544de8b2824967b39d33c120e0f7b9%40thread.tacv2/messages\r\n";
                var response = await httpClient.PostAsync(requestUri, requestContent);


                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var strStatusCode = response.StatusCode;
                    var strresponsecontent = responseContent;

                    return Ok("File uploaded successfully to Teams channel.");
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var strStatusCode = response.StatusCode;
                    var strresponsecontent = responseContent;
                    return BadRequest($"File upload failed. Status code: {HttpStatusCode.OK}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channel/File/Upload")]
        public async Task<IActionResult> UploadFileToTeamsChannele(IFormFile file)
        {

            TeamsHelper _teamsHelper = new TeamsHelper();


            string clientId = "c1f139cf-c98e-4c49-aff3-b2945fdc368c";
            string clientSecret = "mzi8Q~Gma-7KjMspVuAjoqtl.j5.MhDrnAHGgbVo";
            string tenantId = "a36dd3dc-9208-44b9-a84e-1b0808cd56eb";
            string teamId = "17e93b8c-2a9a-4c9f-aca0-69c24ca5bc91";
            string channelId = "19%3abe544de8b2824967b39d33c120e0f7b9%40thread.tacv2";
            string straccessToken;

            string fileMimeType = "text/plain";
            string fileName = "FileToChannel.txt";
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file was uploaded.");
                }

                
                straccessToken =  _teamsHelper.GenerateToken(tenantId, clientId, clientSecret).Result;

                
                var driveItem = new TeamsHelper().GetChannelFileFolder(straccessToken, teamId, channelId).Result;

                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", straccessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = $"https://graph.microsoft.com/v1.0/drives/{driveItem.parentReference.driveId}/items/{driveItem.id}/content?@name='FileToChannel.txt'";
                
                var fileStream = file.OpenReadStream();
                using var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(fileMimeType);

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));

                var response = await httpClient.PutAsync(requestUri, streamContent);

             

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var strStatusCode = response.StatusCode;
                    var strresponsecontent = responseContent;

                    return Ok("File uploaded successfully to Teams channel.");
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var strStatusCode = response.StatusCode;
                    var strresponsecontent = responseContent;
                    return BadRequest($"File upload failed. Status code: {HttpStatusCode.OK}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channel/File/Copy")]
        public async Task<IActionResult> CopyFileToTeamsChannele()
        {

            TeamsHelper _teamsHelper = new TeamsHelper();


            string clientId = "c1f139cf-c98e-4c49-aff3-b2945fdc368c";
            string clientSecret = "mzi8Q~Gma-7KjMspVuAjoqtl.j5.MhDrnAHGgbVo";
            string tenantId = "a36dd3dc-9208-44b9-a84e-1b0808cd56eb";
            string teamId = "17e93b8c-2a9a-4c9f-aca0-69c24ca5bc91";
            string channelId = "19%3abe544de8b2824967b39d33c120e0f7b9%40thread.tacv2";
            string straccessToken;

            string fileMimeType = "text/plain";
            string fileName = "FileToChannel.txt";
            try
            {
               

                string tokenUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
                using var tokenClient = new HttpClient();
                var tokenData = new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}&scope=https://graph.microsoft.com/.default", Encoding.UTF8, "application/x-www-form-urlencoded");
                var tokenResponse = await tokenClient.PostAsync(tokenUrl, tokenData);
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(tokenContent).access_token;
                straccessToken = accessToken.ToString();

                var driveItem = new TeamsHelper().GetChannelFileFolder(straccessToken, teamId, channelId).Result;

                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", straccessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = $"https://graph.microsoft.com/v1.0/drives/{driveItem.parentReference.driveId}/items/{driveItem.id}/content?@name='FileToChannel.txt'";
                //var requestUri = $"https://graph.microsoft.com/v1.0/drives/";
                //var fileStream = null;//.]file.OpenReadStream();
                StreamContent streamContent = null;
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(fileMimeType);

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));

                var response = await httpClient.PutAsync(requestUri, streamContent);



                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var strStatusCode = response.StatusCode;
                    var strresponsecontent = responseContent;

                    return Ok("File uploaded successfully to Teams channel.");
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var strStatusCode = response.StatusCode;
                    var strresponsecontent = responseContent;
                    return BadRequest($"File upload failed. Status code: {HttpStatusCode.OK}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Channel/File/UploadWord")]
        public async Task<IActionResult> UploadWordDocToTeams(IFormFile file)
        {

            TeamsHelper _teamsHelper = new TeamsHelper();


            string clientId = "c1f139cf-c98e-4c49-aff3-b2945fdc368c";
            string clientSecret = "mzi8Q~Gma-7KjMspVuAjoqtl.j5.MhDrnAHGgbVo";
            string tenantId = "a36dd3dc-9208-44b9-a84e-1b0808cd56eb";
            string teamId = "17e93b8c-2a9a-4c9f-aca0-69c24ca5bc91";
            string channelId = "19%3abe544de8b2824967b39d33c120e0f7b9%40thread.tacv2";
            string straccessToken;

            string fileMimeType = "application/msword";
            string fileName = "LDC_Test_Doc.docx";
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file was uploaded.");
                }


                string tokenUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
                using var tokenClient = new HttpClient();
                var tokenData = new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}&scope=https://graph.microsoft.com/.default", Encoding.UTF8, "application/x-www-form-urlencoded");
                var tokenResponse = await tokenClient.PostAsync(tokenUrl, tokenData);
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(tokenContent).access_token;
                straccessToken = accessToken.ToString();

                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", straccessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


 
                var requestUrl = $"https://graph.microsoft.com/beta/teams/{teamId}/channels/{channelId}/tabs";

                var payload = new
                {
                    displayName = "lDC Plattform Testing from code10",
                    teamsAppId = "com.microsoft.teamspace.tab.web",
                    
                    configuration = new
                    {
                        contentUrl = "https://lancesoftinc.sharepoint.com/:w:/r/sites/DotNetTeam504/_layouts/15/Doc.aspx?sourcedoc=%7B97C6C7A4-CFC2-4DBA-A681-2538607D6FE9%7D&file=LDC_Test_Doc.docx&cid=c40b9837-806f-4e8c-8897-ffd358f307b0",
                      
                    }
                };


                var payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var requestContent = new StringContent(payloadJson, Encoding.UTF8, "application/json");


                // Send the POST request
                HttpResponseMessage response = await httpClient.PostAsync(requestUrl, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var strStatusCode = response.StatusCode;
                    var strresponsecontent = responseContent;

                    return Ok("File uploaded successfully to Teams channel.");
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var strStatusCode = response.StatusCode;
                    var strresponsecontent = responseContent;
                    return BadRequest($"File upload failed. Status code: {HttpStatusCode.OK}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("CreateMeeting")]
        public async Task<IActionResult> CreateMeeting()
        {

            TeamsHelper _teamsHelper = new TeamsHelper();


            string clientId = "c1f139cf-c98e-4c49-aff3-b2945fdc368c";
            string clientSecret = "mzi8Q~Gma-7KjMspVuAjoqtl.j5.MhDrnAHGgbVo";
            string tenantId = "a36dd3dc-9208-44b9-a84e-1b0808cd56eb";
            string teamId = "17e93b8c-2a9a-4c9f-aca0-69c24ca5bc91";
            string channelId = "19%3abe544de8b2824967b39d33c120e0f7b9%40thread.tacv2";
            
            string straccessToken;
            try {
                straccessToken = "eyJ0eXAiOiJKV1QiLCJub25jZSI6InM5NmxqcWRLVUJmbU1kYnVrcFF0NzR0bEdHOXhMbkxwRXl4eUFfQmVoeVUiLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9hMzZkZDNkYy05MjA4LTQ0YjktYTg0ZS0xYjA4MDhjZDU2ZWIvIiwiaWF0IjoxNjg0NTA1ODI2LCJuYmYiOjE2ODQ1MDU4MjYsImV4cCI6MTY4NDUxMDE3NiwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhUQUFBQXFNb0x1YWhtU2hobElpaHk4REJBdUtxWmNIdlRvTXlyMEdBUFNVaU54Q2RjWGVCYml6QjJEMlhWK1RJQ1FOa0NaUUJNSjJncC9jaTVxaVUxR0JjSE9wbHIrMnNyWDlhNDYvc2JoSGg3cjhJPSIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiVGVhbXNfTERDIiwiYXBwaWQiOiI3MjU3NjAwMi02Y2EyLTQ1ZDEtOGZmMy1hOTIyMGJkYTY3MmYiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6IlNhbmdhbWFkIiwiZ2l2ZW5fbmFtZSI6IkNoZXRhbmEiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIyNDAxOjQ5MDA6NGJiNDplNjQzOjM5N2E6NmZjNTpjZjEyOjQxMmEiLCJuYW1lIjoiQ2hldGFuYSBTYW5nYW1hZCIsIm9pZCI6IjQ1NTZkYzY4LTNkYmYtNDM0ZS1hODJlLTkxOGI5NmNiZjg5ZCIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMURGQkZEMjQ1IiwicmgiOiIwLkFTb0EzTk50b3dpU3VVU29UaHNJQ00xVzZ3TUFBQUFBQUFBQXdBQUFBQUFBQUFBcUFPdy4iLCJzY3AiOiJDYWxlbmRhcnMuUmVhZFdyaXRlIENoYW5uZWxNZXNzYWdlLlNlbmQgR3JvdXAuUmVhZC5BbGwgR3JvdXAuUmVhZFdyaXRlLkFsbCBNYWlsLlJlYWRXcml0ZSBNYWlsLlNlbmQgb3BlbmlkIHByb2ZpbGUgVXNlci5SZWFkIGVtYWlsIiwic2lnbmluX3N0YXRlIjpbImttc2kiXSwic3ViIjoicTdHdC1nR3IxckxHMUY0RTVvMGVYNlFIMFRvNDBURl9vTVRKZEdvbW81SSIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJBUyIsInRpZCI6ImEzNmRkM2RjLTkyMDgtNDRiOS1hODRlLTFiMDgwOGNkNTZlYiIsInVuaXF1ZV9uYW1lIjoiQ2hldGFuYS5TQGxhbmNlc29mdC5jb20iLCJ1cG4iOiJDaGV0YW5hLlNAbGFuY2Vzb2Z0LmNvbSIsInV0aSI6IlhYRG9RNk94YVVta1NwX3JjaWtZQUEiLCJ2ZXIiOiIxLjAiLCJ3aWRzIjpbImI3OWZiZjRkLTNlZjktNDY4OS04MTQzLTc2YjE5NGU4NTUwOSJdLCJ4bXNfc3QiOnsic3ViIjoieEVJaWtGM3Qzb09mMVZLbHFRSVZmQm1JY1FWTDVGcWlGNkZuWExRRTRMZyJ9LCJ4bXNfdGNkdCI6MTUxMDc1ODE0M30.D7CL-UldOMHxioYdvy_RMLDcNWnEPD6FzXFeaU7HqNGmNHkaHzwtErZ31Orn4HArDSMzsdeLS3XKHJiWn29-rrZwxHejOQFunJYxL43-1iZxZFME4X6UMOF7pjxX2sVcoCBbvmt9zHptlWQR3fGkxU818wZYpg5ACm9Qprmj4yRy0QlfLMTaId07o-1r1nMAFoqRxMiFV85QHHHTGyjHZPv6ocosJ_GkI3FxWYGER1day0o1Opeulth2LVYLmkZbACtVDk1RfNr4dWPcx23VJWYalnZn_Sx8VC081JwJQ5i_hOPaRdWeAZdnQgwRwhmHzwYZBpiNjYiIUNw7m3BMEw";
                string apiUrl = "https://graph.microsoft.com/v1.0/me/events";

                string newId = Guid.NewGuid().ToString();
                string requestBody =
                    $@"{{
        ""subject"": ""POC Testing"",
        ""isOnlineMeeting"": true,
        ""onlineMeetingProvider"": ""teamsForBusiness"",
        ""start"": {{
            ""dateTime"": ""2023-05-20T11:00:00"",
            ""timeZone"": ""Pacific Standard Time""
        }},
        ""end"": {{
            ""dateTime"": ""2023-05-20T11:30:00"",
            ""timeZone"": ""Pacific Standard Time""
        }},
        ""attendees"": [
            {{
                ""emailAddress"": {{
                    ""address"": ""arunachalamr@lancesoft.com"",
                    ""name"": ""Arunachalam Ramaiah""
                }}
            }},
            {{
                ""emailAddress"": {{
                    ""address"": ""Chetana.S@lancesoft.com"",
                    ""name"": ""Chetana Sangamad""
                }}
            }}
        ]
        
    }}";
        HttpResponseMessage response = null;

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", straccessToken);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            //
            StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            //
            response = await client.PostAsync(apiUrl, content);

        }


        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            var strStatusCode = response.StatusCode;
            var strresponsecontent = responseContent;

            return Ok("Calender  created successfully");
        }
        else
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            var strStatusCode = response.StatusCode;
            var strresponsecontent = responseContent;
            return BadRequest($"Calender creation failed. Status code: {HttpStatusCode.OK}");
        }

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
        }

        private void BuildValiationMessage(ValidationResult result, ref baseResponse validationresponse)
        {
            validationresponse.IsSuccess = false;
            validationresponse.Message = "Validation Error";
            validationresponse.HttpStatusCode = HttpStatusCode.BadRequest;
            validationresponse.errors = result.Errors.Select(e => e.ErrorMessage.ToString()).ToList();

        }
    }



}
