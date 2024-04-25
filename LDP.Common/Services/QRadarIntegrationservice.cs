using LDP_APIs.Interfaces;
using LDP_APIs.Models;
using System.Net;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LDP_APIs.Services
{
    public class QRadarIntegrationservice: IQRadarIntegrationservice
    {
        private readonly IHttpClientFactory? _httpClientFactory;
        private static readonly HttpClient _httpClient = new HttpClient(); 

        public QRadarIntegrationservice(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => { return true; };
              
        }

        public async Task<getOffenseResponse> Getoffenses(GetOffenseDTO dto)
        {
            getOffenseResponse methoRresponse = null ;
            string strRange = string.Empty;
            if (dto.alert_MaxPKID==0)
                strRange = string.Format("items=0-{0}",(dto.GetDataBatchSize));
            
            // First time fetching last batch number of records
            // From second time onwards fetch latest records after last alert record logged 
            if (dto.alert_MaxPKID == 0)
                dto.APIUrl = dto.APIUrl + "? status = 'OPEN' & sort=-id";
            else
                dto.APIUrl  = dto.APIUrl+"?filter=id>" + dto.alert_MaxPKID + " and id <="+ (dto.alert_MaxPKID + dto.GetDataBatchSize) + " and status = 'OPEN' & sort=-id";
            
            List<QRadaroffense> qRadarResult = null;
            using (var httpClientHandler = new HttpClientHandler())
            { 
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                };
                HttpClient client;
                using (client = new HttpClient(httpClientHandler))
                {
                    client.DefaultRequestHeaders.Add("Version", dto.APIVersion);
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.Add("SEC", dto.AuthKey);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    if (dto.alert_MaxPKID == 0)
                        client.DefaultRequestHeaders.Add("Range", strRange);
                    using (HttpResponseMessage response = await client.GetAsync(dto.APIUrl))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            qRadarResult = JsonSerializer.Deserialize<List<QRadaroffense>>(responseBody);

                            if (qRadarResult.Count > 0)
                            {
                                methoRresponse = BuildResponse(true, "Success", dto, qRadarResult);
                            }
                            else
                            {
                                methoRresponse = BuildResponse(false, "No records found", dto, null);
                            }
                        }
                        else
                        {
                            methoRresponse = BuildResponse(false, "Some thing went wrong API process", dto, qRadarResult);
                        }
                    }
                }
                return methoRresponse;
       
            }
            
        }

        private  getOffenseResponse BuildResponse(bool isSuccess , string message, GetOffenseDTO dto, List<QRadaroffense> qRadarResult)
        {
            return new getOffenseResponse()
            {
                IsSuccess = isSuccess,
                Message = message,
                offensesList = qRadarResult,
                OrgId = dto.clientRequest.OrgID,
                ToolId = dto.ToolID
            };
        }

       
    }

}
