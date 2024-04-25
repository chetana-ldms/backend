namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    using AutoMapper;
    using LDP.Common.Services.SentinalOneIntegration.Applications.Agent;
    using LDP.Common.Services.SentinalOneIntegration.Applications.CVS;
    using LDP.Common.Services.SentinalOneIntegration.Applications.EndPoints;
    using LDP.Common.Services.SentinalOneIntegration.Applications.Inventory;
    using LDP.Common.Services.SentinalOneIntegration.Applications.ManagementSettings;
    using LDP.Common.Services.SentinalOneIntegration.Applications.RiskApplicaionsEndPoint;

    public class ApplicationEndPointsMappers:Profile
    {
        public ApplicationEndPointsMappers()
        {
            CreateMap<LDP.Common.Services.SentinalOneIntegration.Applications.EndPoints.Data, ApplicationEndPoint>();
            CreateMap<LDP.Common.Services.SentinalOneIntegration.Applications.CVS.Data, ApplicatioCVSData>();
            CreateMap<Agent.Datum ,EndPointAgentDetails>();
            CreateMap<RiskApplicaionsEndPoint.Data, RiskApplicationEndpointData>();
            CreateMap<Inventory.Data, ApplicationInventory>();
            CreateMap<AppManagementSettings, ApplicationManagementSettingsResponse>();
            CreateMap<LDP.Common.Services.SentinalOneIntegration.Applications.ManagementSettings.Data, ApplicationManagementSettings>();
           


        }

    }
}
