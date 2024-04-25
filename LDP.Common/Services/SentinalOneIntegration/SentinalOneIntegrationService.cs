using LDP.Common.BL.Interfaces;
using LDP_APIs.BL.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LDP.Common.Services.SentinalOneIntegration
{
    public class SentinalOneIntegrationService : ISentinalOneIntegrationService
    {
        IApplicationLogBL _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public SentinalOneIntegrationService(IApplicationLogBL logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SentinalOneGetThreatResponse> GetThreats(OrganizationToolModel request, string nextcursor, GetSentinalThreatsRequest apiRequest)
        {
            _logger.AddLogInforation($"SentinalOneIntegrationService action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            SentinalOneGetThreatResponse response = null;
            //
            string apiUrl = request.ToolActions[0].ApiUrl;
            //
            if (!string.IsNullOrEmpty(nextcursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl = apiUrl + $"&cursor={nextcursor}";

            }
            else
            {
                if (apiRequest == null )
                {
                    if (request.ToolActions[0].LastReadAlertDate != null)
                    {
                        // pull the new data after last pull 
                        apiUrl = apiUrl + $"&createdAt__gt={request.ToolActions[0].LastReadAlertDate}";
                    }
                }
                else
                {
                    if (apiRequest.ThreatIds != null)
                    {
                        if (apiRequest.ThreatIds.Count > 0)
                        {
                            string commaSeparatedString = string.Join(",", apiRequest.ThreatIds);

                            apiUrl = apiUrl + $"&ids={commaSeparatedString}";
                        }
                    }
                }
            }
            if (request.GetDataBatchSize > 0)
            {

                apiUrl = apiUrl + $"&limit={request.GetDataBatchSize}";
            }
            apiUrl = apiUrl + "&sortBy=createdAt";
            SentinalThreatData apiThreats = null;
            using (HttpClient client = new HttpClient())
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
                    apiThreats = JsonSerializer.Deserialize<SentinalThreatData>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiThreats.data.Count > 0)
                    {
                        response = BuildResponse(true, "Success", request, apiThreats);
                        _logger.AddLogInforation($"SentinalOne API Returned {apiThreats.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildResponse(false, "No records found", request, null);
                        _logger.AddLogInforation($"SentinalOne API Returned {apiThreats.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildResponse(false, errorResponse, request, null);
                    _logger.AddLogInforation($"SentinalOne API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"SentinalOneIntegrationService action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }

        }
        private SentinalOneGetThreatResponse BuildResponse(bool isSuccess, string message, OrganizationToolModel dto, SentinalThreatData threats)
        {
            return new SentinalOneGetThreatResponse()
            {
                IsSuccess = isSuccess,
                Message = message,
                Alerts = threats,
                OrgId = dto.OrgID,
                ToolId = dto.ToolID
            };
        }
        public async Task<GetThreatTimelineResponse> GetThreatTimeline(GetThreatTimelineDTO request, OrganizationToolModel conndtl)
        {
            _logger.AddLogInforation($"GetThreatTimeline action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            GetThreatTimelineResponse response = null;
            //
            string threatid = request.ThreatId;
            string apiUrl = $"{conndtl.ToolActions[0].ApiUrl}";
            apiUrl = apiUrl.Replace("{threatid}", threatid);
            //
            if (!string.IsNullOrEmpty(request.NextCursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl = apiUrl + $"&cursor={request.NextCursor}";

            }
            else
            {
                //if (conndtl.ToolActions[0].LastReadAlertDate != null)
                //{
                //    // pull the new data after last pull 
                //    apiUrl = apiUrl + $"&createdAt__gt={conndtl.ToolActions[0].LastReadAlertDate}";
                //}
            }
            if (conndtl.GetDataBatchSize > 0)
            {

                apiUrl = apiUrl + $"&limit={conndtl.GetDataBatchSize}";
            }
            apiUrl = apiUrl + "&sortBy=createdAt";
            GetThreatTimeline apiThreatTimeLine = null;
            using (HttpClient client = new HttpClient())
            {
                // Set the api token in the request headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.GetAsync(apiUrl);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    apiThreatTimeLine = JsonSerializer.Deserialize<GetThreatTimeline>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiThreatTimeLine.data.Count > 0)
                    {
                        response = BuildThreatTimelineResponse(true, "Success", HttpStatusCode.OK, apiThreatTimeLine);
                        _logger.AddLogInforation($"GetThreatTimeline API Returned {apiThreatTimeLine.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildThreatTimelineResponse(false, "No records found", HttpStatusCode.NotFound, null);
                        _logger.AddLogInforation($"GetThreatTimeline API Returned {apiThreatTimeLine.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildThreatTimelineResponse(false, errorResponse, HttpStatusCode.InternalServerError, null);
                    _logger.AddLogInforation($"GetThreatTimeline API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetThreatTimeline action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
                //
            }

        }

        private GetThreatTimelineResponse BuildThreatTimelineResponse(bool isSuccess, string message, HttpStatusCode status, GetThreatTimeline threattimeline)
        {
            return new GetThreatTimelineResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,
                ThreatTimeline = threattimeline,
                Message = message,
            };
        }
        public async Task<MitigateActionResponse> ThreatMitigateAction(MitigateActionDTO request, OrganizationToolModel conndtl)
        {

            MitigateActionResponse response = new MitigateActionResponse();


            string apiUrl = $"{conndtl.ToolActions[0].ApiUrl}{request.ActionType}";


            var requestBody = new
            {
                filter = new
                {
                    accountIds = request.AccountIds,
                    siteIds = request.SiteIds,
                    ids = request.ThreatIds// threatIds
                                           // ids = new List<string>() { "1", "2" }
                }
                //,data = new object[]{}
            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PostAsync(apiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();

                    response = BuildMitigateActionResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"Threat mitigation action command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildMitigateActionResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"Threat mitigation action command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"Threat mitigation action command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
        }

        private MitigateActionResponse BuildMitigateActionResponse(bool isSuccess, string message, HttpStatusCode status)
        {
            return new MitigateActionResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,

                Message = message,
            };
        }

        public async Task<UpdateThreatAnalysisVerdictResponse> UpdateThreatAnalysisVerdict(SentinalOneUpdateAnalystVerdictDTO request, OrganizationToolModel conndtl)
        {

            UpdateThreatAnalysisVerdictResponse response = new UpdateThreatAnalysisVerdictResponse();


            var requestBody = new
            {
                filter = new
                {
                    accountIds = request.AccountIds,
                    siteIds = request.SiteIds,
                    ids = request.ThreatIds// threatIds
                    //ids = new List<string>() {"1","2" }
                },
                data = new
                {
                    analystVerdict = request.SentinalOne_Analysis_Verdict  // "true_positive"
                }
            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PostAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildUpdateThreatVerdictAnalysis(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"UpdateThreatAnalysisVerdict command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildUpdateThreatVerdictAnalysis(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"UpdateThreatAnalysisVerdict command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"UpdateThreatAnalysisVerdict command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
            // }

        }

        private UpdateThreatAnalysisVerdictResponse BuildUpdateThreatVerdictAnalysis(bool isSuccess, string message, HttpStatusCode status)
        {
            return new UpdateThreatAnalysisVerdictResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,

                Message = message,
            };
        }

        public async Task<UpdateThreatDetailsResponse> UpdateThreatDetails(SentinalOneUpdateThreatDetailsDTO request, OrganizationToolModel conndtl)
        {
            UpdateThreatDetailsResponse response = new UpdateThreatDetailsResponse();

            object requestdata = null;

            if (!string.IsNullOrEmpty(request.ThreatAnalystVerdict))
            {
                requestdata = new
                {
                    analystVerdict = request.ThreatAnalystVerdict//,
                    //incidentStatus = request.ThreatStatus
                };
            }

            if (!string.IsNullOrEmpty(request.ThreatStatus))
            {
                requestdata = new
                {
                    // analystVerdict = request.ThreatAnalystVerdict,
                    incidentStatus = request.ThreatStatus
                };
            }
            if (!string.IsNullOrEmpty(request.ThreatAnalystVerdict) && !string.IsNullOrEmpty(request.ThreatStatus))
            {
                requestdata = new
                {
                    analystVerdict = request.ThreatAnalystVerdict,
                    incidentStatus = request.ThreatStatus
                };
            }
            var requestBody = new
            {
                filter = new
                {
                    accountIds = request.AccountIds,
                    siteIds = request.SiteIds,
                    ids = request.ThreatIds// threatIds
                                           // ids = new List<string>() { "1", "2" }
                },
                data = requestdata
            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PostAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();

                    response = BuildUpdateThreatDetailsResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"UpdateThreatDetails success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildUpdateThreatDetailsResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"UpdateThreatDetails error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"UpdateThreatDetails action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                return response;
            }
        }

        private UpdateThreatDetailsResponse BuildUpdateThreatDetailsResponse(bool isSuccess, string message, HttpStatusCode status)
        {
            return new UpdateThreatDetailsResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,

                Message = message,
            };
        }
        public async Task<GetThreatNoteServiceResponse> GetThreatNotes(GetThreatNoteeDTO request, OrganizationToolModel conndtl)
        {
            _logger.AddLogInforation($"GetThreatTimeline action Intiated.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            GetThreatNoteServiceResponse response = null;
            //
            string threatid = request.ThreatId;
            string apiUrl = $"{conndtl.ToolActions[0].ApiUrl}";
            apiUrl = apiUrl.Replace("{threatid}", threatid);
            //
            if (!string.IsNullOrEmpty(request.NextCursor))
            {
                // if current criteria has multiple pages of data 
                apiUrl = apiUrl + $"&cursor={request.NextCursor}";

            }
            else
            {
                //if (conndtl.ToolActions[0].LastReadAlertDate != null)
                //{
                //    // pull the new data after last pull 
                //    apiUrl = apiUrl + $"&createdAt__gt={conndtl.ToolActions[0].LastReadAlertDate}";
                //}
            }
            if (conndtl.GetDataBatchSize > 0)
            {

                apiUrl = apiUrl + $"&limit={conndtl.GetDataBatchSize}";
            }
            // apiUrl = apiUrl + "&sortBy=createdAt";
            GetThreatNote apiThreatNotesResponse = null;
            using (HttpClient client = new HttpClient())
            {
                // Set the api token in the request headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.GetAsync(apiUrl);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    apiThreatNotesResponse = JsonSerializer.Deserialize<GetThreatNote>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                    if (apiThreatNotesResponse.data.Count > 0)
                    {
                        response = BuildThreatNoteResponse(true, "Success", HttpStatusCode.OK, apiThreatNotesResponse);
                        _logger.AddLogInforation($"GetThreatNotes API Returned {apiThreatNotesResponse.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                    else
                    {
                        response = BuildThreatNoteResponse(false, "No records found", HttpStatusCode.NotFound, null);
                        _logger.AddLogInforation($"GetThreatNotes API Returned {apiThreatNotesResponse.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                    }
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildThreatNoteResponse(false, errorResponse, HttpStatusCode.InternalServerError, null);
                    _logger.AddLogInforation($"GetThreatNotes API error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"GetThreatNotes action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
                //
            }

        }

        private GetThreatNoteServiceResponse BuildThreatNoteResponse(bool isSuccess, string message, HttpStatusCode status, GetThreatNote threatnoteData)
        {
            return new GetThreatNoteServiceResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,
                ThreatNotes = threatnoteData,
                Message = message,
            };
        }

        public async Task<AddThreatNoteResponse> AddThreatNotes(AddThreatNoteeDTO request, OrganizationToolModel conndtl)
        {

            AddThreatNoteResponse response = new AddThreatNoteResponse();

            var requestBody = new
            {
                filter = new
                {
                    accountIds = request.AccountIds,
                    siteIds = request.SiteIds,
                    ids = request.ThreatIds// threatIds
                    //ids = new List<string>() { "1", "2" }
                },
                data = new
                {
                    text = request.Notes//"Discovered using analysis"
                }
            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PostAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddThreadNoteResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"AddThreatNotes command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddThreadNoteResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"AddThreatNotes command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"AddThreatNotes command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
            // }

        }

        private AddThreatNoteResponse BuildAddThreadNoteResponse(bool isSuccess, string message, HttpStatusCode status)
        {
            return new AddThreatNoteResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,

                Message = message,
            };
        }

        public async Task<AddToNetworkResponse> AddToNetwork(AddToNetworkDTO request, OrganizationToolModel conndtl)
        {
            AddToNetworkResponse response = new AddToNetworkResponse();

            var requestBody = new
            {
                filter = new
                {
                    accountIds = request.AccountIds,
                    siteIds = request.SiteIds,
                    ids = request.AgentIds // 
                }
            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PostAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToNetworkResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"AddToNetwork command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToNetworkResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"AddToNetwork command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"AddToNetwork command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
        }

        private AddToNetworkResponse BuildAddToNetworkResponse(bool isSuccess, string message, HttpStatusCode status)
        {
            return new AddToNetworkResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,
                Message = message,
            };
        }

        public async Task<DisconnectFromNetworkResponse> DisconnectFromNetwork(AddToExclusionList request, OrganizationToolModel conndtl)
        {
            DisconnectFromNetworkResponse response = new DisconnectFromNetworkResponse();

            var requestBody = new
            {
                filter = new
                {
                    accountIds = request.AccountIds,
                    siteIds = request.SiteIds,
                    ids = request.AgentIds // 
                }

            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PostAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildDisconnectFromNetworkResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"DisconnectFromNetwork command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildDisconnectFromNetworkResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"DisconnectFromNetwork command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"DisconnectFromNetwork command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
        }
        private DisconnectFromNetworkResponse BuildDisconnectFromNetworkResponse(bool isSuccess, string message, HttpStatusCode status)
        {
            return new DisconnectFromNetworkResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,
                Message = message,
            };
        }

        public async Task<AddToBlocklistResponse> AddToblockListForThreats(AddToBlocklistDTO request, OrganizationToolModel conndtl)
        {
            AddToBlocklistResponse response = new AddToBlocklistResponse();

            var requestBody = new
            {

                data = new
                {
                    description = request.Description,
                    targetScope = request.TargetScope.ToLower(),
                    externalTicketId = request.ExternalTicketId,
                    note = request.Note
                },
                filter = new
                {
                    accountIds = request.AccountIds,
                    siteIds = request.SiteIds,
                    ids = request.ThreatIds// 
                }

            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PostAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToblockListResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"AddToblockListForThreats command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToblockListResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"AddToblockListForThreats command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"AddToblockListForThreats command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
        }
        public async Task<AddToBlocklistResponse> AddToblockList(AddToBlocklistRequest request, OrganizationToolModel conndtl)
        {
            AddToBlocklistResponse response = new AddToBlocklistResponse();

            if (string.IsNullOrEmpty(request.Source))
            {
                request.Source = Constants.AddToBlockList_Souce;
            }

            object filterobj = null;

            if (!string.IsNullOrEmpty(request.AccountId))
            {
                filterobj = new
                {
                    accountIds = new List<string>() { request.AccountId }
                };
            }
            if (!string.IsNullOrEmpty(request.SiteId))
            {
                filterobj = new
                {
                    siteIds = new List<string>() { request.SiteId }
                };
            }
            if (!string.IsNullOrEmpty(request.GroupId))
            {
                filterobj = new
                {
                    groupIds = new List<string>() { request.GroupId }
                };
            }
            var requestBody = new
            {

                data = new
                {
                    description = request.Description,
                    value = request.Value,
                    osType = request.OSType.ToLower(),
                    source = request.Source.ToLower(),
                    type = Constants.AddToBlockList_Hash
                },
                filter = filterobj
            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PostAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToblockListResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"AddToblockList command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToblockListResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"AddToblockList command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"AddToblockList command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
        }

        public async Task<AddToBlocklistResponse> UpdateAddToblockList(UpdateAddToBlocklistRequest request, OrganizationToolModel conndtl)
        {
            AddToBlocklistResponse response = new AddToBlocklistResponse();

            var requestBody = new
            {

                data = new
                {
                    id = request.Id,
                    source = request.Source,
                    description = request.Description,
                    osType = request.OSType,
                    value = request.Value,
                    type = request.Type
                }

            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PutAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToblockListResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"UpdateAddToblockList command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToblockListResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"UpdateAddToblockList command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"UpdateAddToblockList command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
        }
        public async Task<AddToBlocklistResponse> DeleteAddToblockList(DeleteAddToBlocklistRequest request, OrganizationToolModel conndtl)
        {
            AddToBlocklistResponse response = new AddToBlocklistResponse();
            if (string.IsNullOrEmpty(request.Type))
            {
                request.Type = Constants.AddToBlockList_Hash;
            }
            var requestBody = new
            {

                data = new
                {
                    ids = request.Ids
                    //,type = request.Type
                }

            };
            var requestContent = JsonContent.Create(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var _httpRequest = new HttpRequestMessage(HttpMethod.Delete, conndtl.ToolActions[0].ApiUrl);
                _httpRequest.Content = requestContent;

                var apiResponse = await client.SendAsync(_httpRequest);

                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToblockListResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"DeleteAddToblockList command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddToblockListResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"DeleteAddToblockList command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"DeleteAddToblockList command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
        }
        private AddToBlocklistResponse BuildAddToblockListResponse(bool isSuccess, string message, HttpStatusCode status)
        {
            return new AddToBlocklistResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,
                Message = message,
            };
        }

        public async Task<AddToExclusionlistResponse> AddToExclusionList(AddToExclusionlistDTO request, OrganizationToolModel conndtl)
        {
            AddToExclusionlistResponse response = new AddToExclusionlistResponse();

            object requestBodyObj = null;

            object filterobj = null;

            if (!string.IsNullOrEmpty(request.AccountId))
            {
                filterobj = new
                {
                    accountIds = new List<string>() { request.AccountId }
                };
            }
            if (!string.IsNullOrEmpty(request.SiteId))
            {
                filterobj = new
                {
                    siteIds = new List<string>() { request.SiteId }
                };
            }
            if (!string.IsNullOrEmpty(request.GroupId))
            {
                filterobj = new
                {
                    groupIds = new List<string>() { request.GroupId }
                };
            }


         

           
            object requestBody = null;

            if (request.ThreatIds == null || request.ThreatIds.Count == 0)
            {
                if (request.Type.ToLower() == "path")
                {
                    requestBody = new
                    {
                        data = new
                        {
                            description = request.Description,
                            value = request.Value,
                            pathExclusionType = request.PathExclusionType,
                            actions = request.Actions,
                            osType = request.OSType.ToLower(),
                            source = "user",
                            mode = request.Mode.ToLower(),
                            type = request.Type

                        } ,
                        filter = filterobj
                    };
                }

                else if (request.Type.ToLower() == "file_type")
                {
                    requestBody = new
                    {
                        data = new
                        {
                            description = request.Description,
                            value = request.Value,
                            // pathExclusionType = request.PathExclusionType,
                            actions = request.Actions,
                            osType = request.OSType.ToLower(),
                            source = "user",
                            // mode = request.Mode.ToLower(),
                            type = request.Type
                        },
                        filter = filterobj
                    };
                }
                else
                {
                    requestBody = new
                    {
                        data = new
                        {
                            description = request.Description,
                            value = request.Value,
                            // pathExclusionType = request.PathExclusionType,
                            // actions = request.Actions,
                            osType = request.OSType.ToLower(),
                            source = "user",
                            // mode = request.Mode.ToLower(),
                            type = request.Type
                        },
                        filter = filterobj
                    };

                }
            }
            else
            {
                requestBody = new
                {
                    data = new
                    {
                        // note = request.Note,
                        description = request.Description,
                        // value = request.Value,
                        targetScope = request.TargetScope.ToLower(),
                        //pathExclusionType = request.PathExclusionType,
                        //      actions = request.Actions,
                        //  externalTicketId = request.ExternelTicketId,
                        //    mode = request.Mode,
                        type = Constants.AddToExclusion_Hash,
                    },
                    filter = new
                    {
                        accountIds = request.AccountIds,
                        siteIds = request.SiteIds,
                        ids = request.ThreatIds// 
                    }
                };
               
            }
           
            var requestContent = JsonContent.Create(requestBody);
            //
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PostAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddtoExclusionListResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"AddToExclusionList command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddtoExclusionListResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"AddToExclusionList command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"AddToExclusionList command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
        }

       public async Task<AddToExclusionlistResponse> UpdateAddToExclusionList(UpdateAddToExclusionRequest request, OrganizationToolModel conndtl)
       {
            AddToExclusionlistResponse response = new AddToExclusionlistResponse();

            object requestBody = null;

            if (request.Type.ToLower() == "path")
            {
                requestBody = new
                {
                    data = new
                    {
                        id = request.Id,
                        description = request.Description,
                        value = request.Value,
                        pathExclusionType = request.PathExclusionType,
                        actions = request.Actions,
                        osType = request.OSType.ToLower(),
                        source = request.Source,
                        mode = request.Mode.ToLower(),
                        type = request.Type
                    }
                };
            }

            else if (request.Type.ToLower() == "file_type")
            {
                requestBody = new
                {
                    data = new
                    {
                        id = request.Id,
                        description = request.Description,
                        value = request.Value,
                        // pathExclusionType = request.PathExclusionType,
                        actions = request.Actions,
                        osType = request.OSType.ToLower(),
                        source = request.Source,
                        // mode = request.Mode.ToLower(),
                        type = request.Type
                    }
                };
               
            }
            else
            {
              

                requestBody = new
                {
                    data = new
                    {
                        id = request.Id,
                        description = request.Description,
                        value = request.Value,
                        osType = request.OSType,
                        source = request.Source,
                        type = request.Type.Trim()
                    }
                };
            }

         
            var requestContent = JsonContent.Create(requestBody);
            //
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var apiResponse = await client.PutAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddtoExclusionListResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"UpdateAddToExclusionList command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddtoExclusionListResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"UpdateAddToExclusionList command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                _logger.AddLogInforation($"UpdateAddToExclusionList command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                //          
                return response;
            }
        }

       public async Task<AddToExclusionlistResponse> DeleteAddToExclusionList(DeleteAddToExclusionRequest request, OrganizationToolModel conndtl)
        {
            AddToExclusionlistResponse response = new AddToExclusionlistResponse();

            var requestBody = new
            {
               
                data = new
                {
                    ids = request.Ids
                    //, type = request.Type
                }

            };
            var requestContent = JsonContent.Create(requestBody);
            //var content = new StringContent(requestContent, System.Text.Encoding.UTF8, "application/json");

            //
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiToken", conndtl.AuthKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                // var apiResponse = await client.PostAsync(conndtl.ToolActions[0].ApiUrl, requestContent);
                var _httpRequest = new HttpRequestMessage(HttpMethod.Delete, conndtl.ToolActions[0].ApiUrl);
                _httpRequest.Content = requestContent;

                var apiResponse = await client.SendAsync(_httpRequest);
                string apiResponseString = string.Empty;
                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResponseString = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddtoExclusionListResponse(true, "Success", HttpStatusCode.OK);
                    _logger.AddLogInforation($"DeleteAddToExclusionList command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = BuildAddtoExclusionListResponse(false, errorResponse, HttpStatusCode.InternalServerError);
                    _logger.AddLogInforation($"DeleteAddToExclusionList command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                }
                _logger.AddLogInforation($"DeleteAddToExclusionList command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
                return response;
            }
        }

        private AddToExclusionlistResponse BuildAddtoExclusionListResponse(bool isSuccess, string message, HttpStatusCode status)
        {
            return new AddToExclusionlistResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,
                Message = message,
            };
        }

    }
}
