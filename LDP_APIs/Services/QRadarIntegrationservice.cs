using LDP_APIs.Interfaces;
using LDP_APIs.Models;
using System.Net;
using System.Net.Http;
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
            string strRange = "items=0-49";
            if (ValidateRangeData(dto))
            {
                strRange = string.Format("items={0}-{1}",dto.clientRequest.paging.RangeStart.ToString(),dto.clientRequest.paging.RangeEnd.ToString());
            }
           // string url = "https://10.100.0.102/api/siem/offenses?sort=%2Bid and filter=id%20%3E%2050";
            string url = "https://10.100.0.102/api/siem/offenses?sort=%2Bid";
            IEnumerable<QRadaroffense> qRadarResult = null;
            //string SECToken = "4343411c-68bc-4116-bd4b-f8fecc3c8225";
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            HttpClient client;
            using ( client = new HttpClient(httpClientHandler))
            {
                client.DefaultRequestHeaders.Add("Version", "16.0");
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("SEC", dto.AuthKey);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Range", strRange);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)                    
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        qRadarResult = JsonSerializer.Deserialize<IEnumerable<QRadaroffense>>(responseBody);
                    }
                   
                }
            }
            //
         

            return new getOffenseResponse()
            {
                IsSuccess = true,
                Message = "sucess",
                offensesList = qRadarResult, OrgId = dto.clientRequest.OrgID,
                 ToolId= dto.ToolID
            };
        }
        bool ValidateRangeData(GetOffenseDTO dto )
        {
            bool res = false;
            int startRange = dto.clientRequest.paging.RangeStart;
            int endRange = dto.clientRequest.paging.RangeEnd;
            if (endRange <= startRange)
                return res;
            return true;

        }
    }

}
