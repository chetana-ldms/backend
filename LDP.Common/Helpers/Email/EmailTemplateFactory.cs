using LDP.Common.Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LDP.Common.Helpers.Email
{
    public class EmailTemplateFactory : IEmailTemplateFactory
    {

        private readonly IServiceProvider _serviceProvider;

        public EmailTemplateFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEmailTemplate GetInstance(Constants.Email_types emailType)
        {
            switch (emailType)
            {
                case Constants.Email_types.Password_Reset:
                    return _serviceProvider.GetRequiredService<PasswordResetTemplate>();
                case Constants.Email_types.New_User_Account_Creation:
                    return _serviceProvider.GetRequiredService<NewAccountCreationTemplate>();
                case Constants.Email_types.New_TaskCreation:
                    return _serviceProvider.GetRequiredService<NewTaskCreationTemplate>();
                default:
                    return null;
            }
        }
    }
}
