using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;
using LDPRuleEngine.BL;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.DAL.Repositories;

namespace LDP.Common.BL
{
    public class AlertPlayBookProcessBL : IAlertPlayBookProcessBL
    {
        IAlertPlayBookProcessRepository _alertPlayBookProcessRepository;
        IAlertsBL _alertbl;
        IPlaybookBL _playbookbl;
        IPlayBookDtlsRepository _PlaybookDtlsrepo;
        IRulesConfigurationBL _ruleBl;
        IRuleActionBL _ruleactionbl;
        IAlertPlayBookProcessActionBL _playbookprocessactionbl;
        public AlertPlayBookProcessBL(IAlertPlayBookProcessRepository alertPlayBookProcessRepository
            , IAlertsBL alertbl
            , IPlaybookBL playbookbl
            , IPlayBookDtlsRepository PlaybookDtlsrepo
            , IRulesConfigurationBL ruleBl
            , IRuleActionBL ruleactionbl
            , IAlertPlayBookProcessActionBL playbookprocessactionbl)
        {
            _alertPlayBookProcessRepository = alertPlayBookProcessRepository;
            _alertbl = alertbl;
            _playbookbl = playbookbl;
            _PlaybookDtlsrepo = PlaybookDtlsrepo;
            _ruleBl = ruleBl;
            _ruleactionbl = ruleactionbl;

            _playbookprocessactionbl = playbookprocessactionbl;
        }
        public AlertPlayBookProcessResponse AddAddAlertPlayBookProcess(AddAlertPlayBookProcessRequest request)
        {
            AlertPlayBookProcessResponse response = new AlertPlayBookProcessResponse();
            _alertPlayBookProcessRepository.AddAlertPlayBookProcess(request);
            response.IsSuccess = true;
            response.Message = "Success";
            return response;

        }
        public AlertPlayBookProcessResponse AnalyzeAlertsForAutomation()
        {
            // 1. Get Alerts where automation status = "Pending"
            GetAlertByAutomationStatusRequest statusRequst = new GetAlertByAutomationStatusRequest();
            statusRequst.OrgID = 1;
            statusRequst.ToolTypeID = 29;
            statusRequst.ToolID = 1;
            statusRequst.AutomationStatusList = new List<string>() { "pending" };
            statusRequst.paging = new RequestPaging();
            statusRequst.paging.RangeStart = 0;
            statusRequst.paging.RangeEnd = 49;

            var alerts = _alertbl.GetAlertDataByAutomationStatus(statusRequst);
            AlertPlayBookProcessResponse response = new AlertPlayBookProcessResponse();
            if (alerts == null)
            {
                response.IsSuccess = false;
                response.Message = "Alerts not found to process";
                return response;
            }

            var playbooks = _playbookbl.GetPlayBooks();

            if (playbooks == null)
            {

                response.IsSuccess = false;
                response.Message = "play books not found to process";
                return response;
            }

            if (alerts != null)
            {
                foreach (var alert in alerts.AlertsList)
                {
                    UpdateAutomationStatus(alert.AlertID, "inprogress");
                    try
                    {
                        var PlaybookRuleexectionStatus = ExecuteRulesOnPlaybooks(alert, playbooks);
                    }
                    catch (Exception ex)
                    {
                        UpdateAutomationStatus(alert.AlertID, "error");
                    }

                }
            }
            // 2. Get the play books
            // 3. Execute the rules for each play book
            // 4. if all the rules are satisfied , store the alerts , playbooks , actions in table

            response.IsSuccess = true;
            response.Message = "success";
            return response;
        }
        public AnalyzeAlertsForAutomationResponse AnalyzeAlertsForAutomation(AnalyzeAlertsForAutomationRequest request)
        {
            // 1. Get Alerts where automation status = "Pending"
            GetAlertByAutomationStatusRequest statusRequst = new GetAlertByAutomationStatusRequest();
            statusRequst.OrgID = request.OrgID;
            statusRequst.ToolTypeID = request.ToolTypeID;
            statusRequst.ToolID = request.ToolID;
            statusRequst.AutomationStatusList = new List<string>() { "pending" };
            statusRequst.paging = new RequestPaging();
            statusRequst.paging.RangeStart = request.paging.RangeStart;
            statusRequst.paging.RangeEnd = request.paging.RangeEnd;

            var alerts = _alertbl.GetAlertDataByAutomationStatus(statusRequst);
            AnalyzeAlertsForAutomationResponse response = new AnalyzeAlertsForAutomationResponse();
            if (alerts.IsSuccess == false)
            {
                response.IsSuccess = false;
                response.Message = "Alerts not found to process";
                return response;
            }

            var playbooks = _playbookbl.GetPlayBooks();

            if (playbooks.IsSuccess == false)
            {

                response.IsSuccess = false;
                response.Message = "play books not found to process";
                return response;
            }

            if (alerts.AlertsList != null)
            {
                foreach (var alert in alerts.AlertsList)
                {
                    UpdateAutomationStatus(alert.AlertID, "inprogress");
                    try
                    {
                        var PlaybookRuleexectionStatus = ExecuteRulesOnPlaybooks(alert, playbooks);
                    }
                    catch (Exception ex)
                    {
                        UpdateAutomationStatus(alert.AlertID, "error");
                    }
                    UpdateAutomationStatus(alert.AlertID, "readyforaction");

                }
            }
            // 2. Get the play books
            // 3. Execute the rules for each play book
            // 4. if all the rules are satisfied , store the alerts , playbooks , actions in table

            response.IsSuccess = true;
            response.Message = "success";
            return response;
        }
        private void UpdateAutomationStatus(int alertID, string Status)
        {
            UpdateAutomationStatusRequest statusUpdate = new UpdateAutomationStatusRequest();
            statusUpdate.Status = Status;
            statusUpdate.AlertID = alertID;
            _alertbl.UpdateAlertAutomationStatus(statusUpdate);
        }

        public bool ExecuteRulesOnPlaybooks(AlertModel alertData, GetPlayBookResponse playbooks)
        {

            ExecuteRuleRequest rulerequest = null;
            AddAlertPlayBookProcessRequest addAlertPlayBookProcessRequest = null;
            bool ruleexcutionFlag = false;
            foreach (var playbook in playbooks.Playbooks)
            {
                // Get the rules and actions for the play book
                var PlaybookdtlsData = _PlaybookDtlsrepo.GetPlaybookDtlsByPlaybookID(playbook.PlayBookID)
                    .Result.OrderBy(pbdtl => pbdtl.Execution_Sequence_Number);
                // Executing the rules
                foreach (var dtl in PlaybookdtlsData)
                {
                    if (dtl.Play_Book_Item_Type == "rule")
                    {
                        rulerequest = new ExecuteRuleRequest();
                        rulerequest.RuleID = dtl.Play_Book_Item_Type_RefID;
                        rulerequest.InputTextToRuleExecute = alertData.Name;
                        var ruleexectinResponse = _ruleBl.ExecuteRule(rulerequest);
                        ruleexcutionFlag = ruleexectinResponse.IsSuccess;
                        // if rule exection statis false , break the current play book
                        // and move to next play book execution
                        if (!ruleexcutionFlag) break;
                    }
                }
                // if all the rules are satisfied , Inserting into action table
                if (ruleexcutionFlag)
                {
                    addAlertPlayBookProcessRequest = new AddAlertPlayBookProcessRequest();
                    addAlertPlayBookProcessRequest.AlertPlayBookProcessData = new List<Model.AddAlertPlayBookProcessModel>();
                    AddAlertPlayBookProcessModel addAlertPlayBookProcessModel = null;
                    addAlertPlayBookProcessModel = new AddAlertPlayBookProcessModel();
                    addAlertPlayBookProcessModel.alertid = alertData.AlertID;
                    addAlertPlayBookProcessModel.orgid = alertData.OrgID;
                    addAlertPlayBookProcessModel.CreatedUser = "Backgroundjob";
                    addAlertPlayBookProcessModel.CreatedDate = DateTime.Now.ToUniversalTime();
                    addAlertPlayBookProcessModel.AlertPlayBookProcessActions = new List<AddAlertPlayBookProcessActionModel>();
                    AddAlertPlayBookProcessActionModel addAlertPlayBookProcessActionModel = null;
                    foreach (var dtl in PlaybookdtlsData)
                    {
                        if (dtl.Play_Book_Item_Type == "action")
                        {
                            addAlertPlayBookProcessActionModel = new AddAlertPlayBookProcessActionModel();
                            addAlertPlayBookProcessActionModel.tooltypeid = 30;
                            addAlertPlayBookProcessActionModel.toolactionid = dtl.Play_Book_Item_Type_RefID;
                            addAlertPlayBookProcessActionModel.playbookid = dtl.Play_Book_ID;
                            addAlertPlayBookProcessActionModel.toolid = 2;

                            addAlertPlayBookProcessModel.AlertPlayBookProcessActions.Add(addAlertPlayBookProcessActionModel);

                        }
                    }
                    addAlertPlayBookProcessRequest.AlertPlayBookProcessData.Add(addAlertPlayBookProcessModel);//= addAlertPlayBookProcessModel;
                    this.AddAddAlertPlayBookProcess(addAlertPlayBookProcessRequest);
                    UpdateAutomationStatus(alertData.AlertID, "readyforaction");
                }
                if (!ruleexcutionFlag)
                {
                    UpdateAutomationStatus(alertData.AlertID, "noteligible");
                }

            }
            return true;
        }

        public ProcessAlertActionsResponse ProcessAlertPlaybookProcessActions()
        {
            ProcessAlertActionsResponse processAlertActionsResponse = new ProcessAlertActionsResponse();
            // 1. Get Alerts where automation status = "Pending"
            GetAlertPlayBookProcessActionsByStatusRequest statusRequst = new GetAlertPlayBookProcessActionsByStatusRequest();
            statusRequst.OrgID = 1;
            statusRequst.ToolTypeID = 30;
            statusRequst.ToolID = 2;
            statusRequst.Status = new List<string>() { "pending" };
            statusRequst.paging = new RequestPaging();
            statusRequst.paging.RangeStart = 0;
            statusRequst.paging.RangeEnd = 49;

            var pendingactions = _playbookprocessactionbl.GetAlertPlayBookProcessActionsByStatus(statusRequst);

            // AlertPlayBookProcessResponse response = new AlertPlayBookProcessResponse();
            if (pendingactions.AlertPlayBookProcessActionData.Count == 0)
            {
                processAlertActionsResponse.IsSuccess = false;
                processAlertActionsResponse.Message = "actions not found to process";
                return processAlertActionsResponse;
            }
            processActions(pendingactions);
            // 2. Get the play books
            // 3. Execute the rules for each play book
            // 4. if all the rules are satisfied , store the alerts , playbooks , actions in table

            processAlertActionsResponse.IsSuccess = true;
            processAlertActionsResponse.Message = "success";
            return processAlertActionsResponse;
        }

        public ProcessAlertActionsResponse ProcessAlertPlaybookProcessActions(PlayBookProcessActionRequest request)
        {
            ProcessAlertActionsResponse processAlertActionsResponse = new ProcessAlertActionsResponse();
            // 1. Get Alerts where automation status = "Pending"
            GetAlertPlayBookProcessActionsByStatusRequest statusRequst = new GetAlertPlayBookProcessActionsByStatusRequest();
            statusRequst.OrgID = request.OrgID;
            statusRequst.ToolTypeID = request.ToolTypeID;
            statusRequst.ToolID = request.ToolID;
            statusRequst.Status = new List<string>() { "pending" };
            statusRequst.paging = new RequestPaging();
            statusRequst.paging.RangeStart = request.paging.RangeStart;
            statusRequst.paging.RangeEnd = request.paging.RangeEnd;

            var pendingactions = _playbookprocessactionbl.GetAlertPlayBookProcessActionsByStatus(statusRequst);

            if (pendingactions.AlertPlayBookProcessActionData.Count == 0)
            {
                processAlertActionsResponse.IsSuccess = false;
                processAlertActionsResponse.Message = "actions not found to process";
                return processAlertActionsResponse;
            }
            processActions(pendingactions);
            // 2. Get the play books
            // 3. Execute the rules for each play book
            // 4. if all the rules are satisfied , store the alerts , playbooks , actions in table

            processAlertActionsResponse.IsSuccess = true;
            processAlertActionsResponse.Message = "success";
            return processAlertActionsResponse;
        }

        private bool ExecuteAction(GetAlertPlayBookProcessActionModel action, List<AlertPlayBookProcess> pendingPlaybookProcess)
        {
            bool response = false;

            ExecuteRuleActionRequest ruleactionrequest = new ExecuteRuleActionRequest();
            ruleactionrequest.ruleActionID = action.toolactionid;
            ruleactionrequest.ToolTypeID = action.tooltypeid;
            ruleactionrequest.ToolID = action.toolid;
            ruleactionrequest.alertiD = pendingPlaybookProcess.Find(p => p.alert_playbooks_process_id == action.alertplaybooksprocessid).alert_id;
            ruleactionrequest.OrgID = pendingPlaybookProcess.Find(p => p.alert_playbooks_process_id == action.alertplaybooksprocessid).org_id;
            var ruleactionresponse = _ruleactionbl.ExecuteRuleAction(ruleactionrequest);
            UpdateActionStatus(action.alertplaybooksprocessactionid, "completed");
            return response;
        }

        public void processActions(GetAlertPlayBookProcessactionResponse pendingactions)
        {
            


            List<int> playbookprocessID = pendingactions.AlertPlayBookProcessActionData
                .Select(x => x.alertplaybooksprocessid).ToList();

            playbookprocessID = playbookprocessID.Distinct().ToList();

            var pendingPlaybookProcess = _alertPlayBookProcessRepository
                .GetAlertPlayBookProcessByIDs(playbookprocessID);

            if (pendingactions.AlertPlayBookProcessActionData.Count > 0)
            {
                foreach (var action in pendingactions.AlertPlayBookProcessActionData)
                {
                    UpdateActionStatus(action.alertplaybooksprocessactionid, "inprogress");
                    try
                    {
                        var ExecutreActionstatus = ExecuteAction(action, pendingPlaybookProcess.Result);
                    }
                    catch (Exception ex)
                    {
                        UpdateActionStatus(action.alertplaybooksprocessactionid, "error");
                    }

                }
            }
        }
        private void UpdateActionStatus(int alertplaybooksprocessActionid, string Status)
        {
            UpdateActionStatusRequest statusUpdate = new UpdateActionStatusRequest();
            statusUpdate.Status = Status;
            statusUpdate.alertplaybooksprocessactionid = alertplaybooksprocessActionid;
            _playbookprocessactionbl.UpdateActionStatus(statusUpdate);
        }
    }
}
