using LDP.Common.BL.Interfaces;
using LDP.Common.Responses;

namespace LDP.Common.Services.Notifications.SMS
{
    public class SMSSenderBL : ISMSSenderBL
    {
        ISMSSenderService _smsSender;
        IConfigurationDataBL _configdata;
        public SMSSenderBL(ISMSSenderService smsSender, IConfigurationDataBL configdata)
        {
            _smsSender = smsSender;
            _configdata = configdata;
        }

        public GetSMSDataResponse GetReplyMessage(GetReplyMessageRequest request)
        {
           // GetSMSDataResponse response = new GetSMSDataResponse();
            SMSConfiguration config = GetConfigData();
            var _replyMsgdata = _smsSender.GetReplyMessage(request, config).Result;
            return _replyMsgdata;
        }

        public GetSMSDataResponse GetSMSMessages(GetSMSDetailsRequest request)
        {
            GetSMSDataResponse response = new GetSMSDataResponse();
            SMSConfiguration config = GetConfigData();
            var _smsData = _smsSender.GetSMSMessages(request, config);
            return response;
        }

        public ResplySMSResponse ResplySMS(ResplySMSRequest request)
        {
            SMSConfiguration config = GetConfigData();
            return _smsSender.ResplySMS(request, config).Result;
        }

        public SendSMSResponse SendSMS(SendSMSRequest request)
        {
            SMSConfiguration config = GetConfigData();
            return _smsSender.SendSMS(request, config).Result;
        }

        private SMSConfiguration GetConfigData()
        {
            var configdata = _configdata.GetConfigurationData
                (new Requests.ConfigurationDataRequest()
                {
                    DataType = Constants.Configdata_Type
                });
            SMSConfiguration config = new SMSConfiguration()
            {
                AuthToken = GetConfigItemValue(configdata, Constants.Configdata_AuthKey),
                SID = GetConfigItemValue(configdata, Constants.Configdata_SID),
                URL = GetConfigItemValue(configdata, Constants.Configdata_SendUrl),
                TwilioPhoneNumber = GetConfigItemValue(configdata, Constants.Configdata_FromPhneNumber)
            };
            return config;
        }

        private  string? GetConfigItemValue(ConfigurationDataResponse configdata ,string configname)
        {
            return configdata.Data.Where(c => c.DataName == configname).FirstOrDefault().DataValue;
        }
    }
}
