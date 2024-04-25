using LDP.Common.Services;
using LDP.Common.Services.SentinalOneIntegration;
using LDP.Common.Services.SentinalOneIntegration.Applications;
using LDP.Common.Services.SentinalOneIntegration.Applications.Agent;
using LDP.Common.Services.SentinalOneIntegration.Applications.CVS;
using LDP.Common.Services.SentinalOneIntegration.Applications.EndPoints;
using LDP.Common.Services.SentinalOneIntegration.Applications.Inventory;
using LDP.Common.Services.SentinalOneIntegration.Applications.ManagementSettings;
using LDP.Common.Services.SentinalOneIntegration.Applications.RiskApplicaionsEndPoint;
using LDP.Common.Services.SentinalOneIntegration.Applications.Risks;
using LDP.Common.Services.SentinalOneIntegration.Sentinel;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LDP_APIs.Controllers.V1
{


    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("SentinalOne Integration")]
    public class SentinalOneController : ControllerBase
    {
        ISentinalOneIntegrationBL _sentinalIntegrationBl = null;
        ISentinalOneApplicationsBL _sentinelApplicationBL = null;
        ISentinalBL _sentinalBL = null;
        public SentinalOneController(ISentinalOneIntegrationBL sentinalBl, ISentinalOneApplicationsBL sentinelApplicationBL
,               ISentinalBL sentinalTabBL )
        {
            _sentinalIntegrationBl = sentinalBl;
            _sentinelApplicationBL = sentinelApplicationBL;
            _sentinalBL = sentinalTabBL;
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Account/Details")]
        public AccountResponse GetAccounts(GetAccountsRequest request)
        {
            return _sentinalBL.GetAccounts(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Policy/Details")]
        public AccountPolicy GetAccountPolicy(GetAccountPolicyRequest request)
        {
            return _sentinalBL.GetAccountPolicy(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Sites")]
        public SentinelOneSiteDataResponse GetSites(GetSitesRequest request)
        {
            return _sentinalBL.GetSites(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Groups")]
        public SentinelOneGroupResponse GetGroups(GetGroupsRequest request)
        {
            return _sentinalBL.GetGroups(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Accounts/Structure")]
        public AccountStructureResponse GetAccountStructure(GetAccountStructureRequest request)
        {
            return _sentinalBL.GetAccountStructure(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threats")]
        public GetSentinalThreatAlertsResponse GetThreats(GetSentinalThreatsRequest request)
        {
           return _sentinalIntegrationBl.GetThreats(request); 
            //
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threats/Action")]
        public ThreatActionResponse ThreatAction(ThreatActionRequest request)
        {
            
            return _sentinalIntegrationBl.ThreatAction(request);

            //
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threats/MitigateAction")]
        public MitigateActionResponse ThreatMitigateAction(MitigateActionRequest request)
        {
   

            return _sentinalIntegrationBl.ThreatMitigateAction(request);
        }
       

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threat/Timeline")]
        public GetAlertTimelineResponse GetThreatTimeline(GetThreatTimelineRequest request)
        {
            return _sentinalIntegrationBl.GetThreatTimeline(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threats/AnalystVerdict/Update")]
        public UpdateThreatAnalysisVerdictResponse UpdateThreatAnalystVerdict(SentinalOneUpdateAnalystVerdictRequest request)
        {
            return _sentinalIntegrationBl.UpdateThreatAnalysisVerdict(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threats/Details/Update")]
        public UpdateThreatDetailsResponse UpdateThreatDetails(SentinalOneUpdateThreatDetailsequest request)
        {
          

            return _sentinalIntegrationBl.UpdateThreatDetails(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threat/Notes")]
        public GetThreatNoteResponse GetThreatNotes(GetThreatTimelineRequest request)
        {
            return _sentinalIntegrationBl.GetThreatNotes(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threats/Notes/Add")]
        public AddThreatNoteResponse AddThreatNotes(AddThreatNoteRequest request)
        {
          


            return _sentinalIntegrationBl.AddThreatNotes(request);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Threat/Details")]
        public GetThreatDetailsResponse GetThreatDetails(double alertId)
        {
            GetThreatDetailsResponse response = new GetThreatDetailsResponse();
            if (alertId == 0)
            {
                response.IsSuccess = false;
                response.Message = "Alert Id should be greater than zero ";
                response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
            return _sentinalIntegrationBl.GetThreatDetails(alertId);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Threat/DetailsFromTool")]
        public GetThreatDetailsResponse GetThreatDetailsFromTool(double alertId)
        {
            GetThreatDetailsResponse response = new GetThreatDetailsResponse();
            if (alertId == 0)
            {
                response.IsSuccess = false;
                response.Message = "Alert Id should be greater than zero ";
                response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
            return _sentinalIntegrationBl.GetThreatDetailsFromTool(alertId);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Agent/ConnectToNetwork")]

        public AddToNetworkResponse ConnectToNetwork(AddToNetworkRequest request)
        {
            
            return _sentinalIntegrationBl.AddToNetwork(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Agent/DisConnectFromNetwork")]
        public DisconnectFromNetworkResponse DisconnectFromNetwork(DisconnectFromNetworkRequest request)
        {
  
            return _sentinalIntegrationBl.DisconnectFromNetwork(request);

        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("BlokckedList")]
        public BlockListResponse GetBlockList(BlockListRequest request)
        {
            return _sentinalBL.GetBlockList(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threats/AddToblockList")]

        public AddToBlocklistResponse AddToblockListForThreats(AddToBlocklistForThreatsRequest request)
        {
         //
            return _sentinalIntegrationBl.AddToblockListForThreats(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddToblockList")]

        public AddToBlocklistResponse AddToblockList(AddToBlocklistRequest request)
        {
            return _sentinalIntegrationBl.AddToblockList(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddToblockList/Update")]
        public AddToBlocklistResponse UpdateAddToblockList(UpdateAddToBlocklistRequest request)
        {
            return _sentinalIntegrationBl.UpdateAddToblockList(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddToblockList/Delete")]
        public AddToBlocklistResponse DeleteAddToblockList(DeleteAddToBlocklistRequest request)
        {
            return _sentinalIntegrationBl.DeleteAddToblockList(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Threats/AddToExclusionList")]

        public AddToExclusionlistResponse AddToExclusionList(AddToExclusionRequest request)
        {
              return _sentinalIntegrationBl.AddToExclusionList(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddToExclusionList/Update")]
        public AddToExclusionlistResponse UpdateAddToExclusionList(UpdateAddToExclusionRequest request)
        {

            return _sentinalIntegrationBl.UpdateAddToExclusionList(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddToExclusionList/Delete")]
        public AddToExclusionlistResponse DeleteAddToExclusionList(DeleteAddToExclusionRequest request)
        {

            return _sentinalIntegrationBl.DeleteAddToExclusionList(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Exclusion/List")]
        public ExclustionsResponse GetExclusions(ExclusionRequest request)
        {
            return _sentinalBL.GetExclusions(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Risks/Applications")]
        public ApplicationsWithRisks GetApplicationsAndRisks(GetSentinalOneEndPointApplicationsRisksRequest request)
        {
            
            return _sentinelApplicationBL.GetApplicationsAndRisks(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Risks/Application/Endpoints")]
        public RiskApplicationsEndPointsResponse GetRiskApplicationEndpoints(GetRiskApplicationEndpointRequest request)
        {
            return _sentinelApplicationBL.GetRiskApplicationEndpoints(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Risks/Application/CVEs")]
        public ApplicationCVSResponse GetSentinalOneApplicationCVS(SentinalOneApplicationsCVSRequest request)
        {
            return _sentinelApplicationBL.GetSentinalOneApplicationCVS(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Inventory/Applications")]
        public SentinalOneApplicationInventoryResponse GetSentinalOneApplicationInventory(SentinalOneApplicationsInventoryRequest request)
        {
             return _sentinelApplicationBL.GetSentinalOneApplicationInventory(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Inventory/Applications/Endpoints")]

        public ApplicationEndPointsResponse GetSentinalOneApplicationEndPoints(SentinalOneApplicationsEndPointsRequest request)
        {
            return _sentinelApplicationBL.GetSentinalOneApplicationEndPoints(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("EndPoint/Applications")]
        public EndPointApplications GetSentinalOneEndPointApplications(GetSentinalOneEndPointApplicationsRequest request)
        {
            return _sentinelApplicationBL.GetSentinalOneEndPointApplications(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("EndPoint/Details")]
        public EndPointAgentDetailsResponse GetSentinalOneApplicationEndPointDetails(SentinalOneApplicationsAgentRequest request)
        {
            return _sentinelApplicationBL.GetSentinalOneApplicationEndPointDetails(request);
        }

        //
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ApplicationManagement/Settings")]
        public ApplicationManagementSettingsResponse GetApplicationSettings(GetApplicationSettingsRequest request)
        {
            return _sentinelApplicationBL.GetApplicationSettings(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("EndPoint/Updates")]
        public EndPointUpdatesResponse GetEndPointUpdates(GetEndPointUpdatesRequest request)
        {
            return _sentinelApplicationBL.GetEndPointUpdates(request);
        }



    }
}

