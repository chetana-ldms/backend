namespace LDP.Common.Helpers.Interfaces
{
    public interface IEmailTemplateFactory
    {
        IEmailTemplate GetInstance(Constants.Email_types emailType);
    }
}
