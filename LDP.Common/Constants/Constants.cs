using LDP_APIs.DAL.Entities;
using Org.BouncyCastle.Asn1.Crmf;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LDP.Common
{
    public class Constants
    {
        public const string AlertNewStatus = "New";
        public const string AlertStatusType = "alert_status";
        public const string Alert_Resolved_Status = "Resolved";
        public const string Alert_Closed_Status = "Closed";
        public const string Alert_Irrelavant_Status = "Irrelavant/Ignore";
        public const string Alert_Escalate_Status = "Escalate";

        public const string AlertSevirityType = "alert_Sevirity";
        public const string AlertSevirityFieldSLAHours = "sla_hours";

        public const string AlertAlertRuleType = "Alert_Rules";

        // Notes Action constants 

        public const string AlertActionType = "Alert_Action";
        public const int AlertEscalateActionId = 1;
        public const String AlertEscalateActionName = "Escalate";
        public const string AlertMitigateActionType = "Alert_Mitigate_Action";

        public const int AlertIgnoreIrrelavantActionId = 2;
        public const String AlertIgnoreIrrelavantActionName = "Ignore OR Irrelavant";

        public const int AlertAddNoteActionId = 3;
        public const String AlertAddNoteActionName = "Add Note";
        public const String AlertUpdateactionName = "Update Alert";

        public const string IncidentNewStatus = "New";
        public const string IncidentClosedStatus = "Closed";

        public const string IncidentStatusType = "incident_status";
        public static readonly string[] IncidentClosedSstatusList = { IncidentClosedStatus };

        public const string FALSE_POSITIVE_Value = "FALSE POSITIVE";
        public const string alert_positive_analysisType = "alert_positive_analysis";

        public const string User_Action_Alert_Type = "Alert";
        public const string User_Action_Incident_Type = "Incident";

        public static string defaultPassword = "123456";

        public static string Source_QRadar = "QRadar";
        public static string Source_SentinalOne = "SentinalOne";

        public static string SentinalOne_true_positive = "true_positive";

        public static string SentinalOne_Resolved_Status = "Resolved";

        public static string User_Background_User = "Background job";

        public static string Tool_Type_TicketManagement = "TicketManagement";

        public static string Tool_Type_SIEM = "SIEM";

        public static string Tool_Type_EDR = "EDR";


        public static string Tool_Type = "Tool_Types";

        // Threat functions

        public static string Tool_Action_Get_Threats = "Get Threats";

        public static string Tool_Action_threat = "Mitigate Threat Actions";

        public static string Tool_Action_Get_Threat_Timeline = "Get Threat Timeline";

        public static string Tool_Action_Get_Threat_Notes = "Get Threat Notes";

        public static string Tool_Action_Add_Threat_Notes = "Add Threat Note";

        public static string Tool_Action_Update_Analyst_Verdict = "Update Analyst Verdict";

        public static string Tool_Action_Update_Incident_Details = "Update Incident Details";

        public static string Tool_Action_AddToNetwork = "Add to Network";

        public static string Tool_Action_DisconnectFromNetwork = "Disconnect from Network";

        public static string Tool_Action_AddToBlockedList = "Add to Blocked List";

        public static string Tool_Action_AddToBlockedList_Update = "Update Blocked List Item";

        public static string Tool_Action_AddToBlockedList_Delete = "Delete Blocked List Item";

        public static string Tool_Action_AddToBlockedList_ForThreats = "Add to Blocked List For Threats";

        public static string Tool_Action_AddToExclusionList = "Add to Exclusion List";

        public static string Tool_Action_AddToExclusionList_Update = "Update Exclusion List Item";

        public static string Tool_Action_AddToExclusionList_Delete = "Delete Exclusion List Item";

        public static string Tool_Action_AddToExclusionList_ForThreats = "Add to Exclusion List For Threats";

        public static string Tool_Action_Get_Application_Data = "Get Application data";

        public static string Tool_Action_Get_Application_Inventory = "Applications Inventory";

        public static string Tool_Action_Get_Application_EndPoints = "Applications End Points";
        public static string Tool_Action_Get_Application_CVS = "Applications CVS";

        public static string Tool_Action_Get_Application_EndPoint_Details = "Application End point Details";
        public static string Tool_Action_Get_EndPoint_Applications = "End point applications";

        public static string  Tool_Action_Get_Application_Risks_Data = "Application and Risks data";

        public static string Tool_Action_Get_Risks_Application_Endpoints_Data = "Risk Application end points";

        public static string Tool_Action_Get_Application_Management_Settings = "Get Application Management Settings";

        public static string Tool_Action_Get_EndPoint_Updates = "Get Endpoint Updates";

        public static string Tool_Action_Get_Exclusions = "Get Exclusions";

        public static string Tool_Action_Get_Blocked_List = "Get Blockded List";
        public static string Tool_Action_Get_Account_Details = "Get Account Details";
        
        public static string Tool_Action_Get_Tenant_Policy_Details = "Get Tenant Policy details";
        public static string Tool_Action_Get_Account_Policy_Details = "Get Account Policy details";
        public static string Tool_Action_Get_Site_Policy_Details = "Get Site Policy details";
        public static string Tool_Action_Get_Group_Policy_Details = "Get Group Policy details";

        public static string Tool_Action_Get_Sites = "Get Sites";
        public static string Tool_Action_Get_Groups = "Get Groups";


        public static readonly int[] GetAlertSLAMeasurementSummeryDays = { 7, 30, 365 };

        


        // Configuration data constants

        public const string Configdata_Type = "SMS";

        public const string Configdata_Provider = "Provider";

        public const string Configdata_SendUrl = "SendUrl";

        public const string Configdata_SID = "SID";

        public const string Configdata_AuthKey = "AuthKey";

        public const string Configdata_FromPhneNumber = "FromPhneNumber";

        public const string DataType_String = "String";

        public const string Configdata_SIEMToolsDataPullingbackgroundjob = "SIEMToolsDataPullingbackgroundjob";
        public const string Configdata_EDRToolsDataPullingbackgroundjob = "EDRToolsDataPullingbackgroundjob";

        public const string Configdata_Enabled = "Enabled";

        public const string Configdata_AlertPlayBookProcessActionbackgroundjob = "SIEMToolsDataPullingbackgroundjob";

        public const string Configdata_AnalyzeAlertsForAutomationbackgroundjob = "SIEMToolsDataPullingbackgroundjob";

        public const string Configdata_DelayDurationInMilliSeconds = "DelayDurationInMilliSeconds";

        public const string Configdata_DelayDurationInMinutesForEDRJob = "DelayDurationInMintesForEDRJob";

        public const string Incident_SortOption_Recent_Created = "Recent Create";

        public const string Incident_SortOption_Recent_Updated = "Recent Update";

        // Channel Types
        public const string Channel_Type = "Channel_Type";
        public const string Channel_Type_UnderReview = "UnderReview";
        public const string Channel_Type_UnderConstruction = "UnderConstruction";
        public const string Channel_Type_ReadyToDeployment = "ReadyToDeployment";
        public const string Channel_Type_Report = "Report";
        public const string Channel_Type_QuestionAndAnswer = "QuestionAndAnswer";
        public const string Channel_Type_Document = "Document";

        //
        public const string LDC_Upload_FolderPath = "LDCDocuments";
        public const string LDC_Upload_ChatFolderName = "Chat_Documents";

        //
        public const string Azure_Type = "AzURE";
        public const string Azure_TenantId = "tenantId";
        public const string Azure_ClientId = "clientId";
        public const string Azure_ClientSecret = "clientSecret";
        //
        public const string chat_message_alert_Subject = "Alert";
        public const string chat_message_Incident_Subject = "Incident";

        public const string Api_Url_group_Teams_Graph = "Teams_Graph";

        public const string Generate_Token_Api_Url = "generate_token";

        public const string Create_Teams_Channel_Api_Url = "create_teams_channel";

        //
        public const string Chat_Message_Type_Chat_Message = "Chat_Message";
        public const string Chat_Message_Type_Attachment = "Attachment";

        //Logger constants
        public const string Logger_severity_Information = "Information";
        public const string Logger_severity_Error = "Error";
        public const string Logger_severity_Warning = "Warning";
        public const string Logger_severity_Debug = "Debug";

        //Logger Source
        public const string Logger_source_SentinalOneJob = "SentinalOne Integration Job";

        // SentinalOne Actions
        public const string SentinalOne_Action_Kill = "kill";
        public const string SentinalOne_Action_Quarantine = "quarantine";
        public const string SentinalOne_Action_Remediate = "remediate";
        public const string SentinalOne_Action_Rollback_Remediation = "rollback-remediation";
        public const string SentinalOne_Action_UnQuarantine = "un-quarantine";
        public const string SentinalOne_Action_Network_Quarantine = "network-quarantine";

        //SentinalOne Analyst Verdict

        public const string SentinalOne_Analysis_Verdict_UnDefined = "undefined";
        public const string SentinalOne_Analysis_Verdict_TruePositive = "true_positive";
        public const string SentinalOne_Analysis_Verdict_FalsePositive = "false_positive";
        public const string SentinalOne_Analysis_Verdict_Suspicious = "suspicious";

        //SentinalOne Status
        public const string SentinalOne_Status_Unresolved = "unresolved";
        public const string SentinalOne_Status_Inprogress = "in_progress";
        public const string SentinalOne_Status_Resolved = "resolved";
        // LDC Status and Sentinal status mapping

        public static Dictionary<string, string> Sentinal_Status_Mapper = new Dictionary<string, string>
        {
            {"New",SentinalOne_Status_Unresolved } ,
            {"Owner assigned",SentinalOne_Status_Inprogress } ,
            {"In Progress",SentinalOne_Status_Inprogress },
            {"Resolved",SentinalOne_Status_Resolved },
            {"Closed",SentinalOne_Status_Resolved } 
        };

        public const string SentinalOne_AcccountStrucure_AccountId = "AccountId";
        public const string SentinalOne_AcccountStrucure_SiteId = "SiteId";
        public const string SentinalOne_AcccountStrucure_GroupId = "GroupId";

        // Add to exclusion types

        public const string SentinalOne_AddToexclustion_Type_Hash = "hash";

        // LDC Analyst verdict constants 
        public const string LDC_Type_Analyst_Verdict = "analyst_verdict";
        public const string LDC_Analyst_Verdict_Undefined = "Undefined";
        public const string LDC_Analyst_Verdict_Suspicious = "Suspicious";
        public const string LDC_Analyst_Verdict_TruePositived = "True Positive";
        public const string LDC_Analyst_Verdict_FalsePositive = "False Positive";

        public enum SentinalOne_Analysis_Verdict
        {
          undefined,
          true_positive,
          false_positive,
          suspicious
        };

        public enum SentinalOne_OS_Types
        {
          windows_legacy,
          macos,
          windows,
          linux
        }
        public enum SentinalOneMitigationTypes
        {
            kill,
            quarantine,
            remediate,
            rollback_remediation,
            un_quarantine,
            network_quarantine
        }

        public enum Task_Types
        {
            Password_Reset
        }

        public enum Task_Status
        {
            New ,
            InProgress ,
            Closed
        }

        public enum Email_types
        {
            Password_Reset ,
            New_User_Account_Creation ,
            New_TaskCreation
        }
        public static string Acvities_Template_username = "username";


        public static string Activity_Template_User_logged_in = "User Logged In";

//        public static string Activity_Template_User_logIn_Failed = "User login Failed";

        public static string Activity_Template_User_logged_Out = "User Logged Out";
        public static string Activity_Template_User_Add = "Add new user";
        public static string Activity_Template_User_Update = "User update";
        public static string Activity_Template_User_Delete = "Delete user";
        public static string Activity_Template_User_Password_Change = "User Password Change";
        public static string Activity_Template_User_Password_Reset = "User Password Reset";
        public static string Activity_Template_Role_Add = "Add new role";
        public static string Activity_Template_Role_Update = "role update";
        public static string Activity_Template_Role_Delete = "Delete role";

        public static string Activity_Template_Organization_Add = "Add new organization";
        public static string Activity_Template_Organization_Update = "Organization update";
        public static string Activity_Template_Organization_Delete = "Delete Organization";

        public static string Activity_Template_Tool_Add = "Add new tool";
        public static string Activity_Template_Tool_Update = "Tool update";
        public static string Activity_Template_Tool_Delete = "Delete Tool";

        public static string Activity_Template_OrgTool_Add = "Add new organization tool";
        public static string Activity_Template_OrgTool_Update = "Organiaztion Tool update";
        public static string Activity_Template_OrgTool_Delete = "Delete Organization Tooll";

        public static string Activity_Template_ToolType_Action_Add = "Add new tool type action";
        public static string Activity_Template_ToolType_Action_Update = "Organiaztion Tool type action";
        public static string Activity_Template_ToolType_Action_Delete = "Delete  Tool type action";

        public static string Activity_Template_Tool_Action_Add = "Add new tool  action";
        public static string Activity_Template_Tool_Action_Update = "Updae Tool  action";
        public static string Activity_Template_Tool_Action_Delete = "Delete Tool action";

        public static string Activity_Template_Alert_Create = "Create Alert";
        public static string Activity_Template_Alert_Update = "Update Alert";
        public static string Activity_Template_Alert_Assign_Owner = "Alert Assign Owner";
        public static string Activity_Template_Alert_Assign_Status = "Alert Assign Status";
        public static string Activity_Template_Alert_Assign_Priority = "Alert Assign Priority";
        public const string Activity_Template_Alert_Assign_Severity = "Alert Assign Severity";
        public const string Activity_Template_Alert_Assign_Analyst_Verdict = "Assign Analyst Verdict";
        public const string Activity_Template_Alert_Mitigate_Action = "Mitigate Action";
        public const string Activity_Template_Alert_Kill_Action = "Kill Action";
        public const string Activity_Template_Alert_Quarantine_Action = "Quarantine Action";
        public const string Activity_Template_Alert_Remediate_Action = "Remediate Action";
        public const string Activity_Template_Alert_Rollback_Action = "Rollback Action";
        public const string Activity_Template_Alert_UnQuarantine_Action = "Unquarantine Action";
        public const string Activity_Template_Alert_Alert_Note = "Alert Note";
        public const string Activity_Template_Alert_Add_to_Network = "Add to Network";
        public const string Activity_Template_Alert_Disconnect_from_Network = "Disconnect from Network";
        public const string Activity_Template_Alert_Escalate_Action = "Escalate Action";
        public const string Activity_Template_Alert_Ignore_Action = "Ignore Action";
        public const string Activity_Template_Alert_Score_Assigned = "Alert Score Assigned";
        public const string Activity_Template_Alert_Tag_Assigned = "Alert Tag Assigned";
        public const string Activity_Template_Alert_Add_to_Blocklist = "Alert Add to Blocklist";
        public const string Activity_Template_Alert_Add_to_Exclusion = "Alert Add to Exclusion";

        public const string Activity_Template_Create_Incident = "Create Incident";
        public const string Activity_Template_Update_Incident = "Update Incident";
        public const string Activity_Template_Incident_Assign_Owner = "Incident Assign Owner";
        public const string Activity_Template_Incident_Assign_Status = "Incident Assign Status";
        public const string Activity_Template_Incident_Assign_Priority = "Incident Assign Priority";
        public const string Activity_Template_Incident_Assign_Severity = "Incident Assign Severity";
        public const string Activity_Template_Incident_Alert_Note = "Incident Alert Note";

        public const string Activity_Template_Incident_Assign_Type = "Incident Assign Type";
        public const string Activity_Template_Incident_Assign_Score = "Incident Assign Score";




        public const string Activity_Create_Incident = "Create Incident";
        public const string Activity_Update_Incident = "Update Incident";
        public const string Activity_Incident_Assign_Owner = "Incident Assign Owner";
        public const string Activity_Incident_Assign_Status = "Incident Assign Status";
        public const string Activity_Incident_Assign_Priority = "Incident Assign Priority";
        public const string Activity_Incident_Assign_Severity = "Incident Assign Severity";
        public const string Activity_Incident_Alert_Note = "Incident Alert Note";





        public static string AddToBlockList_Hash = "black_hash";
        public static string AddToBlockList_Souce = "user";
        public static string AddToExclusion_Hash = "hash";


    }
}
