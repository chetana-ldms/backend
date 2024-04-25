using LDP_APIs.Interfaces;
using LDP_APIs.Models;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace LDP_APIs.Services
{
    public class IncidentManagementService : IIncidentManagementService
    {
        private readonly IHttpClientFactory? _httpClientFactory;
        public IncidentManagementService(IHttpClientFactory? httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> CreateIncident(CreateIncidentDTO dto)
        {

           // string fdDomain = "lancesoft-helpdesk"; // your freshdesk domain
           // string apiKey = "d06qpcQDWc4Z4pY5lUaj";
           // string apiPath = "/api/v2/tickets"; // API path

            FreeDeskIncidentDtls incidentdata = new FreeDeskIncidentDtls();
            incidentdata.status = 2;
            incidentdata.priority = 1;
            incidentdata.email = "Dev@lanceSoft.com";
            incidentdata.subject = dto.AlertData.Name;
            incidentdata.description = dto.AlertData.Name;
            incidentdata.type = "Incident";

           //string   url = " https://lancesoft-support.freshdesk.com/api/v2/tickets";

            string json = null;
            json = (JsonSerializer.Serialize(incidentdata)).ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dto.APIUrl);
            //HttpWebRequest class is used to Make a request to a Uniform Resource Identifier (URI).  
            request.ContentType = "application/json";
            // Set the ContentType property of the WebRequest. 
            request.Method = "POST";
            byte[] vs = Encoding.UTF8.GetBytes(json);
            byte[] byteArray = vs;
            // Set the ContentLength property of the WebRequest. 
            request.ContentLength = byteArray.Length;
            string authInfo = dto.AuthKey + ":X"; // It could be your username:password also.
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;

            //Get the stream that holds request data by calling the GetRequestStream method. 
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream. 
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object. 
            dataStream.Close();


            //    Console.WriteLine("Submitting Request");
            try
            {
                WebResponse response = request.GetResponse();
                // Get the stream containing content returned by the server.
                //Send the request to the server by calling GetResponse. 
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access. 
                StreamReader reader = new StreamReader(dataStream);
                // Read the content. 
                string Response = reader.ReadToEnd();
                //return status code
            //    Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
                //return location header
                // Console.WriteLine("Location: {0}", response.Headers["Location"]);
                //return the response 
                // Console.Out.WriteLine(Response);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); }
            return "success";
        }
       
        public async Task<GetIncidentsResponse> GetIncidents()
        {
            string fdDomain = "lancesoft-support"; // your freshdesk domain
            string apiKey = "1h04pLGZOCNmt7tksqx";
            string apiPath = "/api/v2/tickets"; // API path
           // string apiPath = "/api/v2/tickets/1"; // API path
            string responseBody = String.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + fdDomain + ".freshdesk.com" + apiPath);
            request.ContentType = "application/json";
            request.Method = "GET";
            string authInfo = apiKey + ":X"; // It could be your username:password also.
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
            List<FreeDeskIncidentDtls> incidentDtlsList = new List<FreeDeskIncidentDtls>();
            GetIncidentsResponse returnresponse = new GetIncidentsResponse();
            try
            {
                // Console.WriteLine("Submitting Request");
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    //return status code
                    // Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
                }
                //Console.Out.WriteLine(responseBody);
                incidentDtlsList = JsonSerializer.Deserialize<List<FreeDeskIncidentDtls>>(responseBody);
                returnresponse.IsSuccess = true;
                returnresponse.Message = "success";
                returnresponse.IncidentsList = incidentDtlsList;
            }
            catch (WebException ex)
            {
                //Console.WriteLine("API Error: Your request is not successful. If you are not able to debug this error properly, mail us at support@freshdesk.com with the follwing X-Request-Id");
                //Console.WriteLine("X-Request-Id: {0}", ex.Response.Headers["X-Request-Id"]);
                //Console.WriteLine("Error Status Code : {1} {0}", ((HttpWebResponse)ex.Response).StatusCode, (int)((HttpWebResponse)ex.Response).StatusCode);
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    //Console.Write("Error Response: ");
                    //Console.WriteLine(reader.ReadToEnd());
                }
                returnresponse.IsSuccess = false;
                returnresponse.Message = "Failed";
                returnresponse.IncidentsList = null;
            }

            catch (Exception ex)
            {
                //Console.WriteLine("ERROR");
                //Console.WriteLine(ex.Message);
                returnresponse.IsSuccess = false;
                returnresponse.Message = "Failed";
                returnresponse.IncidentsList = null;
            }
            return returnresponse;
        }
    }
}

