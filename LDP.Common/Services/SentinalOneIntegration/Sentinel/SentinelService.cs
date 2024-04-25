using AutoMapper.Internal;
using LDP.Common.BL.Interfaces;
using LDP.Common.Services.SentinalOneIntegration.Applications;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class SentinelService : ISentinelService
    {


        IApplicationLogBL _logger;

        private readonly IHttpClientFactory _httpClientFactory;

        public SentinelService(IApplicationLogBL logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Exclusions> GetExclusions(ExclusionRequest apiRrequest, OrganizationToolModel request, string nextcursor = null)
        {
            Exclusions response = new Exclusions();

            _logger.AddLogInforation($"GetExclusions action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            //
            //string apiUrl = request.ToolActions[0].ApiUrl;
            ////
            //if (!string.IsNullOrEmpty(nextcursor))
            //{
            //    // if current criteria has multiple pages of data 
            //    apiUrl = apiUrl + $"?cursor={nextcursor}";
            //    apiUrl = apiUrl + "&sortBy=updatedAt&sortOrder=desc";

            //}
            //else
            //{
            //    apiUrl = apiUrl + $"?includeChildren={apiRrequest.IncludeChildren}&includeParents={apiRrequest.IncludeParents}";

            //    if (!string.IsNullOrEmpty(apiRrequest.ExclusionListItemId))
            //    {
            //        apiUrl = apiUrl + $"&ids={apiRrequest.ExclusionListItemId}";

            //    }
            //    if (request.ToolActions[0].GetDataBatchSize > 0)
            //    {
            //        apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
            //    }
            //    apiUrl = apiUrl + "&sortBy=updatedAt&sortOrder=desc";

            // }

            var structureFilter = ParseAccountStrurctureData(apiRrequest.OrgAccountStructureLevel);

            if (string.IsNullOrEmpty(structureFilter.AccountId))
            {
                response.IsSuccess = false;
                response.Message = "Please check , AccountId is null or empty";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            StringBuilder apiUrlBuilder = new StringBuilder(request.ToolActions[0].ApiUrl);

            if (!string.IsNullOrEmpty(nextcursor))
            {
                apiUrlBuilder.Append($"?cursor={nextcursor}");
                apiUrlBuilder.Append("&sortBy=updatedAt&sortOrder=desc");
            }
            else
            {
                apiUrlBuilder.Append($"?includeChildren={apiRrequest.IncludeChildren}&includeParents={apiRrequest.IncludeParents}");
                apiUrlBuilder.Append($"&accountIds={structureFilter.AccountId}");
                if (!string.IsNullOrEmpty(structureFilter.SiteId))
                {
                    apiUrlBuilder.Append($"&siteIds={structureFilter.SiteId}");
                }
                if (!string.IsNullOrEmpty(structureFilter.GroupId))
                {
                    apiUrlBuilder.Append($"&groupIds={structureFilter.GroupId}");
                }
                if (!string.IsNullOrEmpty(apiRrequest.ExclusionListItemId))
                {
                    apiUrlBuilder.Append($"&ids={apiRrequest.ExclusionListItemId}");
                }

                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrlBuilder.Append($"&limit={request.ToolActions[0].GetDataBatchSize}");
                }

                apiUrlBuilder.Append("&sortBy=updatedAt&sortOrder=desc");
            }

            string apiUrl = apiUrlBuilder.ToString();


            Exclusions apiApplications = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                // Set the api token in the request headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", request.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.GetAsync(apiUrl);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    apiApplications = JsonSerializer.Deserialize<Exclusions>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.Data.Count > 0)
                    {
                        response = BuildResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetExclusions API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildResponse(false, "No records found", request, null);
                        _logger.AddLogInforation($"GetExclusions API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                        response.HttpStatusCode = HttpStatusCode.NotFound;
                        response.Message = "No records found";
                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildResponse(false, "API Failed...", request, null);
                    response.errors = new List<string>() { errorResponse };
                    _logger.AddLogInforation($"GetExclusions API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetExclusions action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                //return response;

                return response;
            }
        }

        private Exclusions BuildResponse(bool isSuccess, string message, OrganizationToolModel dto, Exclusions applications)
        {
            var res = new Exclusions()
            {
                IsSuccess = isSuccess,
                Message = message,

            };

            if (applications != null)
            {
                res.Data = applications.Data;
                res.Pagination = applications.Pagination;
            }
            return res;
        }

        private SentinelOneAccountStructure ParseAccountStrurctureData(List<AccountStructureLevel> data)
        {
            SentinelOneAccountStructure ret = new SentinelOneAccountStructure();

            if (data != null)
            {
                ret.AccountId = data.Where(d => d.LevelName == Constants.SentinalOne_AcccountStrucure_AccountId).FirstOrDefault().LevelValue;
                var siteData = data.Where(d => d.LevelName == Constants.SentinalOne_AcccountStrucure_SiteId).FirstOrDefault();
                if (siteData != null)
                {
                    ret.SiteId = siteData.LevelValue;

                }
                var groupdata = data.Where(d => d.LevelName == Constants.SentinalOne_AcccountStrucure_GroupId).FirstOrDefault();
                if (groupdata != null)
                {
                    ret.GroupId = siteData.LevelValue;

                }

            }
            return ret;
        }
        public async Task<BlockList> GetBlockList(BlockListRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            BlockList response = new BlockList();

            _logger.AddLogInforation($"GetExclusions action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            var structureFilter = ParseAccountStrurctureData(apiRequest.OrgAccountStructureLevel);

            if (string.IsNullOrEmpty(structureFilter.AccountId))
            {
                response.IsSuccess = false;
                response.Message = "Please check , AccountId is null or empty";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            //
            //string apiUrl = request.ToolActions[0].ApiUrl;
            StringBuilder apiUrl = new StringBuilder().Append(request.ToolActions[0].ApiUrl);
            //
            if (!string.IsNullOrEmpty(nextcursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl.Append($"?cursor={nextcursor}");

                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrl.Append($"&limit={request.ToolActions[0].GetDataBatchSize}");
                   // apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
                }
               
            }
            else
            {
                //
                apiUrl.Append($"?includeChildren={apiRequest.IncludeChildren}&includeParents={apiRequest.IncludeParents}");
                apiUrl.Append($"&accountIds={structureFilter.AccountId}");
                if (!string.IsNullOrEmpty(structureFilter.SiteId))
                {
                    apiUrl.Append($"&siteIds={structureFilter.SiteId}");
                }
                if (!string.IsNullOrEmpty(structureFilter.GroupId))
                {
                    apiUrl.Append($"&groupIds={structureFilter.GroupId}");
                }
                if (!string.IsNullOrEmpty(apiRequest.BlockListItemId))
                {
                    apiUrl.Append($"&ids={apiRequest.BlockListItemId}");
                }
                //
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {

                    apiUrl.Append($"&limit={request.ToolActions[0].GetDataBatchSize}");
                }
               
                


                //}
            }
            apiUrl.Append("&sortBy=updatedAt&sortOrder=desc");
            //
            BlockList apiApplications = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                // Set the api token in the request headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", request.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.GetAsync(apiUrl.ToString());
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    apiApplications = JsonSerializer.Deserialize<BlockList>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.Data.Count > 0)

                    {
                        response = BuildBlockListResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetBlockList API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildBlockListResponse(false, "No records found", request, null);
                        _logger.AddLogInforation($"GetBlockList API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildBlockListResponse(false, "API Failed...", request, null);
                    response.errors = new List<string>() { errorResponse };
                    _logger.AddLogInforation($"GetBlockList API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetBlockList action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                //return response;

                return response;
            }
        }

        private BlockList BuildBlockListResponse(bool isSuccess, string message, OrganizationToolModel dto, BlockList applications)
        {
            var res = new BlockList()
            {
                IsSuccess = isSuccess,
                Message = message,

            };

            if (applications != null)
            {
                res.Data = applications.Data;
                res.Pagination = applications.Pagination;
            }
            return res;
        }

        public async Task<Account> GetAccounts(GetAccountsRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            Account response = new Account();

            _logger.AddLogInforation($"GetAccounts action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            //
            string apiUrl = request.ToolActions[0].ApiUrl;
            ;
            //
            if (!string.IsNullOrEmpty(nextcursor))
            {
                apiUrl = apiUrl + $"?cursor={nextcursor}";

                if (request.ToolActions[0].GetDataBatchSize > 0)
                {

                    apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
                }
            }
            else
            {
                //
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {

                    apiUrl = apiUrl + $"?limit={request.ToolActions[0].GetDataBatchSize}";
                }
                //}
            }
            //
            Account apiResponse = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                // Set the api token in the request headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", request.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponseData = await client.GetAsync(apiUrl);
                string apiResponseString = string.Empty;
                if (apiResponseData.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponseData.Content.ReadAsStringAsync();
                    apiResponse = JsonSerializer.Deserialize<Account>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiResponse.data.Count > 0)

                    {
                        BuildCommonResponse(true, "GetAccount details", HttpStatusCode.OK, apiResponse);
                        _logger.AddLogInforation($"GetAccounts API Returned account details.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        apiResponse = new Account();
                        BuildCommonResponse(false, "No records found", HttpStatusCode.NotFound, apiResponse);
                        _logger.AddLogInforation($"GetAccounts API Returned 0 records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    apiResponse = new Account();
                    var errorResponse = await apiResponseData.Content.ReadAsStringAsync();
                    BuildCommonResponse(false, "API Failed...", HttpStatusCode.UnprocessableEntity, apiResponse);
                    apiResponse.errors = new List<string> { errorResponse };
                    _logger.AddLogInforation($"GetAccounts API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetAccounts action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          

                return apiResponse;
            }
        }
        public async Task<AccountPolicy> GetAccountPolicy(GetAccountPolicyRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {

            _logger.AddLogInforation($"GetAccounts action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
            //
            string apiUrl = request.ToolActions[0].ApiUrl.Replace("{id}", apiRequest.ScopeId); 
            //
            AccountPolicy apiResponse = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                // Set the api token in the request headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", request.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponseData = await client.GetAsync(apiUrl);
                string apiResponseString = string.Empty;
                if (apiResponseData.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponseData.Content.ReadAsStringAsync();
                    apiResponse = JsonSerializer.Deserialize<AccountPolicy>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiResponse.data != null )
                    {
                        BuildCommonResponse(true, "GetAccount details", HttpStatusCode.OK, apiResponse);
                        _logger.AddLogInforation($"GetAccounts API Returned account details.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        apiResponse = new AccountPolicy();
                        BuildCommonResponse(false, "No records found", HttpStatusCode.NotFound, apiResponse);
                        _logger.AddLogInforation($"GetAccounts API Returned 0 records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    apiResponse = new AccountPolicy();
                    var errorResponse = await apiResponseData.Content.ReadAsStringAsync();
                    BuildCommonResponse(false, "API Failed...", HttpStatusCode.UnprocessableEntity, apiResponse);
                    apiResponse.errors = new List<string> { errorResponse };
                    _logger.AddLogInforation($"GetAccounts API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetAccounts action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                //return response;

                return apiResponse;
            }
        }

        public async Task<SentinelOneSiteData> GetSites(GetSitesRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            _logger.AddLogInforation($"GetSites action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
            SentinelOneSiteData apiResponse = null;

            if (!string.IsNullOrEmpty(apiRequest.AccountId))
            {
                request.ToolActions[0].ApiUrl = request.ToolActions[0].ApiUrl + $"?accountId={apiRequest.AccountId}";
            }
            using (var client = _httpClientFactory.CreateClient())
            {
                // Set the api token in the request headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", request.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponseData = await client.GetAsync(request.ToolActions[0].ApiUrl);
                string apiResponseString = string.Empty;
                if (apiResponseData.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponseData.Content.ReadAsStringAsync();
                    apiResponse = JsonSerializer.Deserialize<SentinelOneSiteData>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiResponse.data != null)
                    {
                        BuildCommonResponse(true, "GetSites details", HttpStatusCode.OK, apiResponse);
                        _logger.AddLogInforation($"GetSites API Returned account details.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        apiResponse = new SentinelOneSiteData();
                        BuildCommonResponse(false, "No records found", HttpStatusCode.NotFound, apiResponse);
                        _logger.AddLogInforation($"GetSites API Returned 0 records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    apiResponse = new SentinelOneSiteData();
                    var errorResponse = await apiResponseData.Content.ReadAsStringAsync();
                    BuildCommonResponse(false, "API Failed...", HttpStatusCode.UnprocessableEntity, apiResponse);
                    apiResponse.errors = new List<string> { errorResponse };
                    _logger.AddLogInforation($"GetSites API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetSites action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          

                return apiResponse;
            }
        }
        public async Task<SentinelOneGroup> GetGroups(GetGroupsRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            _logger.AddLogInforation($"GetGroups action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
            //
            if (!string.IsNullOrEmpty(apiRequest.SiteId))
            {
                request.ToolActions[0].ApiUrl = request.ToolActions[0].ApiUrl + $"?siteIds={apiRequest.SiteId}";
            }
            SentinelOneGroup apiResponse = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                // Set the api token in the request headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", request.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponseData = await client.GetAsync(request.ToolActions[0].ApiUrl);
                string apiResponseString = string.Empty;
                if (apiResponseData.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponseData.Content.ReadAsStringAsync();
                    apiResponse = JsonSerializer.Deserialize<SentinelOneGroup>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiResponse.data != null)
                    {
                        BuildCommonResponse(true, "GetGroups details", HttpStatusCode.OK, apiResponse);
                        _logger.AddLogInforation($"GetGroups API Returned group details.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        apiResponse = new SentinelOneGroup();
                        BuildCommonResponse(false, "No records found", HttpStatusCode.NotFound, apiResponse);
                        _logger.AddLogInforation($"GetGroups API Returned 0 records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    apiResponse = new SentinelOneGroup();
                    var errorResponse = await apiResponseData.Content.ReadAsStringAsync();
                    BuildCommonResponse(false, "API Failed...", HttpStatusCode.UnprocessableEntity, apiResponse);
                    apiResponse.errors = new List<string> { errorResponse };
                    _logger.AddLogInforation($"GetGroups API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetGroups action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                      

                return apiResponse;
            }
        }

        private void BuildCommonResponse(bool isSuccess, string message, HttpStatusCode statusCode, baseResponse applications)
        {
            applications.IsSuccess = isSuccess;
            applications.Message = message;
            applications.HttpStatusCode = statusCode;

        }
    }
}
