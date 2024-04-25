using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;

namespace LDP.Common.Services.SentinalOneIntegration
{

    public class GetSentinalThreatsRequest
    {
        public int OrgID { get; set; }

        public List<string>? ThreatIds { get; set; }

    }


    public class GetThreatDataRequest
    {
        public int OrgID { get; set; }
        public string? ThreatId { get; set; }

        public double AlertId { get; set; }

    }

    public class GetThreatTimelineRequest : GetThreatDataRequest
    {
        //public int OrgID { get; set; }
        ////  public string? ThreatId { get; set; }

        //public double AlertId { get; set; }

    }

        public class GetThreatTimelineDTO : GetThreatTimelineRequest
    {

        public string? ThreatId { get; set; }

        public string? NextCursor { get; set; }
    }

    public class GetThreatNoteRequest : GetThreatDataRequest
    {
        //public int OrgID { get; set; }
        ////  public string? ThreatId { get; set; }

        //public double AlertId { get; set; }

    }

    public class GetThreatNoteeDTO : GetThreatTimelineRequest
    {

        public string? ThreatId { get; set; }

        public string? NextCursor { get; set; }
    }

    public class AddThreatNoteRequest 
    {
        public int OrgID { get; set; }
        public List<double>? AlertIds { get; set; }

        public string? Notes { get; set;}
    }

    public class AddThreatNoteeDTO 
    {

        public List<string>? AccountIds { get; set; }

        public List<string>? SiteIds { get; set; }

        public List<string>? ThreatIds { get; set; }

        public string? Notes { get; set; }
    }
    public class SentinalOneActionRequest
    {
        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }
        public int OrgId { get; set; }
        public List<double>? AlertIds { get; set; }
    }

    public class MitigateActionRequest: SentinalOneActionRequest
    {
        [DefaultValue(false)]  

        public bool Kill { get; set; }
        [DefaultValue(false)]
        public bool Quarantine { get; set; }
        [DefaultValue(false)]
        public bool Remediate { get; set; }
        [DefaultValue(false)]
        public bool Rollback { get; set; }
        [DefaultValue(false)]
        public bool ResolvedStatus { get; set; }
        [DefaultValue(false)]
        public bool AddToBlockedList { get; set; }
       
        public string? Scope { get; set; }
        [DefaultValue("")]
        public string? Notes { get; set; }
        [DefaultValue(false)]
        public bool AnalystVerdict_TruePositive { get; set; }
        [DefaultValue(false)]
        public bool AnalystVerdict_FalsePositive { get; set; }
        [DefaultValue(false)]
        public bool AnalystVerdict_Suspecious { get; set; }


    }

    public class MitigateActionDTO
    {
     
        public List<string>? AccountIds { get; set; }

        public List<string>? SiteIds { get; set; }

        public List<string>? ThreatIds { get; set; }

        public string? ActionType { get; set; }
    }

    public class ThreatActionRequest : SentinalOneActionRequest
    {
        [DefaultValue(false)]

        public bool Kill { get; set; }
        [DefaultValue(false)]
        public bool Quarantine { get; set; }
        [DefaultValue(false)]
        public bool Remediate { get; set; }
        [DefaultValue(false)]
        public bool Rollback { get; set; }
        [DefaultValue(false)]
        public bool UnQuarantine { get; set; }
        [DefaultValue(false)]
        public bool NetworkQuarantine { get; set; }

    }

    public class ThreatActionDTO : ThreatActionRequest
    {

        public List<string>? AccountIds { get; set; }

        public List<string>? SiteIds { get; set; }

        public List<string>? ThreatIds { get; set; }


    }

    public class SentinalOneUpdateAnalystVerdictRequest: SentinalOneActionRequest
    {

        [DefaultValue(false)]
        public bool Undefined { get; set; }

        [DefaultValue(false)]
        public bool TruePositive { get; set; }

        [DefaultValue(false)]
        public bool FalsePositive { get; set; }

        [DefaultValue(false)]
        public bool Suspicious { get; set; }


    }
    public class SentinalOneUpdateAnalystVerdictDTO 
    {
     
        public List<string>? AccountIds { get; set; }

        public List<string>? SiteIds { get; set; }

        public List<string>? ThreatIds { get; set; }

        public string? SentinalOne_Analysis_Verdict { get; set; }
    }
    public class SentinalOneUpdateThreatDetailsequest : SentinalOneActionRequest
    {
        [DefaultValue(false)]
        public bool ThreatStatus_Unresolved { get; set; }
        [DefaultValue(false)]
        public bool ThreatStatus_Inprogress { get; set; }
        [DefaultValue(false)]
        public bool ThreatStatus_Resolved { get; set; }

        //public string? ThreatAnalystVerdict { get; set; }

        [DefaultValue(false)]
        public bool AnalystVerdict_TruePositive { get; set; }
        [DefaultValue(false)]
        public bool AnalystVerdict_FalsePositive { get; set; }
        [DefaultValue(false)]
        public bool AnalystVerdict_Suspecious { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }
    }

    public class SentinalOneUpdateThreatDetailsDTO 
    {

        public List<string>? AccountIds { get; set; }

        public List<string>? SiteIds { get; set; }

        public List<string>? ThreatIds { get; set; }

        public string? ThreatAnalystVerdict { get; set; }

        public string? ThreatStatus { get; set; }
    }

    public class AddToNetworkRequest
    {
        public int OrgID { get; set; }
        public List<double>? AlertIds { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }

    }

    public class AddToNetworkDTO
    {
        public List<string>? AccountIds { get; set; }

        public List<string>? SiteIds { get; set; }

        public List<string>? AgentIds { get; set; }
        
    }

    public class DisconnectFromNetworkRequest
    {
        public int OrgId { get; set; }
        public List<double>? AlertIds { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }


    }

    public class AddToExclusionList
    {
        public List<string>? AccountIds { get; set; }

        public List<string>? SiteIds { get; set; }

        public List<string>? AgentIds { get; set; }

    }


    public class AddToBlocklistForThreatsRequest
    {
        public int OrgID { get; set; }
        public List<double>? AlertIds { get; set; }
        [DefaultValue("Group")]
        public string? TargetScope { get; set; }

        // public string? ExternalTicketId { get; set; }
        [DefaultValue("")]
        public string? Description { get; set; }

        [DefaultValue("")]
        public string? Note { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }
    }

    public class AddToBlocklist
    {
        public int OrgId { get; set; }

        public string? Source { get; set; }

        [DefaultValue("")]
        public string? Description { get; set; }

        [DefaultValue("")]
        public string? OSType { get; set; }

        [DefaultValue("")]
        public string? Value { get; set; }

        [DefaultValue("")]
        public string? Type { get; set; }

    }
    public class AddToBlocklistRequest: AddToBlocklist
    {
       

       public DateTime? CreatedDate { get; set; }

        public int CreatedUserId { get; set; }
        
        public string? GroupId { get; set; }
        
        public string? SiteId { get; set; }
        
        public string? AccountId { get; set; }


    }
    public class UpdateAddToBlocklistRequest : AddToBlocklist
    {
        public int OrgId { get; set; }
        public string? Id { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int ModifiedUserId { get; set; }
    }

    public class DeleteAddToBlocklistRequest 
    {
        public int OrgId { get; set; }
        public List<string>? Ids { get; set; }

        public string? Type { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedUserId { get; set; }

    }

    public class AddToBlocklistDTO
    {
        public List<string>? AccountIds { get; set; }

        public List<string>? SiteIds { get; set; }

        public List<string>? ThreatIds { get; set; }

        public string? TargetScope { get; set; }

        public string? ExternalTicketId { get; set; }

        public string? Description { get; set; }

        public string? Note { get; set; }


    }
    public class AddToExclusionBase
    {
        public string? Note { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
        public string? TargetScope { get; set; }
        public string? PathExclusionType { get; set; }

        public List<string>? Actions { get; set; }

        public string? ExternelTicketId { get; set; }

        public string? Mode { get; set; }
        public string? OSType { get; set; }
        public string? Source { get; set; }
        public string? Type { get; set; }

    }
    public class AddToExclusionRequest : AddToExclusionBase
    {
        public DateTime? CreatedDate { get; set; }

        public int CreatedUserId { get; set; }

        public int OrgID { get; set; }
        public string? GroupId { get; set; }

        public string? SiteId { get; set; }

        public string? AccountId { get; set; }
        public List<double>? AlertIds { get; set; }
    }
    public class UpdateAddToExclusionRequest : AddToExclusionBase
    {
        public string Id { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ModifiedUserId { get; set; }
        public int OrgId { get; set; }
       
    }

    public class DeleteAddToExclusionRequest
    {
        public DateTime? DeletedDate { get; set; }
        public int DeletedUserId { get; set; }
        public string? Type { get; set; }
        public List<string>? Ids { get; set; }

        public int OrgId { get; set; }
    }


    public class AddToExclusionlistDTO : AddToExclusionBase
    {
        public List<string>? AccountIds { get; set; }

        public List<string>? SiteIds { get; set; }

        public List<string>? ThreatIds { get; set; }

        public string? GroupId { get; set; }

        public string? SiteId { get; set; }

        public string? AccountId { get; set; }

    }

}
