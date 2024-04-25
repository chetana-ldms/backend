using LDP.Common.BL;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Interfaces;
using LDP.Common.DAL.Repositories;
using LDP.Common.Mappers;
using LDP.Common.Services;
using LDP.Common.Services.SentinalOneIntegration;
using LDP.Common.Services.SentinalOneIntegration.Sentinel;
using LDP_APIs.BL;
using LDP_APIs.BL.Factories;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.DAL;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Interfaces;
using LDP_APIs.DAL.Respository;
using LDP_APIs.Interfaces;
using LDP_APIs.Services;
using LDP_Background_jobs;
using LDPRuleEngine.BL;
using LDPRuleEngine.BL.Framework.Actions;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.DAL.DataContexts;
using LDPRuleEngine.DAL.Interfaces;
using LDPRuleEngine.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
using IHost host = Host.CreateDefaultBuilder().Build();

// Ask service provider for configuration
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

// Get connection string
string connectionString = config.GetValue<string>("ConnectionStrings:LDPConnection");

builder.Services.AddDbContextFactory<LDPlattformDataContext>(options => options.UseMySQL(connectionString));
builder.Services.AddDbContextFactory<AlertsDataContext>(options => options.UseMySQL(connectionString));
builder.Services.AddDbContextFactory<RuleEngineDataContext>(options => options.UseMySQL(connectionString));
builder.Services.AddDbContextFactory<CommonDataContext>(options => options.UseMySQL(connectionString));
builder.Services.AddDbContextFactory<LDPSecurityDataContext>(options => options.UseMySQL(connectionString));


//builder.Services.AddScoped<SIEMToolsDataPullingService>();
//builder.Services.AddScoped<AlertPlayBookProcessActionService>();
//builder.Services.AddScoped<AnalyzeAlertsForAutomationService>();
builder.Services.AddScoped<EDRToolsDataPullingService>();
builder.Services.AddScoped<IQRadarIntegrationBL, QRadarIntegrationBL>();
builder.Services.AddScoped<IQRadarIntegrationservice, QRadarIntegrationservice>();
builder.Services.AddScoped<IAlertPlayBookProcessBL, AlertPlayBookProcessBL>();
builder.Services.AddScoped<IAlertPlayBookProcessRepository, AlertPlayBookProcessRepository>();
builder.Services.AddScoped<IAlertsRepository, AlertsRepository>();
builder.Services.AddScoped<IAlertsRepository, AlertsRepository>();
builder.Services.AddScoped<IAlertsBL, AlertsBL>();
builder.Services.AddScoped<IPlaybookBL, PlayBookBL>();
builder.Services.AddScoped<IPlayBookDtlsRepository, PlayBookDtlsRepository>();
builder.Services.AddScoped<IRulesConfigurationBL, RulesConfigurationBL>();
builder.Services.AddScoped<IRuleActionBL, RuleActionBL>();
builder.Services.AddScoped<TicketManagementRuleActionExecuter>();
builder.Services.AddScoped<baseRuleActionExecuter, TicketManagementRuleActionExecuter>(s => s.GetService<TicketManagementRuleActionExecuter>());

builder.Services.AddTransient<IRulesConfigurationBL, RulesConfigurationBL>();
builder.Services.AddTransient<IRuleEngineRepository, RuleEngineRepository>();

builder.Services.AddScoped<IAlertPlayBookProcessActionBL, AlertPlayBookProcessActionBL>();
builder.Services.AddScoped<IAlertPlayBookProcessActionRepository, AlertPlayBookProcessActionRepository>();
builder.Services.AddTransient<RuleActonExecuterFactory>();
builder.Services.AddScoped<IRuleActionRespository, RuleActionRespository>();
builder.Services.AddScoped<IPlayBookRepository, PlayBookRepository>();
builder.Services.AddScoped<IIncidentsBL, IncidentsBL>();
builder.Services.AddTransient<IInternalIncidentsRepository, InternalIncidentsRepository>();

builder.Services.AddTransient<IAlertIncidentMappingBL, AlertIncidentMappingBL>();
builder.Services.AddTransient<IAlertIncidentMappingRepository, AlertIncidentMappingRepository>();

builder.Services.AddScoped<IIncidentManagementService, IncidentManagementService>();

builder.Services.AddScoped<IIncidentManagementService, IncidentManagementService>
                (s => s.GetService<IncidentManagementService>());

builder.Services.AddScoped<TicketManagementFactory>();

builder.Services.AddTransient<IAlertIncidentMappingBL, AlertIncidentMappingBL>();
builder.Services.AddTransient<IAlertIncidentMappingRepository, AlertIncidentMappingRepository>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<ILDPlattformBL, LDPlattformBL>();
builder.Services.AddScoped<ILDPlattformRepository, LDPlattformRepository>();

builder.Services.AddTransient<ILDPSecurityRepository, LDPSecurityRepository>();
builder.Services.AddTransient<ILDPSecurityBL, LDPSecurityBL>();

builder.Services.AddTransient<IUserActionsRepository, UserActionRepository>();
builder.Services.AddTransient<IUserActionBL, UserActionBL>();

builder.Services.AddTransient<IAlertsRepository, AlertsRepository>();
builder.Services.AddTransient<IAlertsBL, AlertsBL>();

builder.Services.AddTransient<IAlertHistoryBL, AlertHistoryBL>();
builder.Services.AddScoped<IAlertHistoryRepository, AlertHistoryRepository>();

builder.Services.AddScoped<ILdpMasterDataBL, LDPMasterDataBL>();
builder.Services.AddScoped<ILdpMasterDataRepository, LdpMasterDataRepository>();

builder.Services.AddTransient<IConfigurationDataBL, ConfigurationDataBL>();
builder.Services.AddTransient<IConfigurationDataRepository, ConfigurationDataRepository>();

builder.Services.AddScoped<IAlertExtnFieldBL, AlertExtnFieldBL>();
builder.Services.AddScoped<IAlertExtnFieldRepository, AlertExtnFieldRepository>();

builder.Services.AddAutoMapper(typeof(GetAlertPlayBookProcessMapper).Assembly);

// Register as singleton first so it can be injected through Dependency Injection
//builder.Services.AddSingleton<SIEMToolsDataPullingbackgroundjob>();

//builder.Services.AddSingleton<AnalyzeAlertsForAutomationbackgroundjob>();
//builder.Services.AddSingleton<AlertPlayBookProcessActionbackgroundjob>();
// Add as hosted service using the instance registered as singleton before
//builder.Services.AddHostedService(
//    provider => provider.GetRequiredService<SIEMToolsDataPullingbackgroundjob>());

//builder.Services.AddHostedService(
//    provider => provider.GetRequiredService<AnalyzeAlertsForAutomationbackgroundjob>());

//builder.Services.AddHostedService(
//    provider => provider.GetRequiredService<AlertPlayBookProcessActionbackgroundjob>());

builder.Services.AddSingleton<EDRToolsDataPullingbackgroundjob>();

builder.Services.AddHostedService(
    provider => provider.GetRequiredService<EDRToolsDataPullingbackgroundjob>());

builder.Services.AddScoped<ISentinalOneIntegrationBL, SentinalOneIntegrationBL>();
builder.Services.AddScoped<ISentinalOneIntegrationService, SentinalOneIntegrationService>();

builder.Services.AddScoped<IApplicationLogBL, ApplicationLogBL>();
builder.Services.AddScoped<IApplicationlogsRepository, ApplicationlogsRepository>();
builder.Services.AddTransient<ICommonBL, CommonBL>();
builder.Services.AddTransient<ICommonRepository, CommonRepository>();
builder.Services.AddTransient<ISentinalBL, SentinelBL>();
builder.Services.AddTransient<ISentinelService, SentinelService>();


WebApplication app = builder.Build();


app.MapGet("/", () => "Lancesoft Defence Platform Background jobs running...");

app.MapGet("/background", (
    EDRToolsDataPullingbackgroundjob service) =>
{
    return new BackgroundjobState(service.IsEnabled);
});

app.MapMethods("/background", new[] { "PATCH" }, (
    BackgroundjobState state,
    EDRToolsDataPullingbackgroundjob service) =>
{
    service.IsEnabled = state.IsEnabled;
});




//app.MapGet("/background", (
//    SIEMToolsDataPullingbackgroundjob service) =>
//{
//    return new BackgroundjobState(service.IsEnabled);
//});

//app.MapMethods("/background", new[] { "PATCH" }, (
//    BackgroundjobState state,
//    SIEMToolsDataPullingbackgroundjob service) =>
//{
//    service.IsEnabled = state.IsEnabled;
//});

//app.MapGet("/background", (
//    AnalyzeAlertsForAutomationbackgroundjob service) =>
//{
//    return new BackgroundjobState(service.IsEnabled);
//});
//app.MapMethods("/background", new[] { "PATCH" }, (
//    BackgroundjobState state,
//    AnalyzeAlertsForAutomationbackgroundjob service) =>
//{
//    service.IsEnabled = state.IsEnabled;
//});

//app.MapGet("/background", (
//    AlertPlayBookProcessActionbackgroundjob service) =>
//{
//    return new BackgroundjobState(service.IsEnabled);
//});
//app.MapMethods("/background", new[] { "PATCH" }, (
//    BackgroundjobState state,
//    AlertPlayBookProcessActionbackgroundjob service) =>
//{
//    service.IsEnabled = state.IsEnabled;
//});


app.Run();
