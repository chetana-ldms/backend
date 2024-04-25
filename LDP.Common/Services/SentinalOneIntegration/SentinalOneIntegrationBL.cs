using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests.Common;
using LDP.Common.Services.SentinalOneIntegration.Activities;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;
using LDP_APIs.DAL.Interfaces;
using System.Text.Json;

namespace LDP.Common.Services.SentinalOneIntegration
{
    public class SentinalOneIntegrationBL : ISentinalOneIntegrationBL
    {
        ISentinalOneIntegrationService _service;
        IAlertsRepository _repo;
        public readonly IMapper _mapper;
        ILDPlattformBL _plattformBL;
        IAlertHistoryBL _alertHistoryBL;
        ILdpMasterDataBL _masterDataBL;
        IAlertExtnFieldBL _alertExtnFieldBL;
        ICommonBL _commonBl;

        public SentinalOneIntegrationBL(ILDPlattformBL plattformBL, ISentinalOneIntegrationService service, IMapper mapper, IAlertsRepository repo, IAlertHistoryBL alertHistoryBL, ILdpMasterDataBL masterDataBL, IAlertExtnFieldBL alertExtnFieldBL, ICommonBL commonBl)
        {
            _plattformBL = plattformBL;
            _service = service;
            _mapper = mapper;
            _repo = repo;
            _alertHistoryBL = alertHistoryBL;
            _masterDataBL = masterDataBL;
            _alertExtnFieldBL = alertExtnFieldBL;
            _commonBl = commonBl;
        }

        

        public GetSentinalThreatAlertsResponse GetThreats(GetSentinalThreatsRequest request)
        {
            GetSentinalThreatAlertsResponse methodResponse = new GetSentinalThreatAlertsResponse();
    
            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgID, tooltypeID);
            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_Get_Threats);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<SentinalThreatDetails> sentinalData = new List<SentinalThreatDetails>();
            //
            var res = _service.GetThreats(conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                return methodResponse;
            }
            //
            sentinalData.AddRange(res.Alerts.data);
            //
            while (res.Alerts != null && res.Alerts.Pagination != null && res.Alerts.Pagination.nextcursor != null)
            {
                res = _service.GetThreats(conndtl,res.Alerts.Pagination.nextcursor ).Result;
                if (res.Alerts != null && res.Alerts.data.Count > 0)
                {
                    sentinalData.AddRange(res.Alerts.data);
                }
            }
            //
            
            var _sentinalMappedAlerts = _mapper.Map<List<SentinalThreatDetails>, List<Alerts>>(sentinalData);
            //
            _sentinalMappedAlerts.OrderBy(a => a.detected_time);

            _sentinalMappedAlerts.ForEach(
                a => { a.org_id = request.OrgID;
                    a.tool_id = conndtl.ToolID;
                     }              

                );
            //
            MapAlertStatus(request, conndtl, _sentinalMappedAlerts);
            //
            var OrderedAlerts = _sentinalMappedAlerts.OrderBy(a => a.detected_time);
            //
            var a1 = _repo.Addalerts(_sentinalMappedAlerts);
          
            // Adding Alerrt Rule data into AlertExtnFields Table 
            AddAlertRulesData(res, _sentinalMappedAlerts); ;
            //
            //Adding History data 
            AddHistoryData(_sentinalMappedAlerts);

            var _sentinalMappedAlertsModel = _mapper.Map<List<Alerts>, List<AlertModel>>(_sentinalMappedAlerts);

            var apiThreats = JsonSerializer.Deserialize<SentinalThreatDetailsTemp>(_sentinalMappedAlertsModel.Last().AlertData,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive =  true
                         });;
            conndtl.ToolActions[0].LastReadAlertDate = apiThreats.threatInfo.createdAt;

             _plattformBL.UpdateLastReadAlertDate(conndtl);

            var accStrucRes = AddThreatAccountStructure(_sentinalMappedAlertsModel);

            methodResponse.IsSuccess = true;
            methodResponse.Message = "List of alerts from sentinalOne tool";
            methodResponse.Alerts = _sentinalMappedAlertsModel;
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            methodResponse.OrgId = request.OrgID;
            methodResponse.ToolId = conndtl.ToolID;
            return methodResponse;
        }


      private string AddThreatAccountStructure(List<AlertModel> alerts)
      {
            List<AlertsAccountStructure> structureList = new List<AlertsAccountStructure>();
            AlertsAccountStructure strucutre = null;
            foreach (AlertModel alert in alerts)
            {
                strucutre = new AlertsAccountStructure();
                strucutre.alert_id = alert.AlertID;
                strucutre.org_id = alert.OrgID;
                var apiThreatdtl = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.AlertData,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                strucutre.account_id = apiThreatdtl.AgentDetectionInfo.AccountId;
                strucutre.site_id = apiThreatdtl.AgentDetectionInfo.SiteId;
                strucutre.grouup_id = apiThreatdtl.AgentDetectionInfo.GroupId;
                strucutre.threat_id = apiThreatdtl.threatInfo.threatId;
                strucutre.created_date = DateTime.UtcNow;
                strucutre.created_user = Constants.User_Background_User;
                structureList.Add(strucutre);
            }
           return _repo.AddAlertAccountStructureRange(structureList).Result;

      }

        private void MapAlertStatus(GetSentinalThreatsRequest request, OrganizationToolModel conndtl, List<Alerts> _mappedResponse)
        {
            var clientAlertStatusMappingdata = _masterDataBL.GetOrgMasterData(Constants.AlertStatusType, request.OrgID);
            
            var alertStatusdata = _plattformBL
                .GetMasterDataByDatType(new LDP_APIs.BL.APIRequests.LDPMasterDataRequest()
                { MaserDataType = Constants.AlertStatusType }).MasterData;
            //

            int _newStatusId = _plattformBL.GetMasterDataByDataValue(Constants.AlertStatusType, Constants.AlertNewStatus);

            _mappedResponse.ForEach(obj =>
            {
                var alertData = JsonSerializer.Deserialize<SentinalThreatDetails>(obj.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });

                obj.tool_id = conndtl.ToolID;
                obj.org_id = request.OrgID;
                 obj.status_ID = _newStatusId;
                var threatStatusobj = clientAlertStatusMappingdata.Data.Where(dt => dt.OrgDataName.ToLower() == alertData.threatInfo.IncidentStatus.ToLower()).FirstOrDefault();
                if (threatStatusobj != null)
                {
                    obj.status_ID = threatStatusobj.DataId;
                    obj.status = alertStatusdata.Where(alertstatus => alertstatus.DataID == obj.status_ID).FirstOrDefault().DataValue;

                }

            });
        }

        void AddAlertRulesData(SentinalOneGetThreatResponse res, List<Alerts> alerts)
        {
            List<AlertExtnFieldModel> alertExtnFields = new List<AlertExtnFieldModel>();
            AlertExtnFieldModel alertExtnField = null;
            //int alertId = 0;
            foreach (var alert in alerts)
            {
                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });

                    alertExtnField = new AlertExtnFieldModel();
                    alertExtnField.AlertId = alert.alert_id;
                    alertExtnField.DataType = Constants.AlertAlertRuleType;
                    alertExtnField.DataFieldValue = apiThreat.threatInfo.Classification;
                    alertExtnField.DataFieldValueType = Constants.DataType_String;
                    alertExtnField.DataFieldName = "Classification";
                    alertExtnField.CreatedDate = DateTime.UtcNow;
                    alertExtnField.CreatedUser = Constants.User_Background_User;

                    alertExtnField.Active = 1;
                    alertExtnFields.Add(alertExtnField);
            }

            _alertExtnFieldBL.AddRangeAlertExtnFields(alertExtnFields);
        }
        private void AddHistoryData(List<Alerts> _mappedResponse)
        {
            //AlertHistoryModel alertHistoryrequst = null;
            //List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
            //List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            foreach (var alert in _mappedResponse)
            {

                var apiThreats = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                             new JsonSerializerOptions()
                             {
                                 PropertyNameCaseInsensitive = true
                             }); ;


                var getTimelineResponse = this.GetThreatTimeline(
                       new GetThreatTimelineRequest()
                       {
                           AlertId = alert.alert_id,
                           OrgID = alert.org_id,
                           ThreatId = apiThreats.threatInfo.threatId,

                       });
                if (getTimelineResponse.IsSuccess && getTimelineResponse.ThreatTimeLines.Count > 0)
                {
                    AddActivityRequest _activityReqiest = null;

                    List<AddActivityRequest> _activityLogList = new List<AddActivityRequest>();
                    // alertHistoryList.AddRange(getTimelineResponse.alertTimelineHistory);
                    foreach (var timeline in getTimelineResponse.ThreatTimeLines)
                    {
                        _activityReqiest = new AddActivityRequest();
                        _activityReqiest.AlertId = alert.alert_id;
                        _activityReqiest.ActivityDate = alert.Created_Date;
                        _activityReqiest.CreatedDate = alert.Created_Date;
                        _activityReqiest.CreatedUserId = 0;
                        _activityReqiest.ToolId = alert.tool_id;
                        _activityReqiest.OrgId = alert.org_id;
                        _activityReqiest.ActivityExistToolAndLDC = 1;
                        _activityReqiest.PrimaryDescription = timeline.primaryDescription;
                        _activityReqiest.SecondaryDescription = timeline.secondaryDescription;
                        _activityReqiest.Source = "SentinelOne";
                        _activityLogList.Add(_activityReqiest);
                    }
                    if (_activityLogList.Count>0)
                    {
                        var acRes = _commonBl.AddRangeActivity(_activityLogList);
                    }
                }
                //alertHistoryrequst = new AlertHistoryModel()
                //{

                //    AlertId = alert.alert_id,
                //    HistoryDate = alert.Created_Date,
                //    CreatedUser = alert.Created_User,
                //    HistoryDescription = $"Alert # {alert.alert_id} created in LDC ",
                //    OrgId = alert.org_id,
                //    CreatedUserId = 0
                // };
                //alertHistoryList.Add(alertHistoryrequst);
                List<AddActivityRequest>  activityLogList = new List<AddActivityRequest>();
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username,Constants.User_Background_User);
                _templateData.Add("AlerId", alert.alert_id.ToString());

                var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Create, _templateData, 0, alert.Created_Date, alert.org_id, true, alert.alert_id, 0);

                activityLogList.Add(activityobj);

            }
            //_alertHistoryBL.AddRangealertHistory(alertHistoryList);

            
        }
        private AddActivityRequest BuildActivityLogObj(string templateConstant, Dictionary<string, string> templateData, int createdUserId, DateTime? createdDate, int orgId, bool isSuccess, double alertId, int activityExistinToolAndLDC)
        {
            return new AddActivityRequest()
            {
                CreatedUserId = createdUserId,
                OrgId = orgId,
                CreatedDate = createdDate,
                ActivityDate = createdDate,
                ActivityType = templateConstant,
                IsSuccess = isSuccess,
                TemplateData = templateData,
                AlertId = alertId,
                ActivityExistToolAndLDC = activityExistinToolAndLDC
            };
        }

        public MitigateActionResponse ThreatMitigateAction(MitigateActionRequest request)
        {
            MitigateActionResponse methodResponse = new MitigateActionResponse();
            // Actions
            // 1. Kill action
            ThreatActionResponse actionResponse = null;
            if (request.Kill)
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Kill);
                if (actionResponse.IsSuccess == false)
                {
                    methodResponse.IsSuccess = false;
                    methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    methodResponse.Message = actionResponse.Message;
                    return methodResponse;
 
                }

            }
            if (request.Quarantine)
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Quarantine);
                if (actionResponse.IsSuccess == false)
                {
                    methodResponse.IsSuccess = false;
                    methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    methodResponse.Message = actionResponse.Message;
                    return methodResponse;

                }
            }
            if (request.Remediate)
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Remediate);
                if (actionResponse.IsSuccess == false)
                {
                    methodResponse.IsSuccess = false;
                    methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    methodResponse.Message = actionResponse.Message;
                    return methodResponse;

                }
            }
            if (request.Rollback)           
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Rollback_Remediation);
                if (actionResponse.IsSuccess == false)
                {
                    methodResponse.IsSuccess = false;
                    methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    methodResponse.Message = actionResponse.Message;
                    return methodResponse;

                }
            }

            // Analyst Verdict 

            UpdateThreatAnalysisVerdictResponse updateAnslystVerdictResponse = null;

            if (request.AnalystVerdict_TruePositive) 
            {
                updateAnslystVerdictResponse = UpdateThreatAnalysisVerdict(request.OrgId, request.AlertIds, Constants.SentinalOne_Analysis_Verdict_TruePositive);
            }
            else if (request.AnalystVerdict_FalsePositive)
            {
                updateAnslystVerdictResponse = UpdateThreatAnalysisVerdict(request.OrgId, request.AlertIds, Constants.SentinalOne_Analysis_Verdict_FalsePositive);
            }
            else if (request.AnalystVerdict_Suspecious)
            {
                updateAnslystVerdictResponse = UpdateThreatAnalysisVerdict(request.OrgId, request.AlertIds, Constants.SentinalOne_Analysis_Verdict_Suspicious);

            }
            //

            if (updateAnslystVerdictResponse != null && !updateAnslystVerdictResponse.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                methodResponse.Message = actionResponse.Message;
                return methodResponse;
            }
            if (request.AddToBlockedList) 
            {
                AddToBlocklistForThreatsRequest addtoBlockListRequest = new AddToBlocklistForThreatsRequest()
                {
                     AlertIds = request.AlertIds,
                     OrgID = request.OrgId,
                     TargetScope = request.Scope,
                     Note = request.Notes
                };
                var addtoBlockListResponse = AddToblockListForThreats(addtoBlockListRequest);
            }
            // Status update 

            if (request.ResolvedStatus)
            {
               var res = UpdateThreatDetails(request.OrgId, request.AlertIds, Constants.SentinalOne_Resolved_Status, null);
            }

            if (!string.IsNullOrEmpty(request.Notes))
            {
                AddThreatNoteRequest noteRequest = new AddThreatNoteRequest()
                {
                    OrgID = request.OrgId,
                    AlertIds = request.AlertIds,
                    Notes = request.Notes
                };

                var noteRes = AddThreatNotes(noteRequest);
            }
            // 
            methodResponse.IsSuccess = true;
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            methodResponse.Message = "Threat mitigate action completed ";
            return methodResponse;

        }


        public ThreatActionResponse ThreatAction(int orgId , List<double> alertIds , string actionType)
        {
            ThreatActionResponse methodResponse = new ThreatActionResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(orgId, tooltypeID);
            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_threat);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<SentinalThreatDetails> sentinalData = new List<SentinalThreatDetails>();

            // Get Alerts

            var alerts = _repo.GetalertsByAlertIds(alertIds);

            HashSet<string> accountIds = new HashSet<string>();
            HashSet<string> siteIds = new HashSet<string>();
            HashSet<string> threatIds = new HashSet<string>();

            foreach (var alert in alerts)
            {

                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                threatIds.Add(apiThreat.threatInfo.threatId);
                siteIds.Add(apiThreat.AgentDetectionInfo.SiteId);
                accountIds.Add(apiThreat.AgentDetectionInfo.AccountId);
            }
            //
            MitigateActionDTO dto = new MitigateActionDTO();

            dto.AccountIds = accountIds.ToList();
            dto.SiteIds = siteIds.ToList();
            dto.ThreatIds = threatIds.ToList();
            dto.ActionType = actionType;

            var res = _service.ThreatMitigateAction(dto, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }
            //Adding History data 

            methodResponse.IsSuccess = true;
            methodResponse.Message = "Threat action successfull ";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;

        }
        public GetAlertTimelineResponse GetThreatTimeline(GetThreatTimelineRequest request)
        {
            GetAlertTimelineResponse methodResponse = new GetAlertTimelineResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgID, tooltypeID);
            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_Get_Threat_Timeline);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<ThreatTimeLine> sentinalData = new List<ThreatTimeLine>();

            GetThreatTimelineDTO dto = new GetThreatTimelineDTO();
            dto.ThreatId = request.ThreatId;
            //
            var res = _service.GetThreatTimeline(dto , conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                return methodResponse;
            }
            //
            sentinalData.AddRange(res.ThreatTimeline.data);
            //
            while (res.ThreatTimeline != null && res.ThreatTimeline.pagination != null && res.ThreatTimeline.pagination.nextCursor != null)
            {
                dto.NextCursor = res.ThreatTimeline.pagination.nextCursor;

                res = _service.GetThreatTimeline(dto, conndtl).Result;
                if (res.ThreatTimeline != null && res.ThreatTimeline.data.Count > 0)
                {
                    sentinalData.AddRange(res.ThreatTimeline.data);
                }
            }
            //
            //var _threatTimeline = _mapper.Map<List<ThreatTimeLine>, List<AlertHistoryModel>>(sentinalData);
            ////
            
            //_threatTimeline.ForEach(
            //    a =>
            //    {
            //        a.OrgId = request.OrgID;
            //        a.AlertId = request.AlertId;
            //    }

            //  );

            methodResponse.IsSuccess = true;
            methodResponse.Message = "List of alert time line ";
            methodResponse.ThreatTimeLines = sentinalData;
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }
        public UpdateThreatAnalysisVerdictResponse UpdateThreatAnalysisVerdict(SentinalOneUpdateAnalystVerdictRequest request)
        {
            UpdateThreatAnalysisVerdictResponse methodResponse = null ;
            string _analystVerdict = string.Empty;

            if (request.Undefined) _analystVerdict = Constants.SentinalOne_Analysis_Verdict_UnDefined;
            if (request.TruePositive) _analystVerdict = Constants.SentinalOne_Analysis_Verdict_TruePositive;
            if (request.FalsePositive) _analystVerdict = Constants.SentinalOne_Analysis_Verdict_FalsePositive;
            if (request.Suspicious) _analystVerdict = Constants.SentinalOne_Analysis_Verdict_Suspicious;

            methodResponse = UpdateThreatAnalysisVerdict(request.OrgId, request.AlertIds, _analystVerdict);

            return methodResponse;
        }
        public UpdateThreatAnalysisVerdictResponse UpdateThreatAnalysisVerdict(int orgId , List<double> alertIds , string analystVerdict )
        {
            UpdateThreatAnalysisVerdictResponse methodResponse = new UpdateThreatAnalysisVerdictResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(orgId, tooltypeID);
            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_Update_Analyst_Verdict);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<SentinalThreatDetails> sentinalData = new List<SentinalThreatDetails>();

            // Get Alerts

            var alerts = _repo.GetalertsByAlertIds(alertIds);

            HashSet<string> accountIds = new HashSet<string>();
            HashSet<string> siteIds = new HashSet<string>();
            HashSet<string> threatIds = new HashSet<string>();

            foreach (var alert in alerts)
            {

                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                threatIds.Add(apiThreat.threatInfo.threatId);
                siteIds.Add(apiThreat.AgentDetectionInfo.SiteId);
                accountIds.Add(apiThreat.AgentDetectionInfo.AccountId);
            }
            //
            SentinalOneUpdateAnalystVerdictDTO dto = new SentinalOneUpdateAnalystVerdictDTO();

            dto.AccountIds = accountIds.ToList();
            dto.SiteIds = siteIds.ToList();
            dto.ThreatIds = threatIds.ToList();
            dto.SentinalOne_Analysis_Verdict = analystVerdict;

            var res = _service.UpdateThreatAnalysisVerdict(dto, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }
            //Adding History data 

            methodResponse.IsSuccess = true;
            methodResponse.Message = "Update threat analyst verdict action successfull ";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;

        }

        public UpdateThreatDetailsResponse UpdateThreatDetails(SentinalOneUpdateThreatDetailsequest request)
        {
            UpdateThreatDetailsResponse methodResponse = new UpdateThreatDetailsResponse();

            string _threatStatus = string.Empty;
            string _threatAnalystVerdict = string.Empty;

            if (request.ThreatStatus_Inprogress) _threatStatus = Constants.SentinalOne_Status_Inprogress;
            if (request.ThreatStatus_Resolved) _threatStatus = Constants.SentinalOne_Status_Resolved;
            if (request.ThreatStatus_Unresolved) _threatStatus = Constants.SentinalOne_Status_Unresolved;

            if (request.AnalystVerdict_TruePositive) _threatAnalystVerdict = Constants.SentinalOne_Analysis_Verdict_TruePositive;
            if (request.AnalystVerdict_FalsePositive) _threatAnalystVerdict = Constants.SentinalOne_Analysis_Verdict_FalsePositive;
            if (request.AnalystVerdict_Suspecious) _threatAnalystVerdict = Constants.SentinalOne_Analysis_Verdict_Suspicious;

            methodResponse = UpdateThreatDetails(request.OrgId, request.AlertIds, _threatStatus, _threatAnalystVerdict);

            return methodResponse;

        }
        public UpdateThreatDetailsResponse UpdateThreatDetails(int orgId ,List<double> alertIds , string threatStatus , string threatAnalystVerdict )
        {
            UpdateThreatDetailsResponse methodResponse = new UpdateThreatDetailsResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(orgId, tooltypeID);
            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_Update_Incident_Details);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<SentinalThreatDetails> sentinalData = new List<SentinalThreatDetails>();

            // Get Alerts

            var alerts = _repo.GetalertsByAlertIds(alertIds);

            HashSet<string> accountIds = new HashSet<string>();
            HashSet<string> siteIds = new HashSet<string>();
            HashSet<string> threatIds = new HashSet<string>();

            foreach (var alert in alerts)
            {

                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                threatIds.Add(apiThreat.threatInfo.threatId);
                siteIds.Add(apiThreat.AgentDetectionInfo.SiteId);
                accountIds.Add(apiThreat.AgentDetectionInfo.AccountId);
            }
            //
            SentinalOneUpdateThreatDetailsDTO dto = new SentinalOneUpdateThreatDetailsDTO();

            dto.AccountIds = accountIds.ToList();
            dto.SiteIds = siteIds.ToList();
            dto.ThreatIds = threatIds.ToList();
            dto.ThreatStatus = threatStatus;
            dto.ThreatAnalystVerdict = threatAnalystVerdict;

            
            var res = _service.UpdateThreatDetails(dto, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }

            methodResponse.IsSuccess = true;
            methodResponse.Message = "Threat details update successfull ";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }

        public GetThreatNoteResponse GetThreatNotes(GetThreatTimelineRequest request)
        {
            GetThreatNoteResponse methodResponse = new GetThreatNoteResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgID, tooltypeID);
            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_Get_Threat_Notes);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<ThreatNoteData> sentinalData = new List<ThreatNoteData>();

            GetThreatNoteeDTO dto = new GetThreatNoteeDTO();
            if (string.IsNullOrEmpty(request.ThreatId) || request.ThreatId.ToLower() == "string")
            {
                var alerts = _repo.GetalertsByAlertIds(new List<double>() { request.AlertId});
                
                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alerts[0].alert_data,
                             new JsonSerializerOptions()
                             {
                                 PropertyNameCaseInsensitive = true
                             });
                dto.ThreatId = apiThreat.threatInfo.threatId;
                
            }
            else
            {
                dto.ThreatId = request.ThreatId;
            }
            //
            var res = _service.GetThreatNotes(dto, conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                return methodResponse;
            }
            //
            sentinalData.AddRange(res.ThreatNotes.data);
            //
            while (res.ThreatNotes != null && res.ThreatNotes.pagination != null && res.ThreatNotes.pagination.nextCursor != null)
            {
                dto.NextCursor = res.ThreatNotes.pagination.nextCursor;

                res = _service.GetThreatNotes(dto, conndtl).Result;
                if (res.ThreatNotes != null && res.ThreatNotes.data.Count > 0)
                {
                    sentinalData.AddRange(res.ThreatNotes.data);
                }
            }
            //
            var _threatNotes = _mapper.Map<List<ThreatNoteData>, List<AlertNoteModel>>(sentinalData);
         
         
            methodResponse.IsSuccess = true;
            methodResponse.Message = "List of alert note ";
            methodResponse.ThreatNotes = _threatNotes;
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }


        public AddThreatNoteResponse AddThreatNotes(AddThreatNoteRequest request)
        {
            AddThreatNoteResponse methodResponse = new AddThreatNoteResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgID, tooltypeID);
            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_Add_Threat_Notes);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<SentinalThreatDetails> sentinalData = new List<SentinalThreatDetails>();

            // Get Alerts

            var alerts = _repo.GetalertsByAlertIds(request.AlertIds);

            HashSet<string> accountIds = new HashSet<string>();
            HashSet<string> siteIds = new HashSet<string>();
            HashSet<string> threatIds = new HashSet<string>();

            foreach (var alert in alerts)
            {

                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                threatIds.Add(apiThreat.threatInfo.threatId);
                siteIds.Add(apiThreat.AgentDetectionInfo.SiteId);
                accountIds.Add(apiThreat.AgentDetectionInfo.AccountId);
            }
            //
            AddThreatNoteeDTO dto = new AddThreatNoteeDTO();

            dto.AccountIds = accountIds.ToList();
            dto.SiteIds = siteIds.ToList();
            dto.ThreatIds = threatIds.ToList();
            dto.Notes = request.Notes;

            var res = _service.AddThreatNotes(dto, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }
         

            methodResponse.IsSuccess = true;
            methodResponse.Message = "Threat notes added successfully ";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }

        public GetThreatDetailsResponse GetThreatDetails(double alertId)
        {

            GetThreatDetailsResponse response = new GetThreatDetailsResponse();

            var alerts = _repo.GetalertsByAlertIds(new List<double>() { alertId });


            if (alerts == null || alerts.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Threat Details not found";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            if (alerts[0].source != Constants.Source_SentinalOne)
            {
                response.IsSuccess = false;
                response.Message = "Alert is not related to SentinalOne Tool";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotImplemented;
                return response;
            }

            var _threatData = JsonSerializer.Deserialize<SentinalThreatDetails>(alerts[0].alert_data,
                     new JsonSerializerOptions()
                     {
                         PropertyNameCaseInsensitive = true
                     });
            //Header Data mapper
            MapToThreatDetails(response, _threatData);

            response.IsSuccess = true;
            response.Message = "Get Threat Details Success";
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;

            return response;
        }

        public GetThreatDetailsResponse GetThreatDetailsFromTool(double alertId)
        {

            GetThreatDetailsResponse response = new GetThreatDetailsResponse();

            //var alerts = _repo.GetalertsByAlertIds(new List<double>() { alertId });

            var alerts = _repo.GetalertsByAlertIds(new List<double>() { alertId });


            if (alerts == null || alerts.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Threat Details not found";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            if (alerts[0].source != Constants.Source_SentinalOne)
            {
                response.IsSuccess = false;
                response.Message = "Alert is not related to SentinalOne Tool";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotImplemented;
                return response;
            }

            //GetSentinalThreatAlertsResponse methodResponse = new GetSentinalThreatAlertsResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(alerts[0].org_id, tooltypeID);
            if (conndtl == null)
            {
                response.IsSuccess = false;
                response.Message = "Tool connection details not found , please check the request or configuration";
                return response;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_Get_Threats);

            if (conndtl == null)
            {
                response.IsSuccess = false;
                response.Message = "Tool connection details not found , please check the request or configuration";
                return response;
            }
            
            HashSet<string> threatIds = new HashSet<string>();

            foreach (var alert in alerts)
            {

                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                threatIds.Add(apiThreat.threatInfo.threatId);
            
            }
            //
            var res = _service.GetThreats(conndtl , null , new GetSentinalThreatsRequest() { OrgID = alerts[0].org_id, ThreatIds = threatIds.ToList() }).Result;
            if (!res.IsSuccess)
            {
                response.IsSuccess = false;
                response.Message = res.Message;
                return response;
            }
                       

            MapToThreatDetails(response, res.Alerts.data[0]);
            

           
            response.IsSuccess = true;
            response.Message = "Get Threat Details Success";
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;

            return response;
        }
        private  void MapToThreatDetails(GetThreatDetailsResponse response, SentinalThreatDetails? _threatData)
        {
            response.ThreatHeaderDtls = new ThreatHeaderDtls();
            response.ThreatHeaderDtls.ThreatStatus = _threatData.threatInfo.MitigationStatusDescription;
            response.ThreatHeaderDtls.IncidentStatus = _threatData.threatInfo.IncidentStatusDescription;
            response.ThreatHeaderDtls.AnalysisVerdict = _threatData.threatInfo.AnalystVerdictDescription;
            response.ThreatHeaderDtls.IdentifiedTime = _threatData.threatInfo.IdentifiedAt;
            response.ThreatHeaderDtls.ReportingTime = _threatData.threatInfo.createdAt;
            response.ThreatHeaderDtls.AIConfidenceLevel = _threatData.threatInfo.ConfidenceLevel;
            response.ThreatHeaderDtls.MiticationActions = _threatData.MitigationStatus.Select(m => m.Action).ToList<string>();


            //Network data 
            response.NetworkHistory = new NetworkHistory();

            //Threat data mapper
            response.ThreatInfo = new Threat_Info();
            response.ThreatInfo.Name = _threatData.threatInfo.threatName;
            response.ThreatInfo.Path = _threatData.threatInfo.FilePath;
            response.ThreatInfo.ThreatId = _threatData.threatInfo.threatId;
            response.ThreatInfo.OriginatingProcess = _threatData.threatInfo.OriginatorProcess;
            response.ThreatInfo.SHA1 = _threatData.threatInfo.Sha1;
            response.ThreatInfo.ProcessUser = _threatData.threatInfo.ProcessUser;
            response.ThreatInfo.Classification = _threatData.threatInfo.Classification;
            response.ThreatInfo.DetectionType = _threatData.threatInfo.DetectionType;
            response.ThreatInfo.Storyline = _threatData.threatInfo.Storyline;
            response.ThreatInfo.DetectionType = _threatData.threatInfo.DetectionType;
            response.ThreatInfo.FileSize = _threatData.threatInfo.FileSize.ToString();
            response.ThreatInfo.InitiatedBy = _threatData.threatInfo.InitiatedBy;

            // End point info
            response.Endpoint_Info = new Endpoint_Info();
            response.Endpoint_Info.ComputerName = _threatData.AgentRealtimeInfo.AgentComputerName;
            response.Endpoint_Info.FullDiskScan = _threatData.AgentRealtimeInfo.scanstatus + " at " + _threatData.AgentRealtimeInfo.scanfinishedat;
            response.Endpoint_Info.PendinRreboot = _threatData.AgentRealtimeInfo.rebootrequired.ToString();
            response.Endpoint_Info.NetworkStatus = _threatData.AgentRealtimeInfo.agentnetworkstatus;
            response.Endpoint_Info.Scope = _threatData.AgentDetectionInfo.AccountName + " / " + _threatData.AgentDetectionInfo.SiteName + " / " + _threatData.AgentDetectionInfo.GroupName; ;
            response.Endpoint_Info.OSVersion = _threatData.AgentRealtimeInfo.agentosname + " " + _threatData.AgentDetectionInfo.AgentOsRevision;
            response.Endpoint_Info.AgentVersion = _threatData.AgentRealtimeInfo.agentversion;
            response.Endpoint_Info.Policy = _threatData.AgentRealtimeInfo.agentmitigationmode;
            response.Endpoint_Info.LoggedInUser = _threatData.AgentDetectionInfo.AgentLastLoggedInUserName;
            response.Endpoint_Info.UUID = _threatData.AgentDetectionInfo.AgentUuid;
            response.Endpoint_Info.Domain = _threatData.AgentDetectionInfo.AgentDomain;
            response.Endpoint_Info.IPV4Address = _threatData.AgentDetectionInfo.AgentIpV4;
            response.Endpoint_Info.IPV6Address = _threatData.AgentDetectionInfo.AgentIpV6;
            response.Endpoint_Info.ConsoleVisibleIPAddress = _threatData.AgentDetectionInfo.ExternalIp;
            response.Endpoint_Info.SubscriptionTime = _threatData.AgentDetectionInfo.AgentRegisteredAt;
        }

        public ThreatActionResponse ThreatAction(ThreatActionRequest request)
        {
           
            ThreatActionResponse actionResponse = null;
            if (request.Kill)
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Kill);
               
                if (!actionResponse.IsSuccess) return actionResponse;

            }
            if (request.Quarantine)
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Quarantine);

                if (!actionResponse.IsSuccess) return actionResponse;

            }
            if (request.Remediate)
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Remediate);

                if (!actionResponse.IsSuccess) return actionResponse;

            }
            if (request.Rollback)
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Rollback_Remediation);

                if (!actionResponse.IsSuccess) return actionResponse;

            }
            if (request.UnQuarantine)
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_UnQuarantine);
           
                if (!actionResponse.IsSuccess) return actionResponse;

            }
            if (request.NetworkQuarantine)
            {
                actionResponse = ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Network_Quarantine);
           
                if (!actionResponse.IsSuccess) return actionResponse;
            }
            if (actionResponse != null)
                return actionResponse;
            else
            {
                actionResponse = new ThreatActionResponse();
                actionResponse.IsSuccess = false;
                actionResponse.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                actionResponse.Message = "No threat actions are selected  ";
            }
            return actionResponse;
        }

        public AddToNetworkResponse AddToNetwork(AddToNetworkRequest request)
        {
            AddToNetworkResponse methodResponse = new AddToNetworkResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgID, tooltypeID);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_AddToNetwork);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<SentinalThreatDetails> sentinalData = new List<SentinalThreatDetails>();

            // Get Alerts

            var alerts = _repo.GetalertsByAlertIds(request.AlertIds);

            HashSet<string> accountIds = new HashSet<string>();
            HashSet<string> siteIds = new HashSet<string>();
            HashSet<string> agentIds = new HashSet<string>();

            foreach (var alert in alerts)
            {

                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                agentIds.Add(apiThreat.AgentRealtimeInfo.agentId);
                siteIds.Add(apiThreat.AgentDetectionInfo.SiteId);
                accountIds.Add(apiThreat.AgentDetectionInfo.AccountId);
            }
            //
            AddToNetworkDTO dto = new AddToNetworkDTO();

            dto.AccountIds = accountIds.ToList();
            dto.SiteIds = siteIds.ToList();
            dto.AgentIds = agentIds.ToList();

            var res = _service.AddToNetwork(dto, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }
            methodResponse.IsSuccess = true;
            methodResponse.Message = "Endpoint succesfully added to network  ";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }

        public DisconnectFromNetworkResponse DisconnectFromNetwork(DisconnectFromNetworkRequest request)
        {
            DisconnectFromNetworkResponse methodResponse = new DisconnectFromNetworkResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgId, tooltypeID);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_DisconnectFromNetwork);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<SentinalThreatDetails> sentinalData = new List<SentinalThreatDetails>();

            // Get Alerts

            var alerts = _repo.GetalertsByAlertIds(request.AlertIds);

            HashSet<string> accountIds = new HashSet<string>();
            HashSet<string> siteIds = new HashSet<string>();
            HashSet<string> agentIds = new HashSet<string>();

            foreach (var alert in alerts)
            {

                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                agentIds.Add(apiThreat.AgentRealtimeInfo.agentId);
                siteIds.Add(apiThreat.AgentDetectionInfo.SiteId);
                accountIds.Add(apiThreat.AgentDetectionInfo.AccountId);
            }
            //
            AddToExclusionList dto = new AddToExclusionList();
            //
            dto.AccountIds = accountIds.ToList();
            dto.SiteIds = siteIds.ToList();
            dto.AgentIds = agentIds.ToList();

            var res = _service.DisconnectFromNetwork(dto, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }

            methodResponse.IsSuccess = true;
            methodResponse.Message = "Endpoint succesfully disconnected from network  ";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }

        public AddToBlocklistResponse AddToblockListForThreats(AddToBlocklistForThreatsRequest request)
        {
            AddToBlocklistResponse methodResponse = new AddToBlocklistResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgID, tooltypeID);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_AddToBlockedList_ForThreats);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            List<SentinalThreatDetails> sentinalData = new List<SentinalThreatDetails>();

            // Get Alerts

            var alerts = _repo.GetalertsByAlertIds(request.AlertIds);

            HashSet<string> accountIds = new HashSet<string>();
            HashSet<string> siteIds = new HashSet<string>();
            HashSet<string> threatIds = new HashSet<string>();

            foreach (var alert in alerts)
            {

                var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                         new JsonSerializerOptions()
                         {
                             PropertyNameCaseInsensitive = true
                         });
                threatIds.Add(apiThreat.threatInfo.threatId);
                siteIds.Add(apiThreat.AgentDetectionInfo.SiteId);
                accountIds.Add(apiThreat.AgentDetectionInfo.AccountId);
            }
            //
            AddToBlocklistDTO dto = new AddToBlocklistDTO();
            //
            dto.AccountIds = accountIds.ToList();
            dto.SiteIds = siteIds.ToList();
            dto.ThreatIds = threatIds.ToList();
            dto.TargetScope = request.TargetScope;
            dto.Description = request.Description;
            dto.Note = request.Note;
            var res = _service.AddToblockListForThreats(dto, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }

            methodResponse.IsSuccess = true;
            methodResponse.Message = "Add item to blocklist success";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }
        public AddToBlocklistResponse AddToblockList(AddToBlocklistRequest request)
        {
            AddToBlocklistResponse methodResponse = new AddToBlocklistResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgId, tooltypeID);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_AddToBlockedList);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            var res = _service.AddToblockList(request, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }

            methodResponse.IsSuccess = true;
            methodResponse.Message = "Add item to blocklist success";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }
        public AddToBlocklistResponse UpdateAddToblockList(UpdateAddToBlocklistRequest request)
        {
            AddToBlocklistResponse methodResponse = new AddToBlocklistResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgId, tooltypeID);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_AddToBlockedList_Update);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            var res = _service.UpdateAddToblockList(request, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }

            methodResponse.IsSuccess = true;
            methodResponse.Message = "Update Blocked list item success";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }
        public AddToBlocklistResponse DeleteAddToblockList(DeleteAddToBlocklistRequest request)
        {
            AddToBlocklistResponse methodResponse = new AddToBlocklistResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgId, tooltypeID);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_AddToBlockedList_Delete);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            var res = _service.DeleteAddToblockList(request, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }

            methodResponse.IsSuccess = true;
            methodResponse.Message = "Delete Blocked list item success ";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }
        public AddToExclusionlistResponse AddToExclusionList(AddToExclusionRequest request)
         {
            AddToExclusionlistResponse methodResponse = new AddToExclusionlistResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgID, tooltypeID);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            if (request.AlertIds == null || request.AlertIds.Count==0)
                conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_AddToExclusionList);
             else
                conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_AddToExclusionList_ForThreats);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            AddToExclusionlistDTO dto = new AddToExclusionlistDTO();

            dto = _mapper.Map<AddToExclusionRequest, AddToExclusionlistDTO>(request);
            // Get Alerts

            AddToExclusionlistResponse res = null; 
            if (request.AlertIds == null || request.AlertIds.Count == 0)
            {
                 res = _service.AddToExclusionList(dto, conndtl).Result;
            }
            else 
            { 
                var alerts = _repo.GetalertsByAlertIds(request.AlertIds);

                HashSet<string> accountIds = new HashSet<string>();
                HashSet<string> siteIds = new HashSet<string>();
                HashSet<string> threatIds = new HashSet<string>();

                foreach (var alert in alerts)
                {

                    var apiThreat = JsonSerializer.Deserialize<SentinalThreatDetails>(alert.alert_data,
                             new JsonSerializerOptions()
                             {
                                 PropertyNameCaseInsensitive = true
                             });
                    threatIds.Add(apiThreat.threatInfo.threatId);
                    siteIds.Add(apiThreat.AgentDetectionInfo.SiteId);
                    accountIds.Add(apiThreat.AgentDetectionInfo.AccountId);
                }
                //
                dto.AccountIds = accountIds.ToList();
                dto.SiteIds = siteIds.ToList();
                dto.ThreatIds = threatIds.ToList();
                res = _service.AddToExclusionList(dto, conndtl).Result;
            }

           // var res = _service.AddToExclusionList(dto, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }
            methodResponse.IsSuccess = true;
            methodResponse.Message = "Add to exclusion list item success";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }

        public AddToExclusionlistResponse UpdateAddToExclusionList(UpdateAddToExclusionRequest request)
        {
            AddToExclusionlistResponse methodResponse = new AddToExclusionlistResponse();
            var tooltypeId = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgId, tooltypeId);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_AddToExclusionList_Update);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            var res = _service.UpdateAddToExclusionList(request, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }
            methodResponse.IsSuccess = true;
            methodResponse.Message = "Update exclusion list item success ";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }

        public AddToExclusionlistResponse DeleteAddToExclusionList(DeleteAddToExclusionRequest request)
        {
            AddToExclusionlistResponse methodResponse = new AddToExclusionlistResponse();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgId, tooltypeID);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_AddToExclusionList_Delete);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

             var res = _service.DeleteAddToExclusionList(request, conndtl).Result;
            //
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                return methodResponse;
            }
            methodResponse.IsSuccess = true;
            methodResponse.Message = "Delete exclusion list success ";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }
    }
}
