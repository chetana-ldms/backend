using LDP.Common.BL.Interfaces;
using LDP.Common.Services.SentinalOneIntegration.Applications.Agent;
using LDP.Common.Services.SentinalOneIntegration.Applications.CVS;
using LDP.Common.Services.SentinalOneIntegration.Applications.EndPoints;
using LDP.Common.Services.SentinalOneIntegration.Applications.Inventory;
using LDP.Common.Services.SentinalOneIntegration.Applications.ManagementSettings;
using LDP.Common.Services.SentinalOneIntegration.Applications.RiskApplicaionsEndPoint;
using LDP.Common.Services.SentinalOneIntegration.Applications.Risks;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    public class SentinalOneApplicationsIntegrationService : ISentinalOneApplicationsIntegrationService
    {
        IApplicationLogBL _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public SentinalOneApplicationsIntegrationService(IApplicationLogBL logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetAggragatedApplicationsWithRiskResponse> GetAggregatedApplications(OrganizationToolModel request, string nextcursor = null)
        {
            GetAggragatedApplicationsWithRiskResponse response = new GetAggragatedApplicationsWithRiskResponse();

            _logger.AddLogInforation($"GetAggregatedApplications action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            // SentinalOneGetThreatResponse response = null;
            //
            string apiUrl = request.ToolActions[0].ApiUrl;
            //
            if (!string.IsNullOrEmpty(nextcursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl = apiUrl + $"?cursor={nextcursor}";

            }
            else
            {
                if (request.ToolActions[0].LastReadAlertDate != null)
                {
                    // pull the new data after last pull 
                    apiUrl = apiUrl + $"&createdAt__gt={request.ToolActions[0].LastReadAlertDate}";
                }
            }
            if (request.GetDataBatchSize > 0)
            {

                apiUrl = apiUrl + $"&limit={request.GetDataBatchSize}";
            }
            // apiUrl = apiUrl + "&sortBy=createdAt";
            GetAggragatedApplicationsWithRiskResponse apiApplications = null;
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
                    apiApplications = JsonSerializer.Deserialize<GetAggragatedApplicationsWithRiskResponse>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.data.Count > 0)
                    {
                        response = BuildResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetAggregatedApplications API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildResponse(false, "No records found", request, null);
                        _logger.AddLogInforation($"GetAggregatedApplications API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildResponse(false, errorResponse, request, null);
                    _logger.AddLogInforation($"GetAggregatedApplications API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetAggregatedApplications action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                //return response;

                return response;
            }

        }

        private GetAggragatedApplicationsWithRiskResponse BuildResponse(bool isSuccess, string message, OrganizationToolModel dto, GetAggragatedApplicationsWithRiskResponse applications)
        {
            var res = new GetAggragatedApplicationsWithRiskResponse()
            {
                IsSuccess = isSuccess,
                Message = message,

            };

            if (applications != null)
            {
                res.data = applications.data;
                res.pagination = applications.pagination;
            }
            return res;
        }
        private SentinelOneAccountStructure ParseAccountStrurctureData(List<AccountStructureLevel> data)
        {
            SentinelOneAccountStructure ret = new SentinelOneAccountStructure();

            if (data != null )
            {
                ret.AccountId = data.Where(d => d.LevelName == Constants.SentinalOne_AcccountStrucure_AccountId).FirstOrDefault().LevelValue;
                var siteData = data.Where(d => d.LevelName == Constants.SentinalOne_AcccountStrucure_SiteId).FirstOrDefault();
                if (siteData != null ) 
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
        
        public async Task<ApplicationsWithRisks> GetApplicationsAndRisks(GetSentinalOneEndPointApplicationsRisksRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            ApplicationsWithRisks response = new ApplicationsWithRisks();

            _logger.AddLogInforation($"GetApplicationsAndRisks action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            var structureFilter = ParseAccountStrurctureData(apiRequest.OrgAccountStructureLevel);

            if (string.IsNullOrEmpty(structureFilter.AccountId))
            {
                response.IsSuccess = false;
                response.Message = "Please check , AccountId is null or empty";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            StringBuilder apiUrl = new StringBuilder().Append(request.ToolActions[0].ApiUrl );
            //
            if (!string.IsNullOrEmpty(nextcursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl.Append($"?cursor={nextcursor}");
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrl.Append($"&limit={request.ToolActions[0].GetDataBatchSize}");
                }
                apiUrl.Append("&sortBy=name&sortOrder=asc");
            }
            else
            {
                apiUrl.Append($"?accountIds={structureFilter.AccountId}");
                if (!string.IsNullOrEmpty(structureFilter.SiteId))
                {
                    apiUrl.Append($"&siteIds={structureFilter.SiteId}");
                }
                if (!string.IsNullOrEmpty(structureFilter.GroupId))
                {
                    apiUrl.Append($"&groupIds={structureFilter.GroupId}");
                }
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrl.Append($"&limit={request.ToolActions[0].GetDataBatchSize}");
                }
                apiUrl.Append("&sortBy=name&sortOrder=asc");
            }
            //if (request.ToolActions[0].GetDataBatchSize > 0)
            //{
            //    apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
            //}
            // apiUrl = apiUrl + "&sortBy=name&sortOrder=asc";
            ApplicationsWithRisks apiApplications = null;
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
                    apiApplications = JsonSerializer.Deserialize<ApplicationsWithRisks>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.Data.Count > 0)
                    {
                        response = BuildApplicationsAndRisksResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetApplicationsAndRisks API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildApplicationsAndRisksResponse(false, "No records found", request, null);
                        response.HttpStatusCode = HttpStatusCode.NotFound;
                        response.Message = "No records found";
                        _logger.AddLogInforation($"GetApplicationsAndRisks API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildApplicationsAndRisksResponse(false, errorResponse, request, null);
                    _logger.AddLogInforation($"GetApplicationsAndRisks API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetApplicationsAndRisks action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                //return response;

                return response;
            }
        }
        private ApplicationsWithRisks BuildApplicationsAndRisksResponse(bool isSuccess, string message, OrganizationToolModel dto, ApplicationsWithRisks applications)
        {
            var res = new ApplicationsWithRisks()
            {
                IsSuccess = isSuccess


            };
            if (isSuccess)
            {
                res.Message = "List of applications and risks from sentinalOne tool";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                res.Message = "API call failure";
                res.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                res.errors = new List<string>() { message };
            }
            if (applications != null)
            {
                res.Data = applications.Data;
                res.Pagination = applications.Pagination;
            }
            return res;
        }
        public async Task<RiskApplicationsEndPoints> GetRiskApplicationEndpoints(GetRiskApplicationEndpointRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            RiskApplicationsEndPoints response = new RiskApplicationsEndPoints();

            _logger.AddLogInforation($"GetApplicationsAndRisks action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            // SentinalOneGetThreatResponse response = null;
            //
            //https://apse1-2001.sentinelone.net/web/api/v2.1/application-management/risks/endpoints?applicationIds=1883950601328333580
            string apiUrl = request.ToolActions[0].ApiUrl;
            //apiUrl = apiUrl + $"?1=1";
            //
            if (!string.IsNullOrEmpty(nextcursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl = apiUrl + $"?cursor={nextcursor}";
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
                }
                apiUrl = apiUrl + "&sortBy=name&sortOrder=asc";

            }
            else
            {
                apiUrl = apiUrl + $"?applicationIds={apiRequest.ApplicationId}";
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
                    //apiUrl = apiUrl + "&sortBy=name&sortOrder=asc";
                }

                apiUrl = apiUrl + "&sortBy=name&sortOrder=asc";
            }

            RiskApplicationsEndPoints apiApplications = null;
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
                    apiApplications = JsonSerializer.Deserialize<RiskApplicationsEndPoints>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.Data.Count > 0)
                    {
                        response = BuildRiskApplicationsEndPointsResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetApplicationsAndRisks API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        apiApplications = new RiskApplicationsEndPoints();
                        apiApplications.Data = new List<RiskApplicaionsEndPoint.Data>();
                        response = BuildRiskApplicationsEndPointsResponse(false, "No records found", request, apiApplications);
                        _logger.AddLogInforation($"GetApplicationsAndRisks API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildRiskApplicationsEndPointsResponse(false, errorResponse, request, null);
                    _logger.AddLogInforation($"GetApplicationsAndRisks API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetApplicationsAndRisks action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                //return response;

                return response;
            }
        }
        private RiskApplicationsEndPoints BuildRiskApplicationsEndPointsResponse(bool isSuccess, string message, OrganizationToolModel dto, RiskApplicationsEndPoints applications)
        {
            var res = new RiskApplicationsEndPoints()
            {
                IsSuccess = isSuccess
            };

            if (applications != null)
            {
                res.Data = applications.Data;
                res.Pagination = applications.Pagination;
                if (applications.Data.Count == 0)
                {
                    res.Message = "No Records found";
                    res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                }
                else
                {
                    res.Message = "List of applications and risks from sentinalOne tool";
                    res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                }
            }
            else
            {
                res.Message = "API call failure";
                res.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                res.errors = new List<string>() { message };
            }
            return res;
        }
        public async Task<SentinalOneApplicationInventory> GetSentinalOneApplicationInventory(SentinalOneApplicationsInventoryRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            SentinalOneApplicationInventory response = new SentinalOneApplicationInventory();

            _logger.AddLogInforation($"GetSentinalOneApplicationInventory action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            // SentinalOneGetThreatResponse response = null;
            //

            var structureFilter = ParseAccountStrurctureData(apiRequest.OrgAccountStructureLevel);

            if (string.IsNullOrEmpty(structureFilter.AccountId))
            {
                response.IsSuccess = false;
                response.Message = "Please check , AccountId is null or empty";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }


            //string apiUrl = request.ToolActions[0].ApiUrl;
            ////
            //if (!string.IsNullOrEmpty(nextcursor))
            //{
            //    // if current criteria has multiple pages of data 
            //    apiUrl = apiUrl + $"?cursor={nextcursor}";
            //    if (request.ToolActions[0].GetDataBatchSize > 0)
            //    {

            //        apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
            //    }

            //}
            //else
            //{

            //    if (!string.IsNullOrEmpty(apiRequest.SiteId))
            //    {
            //        apiUrl = apiUrl + $"?siteIds={apiRequest.SiteId}";
            //        apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
            //    }
            //    else
            //    if (request.ToolActions[0].GetDataBatchSize > 0)
            //    {

            //        apiUrl = apiUrl + $"?limit={request.ToolActions[0].GetDataBatchSize}";
            //    }
            //}
            StringBuilder apiUrlBuilder = new StringBuilder(request.ToolActions[0].ApiUrl);

            if (!string.IsNullOrEmpty(nextcursor))
            {
                apiUrlBuilder.Append($"?cursor={nextcursor}");

                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrlBuilder.Append($"&limit={request.ToolActions[0].GetDataBatchSize}");
                }
            }
            else
            {
                apiUrlBuilder.Append($"?accountIds={structureFilter.AccountId}");
                if (!string.IsNullOrEmpty(structureFilter.SiteId))
                {
                    apiUrlBuilder.Append($"&siteIds={structureFilter.SiteId}");
                }
                if (!string.IsNullOrEmpty(structureFilter.GroupId))
                {
                    apiUrlBuilder.Append($"&groupIds={structureFilter.GroupId}");
                }
                else if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrlBuilder.Append($"&limit={request.ToolActions[0].GetDataBatchSize}");
                }
            }

            string apiUrl = apiUrlBuilder.ToString();

            // apiUrl = apiUrl + "&sortBy=createdAt";
            SentinalOneApplicationInventory apiApplications = null;
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
                    apiApplications = JsonSerializer.Deserialize<SentinalOneApplicationInventory>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.data.Count > 0)
                    {
                        response = BuildApplicationInventoryResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetSentinalOneApplicationInventory API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        apiApplications = new SentinalOneApplicationInventory();
                        apiApplications.data = new List<Inventory.Data>();
                        response = BuildApplicationInventoryResponse(false, "No records found", request, apiApplications);
                        _logger.AddLogInforation($"GetSentinalOneApplicationInventory API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildApplicationInventoryResponse(false, errorResponse, request, null);
                    _logger.AddLogInforation($"GetSentinalOneApplicationInventory API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetSentinalOneApplicationInventory action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                //return response;

                return response;
            }

        }
        private SentinalOneApplicationInventory BuildApplicationInventoryResponse(bool isSuccess, string message, OrganizationToolModel dto, SentinalOneApplicationInventory applications)
        {
            var res = new SentinalOneApplicationInventory()
            {
                IsSuccess = isSuccess,
                //              Message = message,

            };

            if (applications != null)
            {
                res.data = applications.data;
                res.pagination = applications.pagination;
                if (applications.data.Count == 0)
                {
                    res.Message = "No records found";
                    res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;

                }
                else
                {
                    res.Message = message;
                    res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                }
            }
            else
            {
                res.Message = "API call failed";
                res.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                res.errors = new List<string>() { message };
            }
            return res;
        }
        public async Task<ApplicationEndPoints> GetSentinalOneApplicationEndPoints(SentinalOneApplicationsEndPointsRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            ApplicationEndPoints response = new ApplicationEndPoints();

            _logger.AddLogInforation($"GetSentinalOneApplicationEndPoints action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            //
            string apiUrl = request.ToolActions[0].ApiUrl;
            //
            if (!string.IsNullOrEmpty(nextcursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl = apiUrl + $"?cursor={nextcursor}";
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {

                    apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
                }

            }
            else
            {
                //if (request.ToolActions[0].LastReadAlertDate != null)
                //{
                //    // pull the new data after last pull 
                //    apiUrl = apiUrl + $"&createdAt__gt={request.ToolActions[0].LastReadAlertDate}";
                //}
                apiUrl = apiUrl + $"?applicationName={apiRequest.ApplicationName}&applicationVendor={apiRequest.ApplicationVendor}";
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
                }
            }


            ApplicationEndPoints apiApplications = null;
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
                    apiApplications = JsonSerializer.Deserialize<ApplicationEndPoints>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.data.Count > 0)
                    {
                        response = BuildApplicationEndPointResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetSentinalOneApplicationEndPoints API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        apiApplications = new ApplicationEndPoints();
                        apiApplications.data = new List<EndPoints.Data>();
                        response = BuildApplicationEndPointResponse(false, "No records found", request, apiApplications);
                        _logger.AddLogInforation($"GetSentinalOneApplicationEndPoints API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildApplicationEndPointResponse(false, errorResponse, request, null);
                    _logger.AddLogInforation($"GetSentinalOneApplicationInventory API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetSentinalOneApplicationEndPoints action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          

                return response;
            }
        }
        private ApplicationEndPoints BuildApplicationEndPointResponse(bool isSuccess, string message, OrganizationToolModel dto, ApplicationEndPoints applications)
        {
            var res = new ApplicationEndPoints()
            {
                IsSuccess = isSuccess,
                Message = message,

            };

            if (applications != null)
            {
                res.data = applications.data;
                res.pagination = applications.pagination;
                if (res.data.Count == 0)
                {
                    res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                }
                else
                {
                    res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                }

            }
            else
            {
                res.Message = "API call failed";
                res.errors = new List<string>() { message };
                res.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
            }
            return res;
        }
        public async Task<ApplicationCVS> GetSentinalOneApplicationCVS(SentinalOneApplicationsCVSRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            ApplicationCVS response = new ApplicationCVS();

            _logger.AddLogInforation($"GetSentinalOneApplicationCVS action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            //

            string apiUrl = request.ToolActions[0].ApiUrl;
            //apiUrl = apiUrl + $"?1=1";
            //
            if (!string.IsNullOrEmpty(nextcursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl = apiUrl + $"?cursor={nextcursor}";
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
                }
                apiUrl = apiUrl + "&sortBy=name&sortOrder=asc";

            }
            else
            {
                apiUrl = apiUrl + $"?applicationIds={apiRequest.ApplicationId}";
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
                    //apiUrl = apiUrl + "&sortBy=name&sortOrder=asc";
                }

                apiUrl = apiUrl + "&sortBy=publishedDate&sortOrder=desc";
            }

            ApplicationCVS apiApplications = null;
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
                    apiApplications = JsonSerializer.Deserialize<ApplicationCVS>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.data.Count > 0)
                    {
                        response = BuildApplicationCVSResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetSentinalOneApplicationCVS API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildApplicationCVSResponse(false, "No records found", request, null);
                        _logger.AddLogInforation($"GetSentinalOneApplicationCVS API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildApplicationCVSResponse(false, errorResponse, request, null);
                    _logger.AddLogInforation($"GetSentinalOneApplicationCVS API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetSentinalOneApplicationCVS action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          

                return response;

            }
        }

        private ApplicationCVS BuildApplicationCVSResponse(bool isSuccess, string message, OrganizationToolModel dto, ApplicationCVS applications)
        {
            var res = new ApplicationCVS()
            {
                IsSuccess = isSuccess
            };

            if (applications != null)
            {
                res.data = applications.data;
                res.pagination = applications.pagination;
                if (applications.data.Count == 0)
                {
                    res.Message = "No Records found";
                    res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                }
                else
                {
                    res.Message = "List of applications and risks from sentinalOne tool";
                    res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                }
            }
            else
            {
                res.Message = "API call failure";
                res.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                res.errors = new List<string>() { message };
            }
            return res;
        }
        public async Task<EndPointDetails> GetSentinalOneApplicationAgentDetails(SentinalOneApplicationsAgentRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        {
            EndPointDetails response = new EndPointDetails();

            _logger.AddLogInforation($"GetSentinalOneApplicationAgentDetails action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

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
            ////
            //if (!string.IsNullOrEmpty(nextcursor))
            //{
            //    // if current criteria has multiple pages of data 
            //    apiUrl = apiUrl + $"?cursor={nextcursor}";
            //    if (request.ToolActions[0].GetDataBatchSize > 0)
            //    {

            //        apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
            //    }

            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(apiRequest.EndPiontId))
            //    {
            //        apiUrl = apiUrl + $"?ids={apiRequest.EndPiontId}";
            //        if (request.ToolActions[0].GetDataBatchSize > 0)
            //        {

            //            apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
            //        }
            //    }
            //    else
            //    {
            //        if (request.ToolActions[0].GetDataBatchSize > 0)
            //        {

            //            apiUrl = apiUrl + $"?limit={request.ToolActions[0].GetDataBatchSize}";
            //        }
            //    }

            //}
            StringBuilder apiUrlBuilder = new StringBuilder(request.ToolActions[0].ApiUrl);

            if (!string.IsNullOrEmpty(nextcursor))
            {
                apiUrlBuilder.Append($"?cursor={nextcursor}");

                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrlBuilder.Append($"&limit={request.ToolActions[0].GetDataBatchSize}");
                }
            }
            else
            {
                apiUrlBuilder.Append($"?accountIds={structureFilter.AccountId}");
                if (!string.IsNullOrEmpty(structureFilter.SiteId))
                {
                    apiUrlBuilder.Append($"&siteIds={structureFilter.SiteId}");
                }
                if (!string.IsNullOrEmpty(structureFilter.GroupId))
                {
                    apiUrlBuilder.Append($"&groupIds={structureFilter.GroupId}");
                }
                if (!string.IsNullOrEmpty(apiRequest.EndPiontId))
                {
                    apiUrlBuilder.Append($"&ids={apiRequest.EndPiontId}");
                }
                if (request.ToolActions[0].GetDataBatchSize > 0)
                {
                    apiUrlBuilder.Append($"&limit={request.ToolActions[0].GetDataBatchSize}");
                }
                
            }

            string apiUrl = apiUrlBuilder.ToString();
            EndPointDetails apiApplications = null;
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
                    apiApplications = JsonSerializer.Deserialize<EndPointDetails>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.data.Count > 0)
                    {
                        response = BuildApplicationEndPointSResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetSentinalOneApplicationAgentDetails API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildApplicationEndPointSResponse(false, "No records found", request, null);
                        _logger.AddLogInforation($"GetSentinalOneApplicationAgentDetails API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildApplicationEndPointSResponse(false, errorResponse, request, null);
                    _logger.AddLogInforation($"GetSentinalOneApplicationAgentDetails API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetSentinalOneApplicationAgentDetails action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          

                return response;

            }
        }

        private EndPointDetails BuildApplicationEndPointSResponse(bool isSuccess, string message, OrganizationToolModel dto, EndPointDetails applications)
        {
            var res = new EndPointDetails()
            {
                IsSuccess = isSuccess,
                Message = message,

            };

            if (applications != null)
            {
                res.data = applications.data;
                res.pagination = applications.pagination;
            }
            return res;
        }

        //public async Task<EndPointDetails> GetSentinelEndPoints(GetEndPointsRequest apiRequest, OrganizationToolModel request, string nextcursor = null)
        //{
        //    EndPointDetails response = new EndPointDetails();

        //    _logger.AddLogInforation($"GetSentinelEndPoints action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

        //    //
        //    string apiUrl = request.ToolActions[0].ApiUrl;
        //    //
        //    if (!string.IsNullOrEmpty(nextcursor))
        //    {
        //        // if current criteria has multiple pages of data 
        //        apiUrl = apiUrl + $"?cursor={nextcursor}";
        //        if (request.GetDataBatchSize > 0)
        //        {

        //            apiUrl = apiUrl + $"&limit={request.GetDataBatchSize}";
        //        }

        //    }
        //    else
        //    {
        //        if (request.GetDataBatchSize > 0)
        //        {

        //            apiUrl = apiUrl + $"?limit={request.GetDataBatchSize}";
        //        }
        //        // apiUrl = apiUrl + $"?ids={apiRequest.EndPiontId}";
        //    }
            
        //    // apiUrl = apiUrl + "&sortBy=createdAt";

        //    EndPointDetails apiApplications = null;
        //    using (var client = _httpClientFactory.CreateClient())
        //    {
        //        // Set the api token in the request headers
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", request.AuthKey);
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        //        //
        //        client.DefaultRequestHeaders.Add("Accept", "application/json");
        //        var apiResponse = await client.GetAsync(apiUrl);
        //        string apiResponseString = string.Empty;
        //        if (apiResponse.IsSuccessStatusCode)
        //        {
        //            apiResponseString = await apiResponse.Content.ReadAsStringAsync();
        //            apiApplications = JsonSerializer.Deserialize<EndPointDetails>(apiResponseString,
        //                 new JsonSerializerOptions()
        //                 {
        //                     PropertyNameCaseInsensitive = true
        //                 });
        //            if (apiApplications.data.Count > 0)
        //            {
        //                response = BuildApplicationEndPointSResponse(true, "Success", request, apiApplications);
        //                _logger.AddLogInforation($"GetSentinelEndPoints API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

        //            }
        //            else
        //            {
        //                response = BuildApplicationEndPointSResponse(false, "No records found", request, null);
        //                _logger.AddLogInforation($"GetSentinelEndPoints API Returned {apiApplications.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

        //            }
        //        }
        //        else
        //        {
        //            var errorResponse = await apiResponse.Content.ReadAsStringAsync();
        //            response = BuildApplicationEndPointSResponse(false, errorResponse, request, null);
        //            _logger.AddLogInforation($"GetSentinelEndPoints API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

        //        }
        //        _logger.AddLogInforation($"GetSentinelEndPoints action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
        //        //          

        //        return response;

        //    }
        //}
        public async Task<EndPointApplications> GetSentinalOneEndPointApplications(GetSentinalOneEndPointApplicationsRequest apiRequest, OrganizationToolModel request)
        {
            EndPointApplications response = new EndPointApplications();

            _logger.AddLogInforation($"GetSentinalOneEndPointApplications action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            //
            string apiUrl = request.ToolActions[0].ApiUrl;
            //
            
            apiUrl = apiUrl + $"?ids={apiRequest.EndPointId}";

            if (request.GetDataBatchSize > 0)
            {

                apiUrl = apiUrl + $"&limit={request.GetDataBatchSize}";
            }

            EndPointApplications apiApplications = null;
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
                    apiApplications = JsonSerializer.Deserialize<EndPointApplications>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.Data.Count > 0)
                    {
                        response = BuildEndPointApplicationsResponse(true, "Success", request, apiApplications);
                        _logger.AddLogInforation($"GetSentinalOneEndPointApplications API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildEndPointApplicationsResponse(false, "No records found", request, null);
                        _logger.AddLogInforation($"GetSentinalOneEndPointApplications API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildEndPointApplicationsResponse(false, errorResponse, request, null);
                    _logger.AddLogInforation($"GetSentinalOneEndPointApplications API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetSentinalOneEndPointApplications action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          

                return response;

            }
        }
        private EndPointApplications BuildEndPointApplicationsResponse(bool isSuccess, string message, OrganizationToolModel dto, EndPointApplications applications)
        {
            var res = new EndPointApplications()
            {
                IsSuccess = isSuccess
                

            };
            if (isSuccess)
            {
                res.Message = "List of applications End point details from sentinalOne tool";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                res.Message = "API call failure";
                res.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                res.errors = new List<string>() { message };
            }
            if (applications != null)
            {
                res.Data = applications.Data;
               // res.pagination = applications.pagination;
            }
            return res;
        }

        // apiUrl = apiUrl + "&sortBy=createdAt";
        public async Task<AppManagementSettings> GetApplicationSettings(OrganizationToolModel request)
        {
           // AppManagementSettings response = new ManagementSettings.Data();
            _logger.AddLogInforation($"GetApplicationSettings action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
            AppManagementSettings  apiApplications = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                // Set the api token in the request headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", request.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.GetAsync(request.ToolActions[0].ApiUrl);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    apiApplications = JsonSerializer.Deserialize<AppManagementSettings>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications==null )
                    {
                        apiApplications = new AppManagementSettings();
                        BuilSettingsResponse(false, "No records found", HttpStatusCode.NotFound, apiApplications);
                        _logger.AddLogInforation($"GetApplicationSettings API Returned empty object.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                     }
                    else
                    {
                         BuilSettingsResponse(true, "Application management settings ", HttpStatusCode.OK, apiApplications);
                        _logger.AddLogInforation($"GetApplicationSettings API Returned the object data.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                    }
                }
                else
                {
                    apiApplications = new AppManagementSettings();
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    BuilSettingsResponse(false, "API call failed...", HttpStatusCode.UnprocessableEntity, apiApplications);
                    apiApplications.errors = new List<string>() { errorResponse };

                    _logger.AddLogInforation($"GetSentinalOneEndPointApplications API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetSentinalOneEndPointApplications action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          

                return apiApplications;

            }
        }
        private void  BuilSettingsResponse(bool isSuccess, string message, HttpStatusCode statusCode , AppManagementSettings applications)
        {
            applications.IsSuccess = isSuccess;
            applications.Message = message;
            applications.HttpStatusCode = statusCode;
           
        }

        public async Task<EndPointUpdates> GetEndPointUpdates(GetEndPointUpdatesRequest apiRequest, OrganizationToolModel request , string nextcursor = null)
        {
            EndPointUpdates response = new EndPointUpdates();

            _logger.AddLogInforation($"GetEndPointUpdates action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            //
            string apiUrl = request.ToolActions[0].ApiUrl;
            //
            if (!string.IsNullOrEmpty(nextcursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl = apiUrl + $"?cursor={nextcursor}";

            }
            else
            {

                apiUrl = apiUrl + $"?agentId={apiRequest.EndPointId}";
            }
            if (request.ToolActions[0].GetDataBatchSize > 0)
            {

                apiUrl = apiUrl + $"&limit={request.ToolActions[0].GetDataBatchSize}";
            }
            // apiUrl = apiUrl + "&sortBy=createdAt";

            EndPointUpdates apiApplications = null;
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
                    apiApplications = JsonSerializer.Deserialize<EndPointUpdates>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiApplications.Data.Count > 0)
                    {
                        BuildCommonResponse(true, "List of updates for end point", HttpStatusCode.OK, apiApplications);
                        _logger.AddLogInforation($"GetEndPointUpdates API Returned {apiApplications.Data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        apiApplications = new EndPointUpdates();
                         BuildCommonResponse(false, "No records found", HttpStatusCode.NotFound, apiApplications);
                        _logger.AddLogInforation($"GetEndPointUpdates API Returned 0 records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    apiApplications = new EndPointUpdates();
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    BuildCommonResponse(false, "API Failed...", HttpStatusCode.UnprocessableEntity, apiApplications);
                    apiApplications.errors = new List<string> { errorResponse };
                    _logger.AddLogInforation($"GetEndPointUpdates API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetEndPointUpdates action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          

                return apiApplications;

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