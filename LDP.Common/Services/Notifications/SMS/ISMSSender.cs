using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.Services.Notifications.SMS
{
    public interface ISMSSenderService
    {
        Task<SendSMSResponse> SendSMS(SendSMSRequest request, SMSConfiguration config);
        Task<ResplySMSResponse> ResplySMS(ResplySMSRequest request,SMSConfiguration config);

        GetSMSDataResponse GetSMSMessages(GetSMSDetailsRequest request, SMSConfiguration config);

        Task<GetSMSDataResponse> GetReplyMessage(GetReplyMessageRequest request, SMSConfiguration config);

    }
    public interface ISMSSenderBL
    {
        SendSMSResponse SendSMS(SendSMSRequest request);
        ResplySMSResponse ResplySMS(ResplySMSRequest request);

        GetSMSDataResponse GetSMSMessages(GetSMSDetailsRequest request);

        GetSMSDataResponse GetReplyMessage(GetReplyMessageRequest request);
        

    }
}
