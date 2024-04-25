using System.Text;

namespace LDP.Common.Services.Notifications.SMS
{
    public class AirTelSMSSenderService : ISMSSenderService
    {
        public Task<GetSMSDataResponse> GetReplyMessage(GetReplyMessageRequest request, SMSConfiguration config)
        {
            throw new NotImplementedException();
        }

        public GetSMSDataResponse GetSMSMessages(GetSMSDetailsRequest request, SMSConfiguration config)
        {
            throw new NotImplementedException();
        }

        public Task<ResplySMSResponse> GetSMSReply(ResplySMSRequest request, SMSConfiguration config)
        {
            throw new NotImplementedException();
        }

        public Task<ResplySMSResponse> ResplySMS(ResplySMSRequest request, SMSConfiguration config)
        {
            throw new NotImplementedException();
        }

        public async Task<SendSMSResponse> SendSMS(SendSMSRequest request, SMSConfiguration config)
        {
            SendSMSResponse response = new SendSMSResponse ();

           
            string sender = "SENDER_NAME";
           
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.AuthToken}");

                var content = new StringContent($"{{\"from\":\"{sender}\",\"message\":\"{request.SMSMessage}\"," +
                    $"\"to\":\"{request.ToPhoneNumber}\"}}", Encoding.UTF8, "application/json");

                var apiResponse = await client.PostAsync(config.URL, content);
                
                if (apiResponse.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    response.Message = "Success";
                    response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = " Response status code :" + apiResponse.StatusCode;
                    response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                }
            }
           
            
            return response;

        }
    }
}
