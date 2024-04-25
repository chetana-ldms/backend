using LDP.Common.Services.SentinalOneIntegration.Applications.Agent;
using LDP.Common.Services.SentinalOneIntegration.Applications.CVS;
using LDP.Common.Services.SentinalOneIntegration.Applications.EndPoints;
using LDP.Common.Services.SentinalOneIntegration.Applications.Inventory;
using LDP.Common.Services.SentinalOneIntegration.Applications.ManagementSettings;
using LDP.Common.Services.SentinalOneIntegration.Applications.RiskApplicaionsEndPoint;
using LDP.Common.Services.SentinalOneIntegration.Applications.Risks;

namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    public interface ISentinalOneApplicationsBL
    {
        SentinalOneAggregatedApplicationData GetSentinalOneAggregateApplicationData(SentinalOneApplicationsRequest request);

        SentinalOneApplicationInventoryResponse GetSentinalOneApplicationInventory(SentinalOneApplicationsInventoryRequest request);

        ApplicationEndPointsResponse GetSentinalOneApplicationEndPoints(SentinalOneApplicationsEndPointsRequest request);
        ApplicationCVSResponse GetSentinalOneApplicationCVS(SentinalOneApplicationsCVSRequest rquest);

       // EndPointAgentDetailsResponse GetSentinelEndPoints(GetEndPointsRequest request);
        EndPointAgentDetailsResponse GetSentinalOneApplicationEndPointDetails(SentinalOneApplicationsAgentRequest request );

        EndPointApplications GetSentinalOneEndPointApplications(GetSentinalOneEndPointApplicationsRequest request);

        ApplicationsWithRisks GetApplicationsAndRisks(GetSentinalOneEndPointApplicationsRisksRequest request);

        RiskApplicationsEndPointsResponse GetRiskApplicationEndpoints(GetRiskApplicationEndpointRequest request);

        ApplicationManagementSettingsResponse GetApplicationSettings(GetApplicationSettingsRequest request);

        EndPointUpdatesResponse GetEndPointUpdates(GetEndPointUpdatesRequest request);
       

    }


}