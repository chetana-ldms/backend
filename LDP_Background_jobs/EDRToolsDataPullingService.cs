using LDP.Common;
using LDP.Common.BL.Interfaces;
using LDP.Common.Services;
using LDP.Common.Services.SentinalOneIntegration;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.Interfaces;
using LDP_APIs.Models;

namespace LDP_Background_jobs
{
    public class EDRToolsDataPullingService
    {
        //private readonly ILogger<EDRToolsDataPullingService> _logger;
        //IQRadarIntegrationBL _QRadar;
        IApplicationLogBL _logger;
        ISentinalOneIntegrationBL _bl;
        ILDPlattformBL _platformBL;
        IConfigurationDataBL _configurationBL;

        public EDRToolsDataPullingService(IApplicationLogBL logger
           , ISentinalOneIntegrationBL bl
            , ILDPlattformBL platformBL
            , IConfigurationDataBL configurationBL
            )
        {
            _logger = logger;
            _bl = bl;
            _platformBL = platformBL;
            _configurationBL = configurationBL;
        }
        
        public async Task DoSomethingAsync()
        {
            _logger.AddLogInforation($"EDRToolsDataPullingService  method - DoSomethingAsync started...at {DateTime.UtcNow}", "EDRToolsDataPullingService");

            try
            {
                GetSentinalThreatsRequest request;

                var orgs = _platformBL.GetOrganizationToolsByToolType(Constants.Tool_Type_EDR);
                foreach (var org in orgs.OrganizationToolList)
                {
                    request = new GetSentinalThreatsRequest();
                    request.OrgID = org.OrgID;
                    _bl.GetThreats(request);

                }
                _logger.AddLogInforation($"EDRToolsDataPullingService  method - DoSomethingAsync ended...at {DateTime.UtcNow}", "EDRToolsDataPullingService");

            }
            catch (Exception ex) 
            {
                _logger.AddLogError($"Failed to execute EDRToolsDataPullingService with exception message {ex.ToString()}. Good luck next round!", "EDRDataPullingBackgroundJOb");

            }
            //
            finally
            {
                var _data = _configurationBL.GetConfigurationData(Constants.Configdata_EDRToolsDataPullingbackgroundjob, Constants.Configdata_DelayDurationInMinutesForEDRJob);
                int _delayDuration = int.Parse(_data.Data.DataValue);
                await Task.Delay(TimeSpan.FromMinutes(_delayDuration));
            }
           //
        }
    }
}
