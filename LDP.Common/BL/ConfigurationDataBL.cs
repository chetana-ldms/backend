using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using Org.BouncyCastle.Asn1.Ocsp;

namespace LDP.Common.BL
{
    public class ConfigurationDataBL : IConfigurationDataBL
    {
        IConfigurationDataRepository _repo;
        readonly IMapper _mapper;
        public ConfigurationDataBL(IConfigurationDataRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ConfigurationDataResponse GetConfigurationData(ConfigurationDataRequest request)
        {
            ConfigurationDataResponse response = new ConfigurationDataResponse();
            var res = _repo.GetConfigurationData(request);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "Data not found";
                return response;
            }
            response.IsSuccess = true;
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            response.Message = "Success";
            var _mapData = _mapper.Map<List<ConfigurationData>, List<ConfigurationDataModel>>(res.Result);
            response.Data = _mapData;
            return response;
        }

        public ConfigurationSingleDataResponse GetConfigurationData(string ConfigurationType, string ConfigurationName)
        {
            ConfigurationSingleDataResponse response = new ConfigurationSingleDataResponse();
            var res = _repo.GetConfigurationData(ConfigurationType,ConfigurationName);

            if (res.Result == null )
            {
                response.IsSuccess = false;
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "Data not found";
                return response;
            }
            response.IsSuccess = true;
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            response.Message = "Success";
            var _mapData = _mapper.Map<ConfigurationData, ConfigurationDataModel>(res.Result);
            response.Data = _mapData;
            return response;
        }

        public ConfigurationDataResponse GetConfigurationData(string ConfigurationType)
        {
            ConfigurationDataResponse response = new ConfigurationDataResponse();
            var res = _repo.GetConfigurationData(ConfigurationType);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "Data not found";
                return response;
            }
            response.IsSuccess = true;
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            response.Message = "Success";
            var _mapData = _mapper.Map<List<ConfigurationData>, List<ConfigurationDataModel>>(res.Result);
            response.Data = _mapData;
            return response;
        }
    }
}
