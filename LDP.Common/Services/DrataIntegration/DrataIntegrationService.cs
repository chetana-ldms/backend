using System.Net;
using System.Text.Json;

namespace LDP.Common.Services.DrataIntegration
{
    public class DrataIntegrationService : IDrataIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DrataIntegrationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetControlsResponse> GetControls(GetControlsRequest request)
        {
            GetControlsResponse response = null;
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 5c1170d0-e1f7-4485-a81c-86b1900bb97a");
            // list of controls
            var _apirequest = await client.GetAsync("https://public-api.drata.com/public/controls?frameworkTags=SOC_2");
            // list of workspaces
            //var _apirequest = await client.GetAsync("https://public-api.drata.com/public/workspaces");
            // list of frameworks
            //var _apirequest = await client.GetAsync("https://public-api.drata.com/public/workspaces/1/frameworks");

            //var _apiresponse = await _apirequest.Content.ReadAsStringAsync();
            string apiResponseString = string.Empty;
            if (_apirequest.IsSuccessStatusCode)
            {
                apiResponseString = await _apirequest.Content.ReadAsStringAsync();

                var apiResponse = JsonSerializer.Deserialize<Drata_GetControlsResponse>(apiResponseString,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                if (apiResponse.data.Count > 0)
                {
                    response = BuildGetControlResponse(true, "Success", HttpStatusCode.OK, apiResponse);
                   // _logger.AddLogInforation($"GetThreatNotes API Returned {apiThreatNotesResponse.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                else
                {
                    response = BuildGetControlResponse(false, "No records found", HttpStatusCode.NotFound, null);
                    //_logger.AddLogInforation($"GetThreatNotes API Returned {apiThreatNotesResponse.data.Count} records.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

                }
                //response = BuildAddtoExclusionListResponse(true, "Success", HttpStatusCode.OK);
                //_logger.AddLogInforation($"AddToExclusionList command success.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);
            }
            else
            {
                var errorResponse = await _apirequest.Content.ReadAsStringAsync();
                response = BuildGetControlResponse(false, errorResponse, HttpStatusCode.InternalServerError,null );
                //_logger.AddLogInforation($"AddToExclusionList command error {errorResponse} .. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            }
           // _logger.AddLogInforation($"AddToExclusionList command action Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            return response;
        }

        private GetControlsResponse BuildGetControlResponse(bool isSuccess, string message, HttpStatusCode status, Drata_GetControlsResponse ControlList)
        {
            return new GetControlsResponse()
            {
                IsSuccess = isSuccess,
                HttpStatusCode = status,
                DrataControlsList = ControlList ,
                Message = message,
            };
        }


    }
}
