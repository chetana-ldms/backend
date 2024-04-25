using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace LDP.Common.Services.Notifications.SMS
{
    public class TwillioSMSSenderService : ISMSSenderService
    {
        public async Task<GetSMSDataResponse> GetReplyMessage(GetReplyMessageRequest request, SMSConfiguration config)
        {
            GetSMSDataResponse response = new GetSMSDataResponse();
            // Initialize the Twilio client
            TwilioClient.Init(config.SID, config.AuthToken);

            // Get the Sent SMS details based on Message Id
            var _sentMessage = await MessageResource.FetchAsync(request.MessageId);

            if (_sentMessage == null ) 
            {
                response.IsSuccess = false;
                response.Message = "Data not found";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var _replyMessages = await MessageResource.ReadAsync(
                from: new PhoneNumber(_sentMessage.To),
                to: new PhoneNumber(_sentMessage.From.ToString())
              );
            if (_replyMessages.Count() ==0 )
            {
                response.IsSuccess = false;
                response.Message = "Reply not received.." + _sentMessage.DateSent + " utc: " + _sentMessage.DateSent;
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var _replyMsg = _replyMessages.Where(msg => msg.Direction == MessageResource.DirectionEnum.Inbound
                  && msg.DateSent > _sentMessage.DateSent)
                .OrderByDescending(msg => msg.DateCreated);
            if (_replyMsg.Count()== 0 ) 
            {
                response.IsSuccess = false;
                response.Message = "Reply not received..";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            response.IsSuccess = true;
            response.Message = "Reply received..";
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            response.Content = _replyMsg.FirstOrDefault().Body;
            return response;

        }

        public GetSMSDataResponse GetSMSMessages(GetSMSDetailsRequest request, SMSConfiguration config)
        {
            GetSMSDataResponse response = new GetSMSDataResponse();
            // Initialize the Twilio client
            TwilioClient.Init(config.SID, config.AuthToken);

            // Define the filters
            string fromNumber = "+17145482987"; //request.FromPhoneNumber; // The phone number messages were sent from
            string toNumber = config.TwilioPhoneNumber; // The phone number messages were sent to
            string direction = "inbound"; // The direction of the message (inbound/outbound)
           // DateTime startDate = new DateTime(2022, 1, 1); // The start date to search for messages
           // DateTime endDate = new DateTime(2022, 1, 31); // The end date to search for messages

            // Get the messages that match the filters
            var messages = MessageResource.Read(
                from: new PhoneNumber(fromNumber),
                to: new PhoneNumber(toNumber)
                // , direction: direction
             //   dateSentAfter: startDate,
              //  dateSentBefore: endDate
            );
          var _replyMsg =  messages.Where(msg => msg.Direction == MessageResource.DirectionEnum.Inbound 
                           ).OrderByDescending(msg => msg.DateCreated).FirstOrDefault();
            response.IsSuccess = true;
            response.Message = _replyMsg.Body;
            return response;

        }

        public async  Task<ResplySMSResponse> ResplySMS(ResplySMSRequest request, SMSConfiguration config)
        {
            ResplySMSResponse getSMSReplyResponse = new ResplySMSResponse();

            TwilioClient.Init(config.SID, config.AuthToken);

            var message = MessageResource.Create(
                to: new PhoneNumber(config.TwilioPhoneNumber),
                from: new PhoneNumber(request.ToPhoneNumber),
                body: request.SMSMessage
            ) ;


            return getSMSReplyResponse;


        }

        public async Task<SendSMSResponse> SendSMS(SendSMSRequest request, SMSConfiguration config)
        {
            SendSMSResponse response = new SendSMSResponse ();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.SID}:{config.AuthToken}")));

            var values = new Dictionary<string, string>
            {
                { "From", config.TwilioPhoneNumber },
                { "To", request.ToPhoneNumber },
                { "Body", request.SMSMessage }
            };

            var content = new FormUrlEncodedContent(values);
       
            using (var httpClient = new HttpClient())
            {
                using (var apirequest = new HttpRequestMessage(new HttpMethod("POST"), config.URL + config.SID + "/Messages.json"))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes(config.SID+":"+config.AuthToken));
                    apirequest.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");
                    apirequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                             "Basic", base64authorization);

                    apirequest.Content = content;
                    apirequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var apiresponse = await httpClient.SendAsync(apirequest);
                    if (apiresponse.IsSuccessStatusCode)
                    {
                        var apiresponseString = await apiresponse.Content.ReadAsStringAsync();
                        
                        var  apiResponseObject = JsonSerializer.Deserialize<SMSMessageSID>(apiresponseString);
                        response.IsSuccess = true;
                        response.Message = apiResponseObject.sid;
                        response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        var apiresponseString = await apiresponse.Content.ReadAsStringAsync();
                        var apiResponseObject = JsonSerializer.Deserialize<TwillioSendAPIResponse>(apiresponseString);
                        response.IsSuccess = false;
                        response.Message = "Send SMS  - Some thing went wrong ";
                        response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                        response.errors = new List<string>() { apiResponseObject.message };
                    }
                }
            }

            return response;

        }
    }

    public class SMSMessageSID
    {
        public string sid { get; set; }
    }
}
