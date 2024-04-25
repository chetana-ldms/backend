using LDP.Common;
using LDP.Common.BL.Interfaces;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.Models;

namespace LDP_Background_jobs
{
    public class AlertPlayBookProcessActionService
    {
        private readonly ILogger<AlertPlayBookProcessActionService> _logger;
        IAlertPlayBookProcessBL _PlaybookProcess;
        IConfigurationDataBL _configurationBL;
        public AlertPlayBookProcessActionService
            (ILogger<AlertPlayBookProcessActionService> logger
            , IAlertPlayBookProcessBL PlaybookProcess
            , IConfigurationDataBL configurationBL

            )
        {
            _logger = logger;
            _PlaybookProcess = PlaybookProcess;
            _configurationBL = configurationBL;
        }
        PlayBookProcessActionRequest request;
        public async Task DoSomethingAsync()
        {
           // await Task.Delay(100);
            _logger.LogInformation(
                "alerts analysis cycle started " + DateTime.Now.ToUniversalTime);
            request = new PlayBookProcessActionRequest();
            request.ToolTypeID = 30;
            request.OrgID = 1;
            request.ToolID = 2;
            request.paging = new RequestPaging();
            request.paging.RangeStart = 0;
            request.paging.RangeEnd = 50;
          //PlaybookProcess.ProcessAlertPlaybookProcessActions(request);
            _logger.LogInformation(
                "alerts analysis cycle completed " + DateTime.Now.ToUniversalTime) ;

            var _data = _configurationBL.GetConfigurationData(Constants.Configdata_AlertPlayBookProcessActionbackgroundjob, Constants.Configdata_DelayDurationInMilliSeconds);

            int _delayDuration = int.Parse(_data.Data.DataValue);

            await Task.Delay(_delayDuration);

        }
    }
}
