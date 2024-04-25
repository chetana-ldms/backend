
using LDP.Common.Helpers.Email;

namespace LDP.Common.Helpers.Interfaces
{
    public interface IEmailHelper
    {
        EmailPrepResponse PrepareAndSendEmail(EmailPrepRequest request);
    }
}
