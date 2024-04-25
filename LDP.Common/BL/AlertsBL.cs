using AutoMapper;
using LDP.Common;
using LDP.Common.BL;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Requests.Common;
using LDP.Common.Responses;
using LDP.Common.Services;
using LDP.Common.Services.SentinalOneIntegration;
using LDP.Common.Services.SentinalOneIntegration.Activities;
using LDP.Common.Services.SentinalOneIntegration.Sentinel;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;
using LDP_APIs.DAL.Interfaces;
using LDP_APIs.Models;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace LDP_APIs.BL
{
    public class AlertsBL : IAlertsBL
    {
        IAlertsRepository _repo;
        public readonly IMapper _mapper;
        ILDPlattformBL _plattformBL;
        IUserActionBL _useractionBL;
        IAlertHistoryBL _alertHistoryBL;
        ILDPSecurityBL _securityBl;
        ISentinalOneIntegrationBL _sentinalOneOplBL;
        ISentinalBL _sentinelTabBl;
        ICommonBL _commonBL;
        public AlertsBL(IAlertsRepository repo, IMapper mapper, ILDPlattformBL plattformBL,
            IUserActionBL useractionBL, IAlertHistoryBL alertHistoryBL
            , ILDPSecurityBL securityBl, ISentinalOneIntegrationBL sentinalOneOplBL, ISentinalBL sentinelTabBl, ICommonBL commonBL)
        {
            _repo = repo;
            _mapper = mapper;
            _plattformBL = plattformBL;
            _useractionBL = useractionBL;
            _alertHistoryBL = alertHistoryBL;
            _securityBl = securityBl;
            _sentinalOneOplBL = sentinalOneOplBL;
            _sentinelTabBl = sentinelTabBl;
            _commonBL = commonBL;
        }
        public getAlertsResponse GetAlerts(GetAlertsRequest request)
        {
            getAlertsResponse returnobj = new getAlertsResponse();
            List<Alerts> alertsdata = null;
            //
            bool isAdinuser = false;
            var user = _securityBl.GetUserbyID(request.LoggedInUserId);

            if (user.Userdata != null)
            {
                if (user.Userdata.ClientAdminRole == 1 || user.Userdata.GlobalAdminRole == 1)
                {
                    isAdinuser = true;
                }
            }
            alertsdata = _repo.Getalerts(request, isAdinuser);
            if (alertsdata.Count > 0)
            {
                var _mappedResponse = _mapper.Map<List<Alerts>, List<AlertModel>>(alertsdata);

                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.AlertsList = _mappedResponse;
                returnobj.TotalOffenseCount = _repo.GetAlertsDataCount(request, isAdinuser).Result;
                returnobj.Source = _mappedResponse.Select(alrt => alrt.Source).Distinct().ToList();
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "No data found";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                returnobj.TotalOffenseCount = 0;
            }

            return returnobj;

        }
        public getAlertResponse GetAlertData(GetAlertRequest request)
        {
            var alertsDBdata = _repo.GetAlertData(request).Result.FirstOrDefault();
            getAlertResponse returnobj = new getAlertResponse();
            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            var _mappedResponse = _mapper.Map<Alerts, AlertModel>(alertsDBdata);

            returnobj.AlertsList = new List<AlertModel>()
            {
                _mappedResponse
            };

            return returnobj;

        }
        public GetThreatDetailsResponse GetAlertDetailsFromTool(double alertId)
        {
            return _sentinalOneOplBL.GetThreatDetailsFromTool(alertId);
        }
        public getAlertResponse GetAlertDataFromTool(GetAlertRequest request)
        {
            var alertsDBdata = _repo.GetAlertData(request).Result.FirstOrDefault();
            getAlertResponse returnobj = new getAlertResponse();
            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            var _mappedResponse = _mapper.Map<Alerts, AlertModel>(alertsDBdata);

            returnobj.AlertsList = new List<AlertModel>()
            {
                _mappedResponse
            };

            return returnobj;

        }

        public getAlertsResponse GetAlertsByAssignedUser(GetAlertByAssignedOwnerRequest request)
        {
            var alertsdata = _repo.GetAlertsByAssignedUser(request);
            var _mappedResponse = _mapper.Map<IEnumerable<Alerts>, List<AlertModel>>(alertsdata.Result);
            getAlertsResponse returnobj = new getAlertsResponse();
            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.AlertsList = _mappedResponse;
            returnobj.TotalOffenseCount = _repo.GetAlertsCountByAssignedUser(request).Result;
            return returnobj;
        }

        public getAlertsResponse GetAlertDataByAutomationStatus(GetAlertByAutomationStatusRequest request)
        {
            var alertsdata = _repo.GetAlertDataByAutomationStatus(request);
            var _mappedResponse = _mapper.Map<IEnumerable<Alerts>, List<AlertModel>>(alertsdata.Result);
            getAlertsResponse returnobj = new getAlertsResponse();
            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.AlertsList = _mappedResponse;
            returnobj.TotalOffenseCount = _repo.GetAlertsCountByAutomationStatus(request).Result;
            return returnobj;
        }
        public UpdateAutomationStatusResponse UpdateAlertAutomationStatus(UpdateAutomationStatusRequest request)
        {
            UpdateAutomationStatusResponse res = new UpdateAutomationStatusResponse();
            var _userData = GetUserDetails(0, request.ModifiedUserId);
            var repores = _repo.UpdateAlertAutomationStatus(request);

            //Add data into alert History
            AlertHistoryRequest historyRequest = new AlertHistoryRequest();
            historyRequest.AlertId = request.AlertID;
            historyRequest.CreatedUser = "Background job";
            historyRequest.HistoryDate = DateTime.UtcNow;
            historyRequest.HistoryDescription = "Automation status changed , Alert Id : " + request.AlertID
               + " ; Status Changed  to " + request.Status + " ; changed by : " + _userData.UpdatedUseName;
            historyRequest.CreatedUserId = 0;
            historyRequest.OrgId = request.OrgId;

            _alertHistoryBL.AddalertHistory(historyRequest);

            if (repores.Result == "")
            {
                res.IsSuccess = true;
            }
            else
            {
                res.IsSuccess = false;
                res.Message = repores.Result;
            }
            return res;
        }

        #region unUsedMethods
        

        public baseResponse AssignOwner(AssignOwnerRequest request)
        {
            baseResponse returnobj = new baseResponse();

            var alertdata = _repo.GetAlertData(new GetAlertRequest() { alertID = request.AlertID });
            if (alertdata.Result == null)
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Alert data not found";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                return returnobj;
            }

            var _userActionRequest = _mapper.Map<Alerts, UserActionRequest>(alertdata.Result.FirstOrDefault());
            var assignownerRes = _useractionBL.AssignOwner(_userActionRequest);
            if (!assignownerRes.IsSuccess)
            {

                returnobj.IsSuccess = false;
                returnobj.Message = assignownerRes.Message;
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                return returnobj;
            }
            var _userData = GetUserDetails(0, request.ModifiedUserId);



            //get alert data to pass to update the user actions


            //Add data into alert History
            AlertHistoryRequest historyRequest = new AlertHistoryRequest();
            historyRequest.AlertId = request.AlertID;
            historyRequest.CreatedUser = _userData.UpdatedUseName;
            historyRequest.HistoryDate = request.ModifiedDate;
            historyRequest.HistoryDescription = "Owner assigned , Alert Id : " + request.AlertID
                + " ; Assinged To " + request.UserName + " ; Assinged by : " + _userData.UpdatedUseName;
            historyRequest.CreatedUserId = request.ModifiedUserId;
            historyRequest.OrgId = request.OrgId;

            _alertHistoryBL.AddalertHistory(historyRequest);

            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.HttpStatusCode = HttpStatusCode.OK;

            return returnobj;
        }
     
        public SetAlertStatusResponse SetAlertStatus(SetAlertStatusRequest request)
        {
            List<AlertHistoryModel> historyRequestList = new List<AlertHistoryModel>();
            AlertHistoryModel historyRequest = null;
            SetAlertStatusResponse returnobj = new SetAlertStatusResponse();

            var _userData = GetUserDetails(0, request.ModifiedUserId);
            var repoResponse = _repo.SetAlertStatus(request, _userData.UpdatedUseName);

            // adding notes

            if (!string.IsNullOrEmpty(request.Notes))
            {
                AddAlertNote(request);

                // Notes capturing in Alert History
                historyRequest = AddAlertHistory(request, historyRequestList);

            }
            // updating user action data 

            SetUserActionStatusRequest statusRequst = new SetUserActionStatusRequest()
            {
                ActionType = Constants.User_Action_Alert_Type,
                Id = request.alertID,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = request.ModifiedUser,
                StatusId = request.StatusID,
                StatusName = request.StatusName
            };
            var useractionRes = _useractionBL.SetUserActionStatus(statusRequst);

            //Add data into alert History
            historyRequest = new AlertHistoryRequest();
            historyRequest.AlertId = request.alertID;
            historyRequest.CreatedUser = request.ModifiedUser;
            historyRequest.HistoryDate = request.ModifiedDate;
            if (!string.IsNullOrEmpty(request.OwnerName))
            {
                historyRequest.HistoryDescription = "Alert status changed , Alert Id : " + request.alertID +
                    " status changed to " + request.StatusName
              + " , owner changed to :" + request.OwnerName
               + " changed BY :" + request.ModifiedUser;

            }
            else
            {
                historyRequest.HistoryDescription = "Alert status changed , Alert Id : " + request.alertID
                    + " Alert status changed  to : " + request.StatusName
                 + " Changed BY :" + request.ModifiedUser;
            }
            historyRequest.CreatedUserId = request.ModifiedUserId;
            historyRequest.OrgId = request.OrgId;

            historyRequestList.Add(historyRequest);

            _alertHistoryBL.AddRangealertHistory(historyRequestList);


            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                //returnobj.ErrorMessage = repoResponse.Result;
            }

            return returnobj;
        }

        
        private AlertHistoryModel AddAlertHistory(SetAlertStatusRequest request, List<AlertHistoryModel> historyRequestList)
        {
            AlertHistoryModel historyRequest = new AlertHistoryModel();
            historyRequest.AlertId = request.alertID;
            historyRequest.CreatedUser = request.ModifiedUser;
            historyRequest.HistoryDate = request.ModifiedDate;
            historyRequest.CreatedUserId = request.ModifiedUserId;
            historyRequest.OrgId = request.OrgId;
            historyRequest.HistoryDescription = "Alert notes added : , Alert Id : " + request.alertID
                    + " Notes : " + request.Notes
                 + "  By :" + request.ModifiedUser;

            historyRequestList.Add(historyRequest);
            return historyRequest;
        }

        private void AddAlertNote(SetAlertStatusRequest request)
        {
            alert_note note = new alert_note()
            {
                action_id = request.StatusID,
                action_name = request.StatusName,
                action_type = Constants.AlertStatusType,
                alert_id = request.alertID,
                Created_date = request.ModifiedDate,
                Created_user = request.ModifiedUser,
                notes = request.Notes,
                notes_date = request.ModifiedDate,
                notes_to_userid = request.OwnerID,
            };


            var noteres = _repo.AddAlertNote(note);
        }

        public SetAlertStatusResponse SetAlertEscalationStatus(SetAlertSpecificStatusRequest request)
        {
            SetAlertStatusResponse returnobj = new SetAlertStatusResponse();

            SetMultipleAlertStatusRequest setMultipleAlertStatusRequest = new SetMultipleAlertStatusRequest();
            var _userData = GetUserDetails(request.OwnerID, request.ModifiedUserId);
            var _escalateStatusID = _plattformBL.GetMasterDataByDataValue(Constants.AlertStatusType, Constants.Alert_Escalate_Status);
            setMultipleAlertStatusRequest.alertIDs = request.alertIDs;
            setMultipleAlertStatusRequest.ModifiedUser = _userData.UpdatedUseName;
            setMultipleAlertStatusRequest.ModifiedDate = request.ModifiedDate;
            setMultipleAlertStatusRequest.StatusName = Constants.Alert_Escalate_Status;
            setMultipleAlertStatusRequest.StatusID = _escalateStatusID;
            setMultipleAlertStatusRequest.OwnerName = _userData.OwnerUserName;
            setMultipleAlertStatusRequest.OwnerID = request.OwnerID;
            setMultipleAlertStatusRequest.Notes = request.Notes;


            var repoResponse = _repo.SetMultipleAlertsStatus(setMultipleAlertStatusRequest, _userData.UpdatedUseName);

            if (!string.IsNullOrEmpty(repoResponse.Result))
            {

                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                return returnobj;
            }

            List<AlertHistoryRequest> alertHistoryRequestList = new List<AlertHistoryRequest>();
            // adding notes

            if (!string.IsNullOrEmpty(request.Notes))
            {
                alert_note note = null;
                List<alert_note> alertNotes = new List<alert_note>();
                foreach (var alertId in request.alertIDs)
                {
                    note = new alert_note()
                    {
                        action_id = setMultipleAlertStatusRequest.StatusID,
                        action_name = Constants.Alert_Escalate_Status,
                        action_type = Constants.AlertStatusType,
                        alert_id = alertId,
                        Created_date = request.ModifiedDate,
                        Created_user = _userData.UpdatedUseName,
                        notes = request.Notes,
                        notes_date = request.ModifiedDate,
                        notes_to_userid = request.OwnerID,
                    };
                    alertNotes.Add(note);
                }

                var noteres = _repo.AddRangeAlertNote(alertNotes);

            }
            // updating user action data 
            SetMultipleUserActionStatusRequest useractionRequest = new SetMultipleUserActionStatusRequest()
            {
                ActionType = Constants.User_Action_Alert_Type,
                Ids = request.alertIDs,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = _userData.UpdatedUseName,
                OwnerId = request.OwnerID,
                OwnerName = _userData.OwnerUserName,
                StatusId = _escalateStatusID,
                StatusName = Constants.Alert_Escalate_Status
            };
            _useractionBL.SetMultipleUserActionStatus(useractionRequest);

            // Add into alert history
            List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
            AlertHistoryModel alertHistoryrequst = null;

            foreach (var alertId in request.alertIDs)
            {
                alertHistoryrequst = CreateAlertHistoryObj(request, _userData);
                alertHistoryrequst.AlertId = alertId;
                alertHistoryrequst.HistoryDescription = "Alert status changed , Alert Id : " + alertId +
                    " Alert status changed  to :" + Constants.Alert_Escalate_Status
                                                   + " , Owner changed to :" + _userData.OwnerUserName
                                                    + " Changed BY :" + _userData.UpdatedUseName;

                alertHistoryList.Add(alertHistoryrequst);

                if (!string.IsNullOrEmpty(request.Notes))
                {
                    alertHistoryrequst = CreateAlertHistoryObj(request, _userData);
                    alertHistoryrequst.AlertId = alertId;

                    alertHistoryrequst.HistoryDescription = "Alert notes added : , Alert Id : " + alertId
                        + " Notes : " + request.Notes
                     + "  By :" + _userData.UpdatedUseName;
                    alertHistoryList.Add(alertHistoryrequst);
                }
            }
            _alertHistoryBL.AddRangealertHistory(alertHistoryList);

            //if (string.IsNullOrEmpty(repoResponse.Result))
            //{
            //    returnobj.IsSuccess = true;
            //    returnobj.Message = "Success";
            //    returnobj.HttpStatusCode = HttpStatusCode.OK;
            //}
            //else
            //{
            //    returnobj.IsSuccess = false;
            //    returnobj.Message = "Failed";
            //    returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            //    //returnobj.ErrorMessage = repoResponse.Result;
            //}

            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.HttpStatusCode = HttpStatusCode.OK;


            return returnobj;
        }

        private static AlertHistoryModel CreateAlertHistoryObj(AddToExclusionRequest request, (string OwnerUserName, string UpdatedUseName) _userData)
        {
            return new AlertHistoryRequest()
            {
                HistoryDate = request.CreatedDate,
                CreatedUser = _userData.UpdatedUseName,
                OrgId = request.OrgID,
                CreatedUserId = request.CreatedUserId


            };
        }
        private  AlertHistoryModel CreateAlertHistoryObj(AddToBlocklistForThreatsRequest request, (string OwnerUserName, string UpdatedUseName) _userData)
        {
            return new AlertHistoryRequest()
            {
                HistoryDate = request.ModifiedDate,
                CreatedUser = _userData.UpdatedUseName,
                OrgId = request.OrgID,
                CreatedUserId = request.ModifiedUserId


            };
        }
        private  AlertHistoryModel CreateAlertHistoryObj(DisconnectFromNetworkRequest request, (string OwnerUserName, string UpdatedUseName) _userData)
        {
            return new AlertHistoryRequest()
            {
                HistoryDate = request.ModifiedDate,
                CreatedUser = _userData.UpdatedUseName,
                OrgId = request.OrgId,
                CreatedUserId = request.ModifiedUserId


            };
        }

        private static AlertHistoryModel CreateAlertHistoryObj(AddToNetworkRequest request, (string OwnerUserName, string UpdatedUseName) _userData)
        {
            return new AlertHistoryRequest()
            {
                HistoryDate = request.ModifiedDate,
                CreatedUser = _userData.UpdatedUseName,
                OrgId = request.OrgID,
                CreatedUserId = request.ModifiedUserId
            };
        }

        private static AlertHistoryModel CreateAlertHistoryObj(SentinalOneActionRequest request, (string OwnerUserName, string UpdatedUseName) _userData)
        {
            return new AlertHistoryRequest()
            {
                HistoryDate = request.ModifiedDate,
                CreatedUser = _userData.UpdatedUseName,
                OrgId = request.OrgId,
                CreatedUserId = request.ModifiedUserId


            };
        }
        private static AlertHistoryModel CreateAlertHistoryObj(AlertBaseCommonRequest request, (string OwnerUserName, string UpdatedUseName) _userData)
        {
            return new AlertHistoryRequest()
            {
                HistoryDate = request.ModifiedDate,
                CreatedUser = _userData.UpdatedUseName,
                OrgId = request.OrgId,
                CreatedUserId = request.ModifiedUserId


            };
        }

        public SetAlertStatusResponse SetAlertIrrelavantStatus(SetAlertSpecificStatusRequest request)
        {
            SetAlertStatusResponse returnobj = new SetAlertStatusResponse();

            SetMultipleAlertStatusRequest setMultipleAlertStatusRequest = new SetMultipleAlertStatusRequest();

            var _userData = GetUserDetails(request.OwnerID, request.ModifiedUserId);

            var _statusId = _plattformBL.GetMasterDataByDataValue(Constants.AlertStatusType, Constants.Alert_Irrelavant_Status);

            setMultipleAlertStatusRequest.alertIDs = request.alertIDs;
            setMultipleAlertStatusRequest.ModifiedUser = _userData.UpdatedUseName;
            setMultipleAlertStatusRequest.ModifiedDate = request.ModifiedDate;
            setMultipleAlertStatusRequest.StatusName = Constants.Alert_Irrelavant_Status;
            setMultipleAlertStatusRequest.OwnerName = _userData.OwnerUserName;
            setMultipleAlertStatusRequest.OwnerID = request.OwnerID;
            setMultipleAlertStatusRequest.Notes = request.Notes;
            setMultipleAlertStatusRequest.StatusID = _statusId;
            var repoResponse = _repo.SetMultipleAlertsStatus(setMultipleAlertStatusRequest, _userData.UpdatedUseName);

            if (!string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                return returnobj;
            }

            if (!string.IsNullOrEmpty(request.Notes))
            {
                alert_note note = null;
                List<alert_note> alertNotes = new List<alert_note>();
                foreach (var alertId in request.alertIDs)
                {
                    note = new alert_note()
                    {
                        action_id = setMultipleAlertStatusRequest.StatusID,
                        action_name = Constants.Alert_Irrelavant_Status,
                        action_type = Constants.AlertStatusType,
                        alert_id = alertId,
                        Created_date = request.ModifiedDate,
                        Created_user = _userData.UpdatedUseName,
                        notes = request.Notes,
                        notes_date = request.ModifiedDate,
                        notes_to_userid = request.OwnerID,
                    };
                    alertNotes.Add(note);

                }

                var noteres = _repo.AddRangeAlertNote(alertNotes);
            }

            // updating user action data 
            SetMultipleUserActionStatusRequest useractionRequest = new SetMultipleUserActionStatusRequest()
            {
                ActionType = Constants.User_Action_Alert_Type,
                Ids = request.alertIDs,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = _userData.UpdatedUseName,
                OwnerId = request.OwnerID,
                OwnerName = _userData.OwnerUserName,
                StatusId = _statusId,
                StatusName = Constants.Alert_Irrelavant_Status
            };
            _useractionBL.SetMultipleUserActionStatus(useractionRequest);

            List<AlertHistoryModel> alertHistoryrequstList = new List<AlertHistoryModel>();
            AlertHistoryModel alertHistoryrequst = null;
            //new AlertHistoryRequest()
            //{

            //    HistoryDate = request.ModifiedDate.Value.ToUniversalTime(),
            //    CreatedUser = _userData.UpdatedUseName,
            //    OrgId = request.OrgId,
            //    CreatedUserId = request.ModifiedUserId
            //};
            foreach (var alertId in request.alertIDs)
            {
                alertHistoryrequst = CreateAlertHistoryObj(request, _userData);
                alertHistoryrequst.AlertId = alertId;

                alertHistoryrequst.HistoryDescription = "Alert status changed , Alert Id : " + alertId +
                   " Alert status changed  to :" + Constants.Alert_Irrelavant_Status
                                        + " Changed BY :" + _userData.UpdatedUseName;


                alertHistoryrequstList.Add(alertHistoryrequst);

                if (!string.IsNullOrEmpty(request.Notes))
                {
                    alertHistoryrequst = CreateAlertHistoryObj(request, _userData);
                    alertHistoryrequst.AlertId = alertId;
                    alertHistoryrequst.HistoryDescription = "Alert notes added: , Alert Id : " + alertId
                + " Notes : " + request.Notes + "  By :" + _userData.UpdatedUseName;


                    alertHistoryrequstList.Add(alertHistoryrequst);
                }
            }
            _alertHistoryBL.AddRangealertHistory(alertHistoryrequstList);

            //if (string.IsNullOrEmpty(repoResponse.Result))
            //{
            //    returnobj.IsSuccess = true;
            //    returnobj.Message = "Success";
            //    returnobj.HttpStatusCode = HttpStatusCode.OK;
            //}
            //else
            //{
            //    returnobj.IsSuccess = false;
            //    returnobj.Message = "Failed";
            //    returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            //    //returnobj.ErrorMessage = repoResponse.Result;
            //}

            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.HttpStatusCode = HttpStatusCode.OK;

            return returnobj;
        }

        public SetAlertPriorityResponse SetAlertPriority(SetAlertPriorityRequest request)
        {
            var _userData = GetUserDetails(0, request.ModifiedUserId);
            SetAlertPriorityResponse returnobj = new SetAlertPriorityResponse();

            var repoResponse = _repo.SetAlertPriority(request, _userData.UpdatedUseName);

            //Update the user actions if any 
            SetUserActionPriorityRequest priorityRequst = new SetUserActionPriorityRequest()
            {
                ActionType = Constants.User_Action_Alert_Type,
                Id = request.AlertID,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = _userData.UpdatedUseName,
                PriorityId = request.PriorityID,
                PriorityValue = request.PriorityValue
            };
            var useractionRes = _useractionBL.SetUserActiontPriority(priorityRequst);

            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                //returnobj.ErrorMessage = repoResponse.Result;
            }
            AlertHistoryRequest alertHistoryrequst = new AlertHistoryRequest()
            {
                AlertId = request.AlertID,
                HistoryDate = request.ModifiedDate,
                CreatedUser = _userData.UpdatedUseName,
                HistoryDescription = "Alert priority changed , Alert Id : " + request.AlertID +
                " Alert priority changed  to :" + request.PriorityValue
                                                 + " Changed BY :" + _userData.UpdatedUseName,
                OrgId = request.OrgId,
                CreatedUserId = request.ModifiedUserId
            };
            _alertHistoryBL.AddalertHistory(alertHistoryrequst);

            return returnobj;
        }

        public SetAlertSevirityResponse SetAlertSevirity(SetAlertSevirityRequest request)
        {
            SetAlertSevirityResponse returnobj = new SetAlertSevirityResponse();
            var _userData = GetUserDetails(0, request.ModifiedUserId);
            var repoResponse = _repo.SetAlertSevirity(request, _userData.UpdatedUseName);

            if (!string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed to update the sevirity";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                //returnobj.ErrorMessage = new repoResponse.Result;
                return returnobj;
            }

            //Update the user actions if any 
            SetUserActionSeviarityRequest severityRequst = new SetUserActionSeviarityRequest()
            {
                ActionType = Constants.User_Action_Alert_Type,
                Id = request.AlertID,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = _userData.UpdatedUseName,
                Sevirity = request.Sevirity,
                SevirityId = request.SevirityId
            };
            var useractionRes = _useractionBL.SetUserActiontSeviarity(severityRequst);

            AlertHistoryRequest alertHistoryrequst = new AlertHistoryRequest()
            {
                AlertId = request.AlertID,
                HistoryDate = request.ModifiedDate,
                CreatedUser = _userData.UpdatedUseName,
                HistoryDescription = "Alert seviarity changed , Alert Id : " + request.AlertID +
                " Alert seviarity changed  to :" + request.Sevirity
                                             + " Changed BY :" + _userData.UpdatedUseName,
                OrgId = request.OrgId,
                CreatedUserId = request.ModifiedUserId
            };
            _alertHistoryBL.AddalertHistory(alertHistoryrequst);


            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.HttpStatusCode = HttpStatusCode.OK;


            return returnobj;
        }

        public AssignAlertTagResponse AssignAlertTag(AssignAlertTagsRequest request)
        {
            AssignAlertTagResponse returnobj = new AssignAlertTagResponse();

            var _userData = GetUserDetails(0, request.ModifiedUserId);

            var repoResponse = _repo.AssignAlertTag(request, _userData.UpdatedUseName);

            AlertHistoryRequest alertHistoryrequst = new AlertHistoryRequest()
            {
                AlertId = request.AlertID,
                HistoryDate = request.ModifiedDate,
                CreatedUser = _userData.UpdatedUseName,
                HistoryDescription = "Alert Tag Added/changed , Alert Id : " + request.AlertID +
                " Alert Tag added/changed :" + request.TagText
                                           + " Changed BY :" + _userData.UpdatedUseName,
                OrgId = request.OrgId,
                CreatedUserId = request.ModifiedUserId
            };
            _alertHistoryBL.AddalertHistory(alertHistoryrequst);

            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                //returnobj.ErrorMessage = repoResponse.Result;
            }

            return returnobj;
        }

        public AssignAlertScoresResponse AssignAlertScores(AssignAlertScoresRequest request)
        {
            AssignAlertScoresResponse returnobj = new AssignAlertScoresResponse();
            var _userData = GetUserDetails(0, request.ModifiedUserId);
            var repoResponse = _repo.AssignAlertScores(request, _userData.UpdatedUseName);

            AlertHistoryRequest alertHistoryrequst = new AlertHistoryRequest()
            {
                AlertId = request.AlertID,
                HistoryDate = request.ModifiedDate,
                CreatedUser = _userData.UpdatedUseName,
                HistoryDescription = "Alert score Added/changed , Alert Id : " + request.AlertID +
                " Alert score added/changed :" + request.Score
                                           + " Changed BY :" + _userData.UpdatedUseName,
                OrgId = request.OrgId,
                CreatedUserId = request.ModifiedUserId
            };
            _alertHistoryBL.AddalertHistory(alertHistoryrequst);

            AssignUserActionScoresRequest UserActionScoreRequest = new AssignUserActionScoresRequest()
            {
                ActionType = Constants.User_Action_Alert_Type,
                Id = request.AlertID,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = _userData.UpdatedUseName,
                Score = request.Score
            };
            var useractionRes = _useractionBL.AssignUserActionScore(UserActionScoreRequest);


            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                //returnobj.ErrorMessage = repoResponse.Result;
            }

            return returnobj;
        }

        public SetAlertPositiveAnalysisResponse SetAlertPositiveAnalysis(SetAlertPositiveAnalysisRequest request)
        {
            SetAlertPositiveAnalysisResponse returnobj = new SetAlertPositiveAnalysisResponse();

            var repoResponse = _repo.SetAlertPositiveAnalysis(request);

            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                //returnobj.ErrorMessage = repoResponse.Result;
            }

            return returnobj;
        }

        #endregion unUsedMethods

        #region DashboardOperations

        public getUnattendedAlertcountResponse GetUnAttendedAlertsCount(GetUnAttendedAlertCount request)
        {

            var statusId = _plattformBL.GetMasterDataByDataValue(Constants.AlertStatusType, Constants.AlertNewStatus);
            getUnattendedAlertcountResponse response = new getUnattendedAlertcountResponse();
            response.UnattendedAlertsCount = _repo.GetUnAttendedAlertsCount(request, statusId).Result;
            response.IsSuccess = true;
            response.Message = "Success";
            return response;
        }

        public GetAlertsCountByPriorityAndStatusResponse GetAlertsCountByPriorityAndStatus(GetAlertsCountByPriorityAndStatusRequest request)
        {
            GetAlertsCountByPriorityAndStatusResponse response = new GetAlertsCountByPriorityAndStatusResponse();
            response.AlertsCount = _repo.GetAlertsCountByPriorityAndStatus(request).Result;
            response.IsSuccess = true;
            response.Message = "Success";
            return response;
        }

        public GetFalsePositiveAlertsCountResponse GetFalsePositiveAlertsCount(GetAlertsCountByPositiveAnalysisRequest request)
        {
            GetFalsePositiveAlertsCountResponse response = new GetFalsePositiveAlertsCountResponse();
            var falsepositiveid = _plattformBL.GetMasterDataByDataValue(Constants.alert_positive_analysisType, Constants.FALSE_POSITIVE_Value);
            request.PositiveAnalysisID = falsepositiveid;
            response.AlertsCount = _repo.GetAlertsCountByPositiveAnalysis(request).Result;
            response.IsSuccess = true;
            response.Message = "Success";
            return response;
        }

       

        public GetAlertsResolvedMeanTimeResponse GetAlertsResolvedMeanTime(GetAlertsResolvedMeanTimeRequest request)
        {
            GetAlertsResolvedMeanTimeResponse response = new GetAlertsResolvedMeanTimeResponse();
            var res = _repo.GetAlertsResolvedMeanTime(request).Result;
            if (res > 0)
            {
                TimeSpan t = TimeSpan.FromSeconds(res);
                response.AlertsResolvedMeanTime = String.Format("{0}d{1}h{2}m", t.Days, t.Hours, t.Minutes);
            }
            else
            {
                response.AlertsResolvedMeanTime = "0";
            }

            response.IsSuccess = true;
            response.Message = "Success";
            return response;
        }

        public GetAlertsMostUsedTagsResponse GetAlertsMostUsedTags(GetAlertsMostUsedTagsRequest request)
        {

            GetAlertsMostUsedTagsResponse response = new GetAlertsMostUsedTagsResponse();
            var alerts = _repo.GetAlertsMostUsedTages(request).Result;

            if (alerts != null && alerts.Count > 0)
            {
                var count = _plattformBL.GetMasterDataByDatType(new LDPMasterDataRequest() { MaserDataType = "mostusedtags_greaterthanCount" });

                int intcount = 0;

                int.TryParse(count.MasterData[0].DataValue.ToString(), out intcount);
                var groupalerts = from alert in alerts
                                  group alert by alert.observable_tag into alertgroup
                                  where alertgroup.Count() > intcount
                                  select new
                                  {
                                      Tag = alertgroup.Key,
                                      Tagcount = alertgroup.Count()
                                  };

                response.MostUsedTags = groupalerts.OrderByDescending(o => o.Tagcount).Select(alert => alert.Tag).ToList();
            }
            else
            {
                response.MostUsedTags = null;
            }

            response.IsSuccess = true;
            response.Message = "Success";
            return response;
        }

        public GetAlertsTrendDataResponse GetAlertsTrendData(GetAlertsTrendDatasRequest request)
        {
            GetAlertsTrendDataResponse response = new GetAlertsTrendDataResponse();

            var hoursdata = _plattformBL.GetMasterDataByDatType(new LDPMasterDataRequest() { MaserDataType = "alert_trend" });

            var inthoursData = hoursdata.MasterData.Select(s => int.Parse(s.DataValue)).ToList();

            //inthoursData.Add(0);

            int maxhour = inthoursData.Max();

            inthoursData.Sort();

            var alerts = _repo.GetAlertsTrendData(request, maxhour);

            if (alerts.Result != null && alerts.Result.Count > 0)
            {
                response.AlertsTrendDatas = new List<AlertTrendData>();

                AlertTrendData alertdata = new AlertTrendData()
                {
                    AlertsCount = 0,
                    TrendHours = 0
                };
                response.AlertsTrendDatas.Add(alertdata);
                DateTime rangeEndTime = alerts.Result[0].detected_time.Value;
                DateTime rangeStartTime = alerts.Result[0].detected_time.Value;
                foreach (int alert in inthoursData)
                {
                    rangeStartTime = rangeStartTime.AddHours(-alert);
                    alertdata = new AlertTrendData();
                    alertdata.TrendHours = alert;

                    var alertcount = alerts.Result.Where(a => a.detected_time >= rangeStartTime
                    && a.detected_time <= rangeEndTime
                    ).Count();

                    //rangeEndTime = rangeStartTime;
                    alertdata.AlertsCount = alertcount;
                    response.AlertsTrendDatas.Add(alertdata);
                }
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.AlertsTrendDatas = null;
                response.IsSuccess = false;
                response.Message = "No data found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }


            return response;
        }
 #endregion DashboardOperations

        public GetAlertNoteResponse GetAlertNotesByAlertID(GetAlertNoteRequest request)
        {
            GetAlertNoteResponse response = new GetAlertNoteResponse();
            var dbresult = _repo.GetAlertNotesByAlertID(request);
            if (dbresult.Result != null)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedData = _mapper.Map<List<alert_note>, List<AlertNoteModel>>(dbresult.Result);
                response.AlertNotesList = _mappedData;
            }
            else
            {
                response.Message = "Notes not found for requested alert id";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public string UpdateAlertIncidentMappingId(List<double> alertIds, int alertIncidentMapId)
        {
            return _repo.UpdateAlertIncidentMappingId(alertIds, alertIncidentMapId).Result;
        }
        public SetAlertStatusResponse UpdateAlertStatus(UpdateAlertStatusRequest request)
        {
            List<AlertHistoryModel> historyRequestList = new List<AlertHistoryModel>();
            AlertHistoryModel historyRequest = null;
            SetAlertStatusResponse returnobj = new SetAlertStatusResponse();

            var _userData = GetUserDetails(0, request.ModifiedUserId);
            var _masterData = GetMasterData(request.StatusId, 0, 0, 0, 0);

            var statusupdateResponse = _repo.UpdateAlertStatus(request, _userData.UpdatedUseName, _masterData.StatusName);

            if (!string.IsNullOrEmpty(statusupdateResponse.Result))
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                return returnobj;
            }


            SetMultipleUserActionStatusRequest statusRequst = new SetMultipleUserActionStatusRequest()
            {
                ActionType = Constants.User_Action_Alert_Type,
                Ids = request.AlertIds,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = _userData.UpdatedUseName,
                StatusId = request.StatusId,
                StatusName = _masterData.StatusName
            };


            var useractionRes = _useractionBL.SetMultipleUserActionStatus(statusRequst);


            //Add data into alert History
            //foreach (var alertId in request.AlertIds)
            //{
            //    historyRequest = new AlertHistoryRequest();
            //    historyRequest.AlertId = alertId;
            //    historyRequest.CreatedUser = _userData.UpdatedUseName;
            //    historyRequest.HistoryDate = request.ModifiedDate;
            //    historyRequest.HistoryDescription = $"Alert status set to {_masterData.StatusName}";
            //    historyRequest.CreatedUserId = request.ModifiedUserId;
            //    historyRequest.OrgId = request.OrgID;

            //    historyRequestList.Add(historyRequest);

            //}

            //_alertHistoryBL.AddRangealertHistory(historyRequestList);

            AddToHistory(request.AlertIds, $"Alert status updated to {_masterData.StatusName}", request.OrgID, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);
            //Update the status on Organization tool 
            // Here need to bring factory 
            // get object based on tool 
            //As of now calling the Sentinalone API directly
            var _toolres = UpdateStatusinTool(request, _masterData.StatusName);
            //
            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.HttpStatusCode = HttpStatusCode.OK;

            return returnobj;
        }
        private bool UpdateStatusinTool(UpdateAlertStatusRequest request, string statusname)
        {
            string _alertStatus = Constants.Sentinal_Status_Mapper[statusname];


            var toolStatusUpdateResponse = _sentinalOneOplBL.UpdateThreatDetails(request.OrgID, request.AlertIds, _alertStatus, null);


            return true;
        }

        /// <summary>
        /// ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UpdateAlertResponse UpdateAlert(UpdateAlertRequest request)
        {
            //
            bool isUserActionInsert = false;

            UpdateAlertResponse response = new UpdateAlertResponse();
            //Get the Alert details from DB
            var _alertData = _repo.GetAlertData(new GetAlertRequest() { alertID = request.AlertId }).Result.FirstOrDefault();
            if (_alertData == null)
            {
                response.IsSuccess = false;
                response.Message = "alert data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }

            List<AlertHistoryModel> historyRequestList = new List<AlertHistoryModel>();
            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();
            AddActivityRequest addActivityRequest = null; 
            AlertHistoryModel historyRequest = null;
            //
            // Get the user action if any for alert 
            var _userData = GetUserDetails(request.OwnerUserId, request.ModifiedUserId);
            var _masterData = GetMasterData(request.StatusId, request.PriorityId, request.SeverityId, request.ObservableTagId,request.AnalystVerdictId);

            var _useractiondata = _useractionBL.GetUserActionsByActionTypeRefID(new GetUserActionByActionTypeRefIDRequest()
            {
                ActionType = Constants.User_Action_Alert_Type,
                ActionTypeRefId = request.AlertId
            }).UserActionData;
            // if user assigned .. create the user action object if not found 
            if (_useractiondata == null && request.OwnerUserId != _alertData.owner_user_id)
            {
                isUserActionInsert = true;
                _useractiondata = new UserActionRequest()
                {
                    ActionType = Constants.User_Action_Incident_Type,
                    ActionTypeRefid = request.AlertId,
                    ActionDate = request.ModifiedDate,
                    CreatedDate = request.ModifiedDate,
                    ActionStatus = _alertData.status_ID,
                    ActionStatusName = _alertData.status,
                    Description = _alertData.name,
                    Owner = _alertData.owner_user_id,
                    OwnerName = _alertData.owner_user_name,
                    Priority = _alertData.priority_id,
                    PriorityName = _alertData.priority_name,
                    Score = _alertData.score,
                    Severity = _alertData.severity_name,
                    SeverityId = _alertData.severity_id,
                    CreatedUser = _userData.UpdatedUseName
                };

            };

            //Update if status changed 
            if (request.StatusId != _alertData.status_ID)
            {

                _alertData.status_ID = request.StatusId;
                _alertData.status = _masterData.StatusName;

                if (_useractiondata != null)
                {
                    _useractiondata.ActionStatus = request.StatusId;
                    _useractiondata.ActionStatusName = _masterData.StatusName;

                }

                
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("StatusName", _masterData.StatusName);
                _templateData.Add("AlerId", request.AlertId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Status, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true ,request.AlertId ,1));


            }

            //Update if priority changed 
            if (request.PriorityId != _alertData.priority_id)
            {

                _alertData.priority_id = request.PriorityId;
                _alertData.priority_name = _masterData.PriorityName;

                if (_useractiondata != null)
                {
                    _useractiondata.Priority = request.PriorityId;
                    _useractiondata.PriorityName = _masterData.PriorityName;

                }

                //historyRequest = CreateHistoryobj(request, _userData);
                //historyRequest.HistoryDescription = $"Alert priority set to {_masterData.PriorityName}";
                //historyRequestList.Add(historyRequest);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("PriorityName", _masterData.PriorityName);
                _templateData.Add("AlerId", request.AlertId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Priority, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true, request.AlertId, 1));


            }

            //Update if severity changed 
            if (request.SeverityId != _alertData.severity_id)
            {

                _alertData.severity_id = request.SeverityId;
                _alertData.severity_name = _masterData.SeverityName;

                if (_useractiondata != null)
                {
                    _useractiondata.SeverityId = request.SeverityId;
                    _useractiondata.Severity = _masterData.SeverityName;

                }
                //historyRequest = CreateHistoryobj(request, _userData);
                //historyRequest.HistoryDescription = $"Alert severity set to {_masterData.SeverityName}";
                //historyRequestList.Add(historyRequest);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("SeviarityName", _masterData.SeverityName);
                _templateData.Add("AlerId", request.AlertId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Severity, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true, request.AlertId, 0));


            }
            //
            //Update if owner changed 
            if (request.OwnerUserId != _alertData.owner_user_id)
            {

                _alertData.owner_user_id = request.OwnerUserId;
                _alertData.owner_user_name = _userData.OwnerUserName;


                if (_useractiondata != null)
                {
                    _useractiondata.Owner = request.OwnerUserId;
                    _useractiondata.OwnerName = _userData.OwnerUserName;

                }


                //historyRequest = CreateHistoryobj(request, _userData);
                //historyRequest.HistoryDescription = $"Alert owner assigned {_userData.OwnerUserName}";
                //historyRequestList.Add(historyRequest);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AssignUser", _userData.OwnerUserName);
                _templateData.Add("AlerId", request.AlertId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Severity, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true, request.AlertId, 0));

            }
            //
            //Update if Tag changed 
            if (request.ObservableTagId != _alertData.observable_tag_ID)
            {

                _alertData.observable_tag_ID = request.ObservableTagId;
                _alertData.observable_tag = _masterData.TagName;


                //historyRequest = CreateHistoryobj(request, _userData);
                //historyRequest.HistoryDescription = $"Alert observable tag set {_masterData.TagName}";
                //historyRequestList.Add(historyRequest);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("TagValue", _masterData.TagName);
                _templateData.Add("AlerId", request.AlertId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Alert_Tag_Assigned, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true, request.AlertId, 0));

            }

            //Update if score changed 
            if (request.Score != _alertData.score)
            {
                _alertData.score = request.Score;

                if (_useractiondata != null)
                {
                    _useractiondata.Score = request.Score;
                }


                //historyRequest = CreateHistoryobj(request, _userData);

                //historyRequest.HistoryDescription = $"Alert score assigned {request.Score}";

                //historyRequestList.Add(historyRequest);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("ScoreValue", request.Score);
                _templateData.Add("AlerId", request.AlertId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Alert_Score_Assigned, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true, request.AlertId, 0));

            }
            if (!string.IsNullOrEmpty(request.AlertNote))
            {
                alert_note note = new alert_note()
                {
                    action_id = request.AlertId,
                    action_name = "Update",
                    action_type = "Alert Update",
                    alert_id = request.AlertId,
                    Created_date = request.ModifiedDate,
                    Created_user = _userData.UpdatedUseName,
                    notes = request.AlertNote,
                    notes_date = request.ModifiedDate
                    //notes_to_userid = request.OwnerID,
                };


                var noteres = _repo.AddAlertNote(note);

                // Notes capturing in Alert History
                //historyRequest = CreateHistoryobj(request, _userData);


                //historyRequest.HistoryDescription = $"Alert notes :  {request.AlertNote}";

                //historyRequestList.Add(historyRequest);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("NoteText", note.notes);
                _templateData.Add("AlerId", request.AlertId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Alert_Alert_Note, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true, request.AlertId, 1));


            }
            if (request.AnalystVerdictId != _alertData.positive_analysis_id)
            {
                _alertData.positive_analysis_id = request.AnalystVerdictId;
                _alertData.positive_analysis = _masterData.AnalystVerdictName;
                if (_masterData.AnalystVerdictName == Constants.LDC_Analyst_Verdict_FalsePositive)
                {
                    _alertData.false_positive = 1;
                }
                else
                {
                    _alertData.false_positive = 0;
                }
            }
            // updating the alert
            var updateres = _repo.UpdateAlert(_alertData);
            if (updateres.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to update";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            else
            {
                if (historyRequestList.Count > 0)
                {
                    _alertHistoryBL.AddRangealertHistory(historyRequestList);
                }
                if (isUserActionInsert && _useractiondata != null)
                {
                    _useractionBL.AddUserAction(_useractiondata);
                }
                if (!isUserActionInsert && _useractiondata != null)
                {
                    _useractionBL.UpdateUserAction(_useractiondata);
                }

                // update the data in tool 

                if (request.StatusId != _alertData.status_ID)
                {
                    string _alertStatus = Constants.Sentinal_Status_Mapper[_masterData.StatusName];


                    var toolStatusUpdateResponse = _sentinalOneOplBL.UpdateThreatDetails(request.OrgID, new List<double>() { request.AlertId }, _alertStatus, null);
                }
                if (request.AnalystVerdictId != _alertData.positive_analysis_id)
                {
                    UpdateAnalystVerdictOnTool(new List<double>() { request.AlertId }, request.OrgID, _masterData.AnalystVerdictName,_userData.UpdatedUseName , request.ModifiedUserId, request.ModifiedDate);
                    Dictionary<string, string> _templateDataAV = new Dictionary<string, string>();
                    _templateDataAV.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateDataAV.Add("AnalystVerdictName", _masterData.AnalystVerdictName);
                    _templateDataAV.Add("AlerId", request.AlertId.ToString());

                    activityLogList.Add
                        (BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Analyst_Verdict, _templateDataAV, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true, request.AlertId, 1));


                }
                if (!string.IsNullOrEmpty(request.AlertNote))
                {
                   var res = UpdateNotesinTool(new List<double>() { request.AlertId }, request.OrgID, request.AlertNote);
                }

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AlertNumber", request.AlertNote);
                //AddActivityLog(Constants.Activity_Template_Alert_Update, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true);
                //Dictionary<string, string> _templateData = new Dictionary<string, string>();
                //_templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                //_templateData.Add("NoteText", note.notes);
                //_templateData.Add("AlerId", request.AlertId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Alert_Update, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true, request.AlertId, 0));

                _commonBL.LogActivity(activityLogList);


                response.IsSuccess = true;
                response.Message = "success";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            return response;
        }
        private void AddActivityLog(string templateConstant , Dictionary<string, string>  templateData , int createdUserId , DateTime createdDate , int orgId , bool isSuccess)
        {
             _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = createdUserId,
                OrgId = orgId,
                CreatedDate = createdDate,
                ActivityDate = createdDate ,
            }, templateData, templateConstant, isSuccess
            );
        }
        private AddActivityRequest BuildActivityLogObj(string templateConstant, Dictionary<string, string> templateData, int createdUserId, DateTime? createdDate, int orgId, bool isSuccess, double alertId , int activityExistinToolAndLDC)
        {
           return  new AddActivityRequest()
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
        public SetAnalystVerdictResponse SetAnalystVerdict(SetAnalystVerdictRequest request)
        {
            SetAnalystVerdictResponse response = new SetAnalystVerdictResponse();
            var _userData = GetUserDetails(0, request.ModifiedUserId);

            var _masterdata = _plattformBL.GetLDPMasterDataByID(request.AnalystVerdictId);

            if (_masterdata != null)
            {
                // update in plattform
                SetAnalystVerdictDTO dto = new SetAnalystVerdictDTO();
                dto.AlertIds = request.AlertIds;
                dto.OrgID = request.OrgID;
                dto.AlertIds = request.AlertIds;
                dto.AnalystVerdictId = request.AnalystVerdictId;
                dto.AnalystVerdictValue = _masterdata.MasterData.DataValue;
                dto.ModifiedDate = request.ModifiedDate;
                dto.ModifiedUser = _userData.UpdatedUseName;
                //
                var res = _repo.SetAnalystVerdict(dto).Result;
                if (!string.IsNullOrEmpty(res))
                {
                    response.IsSuccess = false;
                    response.Message = res;
                    response.HttpStatusCode = HttpStatusCode.InternalServerError;
                }

                //updating into sentinal tool
               var toolResponse =  UpdateAnalystVerdictOnTool(request.AlertIds, request.OrgID, dto.AnalystVerdictValue, _userData.UpdatedUseName, request.ModifiedUserId , request.ModifiedDate);
                List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();
                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateDataAV = new Dictionary<string, string>();
                    _templateDataAV.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateDataAV.Add("AnalystVerdictName", _masterdata.MasterData.DataValue);
                    _templateDataAV.Add("AlerId", alert.ToString());

                    activityLogList.Add
                        (BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Analyst_Verdict, _templateDataAV, request.ModifiedUserId, request.ModifiedDate, request.OrgID, true, alert, 1));

                }

                var activityRes = _commonBL.LogActivity(activityLogList);

                response.IsSuccess = toolResponse.IsSuccess;
                response.Message = toolResponse.Message;
                response.HttpStatusCode = toolResponse.HttpStatusCode;
                response.errors = toolResponse.errors;
            }
            return response;

        }

        private UpdateThreatAnalysisVerdictResponse UpdateAnalystVerdictOnTool( List<double> alertIds , int orgId, string analystVerdictName , string modifiedUserName ,  int modifiedUserId, DateTime? modifiedDateTime)
        {
            SentinalOneUpdateAnalystVerdictRequest toolrequest = new SentinalOneUpdateAnalystVerdictRequest();
            toolrequest.AlertIds = alertIds;
            toolrequest.OrgId = orgId;
            string _strAnalystVerdict = string.Empty;
            if (analystVerdictName == Constants.LDC_Analyst_Verdict_Undefined)
            {
                toolrequest.Undefined = true;
                _strAnalystVerdict = Constants.LDC_Analyst_Verdict_Undefined;
            }
            if (analystVerdictName == Constants.LDC_Analyst_Verdict_Suspicious)
            {
                toolrequest.Suspicious = true;
                _strAnalystVerdict = Constants.LDC_Analyst_Verdict_Suspicious;
            }
            if (analystVerdictName == Constants.LDC_Analyst_Verdict_TruePositived)
            {
                toolrequest.TruePositive = true;
                _strAnalystVerdict = Constants.LDC_Analyst_Verdict_TruePositived;
            }
            if (analystVerdictName == Constants.LDC_Analyst_Verdict_FalsePositive)
            {
                toolrequest.FalsePositive = true;
                _strAnalystVerdict = Constants.LDC_Analyst_Verdict_FalsePositive;
            }

            var toolresponse = _sentinalOneOplBL.UpdateThreatAnalysisVerdict(toolrequest);

            // Updating the history
            AlertHistoryModel historyRequest = null;
            List<AlertHistoryModel> historyRequestList = new List<AlertHistoryModel>();
            foreach (var alertid in alertIds)
            {
                historyRequest = new AlertHistoryModel();
                historyRequest.AlertId = alertid;
                historyRequest.CreatedUser = modifiedUserName;
                historyRequest.HistoryDate = modifiedDateTime;//.Value.ToUniversalTime();

                historyRequest.CreatedUserId = modifiedUserId;
                historyRequest.OrgId = orgId;

                historyRequest.HistoryDescription = $"Alert analyst verdict set to {_strAnalystVerdict}";
                historyRequestList.Add(historyRequest);
            }
            _alertHistoryBL.AddRangealertHistory(historyRequestList);

            return toolresponse;
        }

        private static AlertHistoryRequest CreateHistoryobj(UpdateAlertRequest request, (string OwnerUserName, string UpdatedUseName) _userData)
        {
            AlertHistoryRequest historyRequest = new AlertHistoryRequest();
            historyRequest.AlertId = request.AlertId;
            historyRequest.CreatedUser = _userData.UpdatedUseName;
            historyRequest.HistoryDate = request.ModifiedDate;//.Value.ToUniversalTime();

            historyRequest.CreatedUserId = request.ModifiedUserId;
            historyRequest.OrgId = request.OrgID;
            return historyRequest;
        }

        public (string OwnerUserName, string UpdatedUseName) GetUserDetails(int OwnerUserId, int ModifiedUserId)
        {
            List<int> userIds = new List<int>();

            string _ownerUserName = string.Empty;
            string _UpdatedUserName = string.Empty;
            if (OwnerUserId > 0)
            {
                userIds.Add(OwnerUserId);
            }

            if (ModifiedUserId > 0)
            {
                userIds.Add(ModifiedUserId);
            }

            List<SelectUserModel> users = null;
            if (userIds.Count > 0)
            {
                users = _securityBl.GetUserbyIds(userIds).UsersList;
            }
            if (OwnerUserId > 0)
            {
                _ownerUserName = users.Where(user => user.UserID == OwnerUserId).FirstOrDefault().Name;
            }

            if (ModifiedUserId > 0)
            {
                _UpdatedUserName = users.Where(user => user.UserID == ModifiedUserId).FirstOrDefault().Name;
            }
            return (OwnerUserName: _ownerUserName, UpdatedUseName: _UpdatedUserName);
        }

        public (string StatusName, string PriorityName, string SeverityName, string TagName, string AnalystVerdictName) GetMasterData(int statusId, int priorityId, int severityId, int tagId , int analystVerdictId)
        {
            List<string> masterdatatypes = new List<string>()
                                            {
                                            "alert_status","alert_priority","alert_Sevirity","alert_Tags","analyst_verdict"
                                            };

            LDPMasterDataByMultipleTypesRequest masterdatarequest = new LDPMasterDataByMultipleTypesRequest();
            masterdatarequest.MaserDataTypes = masterdatatypes;
            var masterdata = _plattformBL.GetMasterDataByMultipleTypes(masterdatarequest);
            string _Priority_Name = string.Empty;
            string _Severity_Name = string.Empty;
            string _Status_Name = string.Empty;
            string _tag_Name = string.Empty;
            string _analyst_Veridct_Name = string.Empty;

            if (masterdata != null && masterdata.MasterData.Count > 0)
            {
                if (priorityId > 0)
                {
                    var mdata = masterdata.MasterData.Where(md => md.DataID == priorityId).FirstOrDefault();
                    if (mdata != null)
                    {
                        _Priority_Name = mdata.DataValue;
                    }
                }

                if (severityId > 0)
                {
                    var mdata1 = masterdata.MasterData.Where(md => md.DataID == severityId).FirstOrDefault();
                    if (mdata1 != null)
                    {
                        _Severity_Name = mdata1.DataValue;
                    }
                }

                if (statusId > 0)
                {
                    var mdata2 = masterdata.MasterData.Where(md => md.DataID == statusId).FirstOrDefault();
                    if (mdata2 != null)
                    {
                        _Status_Name = mdata2.DataValue;
                    }
                }

                if (tagId > 0)
                {
                    var mdata3 = masterdata.MasterData.Where(md => md.DataID == tagId).FirstOrDefault();
                    if (mdata3 != null)
                    {
                        _tag_Name = mdata3.DataValue;
                    }
                }

                if (analystVerdictId > 0)
                {
                    var mdata4 = masterdata.MasterData.Where(md => md.DataID == analystVerdictId).FirstOrDefault();
                    if (mdata4 != null)
                    {
                        _analyst_Veridct_Name = mdata4.DataValue;
                    }
                }
            }

            return (StatusName: _Status_Name, PriorityName: _Priority_Name, SeverityName: _Severity_Name, TagName: _tag_Name , AnalystVerdictName:_analyst_Veridct_Name);
        }
        public getAlertsResponse GetalertsByAlertIds(List<double> request)
        {
            getAlertsResponse returnobj = new getAlertsResponse();

            List<Alerts> alertsdata = null;

            alertsdata = _repo.GetalertsByAlertIds(request);
            if (alertsdata.Count > 0)
            {
                var _mappedResponse = _mapper.Map<List<Alerts>, List<AlertModel>>(alertsdata);

                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.AlertsList = _mappedResponse;
                returnobj.TotalOffenseCount = 0;
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "No data found";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                returnobj.TotalOffenseCount = 0;
            }

            return returnobj;
        }

        public AlertEscalateActionResponse AlertEscalateAction(AlertEscalateActionRequest request)
        {
            AlertEscalateActionResponse returnobj = new AlertEscalateActionResponse();

            var _userData = GetUserDetails(request.OwnerUserId, request.ModifiedUserId);


            var repoResponse = _repo.AlertEscalateAction(request, _userData.OwnerUserName, _userData.UpdatedUseName);

            if (!string.IsNullOrEmpty(repoResponse.Result))
            {

                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
               // return returnobj;
            }
            else
            { 
            List<AlertHistoryRequest> alertHistoryRequestList = new List<AlertHistoryRequest>();
            // adding notes

            if (!string.IsNullOrEmpty(request.Notes))
            {
                alert_note note = null;
                List<alert_note> alertNotes = new List<alert_note>();
                foreach (var alertId in request.alertIDs)
                {
                    note = new alert_note()
                    {
                        action_id = Constants.AlertEscalateActionId,
                        action_name = Constants.AlertEscalateActionName,
                        action_type = Constants.AlertActionType,
                        alert_id = alertId,
                        Created_date = request.ModifiedDate,
                        Created_user = _userData.UpdatedUseName,
                        notes = request.Notes,
                        notes_date = request.ModifiedDate,
                        notes_to_userid = request.OwnerUserId,
                    };
                    alertNotes.Add(note);
                }

                var noteres = _repo.AddRangeAlertNote(alertNotes);

            }
            // updating user action data 
            //SetMultipleUserActionStatusRequest useractionRequest = new SetMultipleUserActionStatusRequest()
            //{
            //    ActionType = Constants.User_Action_Alert_Type,
            //    Ids = request.alertIDs,
            //    ModifiedDate = request.ModifiedDate,
            //    ModifiedUser = _userData.UpdatedUseName,
            //    OwnerId = request.OwnerUserId,
            //    OwnerName = _userData.OwnerUserName,
            //    //StatusId = _escalateStatusID,
            //    //StatusName = Constants.Alert_Escalate_Status
            //};
            //_useractionBL.SetMultipleUserActionStatus(useractionRequest);

            // Add into alert history
            //List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
            //AlertHistoryModel alertHistoryrequst = null;
            //List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();
            //foreach (var alertId in request.alertIDs)
            //{
            //    //alertHistoryrequst = CreateAlertHistoryObj(request, _userData);
            //    //alertHistoryrequst.AlertId = alertId;

            //    //if (string.IsNullOrEmpty(request.Notes))
            //    //{
            //    //    alertHistoryrequst.HistoryDescription = $"Escalation to {_userData.OwnerUserName} ";
            //    //}
            //    //else
            //    //{
            //    //    alertHistoryrequst.HistoryDescription = $"Escalation to {_userData.OwnerUserName}  with Notes : {request.Notes}";
            //    //}
            //    //alertHistoryList.Add(alertHistoryrequst);



            //}
            //_alertHistoryBL.AddRangealertHistory(alertHistoryList);

            if (!string.IsNullOrEmpty(request.Notes))
            {
                var toolres = UpdateNotesinTool(request.alertIDs, request.OrgId, request.Notes);
            }

            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.HttpStatusCode = HttpStatusCode.OK;
        }

            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            foreach (var alert in request.alertIDs)
            {
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AlerId", alert.ToString());

                var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Escalate_Action, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, returnobj.IsSuccess, alert, 0);

                    if (!string.IsNullOrEmpty(request.Notes))
                {
                    activityobj.AdditionalText = " , Notes : " + request.Notes;
                }
                activityLogList.Add(activityobj);
            }

            var activityRes = _commonBL.LogActivity(activityLogList);

            return returnobj;
        }
        

        public AlertIgnoreORIrrelavantActionRespnse IgnoreORIrrelavantAction(AlertIgnoreORIrrelavantActionRequest request)
        {
            AlertIgnoreORIrrelavantActionRespnse returnobj = new AlertIgnoreORIrrelavantActionRespnse();

            var _userData = GetUserDetails(0, request.ModifiedUserId);

            var _masterData = _plattformBL.GetMasterDataobjectByDataValue(Constants.LDC_Type_Analyst_Verdict, Constants.LDC_Analyst_Verdict_FalsePositive);
            int _ldc_Analyst_Verdict_FalsePositive_id = 0;
            if (_masterData != null) _ldc_Analyst_Verdict_FalsePositive_id = _masterData.DataID;

            var repoResponse = _repo.IgnoreORIrrelavantAction(request, _ldc_Analyst_Verdict_FalsePositive_id, _userData.UpdatedUseName);

            if (!string.IsNullOrEmpty(repoResponse.Result))
            {

                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                
            }
            else
            {
                List<AlertHistoryRequest> alertHistoryRequestList = new List<AlertHistoryRequest>();
                // adding notes

                if (!string.IsNullOrEmpty(request.Notes))
                {
                    alert_note note = null;
                    List<alert_note> alertNotes = new List<alert_note>();
                    foreach (var alertId in request.alertIDs)
                    {
                        note = new alert_note()
                        {
                            action_id = Constants.AlertIgnoreIrrelavantActionId,
                            action_name = Constants.AlertIgnoreIrrelavantActionName,
                            action_type = Constants.AlertActionType,
                            alert_id = alertId,
                            Created_date = request.ModifiedDate,
                            Created_user = _userData.UpdatedUseName,
                            notes = request.Notes,
                            notes_date = request.ModifiedDate,
                            notes_to_userid = request.ModifiedUserId
                        };
                        alertNotes.Add(note);
                    }

                    var noteres = _repo.AddRangeAlertNote(alertNotes);

                }
                // updating user action data 
                //SetMultipleUserActionStatusRequest useractionRequest = new SetMultipleUserActionStatusRequest()
                //{
                //    ActionType = Constants.User_Action_Alert_Type,
                //    Ids = request.alertIDs,
                //    ModifiedDate = request.ModifiedDate,
                //    ModifiedUser = _userData.UpdatedUseName,
                //    OwnerId = request.OwnerUserId,
                //    OwnerName = _userData.OwnerUserName,
                //    //StatusId = _escalateStatusID,
                //    //StatusName = Constants.Alert_Escalate_Status
                //};
                //_useractionBL.SetMultipleUserActionStatus(useractionRequest);

                // Add into alert history
                //List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
                //AlertHistoryModel alertHistoryrequst = null;

                //foreach (var alertId in request.alertIDs)
                //{
                //    alertHistoryrequst = CreateAlertHistoryObj(request, _userData);
                //    alertHistoryrequst.AlertId = alertId;

                //    if (string.IsNullOrEmpty(request.Notes))
                //    {
                //        alertHistoryrequst.HistoryDescription = $"Action : {Constants.AlertIgnoreIrrelavantActionName} ";
                //    }
                //    else
                //    {
                //        alertHistoryrequst.HistoryDescription = $"Action : {Constants.AlertIgnoreIrrelavantActionName} with Notes : {request.Notes}";
                //    }
                //    alertHistoryList.Add(alertHistoryrequst);


                //}
                //_alertHistoryBL.AddRangealertHistory(alertHistoryList);

                //

                if (!string.IsNullOrEmpty(request.Notes))
                {
                    var toolres = UpdateNotesinTool(request.alertIDs, request.OrgId, request.Notes);
                }
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;

            }
            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            foreach (var alert in request.alertIDs)
            {
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AlerId", alert.ToString());
                var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Ignore_Action, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, returnobj.IsSuccess, alert, 0);

                if (!string.IsNullOrEmpty(request.Notes))
                {
                    activityobj.AdditionalText = " , Notes : " + request.Notes;
                }
                activityLogList.Add(activityobj);

            }

            var activityRes = _commonBL.LogActivity(activityLogList);

            // Activities


            return returnobj;
        }

        public MitigateActionResponse AlertMitigateAction(MitigateActionRequest request)
        {
            //TODO 
            // "DetectionType":"static"
            // Remediate and Rollback actions not allowed 

            MitigateActionResponse methodResponse = new MitigateActionResponse();

            var _userData = GetUserDetails(0, request.ModifiedUserId);
            //List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
            //AlertHistoryModel alertHistoryrequst = null;

            //foreach (var alertId in request.AlertIds)
            //{
            //    alertHistoryrequst = CreateAlertHistoryObj(request, _userData);
            //    alertHistoryrequst.AlertId = alertId;

            //    if (string.IsNullOrEmpty(request.Notes))
            //    {
            //        alertHistoryrequst.HistoryDescription = $"Mitigate action by {_userData.OwnerUserName} ";
            //    }
            //    else
            //    {
            //        alertHistoryrequst.HistoryDescription = $"Mitigate action by {_userData.OwnerUserName}  with Notes : {request.Notes}";
            //    }
            //    alertHistoryList.Add(alertHistoryrequst);


            //}
            //_alertHistoryBL.AddRangealertHistory(alertHistoryList);
            //alertHistoryList = new List<AlertHistoryModel>();

            //AddToHistory(request.AlertIds, "Mitigate action -  Kill performed successfully", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate, request.Notes);


            //if (!string.IsNullOrEmpty(request.Notes))
            //{
            //    // var toolres = UpdateNotesinTool(request.AlertIds, request.OrgId, request.Notes);

            //    AddAlertNote(request, null, Constants.AlertActionType, Constants.AlertAddNoteActionId, Constants.AlertAddNoteActionName);
            //}
            //

            //
            // Actions
            // 1. Kill action
            ThreatActionResponse actionResponse = null;
            if (request.Kill)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Kill);


                // AddToHistory(request.AlertIds, "Mitigate action -  Kill performed successfully", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);

                //if (actionResponse.IsSuccess == false)
                //{
                //    methodResponse.IsSuccess = false;
                //    methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                //    methodResponse.Message = actionResponse.Message;
                //    return methodResponse;

                //}

                List<AddActivityRequest> killactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());

                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Kill_Action, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, actionResponse.IsSuccess, alert, 1);

                    killactivityLogList.Add(activityobj);
                }

                var _killactivityRes = _commonBL.LogActivity(killactivityLogList);



            }
            if (request.Quarantine)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Quarantine);

                // AddToHistory(request.AlertIds, "Mitigate action -  Qurarantine performed successfully", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);

                //if (actionResponse.IsSuccess == false)
                //{
                //    methodResponse.IsSuccess = false;
                //    methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                //    methodResponse.Message = actionResponse.Message;
                //    return methodResponse;

                //}

                List<AddActivityRequest> _qurantinveactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());

                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Quarantine_Action, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, actionResponse.IsSuccess, alert, 1);

                    _qurantinveactivityLogList.Add(activityobj);
                }

                var quractivityRes = _commonBL.LogActivity(_qurantinveactivityLogList);
            }
            if (request.Remediate)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Remediate);

                // AddToHistory(request.AlertIds, "Mitigate action -  Remediate performed successfully", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);


                //if (actionResponse.IsSuccess == false)
                //{
                //    methodResponse.IsSuccess = false;
                //    methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                //    methodResponse.Message = actionResponse.Message;
                //    return methodResponse;
                //}
                List<AddActivityRequest> _RMAactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());

                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Remediate_Action, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, actionResponse.IsSuccess, alert, 1);

                    _RMAactivityLogList.Add(activityobj);
                }
                var _RMactivityRes = _commonBL.LogActivity(_RMAactivityLogList);
            }
            if (request.Rollback)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Rollback_Remediation);
                // AddToHistory(request.AlertIds, "Mitigate action -  Rollback performed successfully", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);

                //if (actionResponse.IsSuccess == false)
                //{
                //    methodResponse.IsSuccess = false;
                //    methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                //    methodResponse.Message = actionResponse.Message;
                //    return methodResponse;

                //}

                List<AddActivityRequest> RBactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());

                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Rollback_Action, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, actionResponse.IsSuccess, alert, 1);

                    RBactivityLogList.Add(activityobj);
                }

              var RBactivityRes= _commonBL.LogActivity(RBactivityLogList);
            }

            // Analyst Verdict 

            UpdateThreatAnalysisVerdictResponse updateAnslystVerdictResponse = null;

            if (request.AnalystVerdict_TruePositive)
            {
                updateAnslystVerdictResponse = _sentinalOneOplBL.UpdateThreatAnalysisVerdict(request.OrgId, request.AlertIds, Constants.SentinalOne_Analysis_Verdict_TruePositive);
                // AddToHistory(request.AlertIds, $"Mitigate action -  updated the analyst verdict - {Constants.SentinalOne_Analysis_Verdict_TruePositive}", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);

                List<AddActivityRequest> AVTPactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());
                    _templateData.Add("AnalystVerdictName", Constants.LDC_Analyst_Verdict_TruePositived);
                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Analyst_Verdict, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, updateAnslystVerdictResponse.IsSuccess, alert, 1);

                    AVTPactivityLogList.Add(activityobj);
                }
                var AVTPactivityRes = _commonBL.LogActivity(AVTPactivityLogList);

            }
            else if (request.AnalystVerdict_FalsePositive)
            {
                updateAnslystVerdictResponse = _sentinalOneOplBL.UpdateThreatAnalysisVerdict(request.OrgId, request.AlertIds, Constants.SentinalOne_Analysis_Verdict_FalsePositive);
                // AddToHistory(request.AlertIds, $"Mitigate action -  updated the analyst verdict - {Constants.SentinalOne_Analysis_Verdict_FalsePositive}", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);
                List<AddActivityRequest> AVFPactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());
                    _templateData.Add("AnalystVerdictName", Constants.LDC_Analyst_Verdict_FalsePositive);
                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Analyst_Verdict, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, updateAnslystVerdictResponse.IsSuccess, alert, 1);

                    AVFPactivityLogList.Add(activityobj);
                }
                var AVFPactivityRes = _commonBL.LogActivity(AVFPactivityLogList);
            }
            else if (request.AnalystVerdict_Suspecious)
            {
                updateAnslystVerdictResponse = _sentinalOneOplBL.UpdateThreatAnalysisVerdict(request.OrgId, request.AlertIds, Constants.SentinalOne_Analysis_Verdict_Suspicious);
                //AddToHistory(request.AlertIds, $"Mitigate action -  updated the analyst verdict - {Constants.SentinalOne_Analysis_Verdict_Suspicious}", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);
                List<AddActivityRequest> AVactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());
                    _templateData.Add("AnalystVerdictName", Constants.LDC_Analyst_Verdict_Suspicious);
                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Analyst_Verdict, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, updateAnslystVerdictResponse.IsSuccess, alert, 1);

                    AVactivityLogList.Add(activityobj);
                }
                var _avactivityRes = _commonBL.LogActivity(AVactivityLogList);

            }
            //

            //if (updateAnslystVerdictResponse != null && !updateAnslystVerdictResponse.IsSuccess)
            //{
            //    methodResponse.IsSuccess = false;
            //    methodResponse.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
            //    methodResponse.Message = actionResponse.Message;
            //    return methodResponse;
            //}
            if (request.AddToBlockedList)
            {
                AddToBlocklistForThreatsRequest addtoBlockListRequest = new AddToBlocklistForThreatsRequest()
                {
                    AlertIds = request.AlertIds,
                    OrgID = request.OrgId,
                    TargetScope = request.Scope,
                    Note = request.Notes
                };
                var addtoBlockListResponse = _sentinalOneOplBL.AddToblockListForThreats(addtoBlockListRequest);
                //AddToHistory(request.AlertIds, $"Mitigate action -  Add to blocklist successfully performed", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate,request.Notes);
                List<AddActivityRequest> ABLactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());
                   // _templateData.Add("AnalystVerdictName", Constants.LDC_Analyst_Verdict_Suspicious);
                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Add_to_Blocklist, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, addtoBlockListResponse.IsSuccess, alert, 1);
                    if (!string.IsNullOrEmpty(request.Notes)) 
                    {
                        activityobj.AdditionalText = $" , Notes : {request.Notes}";
                    }

                    ABLactivityLogList.Add(activityobj);
                }
                var ABLactivityRes = _commonBL.LogActivity(ABLactivityLogList);

            }
           
            // Status update 

            if (request.ResolvedStatus)
            {
                var _statusId = _plattformBL.GetMasterDataByDataValue(Constants.AlertStatusType, Constants.Alert_Closed_Status);
               var statusUpdateRes =  UpdateAlertStatus(new UpdateAlertStatusRequest()
                {
                     AlertIds = request.AlertIds,
                     OrgID = request.OrgId,
                     ModifiedUserId = request.ModifiedUserId,
                     ModifiedDate = request.ModifiedDate,
                     StatusId = _statusId
                });

                List<AddActivityRequest> statusactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());
                     _templateData.Add("StatusName", Constants.Alert_Closed_Status);
                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Assign_Status, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, statusUpdateRes.IsSuccess, alert, 1);

                    statusactivityLogList.Add(activityobj);
                }
                var  _assignStatusactivityRes = _commonBL.LogActivity(statusactivityLogList);

            

            // var res = _sentinalOneOplBL.UpdateThreatDetails(request.OrgId, request.AlertIds, Constants.SentinalOne_Resolved_Status, null);
            // AddToHistory(request.AlertIds, $"Mitigate action -  Updated status to Closed", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);

            }

            if (!string.IsNullOrEmpty(request.Notes))
            {
                AddAlertNoteRequest noteRequest = new AddAlertNoteRequest()
                {
                    OrgID = request.OrgId,
                    alertIDs = request.AlertIds,
                    Notes = request.Notes,
                    ModifiedDate = request.ModifiedDate,
                    ModifiedUserId = request.ModifiedUserId
                };

                //var noteRes = _sentinalOneOplBL.AddThreatNotes(noteRequest);
                var noteRes = AddAlertNote(noteRequest, null, Constants.AlertMitigateActionType, Constants.AlertAddNoteActionId, Constants.AlertAddNoteActionName);

                List<AddActivityRequest> noteactivityLogList = new List<AddActivityRequest>();

                foreach (var alert in request.AlertIds)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                    _templateData.Add("AlerId", alert.ToString());
                    _templateData.Add("NoteText", request.Notes);
                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Alert_Note, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, noteRes.IsSuccess, alert, 1);

                    noteactivityLogList.Add(activityobj);
                }
                var activityRes1 = _commonBL.LogActivity(noteactivityLogList);

            }
            // 
            methodResponse.IsSuccess = true;
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            methodResponse.Message = "Alert mitigate action completed ";
            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            foreach (var alert in request.AlertIds)
            {
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AlerId", alert.ToString());
               // _templateData.Add("NoteText", request.Notes);
                var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Mitigate_Action, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, methodResponse.IsSuccess, alert, 1);

                activityLogList.Add(activityobj);
            }
            var activityRes = _commonBL.LogActivity(activityLogList);


            return methodResponse;

            //return _sentinalOneOplBL.ThreatMitigateAction(request);
        }

        public ThreatActionResponse AlertAction(ThreatActionRequest request)
        {

            ThreatActionResponse actionResponse = null;
            List<string> actionList = new List<string>();
            if (request.Kill)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Kill);

          //      if (!actionResponse.IsSuccess) return actionResponse;
                actionList.Add(Constants.Activity_Template_Alert_Kill_Action);
            }
            if (request.Quarantine)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Quarantine);

          //        if (!actionResponse.IsSuccess) return actionResponse;
                actionList.Add(Constants.Activity_Template_Alert_Quarantine_Action);
            }
            if (request.Remediate)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Remediate);

        //          if (!actionResponse.IsSuccess) return actionResponse;
                actionList.Add(Constants.Activity_Template_Alert_Remediate_Action);
            }
            if (request.Rollback)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Rollback_Remediation);

    //            if (!actionResponse.IsSuccess) return actionResponse;
                actionList.Add(Constants.Activity_Template_Alert_Rollback_Action);
            }
            if (request.UnQuarantine)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_UnQuarantine);

  //              if (!actionResponse.IsSuccess) return actionResponse;
                actionList.Add(Constants.Activity_Template_Alert_UnQuarantine_Action);
            }
            if (request.NetworkQuarantine)
            {
                actionResponse = _sentinalOneOplBL.ThreatAction(request.OrgId, request.AlertIds, Constants.SentinalOne_Action_Network_Quarantine);

//                if (!actionResponse.IsSuccess) return actionResponse;
               // actionList.Add(Constants.SentinalOne_Action_Network_Quarantine);
            }
            if (actionResponse != null)
            {
                if (actionList.Count > 0)
                {
                    List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();
                    Dictionary<string, string> _templateData = null;
                    var _userData = GetUserDetails(0, request.ModifiedUserId);
                    //List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
                    //AlertHistoryModel alertHistoryrequst = null;
                    foreach (var alertId in request.AlertIds)
                    {
                        foreach (var action in actionList)
                        {
                            _templateData = new Dictionary<string, string>();
                            _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                            _templateData.Add("AlerId", alertId.ToString());
                            var activityobj = BuildActivityLogObj(action, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, true, alertId, 1);
                            activityLogList.Add(activityobj);

                        }

                    }
                    var activityRes = _commonBL.LogActivity(activityLogList);

                   // _alertHistoryBL.AddRangealertHistory(alertHistoryList);
                }

                return actionResponse;
            }
            else
            {
                actionResponse = new ThreatActionResponse();
                actionResponse.IsSuccess = false;
                actionResponse.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                actionResponse.Message = "No alert actions are selected  ";
            }
            return actionResponse;
        }

        public AddToNetworkResponse AddToNetwork(AddToNetworkRequest request)
        {
            var _userData = GetUserDetails(0, request.ModifiedUserId);
            //List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
            //AlertHistoryModel alertHistoryrequst = null;

            
            //_alertHistoryBL.AddRangealertHistory(alertHistoryList);
            // AddToHistory(request.AlertIds, "Agent connected from network action performed successfully", request.OrgID, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);

            var res = _sentinalOneOplBL.AddToNetwork(request);

            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            foreach (var alert in request.AlertIds)
            {
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AlerId", alert.ToString());

                var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Add_to_Network, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, res.IsSuccess, alert, 1);

                activityLogList.Add(activityobj);
            }

            var activityRes = _commonBL.LogActivity(activityLogList);

            return res;

        }

        public DisconnectFromNetworkResponse DisconnectFromNetwork(DisconnectFromNetworkRequest request)
        {
            var _userData = GetUserDetails(0, request.ModifiedUserId);

            //AddToHistory(request.AlertIds, "Agent disconnected from network action performed successfully", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);
           
            var res =  _sentinalOneOplBL.DisconnectFromNetwork(request);

            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            foreach (var alert in request.AlertIds)
            {
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AlerId", alert.ToString());

                var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Disconnect_from_Network, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, res.IsSuccess, alert, 1);

                activityLogList.Add(activityobj);
            }

            var activityRes = _commonBL.LogActivity(activityLogList);

            return res;
        }

        private void AddToHistory(List<Double> alerIds , string historyDescription , int orgId , int modifiedUserId , string modifiedUserName , DateTime? modifiedDate , string notes = null )
        {
            List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
            AlertHistoryModel alertHistoryrequst = null;

            foreach (var alertId in alerIds)
            {
                alertHistoryrequst = new AlertHistoryRequest()
                {
                    HistoryDate = modifiedDate,
                    CreatedUser = modifiedUserName,
                    OrgId = orgId,
                    CreatedUserId = modifiedUserId,
                    AlertId = alertId ,
 
                 };
                if (string.IsNullOrEmpty(notes))
                {
                    alertHistoryrequst.HistoryDescription = historyDescription;
                }
                else
                {
                    alertHistoryrequst.HistoryDescription = $"{historyDescription}  with Notes : {notes}";
                }
                alertHistoryList.Add(alertHistoryrequst);

            }
            _alertHistoryBL.AddRangealertHistory(alertHistoryList);
        }

        public BlockListResponse GetBlockList(BlockListRequest request)
        {
            return _sentinelTabBl.GetBlockList(request);
        }
        public AddToBlocklistResponse AddToblockListForThreats(AddToBlocklistForThreatsRequest request)
        {
            var _userData = GetUserDetails(0, request.ModifiedUserId);

           // AddToHistory(request.AlertIds, "Add to blockedlist list action performed successfully", request.OrgID, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate , request.Note);

            
            if (!string.IsNullOrEmpty(request.Note))
            {
                var toolres = UpdateNotesinTool(request.AlertIds, request.OrgID, request.Note);
            }

           var res =  _sentinalOneOplBL.AddToblockListForThreats(request);


            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            foreach (var alert in request.AlertIds)
            {
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AlerId", alert.ToString());

                var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Add_to_Blocklist, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, res.IsSuccess, alert, 1);
                activityobj.AdditionalText = $" , Data :- Targert scope : {request.TargetScope} , Description : {request.Description} , Note : {request.Note}  ";
                activityLogList.Add(activityobj);
            }

            var activityRes = _commonBL.LogActivity(activityLogList);


            return res;
        }


        public AddToBlocklistResponse AddToblockList(AddToBlocklistRequest request)
        {
            var _userData = GetUserDetails(0, request.CreatedUserId);
            //List<double> alertIds = new List<double>();
            //alertIds.Add(0);
            //AddToHistory(alertIds, "Added to blockedlist list action performed successfully", request.OrgId, request.CreatedUserId, _userData.UpdatedUseName, request.CreatedDate);
            
            var res = _sentinalOneOplBL.AddToblockList(request);

            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            //foreach (var alert in request.AlertIds)
            //{
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AlerId", string.Empty);

                var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Add_to_Blocklist, _templateData, request.CreatedUserId, request.CreatedDate, request.OrgId, res.IsSuccess, 0, 1);
                activityobj.AdditionalText = $" , Data :- OS Type : {request.OSType} , Description : {request.Description} , Value : {request.Value}  ";
                activityLogList.Add(activityobj);
            //}

            return res;
        }
        public AddToBlocklistResponse UpdateAddToblockList(UpdateAddToBlocklistRequest request)
        {
            var _userData = GetUserDetails(0, request.ModifiedUserId);
            
            //List<double> alertIds = new List<double>();
            //alertIds.Add(0);
            //AddToHistory(alertIds, "Update blockedlist list item action performed successfully", 0, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);
            
            var res = _sentinalOneOplBL.UpdateAddToblockList(request);

            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
            _templateData.Add("AlerId", string.Empty);

            var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Add_to_Blocklist, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, res.IsSuccess, 0, 1);
            activityobj.AdditionalText = $" , Updated Data :- Id : {request.Id} , Type :{request.Type} , OS Type : {request.OSType} , Description : {request.Description} , Value : {request.Value}  ";
            activityLogList.Add(activityobj);
            var activityRes = _commonBL.LogActivity(activityLogList);

            return res;

        }

        public AddToBlocklistResponse DeleteAddToblockList(DeleteAddToBlocklistRequest request)
        {
            var _userData = GetUserDetails(0, request.DeletedUserId);
            //List<double> alertIds = new List<double>();
            //alertIds.Add(0);
            //AddToHistory(alertIds, "Delete blockedlist list item action performed successfully", 0, request.DeletedUserId, _userData.UpdatedUseName, request.DeletedDate);
            var res = _sentinalOneOplBL.DeleteAddToblockList(request);

            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
            _templateData.Add("AlerId", string.Empty);

            var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Add_to_Blocklist, _templateData, request.DeletedUserId, request.DeletedDate, request.OrgId, res.IsSuccess, 0, 1);
            activityobj.AdditionalText = $" , Delete Data :- Bloclist Ids :{string.Join(", ", request.Ids)}  ";
            activityLogList.Add(activityobj);
            var activityRes = _commonBL.LogActivity(activityLogList);
            return res;
        }
        public ExclustionsResponse GetExclusions(ExclusionRequest request)
        {
            return _sentinelTabBl.GetExclusions(request);
        }
        public AddToExclusionlistResponse AddToExclusionList(AddToExclusionRequest request)
        {
            var _userData = GetUserDetails(0, request.CreatedUserId);

           // AddToHistory(request.AlertIds, "Add to exclustion list action performed successfully", request.OrgID, request.CreatedUserId, _userData.UpdatedUseName, request.CreatedDate);

            var res = _sentinalOneOplBL.AddToExclusionList(request);

            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
            _templateData.Add("AlerId", string.Empty);

            var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Add_to_Exclusion, _templateData, request.CreatedUserId, request.CreatedDate, request.OrgID, res.IsSuccess, 0, 1);
            activityobj.AdditionalText = $" , Added Data :- OS Type : {request.OSType} , Description : {request.Description} , Value : {request.Value}  , Path exclusion type : {request.PathExclusionType} , Target scope : {request.TargetScope} ";
            activityLogList.Add(activityobj);

            return res;


        }
       public  AddToExclusionlistResponse UpdateAddToExclusionList(UpdateAddToExclusionRequest request)
        {
            var _userData = GetUserDetails(0, request.ModifiedUserId);

           // AddToHistory(new List<double>() { 0 }, "Update exclustion list item action performed successfully", request.OrgId, request.ModifiedUserId, _userData.UpdatedUseName, request.ModifiedDate);

            var res = _sentinalOneOplBL.UpdateAddToExclusionList(request);
            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
            _templateData.Add("AlerId", string.Empty);

            var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Add_to_Exclusion, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgId, res.IsSuccess, 0, 1);
            activityobj.AdditionalText = $" , Updated Data :- OS Type : {request.OSType} , Description : {request.Description} , Value : {request.Value}  , Path exclusion type : {request.PathExclusionType} , Target scope : {request.TargetScope} ";
            activityLogList.Add(activityobj);
            return res;
        }

        public AddToExclusionlistResponse DeleteAddToExclusionList(DeleteAddToExclusionRequest request)
        {
            var _userData = GetUserDetails(0, request.DeletedUserId);

           // AddToHistory(new List<double>() {0}, "Delete exclustion list item action performed successfully", request.OrgId, request.DeletedUserId, _userData.UpdatedUseName, request.DeletedDate);

            var res = _sentinalOneOplBL.DeleteAddToExclusionList(request);

            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
            _templateData.Add("AlerId", string.Empty);

            var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Add_to_Exclusion, _templateData, request.DeletedUserId, request.DeletedDate, request.OrgId, res.IsSuccess, 0, 1);
            activityobj.AdditionalText = $" , Delete Data :- Exclusion Ids :{string.Join(", ", request.Ids)}  ";
            activityLogList.Add(activityobj);

            return res;
        }
        public AddAlertNotesResponse AddAlertNote(AddAlertNoteRequest request, string notesuserName, string actionType, int actionId, string actionName)
        {
            string _noteUserName = string.Empty;

            if (string.IsNullOrEmpty(notesuserName))
            {
                var _userData = GetUserDetails(0, request.ModifiedUserId);
                _noteUserName = _userData.UpdatedUseName;
            }
            else
                _noteUserName = notesuserName;

            var res = new AddAlertNotesResponse();
            if (!string.IsNullOrEmpty(request.Notes))
            {
                alert_note note = null;
                List<alert_note> notesList = new List<alert_note>();

                foreach (var alertId in request.alertIDs)
                {
                    note = new alert_note()
                    {
                        action_id = actionId,
                        action_name = actionName,
                        action_type = actionType,
                        alert_id = alertId,
                        Created_date = request.ModifiedDate,
                        Created_user = _noteUserName,
                        notes = request.Notes,
                        notes_date = request.ModifiedDate
                        //notes_to_userid = request.OwnerID,
                    };
                    notesList.Add(note);


                }
                var repores = _repo.AddRangeAlertNote(notesList).Result;

                if (!string.IsNullOrEmpty(repores))
                {
                    res.IsSuccess = false;
                    res.Message = repores;
                    res.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                  //  return res;
                }
                else
                { 
             // Updating to client tool
                //
                    var toolres = UpdateNotesinTool(request.alertIDs, request.OrgID, request.Notes);

                }
                res.IsSuccess = true;
                res.Message = "Threat notes added successfully";
                res.HttpStatusCode = HttpStatusCode.OK;

                List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();

                foreach (var alertId in request.alertIDs)
                {
                    Dictionary<string, string> _templateData = new Dictionary<string, string>();
                    _templateData.Add(Constants.Acvities_Template_username, _noteUserName);
                    _templateData.Add("AlerId", alertId.ToString());
                    _templateData.Add("NoteText", request.Notes);
                    var activityobj = BuildActivityLogObj(Constants.Activity_Template_Alert_Escalate_Action, _templateData, request.ModifiedUserId, request.ModifiedDate, request.OrgID, res.IsSuccess, alertId, 1);
                    activityLogList.Add(activityobj);
                }
                var activityRes = _commonBL.LogActivity(activityLogList);
              
            }
            else
            {
                res.IsSuccess = false;
                res.Message = "Alert notes is empty";
                res.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return res;

        }

        private bool UpdateNotesinTool(List<double> alertIds, int orgId, string notes)
        {
            var toolNotesupdateRes = _sentinalOneOplBL.AddThreatNotes(new AddThreatNoteRequest()
            {
                AlertIds = alertIds,
                OrgID = orgId,
                Notes = notes
            });

            return toolNotesupdateRes.IsSuccess;
        }
    }
}
