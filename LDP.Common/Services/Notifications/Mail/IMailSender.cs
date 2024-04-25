namespace LDP.Common.Services.Notifications.Mail
{
    public interface IMailSender
    {
        Task<EmailResponse> SendEmail(EmailRequest request);
    }
}
