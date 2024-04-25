using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class ConfigurationDataResponse:baseResponse
    {
        public List<ConfigurationDataModel> Data { get; set; }
    }

    public class ConfigurationSingleDataResponse : baseResponse
    {
        public ConfigurationDataModel Data { get; set; }
    }

}
