using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.DAL.Interfaces
{

    public interface IConfigurationDataRepository
    {
        Task<List<ConfigurationData>> GetConfigurationData(ConfigurationDataRequest request);

        Task<List<ConfigurationData>> GetConfigurationData(string ConfigurationType);


        Task<ConfigurationData> GetConfigurationData(string ConfigurationType, string ConfigurationName);
    }
}
