using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using EmailAddress = SendGrid.Helpers.Mail.EmailAddress;

namespace LDP.Common.Services.Notifications.Mail
{
    public class TwillioEmailSender: IMailSender
    {
        public async Task<EmailResponse> SendEmail(EmailRequest sendrequest)
        {
            EmailResponse EmailRes = new EmailResponse();

            if (string.IsNullOrEmpty(sendrequest.fromMail))
            {
                EmailRes.IsSuccess = false;
                EmailRes.Message = "Email  From email not found ,  check the request ";
                EmailRes.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                return EmailRes;
            }
            if (string.IsNullOrEmpty(sendrequest.fromMail))
            {
                EmailRes.IsSuccess = false;
                EmailRes.Message = "Email  To email not found ,  check the request ";
                EmailRes.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                return EmailRes;
            }
            try
            {


                EmailRes.IsSuccess = true;
                string apiKey;

                sendrequest.Data.TryGetValue("Api_Key", out apiKey);

                var client = new SendGridClient(apiKey);


               
                var message1 = new SendGridMessage();
                message1.From = new EmailAddress(sendrequest.fromMail);
                message1.Subject = sendrequest.subject;
                message1.PlainTextContent = sendrequest.plainTextContent;
                message1.HtmlContent = sendrequest.htmlContent;
                message1.AddTo(new EmailAddress(sendrequest.toMail));


                if (!string.IsNullOrEmpty(sendrequest.CCs))
                {
                    //Adding Multiple CC email Id 
                    string[] CC = sendrequest.CCs.Split(',');
                    foreach (string AddCc in CC)
                    {
                        message1.AddCc(new EmailAddress(sendrequest.CCs));
                    }
                }

                if (!string.IsNullOrEmpty(sendrequest.BCCs))
                {
                    //Adding Multiple BCC email Id
                    string[] BCC = sendrequest.BCCs.Split(',');
                    foreach (string AddBcc in BCC)
                    {
                        message1.AddBcc(new EmailAddress(sendrequest.BCCs));
                    }
                }

                var response = await client.SendEmailAsync(message1);



                if (response.IsSuccessStatusCode)
                {

                    EmailRes.IsSuccess = true;
                    EmailRes.Message = "Email Sent Successfully!";
                    EmailRes.HttpStatusCode = System.Net.HttpStatusCode.OK;
                    return EmailRes;
                }
                else
                {
                    EmailRes.IsSuccess = false;
                    EmailRes.Message = "Email  sent Faild";
                    EmailRes.HttpStatusCode = System.Net.HttpStatusCode.ExpectationFailed;
                    return EmailRes;
                }
            }
            catch (Exception ex)
            {
                EmailRes.IsSuccess = false;
                EmailRes.Message = "Some thing went wrong";
                EmailRes.errors = new List<string>() { ex.ToString() };
                return EmailRes;
            }
        }

    }
}
