using LDP.Common;
using LDP.Common.BL.Interfaces;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.Interfaces;
using LDP_APIs.Models;

namespace LDP_Background_jobs
{
    public class SIEMToolsDataPullingService
    {
        //private readonly ILogger<SIEMToolsDataPullingService> _logger;
        IApplicationLogBL _logger;
        IQRadarIntegrationBL _QRadar;
        ILDPlattformBL _platformBL;
        IConfigurationDataBL _configurationBL;

        public SIEMToolsDataPullingService(IApplicationLogBL logger
            , IQRadarIntegrationBL QRadar
            , ILDPlattformBL platformBL
            , IConfigurationDataBL configurationBL
            )
        {
            _logger = logger;
            _QRadar = QRadar;
            _platformBL = platformBL;
            _configurationBL = configurationBL;
        }
        GetOffenseRequest getAlertsrequest;
        public async Task DoSomethingAsync()
        {
            //_logger.LogInformation(
            //    "SIEM data pulling cycle started " + DateTime.Now.ToUniversalTime);
            _logger.AddLogInforation($"SentinalOne Job Ended.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            getAlertsrequest = new GetOffenseRequest();

            var orgs = _platformBL.GetOrganizationList();
            foreach (var org in orgs.OrganizationList)
            {
                getAlertsrequest.OrgID = 1;// org.OrgID;
                _QRadar.Getoffenses(getAlertsrequest);

            }

            //_logger.LogInformation(
            //    "SIEM data pulling cycle coompleted " + DateTime.Now.ToUniversalTime) ;
            _logger.AddLogInforation($"SIEM data pulling cycle coompleted.. at {DateTime.UtcNow}", Constants.Logger_severity_Information);

            var _data = _configurationBL.GetConfigurationData(Constants.Configdata_SIEMToolsDataPullingbackgroundjob, Constants.Configdata_DelayDurationInMilliSeconds);
            int _delayDuration = int.Parse(_data.Data.DataValue);
            await Task.Delay(_delayDuration);
        }
    }
}
