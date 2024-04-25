using LDP.Common;
using LDP.Common.BL.Interfaces;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.Models;

namespace LDP_Background_jobs
{
    public class AnalyzeAlertsForAutomationService
    {
        private readonly ILogger<AnalyzeAlertsForAutomationService> _logger;
        IAlertPlayBookProcessBL _PlaybookProcess;
        IConfigurationDataBL _configurationBL;
        public AnalyzeAlertsForAutomationService(ILogger<AnalyzeAlertsForAutomationService> logger
            , IAlertPlayBookProcessBL PlaybookProcess
            , IConfigurationDataBL configurationBL
            )
        {
            _logger = logger;
            _PlaybookProcess = PlaybookProcess;
            _configurationBL = configurationBL;
        }
        AnalyzeAlertsForAutomationRequest request;
        public async Task DoSomethingAsync()
        {
            
            _logger.LogInformation(
                "alerts analysis cycle started " + DateTime.Now.ToUniversalTime);
            request = new AnalyzeAlertsForAutomationRequest();
            request.ToolTypeID = 29;
            request.OrgID = 1;
            request.ToolID = 1;
            request.paging = new RequestPaging();
            request.paging.RangeStart = 0;
            request.paging.RangeEnd = 50;

           //var res = _PlaybookProcess.AnalyzeAlertsForAutomation(request);
  
            _logger.LogInformation(
                "alerts analysis cycle completed " + DateTime.Now.ToUniversalTime) ;

            var _data = _configurationBL.GetConfigurationData(Constants.Configdata_AnalyzeAlertsForAutomationbackgroundjob, Constants.Configdata_DelayDurationInMilliSeconds);
            int _delayDuration = int.Parse(_data.Data.DataValue);
            await Task.Delay(_delayDuration);

        }
    }
}
