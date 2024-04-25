using LDP.Common.Services.SentinalOneIntegration.Applications.Agent;
using LDP.Common.Services.SentinalOneIntegration.Applications.CVS;
using LDP.Common.Services.SentinalOneIntegration.Applications.EndPoints;
using LDP.Common.Services.SentinalOneIntegration.Applications.Inventory;
using LDP.Common.Services.SentinalOneIntegration.Applications.ManagementSettings;
using LDP.Common.Services.SentinalOneIntegration.Applications.RiskApplicaionsEndPoint;
using LDP.Common.Services.SentinalOneIntegration.Applications.Risks;
using LDP_APIs.BL.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    public interface ISentinalOneApplicationsIntegrationService
    {
        Task<GetAggragatedApplicationsWithRiskResponse> GetAggregatedApplications(OrganizationToolModel request, string nextcursor = null );
        Task<SentinalOneApplicationInventory> GetSentinalOneApplicationInventory(SentinalOneApplicationsInventoryRequest apiRequest , OrganizationToolModel request, string nextcursor = null);
        Task<ApplicationEndPoints> GetSentinalOneApplicationEndPoints( SentinalOneApplicationsEndPointsRequest apiRequest , OrganizationToolModel request, string nextcursor = null);
        Task<ApplicationCVS> GetSentinalOneApplicationCVS(SentinalOneApplicationsCVSRequest apiRequest, OrganizationToolModel request, string nextcursor = null);
        Task<EndPointDetails> GetSentinalOneApplicationAgentDetails(SentinalOneApplicationsAgentRequest apiRequest, OrganizationToolModel request, string nextcursor = null);
        Task<EndPointApplications> GetSentinalOneEndPointApplications(GetSentinalOneEndPointApplicationsRequest apiRequest, OrganizationToolModel request);
        Task<ApplicationsWithRisks> GetApplicationsAndRisks(GetSentinalOneEndPointApplicationsRisksRequest apiRequest , OrganizationToolModel request, string nextcursor = null);
        Task<RiskApplicationsEndPoints> GetRiskApplicationEndpoints(GetRiskApplicationEndpointRequest apiRequest, OrganizationToolModel request, string nextcursor = null);
        Task<AppManagementSettings> GetApplicationSettings(OrganizationToolModel request);
        Task<EndPointUpdates> GetEndPointUpdates(GetEndPointUpdatesRequest apiRequest , OrganizationToolModel request, string nextcursor = null);

       // Task<EndPointDetails> GetSentinelEndPoints(GetEndPointsRequest apiRequest, OrganizationToolModel request, string nextcursor = null);

       // Task<EndPointDetails> GetSentinelEndPoints(GetEndPointsRequest apiRequest, OrganizationToolModel request, string nextcursor = null);

    }
}
