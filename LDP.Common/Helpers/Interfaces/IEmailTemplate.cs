using LDP.Common.Responses;

namespace LDP.Common.Helpers.Interfaces
{
    public interface IEmailTemplate
    {
        string BuildEmailTemplate(Dictionary<string, string> data, ConfigurationDataResponse configData);
    }
}
