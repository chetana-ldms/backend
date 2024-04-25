using LDP.Common.Model;
using LDP.Common.Services.SentinalOneIntegration;
using LDP_APIs.BL.Models;

namespace LDP.Common.Services
{
    public interface ISentinalOneIntegrationService
    {
       Task<SentinalOneGetThreatResponse> GetThreats(OrganizationToolModel request ,  string nextCursor = null , GetSentinalThreatsRequest apiRequest = null );

       Task<MitigateActionResponse> ThreatMitigateAction(MitigateActionDTO request , OrganizationToolModel conndtl);

       Task<GetThreatTimelineResponse> GetThreatTimeline(GetThreatTimelineDTO request, OrganizationToolModel conndtl);

       Task<UpdateThreatAnalysisVerdictResponse> UpdateThreatAnalysisVerdict(SentinalOneUpdateAnalystVerdictDTO request, OrganizationToolModel conndtl);
        
       Task<UpdateThreatDetailsResponse> UpdateThreatDetails(SentinalOneUpdateThreatDetailsDTO request, OrganizationToolModel conndtl);

       Task<GetThreatNoteServiceResponse> GetThreatNotes(GetThreatNoteeDTO request, OrganizationToolModel conndtl);

       Task<AddThreatNoteResponse> AddThreatNotes(AddThreatNoteeDTO request, OrganizationToolModel conndtl);

        Task<AddToNetworkResponse> AddToNetwork(AddToNetworkDTO request, OrganizationToolModel conndtl);

        Task<DisconnectFromNetworkResponse> DisconnectFromNetwork(AddToExclusionList request, OrganizationToolModel conndtl);

        Task<AddToBlocklistResponse> AddToblockListForThreats(AddToBlocklistDTO request, OrganizationToolModel conndtl);

        Task<AddToBlocklistResponse> AddToblockList(AddToBlocklistRequest request, OrganizationToolModel conndtl);

        Task<AddToBlocklistResponse> UpdateAddToblockList(UpdateAddToBlocklistRequest request, OrganizationToolModel conndtl);

        Task<AddToBlocklistResponse> DeleteAddToblockList(DeleteAddToBlocklistRequest request, OrganizationToolModel conndtl);

        Task<AddToExclusionlistResponse> AddToExclusionList(AddToExclusionlistDTO request, OrganizationToolModel conndtl);

        Task<AddToExclusionlistResponse> UpdateAddToExclusionList(UpdateAddToExclusionRequest request, OrganizationToolModel conndtl);

        Task<AddToExclusionlistResponse> DeleteAddToExclusionList(DeleteAddToExclusionRequest request, OrganizationToolModel conndtl);




    }
}
