using AutoMapper;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.Models;
using LDP_APIs.Interfaces;
using LDP_APIs.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace LDP_APIs.Services
{
    public class FreshDesk_IncidentManagementService : IIncidentManagementService
    {
       
        private readonly IMapper _mapper;
              
        public FreshDesk_IncidentManagementService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<CreateIncidentInternalResponse> CreateIncident(CreateIncidentDTO dto)
        {
            CreateIncidentInternalResponse returnResponse = new CreateIncidentInternalResponse();
            FreshDeskIncidentDtls incidentData = new FreshDeskIncidentDtls();
            incidentData.status = 2;
            incidentData.priority = 1;
            incidentData.email = "LDPDev@lanceSoft.com";
            incidentData.subject = dto.IncidentSubject;
            incidentData.description = dto.IncidentDescription;
            //incidentdata.Source = "LanceSoftDefencePlatform";
            //incidentData.type = "Incident";
            

       
            string json = null;
            json = (JsonSerializer.Serialize(incidentData)).ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dto.APIUrl);
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


            try
            {
                WebResponse response = null;
                try
                {
                    response = await request.GetResponseAsync();
                }
               catch (WebException ex)
                {

                }
                if (response != null) 
                {
                    // Get the stream containing content returned by the server.
                    //Send the request to the server by calling GetResponse. 
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access. 
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content. 
                    string Response = await reader.ReadToEndAsync();
                    var toolIncidentData = JsonSerializer.Deserialize<FreshDeskIncidentResponse>(Response);
                    returnResponse.IncidentID = toolIncidentData.id;
                    returnResponse.IncidentJsonText = Response;
                    returnResponse.IsSuccess = true;
                }
                else
                {
                    returnResponse.IsSuccess = false;
                    returnResponse.Message = "Some thing went wrong during api call ";
                }
                
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
                returnResponse.IsSuccess = false;
                returnResponse.Message = "Some thing went wrong during api call ";
            }

            
            return returnResponse;
        }
       
        //public async Task<List<Incident>> GetIncidents1()
        //{
        //    string fdDomain = "lancesoft-support"; // your freshdesk domain
        //    string apiKey = "1h04pLGZOCNmt7tksqx";
        //    string apiPath = "/api/v2/tickets"; // API path
        //   // string apiPath = "/api/v2/tickets/1"; // API path
        //    string responseBody = String.Empty;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + fdDomain + ".freshdesk.com" + apiPath);
        //    request.ContentType = "application/json";
        //    request.Method = "GET";
        //    string authInfo = apiKey + ":X"; // It could be your username:password also.
        //    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        //    request.Headers["Authorization"] = "Basic " + authInfo;
        //    List<FreeDeskIncidentDtls> incidentDtlsList = new List<FreeDeskIncidentDtls>();
        //    GetIncidentsResponse returnresponse = new GetIncidentsResponse();
        //    try
        //    {
        //        // Console.WriteLine("Submitting Request");
        //        using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        //        {
        //            Stream dataStream = response.GetResponseStream();
        //            StreamReader reader = new StreamReader(dataStream);
        //            responseBody = reader.ReadToEnd();
        //            reader.Close();
        //            dataStream.Close();
        //            //return status code
        //            // Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
        //        }
        //        //Console.Out.WriteLine(responseBody);
        //        incidentDtlsList = JsonSerializer.Deserialize<List<FreeDeskIncidentDtls>>(responseBody);
        //        returnresponse.IsSuccess = true;
        //        returnresponse.Message = "success";
        //        returnresponse.IncidentsList = incidentDtlsList;
        //    }
        //    catch (WebException ex)
        //    {
        //        //Console.WriteLine("API Error: Your request is not successful. If you are not able to debug this error properly, mail us at support@freshdesk.com with the follwing X-Request-Id");
        //        //Console.WriteLine("X-Request-Id: {0}", ex.Response.Headers["X-Request-Id"]);
        //        //Console.WriteLine("Error Status Code : {1} {0}", ((HttpWebResponse)ex.Response).StatusCode, (int)((HttpWebResponse)ex.Response).StatusCode);
        //        using (var stream = ex.Response.GetResponseStream())
        //        using (var reader = new StreamReader(stream))
        //        {
        //            //Console.Write("Error Response: ");
        //            //Console.WriteLine(reader.ReadToEnd());
        //        }
        //        returnresponse.IsSuccess = false;
        //        returnresponse.Message = "Failed";
        //        returnresponse.IncidentsList = null;
        //    }

        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine("ERROR");
        //        //Console.WriteLine(ex.Message);
        //        returnresponse.IsSuccess = false;
        //        returnresponse.Message = "Failed";
        //        returnresponse.IncidentsList = null;
        //    }
        //    return returnresponse;
        //}
      

       

        public async Task<GetIncidentsResponse> GetIncidents(GetIncidentsRequest request , OrganizationToolModel connectiondtl)
        {
            string Url = connectiondtl.ApiUrl + "?status=open&created_since=7&order_by=created_at&order_type=desc&page=1&per_page=50";

            string responseBody = String.Empty;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(connectiondtl.ApiUrl);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            string authInfo = connectiondtl.AuthKey + ":X"; // It could be your username:password also.
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            httpRequest.Headers["Authorization"] = "Basic " + authInfo;
            List<FreshDeskIncidentDtls> incidentDtlsList = new List<FreshDeskIncidentDtls>();
            GetIncidentsResponse returnresponse = new GetIncidentsResponse();
            try
            {
                // Console.WriteLine("Submitting Request");
                using (HttpWebResponse response = (HttpWebResponse)await httpRequest.GetResponseAsync())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    var hdr = ((HttpWebResponse)response).Headers;
                }
                incidentDtlsList = JsonSerializer.Deserialize<List<FreshDeskIncidentDtls>>(responseBody);
                var _mappedModelData = _mapper.Map<List<FreshDeskIncidentDtls>, List<GetIncidentModel>>(incidentDtlsList);
                returnresponse.IsSuccess = true;
                returnresponse.Message = "success";
                returnresponse.IncidentList = _mappedModelData;
                returnresponse.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    returnresponse.errors = new List<string>() { ex.Message };
                }
                else
                { 
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
           
                        returnresponse.errors = new List<string>() { reader.ReadToEnd() };
                     }
                }
                returnresponse.IsSuccess = false;
                returnresponse.Message = "Some thing went wrong druing api call ";
                returnresponse.IncidentList = null;
                returnresponse.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                returnresponse.IsSuccess = false;
                returnresponse.Message = "Some thing went wrong druing api call ";
                returnresponse.IncidentList = null;
                returnresponse.HttpStatusCode = HttpStatusCode.InternalServerError;
                returnresponse.errors = new List<string>()
                {
                    ex.Message
                };
            }
            return returnresponse;
        }

        public async Task<GetIncidentsResponse> GetIncidentsByClientToolPKIds(List<string> clientToolPKIds, OrganizationToolModel connectiondtl)
        {
            string endpoint = "/show_many";
            string url = connectiondtl.ApiUrl + endpoint + "?include=requester";
            foreach (string pk in clientToolPKIds)
            {
                url += $"&ids[]={pk}";
            }

           // string Url = connectiondtl.ApiUrl + "?";

            string responseBody = String.Empty;
            url = "https://arun-likkesh.freshdesk.com/api/v2/tickets/id/7";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            string authInfo = connectiondtl.AuthKey + ":X"; 
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            httpRequest.Headers["Authorization"] = "Basic " + authInfo;
            List<FreshDeskIncidentDtls> incidentDtlsList = new List<FreshDeskIncidentDtls>();
            GetIncidentsResponse returnresponse = new GetIncidentsResponse();
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await httpRequest.GetResponseAsync())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    var hdr = ((HttpWebResponse)response).Headers;
                }
                incidentDtlsList = JsonSerializer.Deserialize<List<FreshDeskIncidentDtls>>(responseBody);
                var _mappedModelData = _mapper.Map<List<FreshDeskIncidentDtls>, List<GetIncidentModel>>(incidentDtlsList);
                returnresponse.IsSuccess = true;
                returnresponse.Message = "success";
                returnresponse.IncidentList = _mappedModelData;
                returnresponse.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    returnresponse.errors = new List<string>() { ex.Message };
                }
                else
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {

                        returnresponse.errors = new List<string>() { reader.ReadToEnd() };
                    }
                }
                returnresponse.IsSuccess = false;
                returnresponse.Message = "Some thing went wrong druing api call ";
                returnresponse.IncidentList = null;
                returnresponse.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                returnresponse.IsSuccess = false;
                returnresponse.Message = "Some thing went wrong druing api call ";
                returnresponse.IncidentList = null;
                returnresponse.HttpStatusCode = HttpStatusCode.InternalServerError;
                returnresponse.errors = new List<string>()
                {
                    ex.Message
                };
            }
            return returnresponse;
        }
    }
}

