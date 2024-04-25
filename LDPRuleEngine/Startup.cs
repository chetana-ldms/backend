using FluentValidation;
using LDP.Common.BL;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Interfaces;
using LDP.Common.DAL.Repositories;
using LDP.Common.Mappers;
using LDP_APIs.BL;
using LDP_APIs.BL.Factories;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.DAL;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Interfaces;
using LDP_APIs.DAL.Respository;
using LDP_APIs.Helpers.Helpers;
using LDP_APIs.Interfaces;
using LDP_APIs.Services;
using LDPRuleEngine.BL;
using LDPRuleEngine.BL.Framework.Actions;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.DAL.DataContexts;
using LDPRuleEngine.DAL.Interfaces;
using LDPRuleEngine.DAL.Repositories;
using LDPRuleEngine.Validators;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

namespace LDPRuleEngine
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
        public void ConfigureServices(IServiceCollection services)
        {
            //Adding CORS
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddAutoMapper(typeof(GetAlertPlayBookProcessMapper).Assembly);
            //services.AddAutoMapper(typeof(AddAlertPlayBookProcessMapper).Assembly);
            services.AddAutoMapper(typeof(AlertsBL).Assembly);
            
            string? connectionString = configRoot.GetConnectionString("LDPConnection").ToString();
             services.AddDbContextFactory<RuleEngineDataContext>(options => options.UseMySQL(connectionString));
            services.AddDbContextFactory<LDPlattformDataContext>(options => options.UseMySQL(connectionString));
            services.AddDbContextFactory<AlertsDataContext>(options => options.UseMySQL(connectionString));
            services.AddDbContextFactory<CommonDataContext>(options => options.UseMySQL(connectionString));
            services.AddDbContextFactory<LDPSecurityDataContext>(options => options.UseMySQL(connectionString));
            services.AddCors();
            services.AddControllers();
            services.AddValidatorsFromAssemblyContaining<AddRuleRequestValidator>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));

               

            });

            services.AddTransient<IRuleEngineRepository, RuleEngineRepository>();
            services.AddTransient<IRulesConfigurationBL, RulesConfigurationBL>();
            services.AddHttpClient();
            services.AddTransient<ILDPlattformRepository, LDPlattformRepository>();
            services.AddTransient<ILDPlattformBL, LDPlattformBL>();
            services.AddTransient<IAlertIncidentMappingBL, AlertIncidentMappingBL>();
            services.AddTransient<IAlertIncidentMappingBL, AlertIncidentMappingBL>();
            services.AddTransient<IIncidentManagementService, IncidentManagementService>();
            services.AddTransient<IIncidentsBL, IncidentsBL>();
            services.AddTransient<IUserActionBL, UserActionBL>();
            services.AddTransient<IUserActionsRepository, UserActionRepository>();
            
            services.AddTransient<IAlertHistoryRepository, AlertHistoryRepository>();
            services.AddTransient<IAlertHistoryBL, AlertHistoryBL>();
            services.AddTransient<IInternalIncidentsRepository, InternalIncidentsRepository>();
            
            services.AddScoped<TicketManagementRuleActionExecuter>();
            services.AddScoped<TicketManagementFactory>();

            services.AddTransient<IConfigurationDataRepository, ConfigurationDataRepository>();
            services.AddTransient<IConfigurationDataBL, ConfigurationDataBL>();

            services.AddTransient<ILdpMasterDataRepository, LdpMasterDataRepository>();
            services.AddTransient<ILdpMasterDataBL, LDPMasterDataBL>();


            services.AddScoped<baseRuleActionExecuter, TicketManagementRuleActionExecuter>
                (s => s.GetService<TicketManagementRuleActionExecuter>());
            services.AddTransient<RuleActonExecuterFactory>();
            services.AddTransient<IAlertPlayBookProcessBL, AlertPlayBookProcessBL>();
            services.AddTransient<IAlertPlayBookProcessRepository, AlertPlayBookProcessRepository>();
            

            
            services.AddTransient<IAlertPlayBookProcessActionBL, AlertPlayBookProcessActionBL>();
            services.AddTransient<IAlertPlayBookProcessActionRepository, AlertPlayBookProcessActionRepository>();

            services.AddTransient<IRuleActionRespository, RuleActionRespository>();

            services.AddTransient<IAlertsRepository, AlertsRepository>();
            services.AddTransient<IAlertsBL, AlertsBL>();
            services.AddTransient<ILDPSecurityBL, LDPSecurityBL>();
            services.AddTransient<ILDPSecurityRepository, LDPSecurityRepository>();
            services.AddTransient<IRuleActionBL, RuleActionBL>();
            services.AddTransient<IPlaybookBL, PlayBookBL>();
            services.AddTransient<IPlayBookRepository, PlayBookRepository>();

            services.AddTransient<IPlaybookdtlsBL, PlayBookdtlsBL>();
            services.AddTransient<IPlayBookDtlsRepository, PlayBookDtlsRepository>();

            services.AddTransient<IAlertPlayBookProcessBL, AlertPlayBookProcessBL>();

            services.AddTransient<IAlertIncidentMappingBL, AlertIncidentMappingBL>();
            services.AddTransient<IAlertIncidentMappingRepository, AlertIncidentMappingRepository>();
        }
    }
}
