using DinkToPdf;
using DinkToPdf.Contracts;
using FluentValidation;
using LDP.Common.BL;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Interfaces;
using LDP.Common.DAL.Repositories;
using LDP.Common.Filters;
using LDP.Common.Helpers.Email;
using LDP.Common.Helpers.Interfaces;
using LDP.Common.Mappers;
using LDP.Common.Services;
using LDP.Common.Services.DrataIntegration;
using LDP.Common.Services.FileService;
using LDP.Common.Services.Notifications.Mail;
using LDP.Common.Services.Notifications.SMS;
using LDP.Common.Services.Notifications.Teams;
using LDP.Common.Services.SentinalOneIntegration;
using LDP.Common.Services.SentinalOneIntegration.Applications;
using LDP.Common.Services.SentinalOneIntegration.Sentinel;
using LDP.Common.Services.Teams;
using LDP_APIs.BL;
using LDP_APIs.BL.Factories;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Mappers;
using LDP_APIs.DAL;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Interfaces;
using LDP_APIs.DAL.Respository;
using LDP_APIs.Helpers.Helpers;
using LDP_APIs.Interfaces;
using LDP_APIs.Services;
using LDP_APIs.Validators;
using LDPRuleEngine.DAL.Interfaces;
using LDPRuleEngine.DAL.Repositories;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

namespace LDP_APIs
{
    public class LDPStartup
    {
       
        public IConfiguration configRoot
        {
            get;
        }
        public LDPStartup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            //Adding CORS
            services.AddAutoMapper(typeof(GetChatHistoryProfile).Assembly);
            services.AddAutoMapper(typeof(GetAlertPlayBookProcessMapper).Assembly);
            string?  connectionString = configRoot.GetConnectionString("LDPConnection").ToString();
            services.AddDbContextFactory<GetChatHistoryDataConext>(options => options.UseMySQL(connectionString));
            services.AddDbContextFactory<LDPSecurityDataContext>(options => options.UseMySQL(connectionString));
            services.AddDbContextFactory<LDPlattformDataContext>(options => options.UseMySQL(connectionString));
            services.AddDbContext<AlertsDataContext>(options => options.UseMySQL(connectionString));
            services.AddDbContextFactory<CommonDataContext>(options => options.UseMySQL(connectionString));
            services.AddDbContextFactory<ChannelsDataContext>(options => options.UseMySQL(connectionString));

            services.AddHttpContextAccessor();

            services.AddCors();
            services.AddControllers().AddJsonOptions(options =>
            {

                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }); ;
            
            services.AddValidatorsFromAssemblyContaining<AuthenticateUserValidator>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
          

            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<EnumSchemaFilter>();
                c.DocumentFilter<SwaggerEnumDocumentFilter>();
            });


            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
            });

            services.AddTransient<IQRadarIntegrationBL, QRadarIntegrationBL>();
            // Need to implement Polly retry policy here .
            services.AddHttpClient()
                  //  .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1)));
            ;
            services.AddTransient<IQRadarIntegrationservice, QRadarIntegrationservice>();

            services.AddScoped<IncidentManagementService>();

            services.AddScoped<IIncidentManagementService, IncidentManagementService>
                (s => s.GetService<IncidentManagementService>());

            services.AddScoped<TicketManagementFactory>();

                       
            services.AddTransient<IIncidentsBL, IncidentsBL>();
            services.AddTransient<IChatBL, ChatBL>();

            services.AddTransient<IChatHistoryRepository, ChatHistoryRepository>();

            services.AddTransient<ILDPSecurityRepository, LDPSecurityRepository>();
            services.AddTransient<ILDPSecurityBL, LDPSecurityBL>();

            services.AddTransient<ILDPlattformRepository, LDPlattformRepository>();
            services.AddTransient<ILDPlattformBL, LDPlattformBL>();

            services.AddTransient<IUserActionsRepository, UserActionRepository>();
            services.AddTransient<IUserActionBL, UserActionBL>();

            services.AddTransient<IAlertsRepository, AlertsRepository>();
            services.AddTransient<IAlertsBL, AlertsBL>();

            

            services.AddTransient<IAlertPlayBookProcessRepository, AlertPlayBookProcessRepository>();

            services.AddTransient<IAlertPlayBookProcessActionRepository, AlertPlayBookProcessActionRepository>();
            services.AddTransient<IAlertPlayBookProcessActionBL, AlertPlayBookProcessActionBL>();

           services.AddTransient<IIncidentsBL, IncidentsBL>();
           services.AddTransient<IInternalIncidentsRepository, InternalIncidentsRepository>();
           services.AddTransient<IUserActionsRepository, UserActionRepository>();
            
            services.AddTransient<IAlertIncidentMappingBL, AlertIncidentMappingBL>();
            services.AddTransient<IAlertIncidentMappingRepository, AlertIncidentMappingRepository>();

            services.AddTransient<IAlertHistoryBL, AlertHistoryBL>();
            services.AddTransient<IAlertHistoryRepository, AlertHistoryRepository>();

            services.AddTransient<IAlertReportsBL, AlertReportsBL>();
            services.AddTransient<IAlertReportsRepository, AlertReportsRepository>();

            services.AddTransient<IIncidentReportsBL, IncidentReportsBL>();
            services.AddTransient<IIncidentReportsRepository, IncidentReportsRepository>();
      

            services.AddTransient<ILdpMasterDataBL, LDPMasterDataBL>();
            services.AddTransient<ILdpMasterDataRepository, LdpMasterDataRepository>();

            services.AddTransient<ISMSSenderBL, SMSSenderBL>();
            services.AddTransient<ISMSSenderService, TwillioSMSSenderService>();

            services.AddTransient<IConfigurationDataBL, ConfigurationDataBL>();
            services.AddTransient<IConfigurationDataRepository, ConfigurationDataRepository>();

            services.AddTransient<IAlertExtnFieldBL, AlertExtnFieldBL>();
            services.AddTransient<IAlertExtnFieldRepository, AlertExtnFieldRepository>();

            services.AddTransient<ITeamsMessageBL, TeamsMessageBL>();
            services.AddTransient<ITeamsMessageService, TeamsMessageService>();

            services.AddTransient<INotificationDetailBL, NotificationDetailBL>();
            services.AddTransient<INotificationDetailsRepository, NotificationDetailsRepository>();

            services.AddTransient<ILDCChannelBL, LDCChannelBL>();
            services.AddTransient<ILDCChannelRespository, LDCChannelRespository>();

            services.AddTransient<IFileHandlerBL, FileHandlerBL>();
            services.AddTransient<IFileHandlerRepository, FileHandlerRepository>();
            services.AddTransient<IFileHandlerService, IISFolderFileService>();

            services.AddTransient<IMSTeamsBL, TeamsBL>();
            services.AddTransient<IAPIUrlBL, LDCAPIUrlBL>();

            services.AddTransient<ILDCAPIUrlRepository, LDCAPIUrlRepository>();
            services.AddTransient<ITeamsService, TeamsService>();

            services.AddTransient<ISentinalOneIntegrationBL, SentinalOneIntegrationBL>();
            services.AddTransient<ISentinalOneIntegrationService, SentinalOneIntegrationService>();

            services.AddTransient<IApplicationLogBL, ApplicationLogBL>();
            services.AddTransient<IApplicationlogsRepository, ApplicationlogsRepository>();

            services.AddTransient<IDrataOperationsBL, DrataOperationsBL>();
            services.AddTransient<IDrataIntegrationService, DrataIntegrationService>();

            services.AddTransient<ITaskBL, TaskBL>();
            services.AddTransient<ICommonRepository, CommonRepository>();

            services.AddTransient<IMailSender, TwillioEmailSender>();

            services.AddSingleton<IEmailTemplateFactory, EmailTemplateFactory>();
            services.AddTransient<PasswordResetTemplate>(); // Register any other email template implementations

            services.AddTransient<IEmailHelper, EmailHelper>();

            services.AddTransient<ISentinalOneApplicationsBL, SentinalOneApplicationsBL>();
            services.AddTransient<ISentinalOneApplicationsIntegrationService, SentinalOneApplicationsIntegrationService>();

            services.AddTransient<ISentinalBL, SentinelBL>();
            services.AddTransient<ISentinelService, SentinelService>();
            services.AddTransient<ICommonBL, CommonBL>();

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
             }
            app.UseStaticFiles(); // For the wwwroot folder  

            var dirpath = Directory.GetCurrentDirectory() + "\\" + LDP.Common.Constants.LDC_Upload_FolderPath;
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), LDP.Common.Constants.LDC_Upload_FolderPath)),
                RequestPath = "/" + LDP.Common.Constants.LDC_Upload_FolderPath
            }); //For the needed folder  

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
