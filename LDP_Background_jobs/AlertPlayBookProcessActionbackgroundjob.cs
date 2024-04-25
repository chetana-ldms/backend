using LDP.Common;
using LDP.Common.BL.Interfaces;

namespace LDP_Background_jobs
{
    public class AlertPlayBookProcessActionbackgroundjob : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
        private readonly ILogger<AlertPlayBookProcessActionbackgroundjob> _logger;
        private readonly IServiceScopeFactory _factory;
        private int _executionCount = 0;

        //private readonly IConfigurationDataBL _configData;
        public bool IsEnabled { get; set; }=false;
        public AlertPlayBookProcessActionbackgroundjob(
        ILogger<AlertPlayBookProcessActionbackgroundjob> logger,
        IServiceScopeFactory factory
        //,IConfigurationDataBL configData
            )
        {
            _logger = logger;
            _factory = factory;
        //    _configData = configData;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // ExecuteAsync is executed once and we have to take care of a mechanism ourselves that is kept during operation.
            // To do this, we can use a Periodic Timer, which, unlike other timers, does not block resources.
            // But instead, WaitForNextTickAsync provides a mechanism that blocks a task and can thus be used in a While loop.
            
            using PeriodicTimer timer = new PeriodicTimer(_period);

           // var _data = _configData.GetConfigurationData(Constants.Configdata_SIEMToolsDataPullingbackgroundjob, Constants.Configdata_Enabled);

           // IsEnabled = bool.Parse(_data.Data.DataValue);


            // When ASP.NET Core is intentionally shut down, the background service receives information
            // via the stopping token that it has been canceled.
            // We check the cancellation to avoid blocking the application shutdown.
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    if (IsEnabled)
                    {
                        // We cannot use the default dependency injection behavior, because ExecuteAsync is
                        // a long-running method while the background service is running.
                        // To prevent open resources and instances, only create the services and other references on a run

                        // Create scope, so we get request services
                        await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();

                        // Get service from scope
                        AlertPlayBookProcessActionService Service = asyncScope.ServiceProvider.GetRequiredService<AlertPlayBookProcessActionService>();
                        await Service.DoSomethingAsync();

                        // Sample count increment
                        _executionCount++;
                        _logger.LogInformation(
                            $"Executed AlertPlayBookProcessActionService - Count: {_executionCount}");
                    }
                    else
                    {
                        _logger.LogInformation(
                            "Skipped AlertPlayBookProcessActionService");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute AlertPlayBookProcessActionService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
