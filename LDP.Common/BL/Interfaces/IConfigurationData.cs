using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IConfigurationDataBL
    {
        ConfigurationDataResponse GetConfigurationData(ConfigurationDataRequest request);

        ConfigurationSingleDataResponse GetConfigurationData(string ConfigurationType , string ConfigurationName);

        ConfigurationDataResponse GetConfigurationData(string ConfigurationType);

    }
}
