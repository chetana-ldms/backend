using LDP.Common.BL.Interfaces;
using LDP.Common.Helpers.Interfaces;
using LDP.Common.Services.Notifications.Mail;
using LDP_APIs.BL.Interfaces;

namespace LDP.Common.Helpers.Email
{
    public class EmailHelper : IEmailHelper
    {
        ILDPSecurityBL _securityBl;
        IMailSender _mailSender;
        IConfigurationDataBL _config;
        IEmailTemplateFactory _emailTemplateFactory;
        public EmailHelper(ILDPSecurityBL securityBl, IMailSender mailSender, IConfigurationDataBL config, IEmailTemplateFactory emailTemplateFactory)
        {
            _securityBl = securityBl;
            _mailSender = mailSender;
            _config = config;
            _emailTemplateFactory = emailTemplateFactory;
        }

        public EmailPrepResponse PrepareAndSendEmail(EmailPrepRequest request)
        {
            EmailPrepResponse response = new EmailPrepResponse();

            EmailRequest mailRequest = new EmailRequest();
            // Get the To email id from user table 
            var _user =  _securityBl.GetUserbyID(request.UseId);

            if (!_user.IsSuccess)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "User not found ";
                return response;
            }
            if (string.IsNullOrEmpty(_user.Userdata.EmailId))
            {
                response.IsSuccess = false;
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "User email id not found ";
                return response;
            }

            mailRequest.toMail = _user.Userdata.EmailId;

            // Get the From email , API key from configuration 

            var _configdata =  _config.GetConfigurationData("Email");

            if (!_configdata.IsSuccess)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "From email id , API key configuration not found ";
                return response;
            }

            mailRequest.fromMail = _configdata.Data.Where(c => c.DataName == "FromEmailId").FirstOrDefault().DataValue;

            var _apikey = _configdata.Data.Where (c => c.DataName == "AuthKey").FirstOrDefault().DataValue ;

            // Create a new dictionary and add the item using object initializer syntax
            
            
            request.Data.Add("Api_Key", _apikey);
            request.Data.Add("Name", _user.Userdata.Name);
            mailRequest.Data = request.Data;
            // Template preparation 
            var templateObject =  _emailTemplateFactory.GetInstance(request.EmailType);
            //
            var emailTemplate = templateObject.BuildEmailTemplate(request.Data, _configdata);
            //
            mailRequest.htmlContent = emailTemplate;
            // send mail
            mailRequest.subject = request.EmailSubject;

            var _mailerResponse = _mailSender.SendEmail(mailRequest);

            return response;
        }
    }
}
