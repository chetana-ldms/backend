using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Requests.Common;
using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Factories;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Interfaces;
using LDP_APIs.Models;
using System.Net;
using System.Text;

namespace LDP.Common.BL
{
    public class IncidentsBL : IIncidentsBL
    {
        IInternalIncidentsRepository _repo;
        private readonly IMapper _mapper;
        ILDPlattformBL _plattformBL;
        IAlertIncidentMappingBL _alertIncidentMappingBL;
        IUserActionBL _useractionBL;
        IAlertHistoryBL _alertHistoryBL;
        IAlertsBL _alertBl;
        ILDPSecurityBL _securityBl;
        IIncidentManagementService _service;
        IConfigurationDataBL _configDataBL;
        ILdpMasterDataBL _masterDataBL;
        ILDPlattformBL _platformBL;
        ICommonBL _commonBL;
        private readonly TicketManagementFactory _ticketMgmtFactory;

        public IncidentsBL(IInternalIncidentsRepository repo, IMapper mapper
            , ILDPlattformBL plattformBL
            , IAlertIncidentMappingBL alertIncidentMappingBL
            , IUserActionBL useractionBL
            , IAlertHistoryBL alertHistoryBL
            , IAlertsBL alertBl
            , ILDPSecurityBL securityBl
            , IIncidentManagementService service
            , TicketManagementFactory ticketMgmtFactory
            , IConfigurationDataBL configDataBL, ILdpMasterDataBL masterDataBL, ILDPlattformBL platformBL, ICommonBL commonBL)
        {
            _repo = repo;
            _mapper = mapper;
            _plattformBL = plattformBL;
            _alertIncidentMappingBL = alertIncidentMappingBL;
            _useractionBL = useractionBL;
            _alertHistoryBL = alertHistoryBL;
            _alertBl = alertBl;
            _securityBl = securityBl;
            _service = service;
            _ticketMgmtFactory = ticketMgmtFactory;
            _configDataBL = configDataBL;
            _masterDataBL = masterDataBL;
            _platformBL = platformBL;
            _commonBL = commonBL;
        }
        public CreateIncidentResponse CreateIncident(CreateIncidentRequest request)
        {
            string _createUserName = string.Empty;
            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();
            CreateIncidentResponse response = new CreateIncidentResponse();

            var _mappedRequest = _mapper.Map<CreateIncidentRequest, Incident>(request);
            _mappedRequest.internal_incident = 1;

            FillMasterData(_mappedRequest);

            List<int> userIds = new List<int>();

            FillUserData(request, _mappedRequest, userIds);
            _createUserName = _mappedRequest.Created_User;


            bool isSuccess = false;
            CreateIncidentInternalResponse res = null;
            OrganizationToolModel conndtl = null;
            int tooltypeID = 0;
            double _incidentId = 0;
            string _clientIncidentResponseText = string.Empty;
            string _clientIncidentId = string.Empty;
            int toolid = 0;
            // SEt the Description and Subject
            // 

            FillAlertData(request, _mappedRequest);

            tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_TicketManagement);

            if (!request.CreateInternalIncident)
            {
                conndtl = _plattformBL.GetToolConnectionDetails(request.OrgId, tooltypeID);
                if (conndtl != null)
                {
                    toolid = conndtl.ToolID;
                    request.Subject = _mappedRequest.incident_subject;

                    res = CreateIncidentForClientTools(request, conndtl);
                    _mappedRequest.internal_incident = 0;
                    if (!res.IsSuccess)
                    {
                        response.IsSuccess = false;
                        response.Message = "Incident creation failed";
                        response.HttpStatusCode = HttpStatusCode.BadRequest;
                        response.errors = res.errors;
                        return response;
                    }
                    _clientIncidentId = res.IncidentID.ToString();
                    _clientIncidentResponseText = res.IncidentJsonText;
                }
            }
            _mappedRequest.org_id = request.OrgId;
            _mappedRequest.tool_id = toolid;
            _mappedRequest.Owner = request.CreateUserId;
            _mappedRequest.Owner_Name = _mappedRequest.Created_User;
            res = _repo.CreateInternalIncident(_mappedRequest).Result;

            //}

            if (!res.IsSuccess)
            {
                response.IsSuccess = false;
                response.Message = "Incident creation failed";
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _createUserName);
                _templateData.Add("IncidentNumber", response.IncidentNumber);

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Create_Incident, _templateData, request.CreateUserId, request.CreateDate, request.OrgId, false, 0, 0));

                var logActivityRes = _commonBL.LogActivity(activityLogList);

                return response;
            }
            _incidentId = res.IncidentID;
            AddAlertIncidentMappingRequest mapRequest = AddAlertMappingData(request, _createUserName, tooltypeID, _incidentId, _clientIncidentResponseText, toolid,_clientIncidentId);


            var _alertIncidentMappingResponse = _alertIncidentMappingBL.AddAlertIncidentMapping(mapRequest);
            // Adding History data 
           // AddAlertHistoryData(request, _createUserName, _incidentId, mapRequest);

            if (res.IsSuccess)
            {
                // update the Alert mapping id
                _alertBl.UpdateAlertIncidentMappingId(request.AlertIDs, _alertIncidentMappingResponse.AlertIncidentMappingId);
                CreateUserActions(_mappedRequest, _incidentId);
                response.IsSuccess = true;
                response.Message = "Incident created";
                response.HttpStatusCode = HttpStatusCode.OK;
                response.IncidentNumber = res.IncidentID.ToString();

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _createUserName);
                _templateData.Add("IncidentNumber", response.IncidentNumber);

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Create_Incident, _templateData, request.CreateUserId, request.CreateDate, request.OrgId, true, _incidentId, 0));

                var logActivityRes = _commonBL.LogActivity(activityLogList);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Incident creation failed";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.IncidentNumber = "Not created";

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _createUserName);
                _templateData.Add("IncidentNumber", response.IncidentNumber);

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Create_Incident, _templateData, request.CreateUserId, request.CreateDate, request.OrgId, false, 0, 0));

                var logActivityRes = _commonBL.LogActivity(activityLogList);
            }
           

            

            return response;
        }

        private void CreateUserActions(Incident request, double _incidentId)
        {
            UserActionRequest _useractiondata = new UserActionRequest()
            {
                ActionType = Constants.User_Action_Incident_Type,
                ActionTypeRefid = _incidentId,
                ActionDate = request.Created_Date,
                CreatedDate = request.Created_Date,
                ActionStatus = request.Incident_Status,
                ActionStatusName = request.Incident_Status_Name,
                Description = request.Description,
                Owner = request.Owner,
                OwnerName = request.Owner_Name,
                Priority = request.Priority,
                PriorityName = request.Priority_Name,
                Score = request.Score,
                Severity = request.Severity_Name,
                SeverityId = request.Severity,
                CreatedUser = request.Created_User
            };

            _useractionBL.AddUserAction(_useractiondata);
        }

    
    private void AddAlertHistoryData(CreateIncidentRequest request, string _createUserName, double _incidentId, AddAlertIncidentMappingRequest mapRequest)
        {
            List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
            AlertHistoryModel alertHistory = null;
            //
            foreach (var alertid in request.AlertIDs)
            {
                alertHistory = new AlertHistoryRequest()
                {
                    AlertId = alertid,
                    HistoryDate = request.CreateDate,
                    CreatedUser = _createUserName,
                    OrgId = request.OrgId,
                    IncidentId = 0,
                    CreatedUserId = request.CreateUserId,
                    HistoryDescription = $"Incident # {mapRequest.incidentnumber} created "
                   
                };
                alertHistoryList.Add(alertHistory);
            }

            alertHistory = new AlertHistoryModel()
            {
                AlertId = 0,
                HistoryDate = request.CreateDate,
                CreatedUser = _createUserName,
                OrgId = request.OrgId,
                IncidentId = _incidentId,
                CreatedUserId = request.CreateUserId,
                HistoryDescription = $"Incident # {mapRequest.incidentnumber} created "

            };
            alertHistoryList.Add(alertHistory);
            alertHistory = new AlertHistoryModel()
            {
                AlertId = 0,
                HistoryDate = request.CreateDate,
                CreatedUser = _createUserName,
                OrgId = request.OrgId,
                IncidentId = _incidentId,
                CreatedUserId = request.CreateUserId,
                HistoryDescription = $"Owner assigned to  # {_createUserName}  "


            };
            alertHistoryList.Add(alertHistory);
            _alertHistoryBL.AddRangealertHistory(alertHistoryList);
        }

        private AddAlertIncidentMappingRequest AddAlertMappingData(CreateIncidentRequest request, string _createUserName
            , int tooltypeID, double _incidentId, string _clientIncidentResponseText, int toolid ,string _clientIncidentId)
        {
            AddAlertIncidentMappingRequest mapRequest = new AddAlertIncidentMappingRequest();
            mapRequest.incidentnumber = _incidentId;
            mapRequest.CreateDate = request.CreateDate;
            mapRequest.CreateUser = _createUserName;
            mapRequest.orgid = request.OrgId;
            mapRequest.SignificantIncident = request.SignificantIncident;
            mapRequest.tooltypeid = tooltypeID;
            mapRequest.toolid = toolid;
            mapRequest.incidentdata = _clientIncidentResponseText;
            mapRequest.ClientToolIncidentId = _clientIncidentId;
            mapRequest.AlertIncidentMappingDtl = new List<AddAlertIncidentMappingDtlModel>();
            AddAlertIncidentMappingDtlModel mapdtl = null;
            foreach (var alertid in request.AlertIDs)
            {
                mapdtl = new AddAlertIncidentMappingDtlModel();
                mapdtl.alertid = alertid;
                mapRequest.AlertIncidentMappingDtl.Add(mapdtl);
            }
           return mapRequest;
        }

        private void FillAlertData(CreateIncidentRequest request, Incident _mappedRequest)
        {
            List<AlertModel> _alerts;

            if (!string.IsNullOrEmpty(request.Description))
            {
                _mappedRequest.Description = request.Description;
            }
            else
            {
                if (request.AlertIDs.Count > 0)
                {
                    _alerts = _alertBl.GetalertsByAlertIds(request.AlertIDs).AlertsList;
                    if (_alerts != null && _alerts.Count > 0)
                    {
                        StringBuilder strDesc = new StringBuilder();
                        StringBuilder strEventId = new StringBuilder();
                        StringBuilder strIp = new StringBuilder();
                        StringBuilder strVendor = new StringBuilder();
                        StringBuilder strUser = new StringBuilder();
                        foreach (var alert in _alerts)
                        {
                            strDesc.AppendLine(alert.Name);
                            strDesc.AppendLine();

                            strEventId.AppendLine(alert.EventId);
                            strEventId.AppendLine();

                            strIp.AppendLine(alert.SourceIp);
                            strIp.AppendLine();


                            strUser.AppendLine(alert.DestinationUser);
                            strUser.AppendLine();

                            strVendor.AppendLine(alert.Vendor);
                            strVendor.AppendLine();


                        }
                        _mappedRequest.Description = strDesc.ToString();
                        request.Description = strDesc.ToString();
                        _mappedRequest.Event_ID = strEventId.ToString();
                        _mappedRequest.Source_IP = strIp.ToString();
                        _mappedRequest.Destination_User = strUser.ToString();
                        _mappedRequest.Vendor = strVendor.ToString();


                    }

                }
                if (!string.IsNullOrEmpty(request.Subject))
                {
                    _mappedRequest.incident_subject = request.Subject;
                }
                else
                {
                    if (_mappedRequest.Description.Length <= 100)
                    {
                        _mappedRequest.incident_subject = _mappedRequest.Description;
                    }
                    else
                    {
                        _mappedRequest.incident_subject = _mappedRequest.Description.Substring(0, 100);
                    }
                }
            }
        }
        private void FillMasterData(Incident _mappedRequest)
        {
            List<string> masterdatatypes = new List<string>()
                {
                "incident_status","incident_priority","incident_severity"
                };
            LDPMasterDataByMultipleTypesRequest masterdatarequest = new LDPMasterDataByMultipleTypesRequest();
            masterdatarequest.MaserDataTypes = masterdatatypes;
            var masterdata = _plattformBL.GetMasterDataByMultipleTypes(masterdatarequest);

            if (masterdata != null && masterdata.MasterData.Count > 0)
            {
                //foreach (var item in _mappedIncidents)
                //{
                if (_mappedRequest.Priority > 0)
                {
                    var mdata = masterdata.MasterData.Where(md => md.DataID == _mappedRequest.Priority).FirstOrDefault();
                    if (mdata != null)
                    {
                        _mappedRequest.Priority_Name = mdata.DataValue;
                    }
                }

                if (_mappedRequest.Severity > 0)
                {
                    var mdata1 = masterdata.MasterData.Where(md => md.DataID == _mappedRequest.Severity).FirstOrDefault();
                    if (mdata1 != null)
                    {
                        _mappedRequest.Severity_Name = mdata1.DataValue;
                    }
                }

                if (_mappedRequest.Incident_Status > 0)
                {
                    var mdata2 = masterdata.MasterData.Where(md => md.DataID == _mappedRequest.Incident_Status).FirstOrDefault();
                    if (mdata2 != null)
                    {
                        _mappedRequest.Incident_Status_Name = mdata2.DataValue;
                    }
                }
                else
                {
                    _mappedRequest.Incident_Status_Name = Constants.IncidentNewStatus;
                    _mappedRequest.Incident_Status = _plattformBL.GetMasterDataByDataValue(Constants.IncidentStatusType, Constants.IncidentNewStatus);
                }
            }
        }

        private void FillUserData(CreateIncidentRequest request, Incident _mappedRequest, List<int> userIds)
        {
            if (request.Owner > 0)
            {
                userIds.Add(request.Owner);
            }

            if (request.CreateUserId > 0)
            {
                userIds.Add(request.CreateUserId);
            }
            List<SelectUserModel> users = null;
            if (userIds.Count > 0)
            {
                users = _securityBl.GetUserbyIds(userIds).UsersList;
            }
            if (request.Owner > 0)
            {
                _mappedRequest.Owner_Name = users.Where(user => user.UserID == request.Owner).FirstOrDefault().Name;
            }

            if (request.CreateUserId > 0)
            {
                _mappedRequest.Created_User = users.Where(user => user.UserID == request.CreateUserId).FirstOrDefault().Name;
            }
        }

       

        private CreateIncidentInternalResponse CreateIncidentForClientTools(CreateIncidentRequest request, OrganizationToolModel conndtl)
        {
            CreateIncidentInternalResponse response = new CreateIncidentInternalResponse();
            CreateIncidentDTO createIncidentDTO = new CreateIncidentDTO();


            createIncidentDTO.APIUrl = conndtl.ApiUrl;
            createIncidentDTO.AuthKey = conndtl.AuthKey;
            createIncidentDTO.IncidentDescription = request.Description;
            createIncidentDTO.IncidentSubject = request.Subject;
            //createIncidentDTO.StatusId = request.statisO
            _service = _ticketMgmtFactory.GetInstance(conndtl.ToolID);
            
            var res = _service.CreateIncident(createIncidentDTO);
            

            return res.Result;
        }
        public GetIncidentsResponse GetIncidents(GetIncidentsRequest request)
        {

            GetIncidentsResponse response = new GetIncidentsResponse();

            List<Alerts>? alertsdata = null;
            // Get the user role
            bool isAdinuser = GetUserAdminFlag(request.LoggedInUserId);


            List<Incident> res = null;
            int tooltypeID = 0;
            OrganizationToolModel conndtl = null;
            List<GetIncidentModel> _mappedIncidents = null;

            res = _repo.GetInternalIncidents(request, isAdinuser).Result;
            if (res.Count == 0)
            {
                response.Message = "Incidents data not found";
                response.TotalIncidentsCount = 0;
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            _mappedIncidents = _mapper.Map<List<Incident>, List<GetIncidentModel>>(res);
            //
            var IncidentIDs = _mappedIncidents.Select(itm => itm.IncidentID);
            //
            var alertIncidentMaps = _alertIncidentMappingBL.GetAlertIncidentMappingByIncidentIDs(IncidentIDs.ToList()).AlertIncidentMappingData;

            //
            // Add can see client incident details / Normal user cannot see as incidents cannot assigned to him
            //
            GetIncidentsResponse _clientToolIncidentData = null;
         
        
            response.IsSuccess = true;
            response.Message = "Success";
            response.HttpStatusCode = HttpStatusCode.OK;
            //
            if (alertIncidentMaps == null)
            {
                response.IncidentList = _mappedIncidents.ToList();
                response.TotalIncidentsCount = _repo.GetInternalIncidentsCount(request, isAdinuser).Result;

                return response;
            }
            if (alertIncidentMaps != null)
            {

                foreach (var incident in _mappedIncidents)
                {
                    // var alertIncidentMapForIncident = alertIncidentMaps.Where(m => m.incidentnumber == incident.IncidentID).FirstOrDefault();

                    var alertIncidentMapForIncident = alertIncidentMaps.Where(m => m.incidentnumber == incident.IncidentID);

                    if (alertIncidentMapForIncident != null)
                    {
                        var alertIncidentMapForIncident1 = alertIncidentMapForIncident.FirstOrDefault();
                        incident.AlertIncidentMapping = new GetAlertIncidentMappingModel();
                        incident.AlertIncidentMapping = alertIncidentMapForIncident1;
                        if (alertIncidentMapForIncident1 != null)
                        incident.SignificantIncident = alertIncidentMapForIncident1.SignificantIncident;
                    }
                }
            }
            response.IncidentList = _mappedIncidents.ToList();
            response.TotalIncidentsCount = _repo.GetInternalIncidentsCount(request, isAdinuser).Result;

            return response;

        }
        public  GetIncidentResponse GetIncidentDetails(int incidentId)
        {
            GetIncidentResponse response = new GetIncidentResponse();

                       
            var res = _repo.GetInternalIncidentData(new GetInternalIncidentDataRequest()
            {
                 IncidentID = incidentId
            }).Result;
            if (res == null)
            {
                response.Message = "Incidents data not found";
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
           var  incidentModel = _mapper.Map<Incident,GetIncidentModel> (res);
            
            var alertIncidentMap = _alertIncidentMappingBL.GetAlertIncidentMappingByIncidentID(incidentId).AlertIncidentMappingData;
                        
            incidentModel.AlertIncidentMapping = new GetAlertIncidentMappingModel();
            incidentModel.AlertIncidentMapping = alertIncidentMap;
            if (alertIncidentMap != null)
            incidentModel.SignificantIncident = alertIncidentMap.SignificantIncident;


            response.IsSuccess = true;
            response.Message = "Success";
            response.HttpStatusCode = HttpStatusCode.OK;
            response.IncidentData = incidentModel;
           
            return response;

            
        }
        private GetIncidentsResponse GetIncidentForClientTools(List<string> _clientToolIncidentPKIds, OrganizationToolModel conndtl)
        {
            _service = _ticketMgmtFactory.GetInstance(conndtl.ToolID);

            GetIncidentsResponse res = null;

            if (_service == null)
            {
                res = new GetIncidentsResponse()
                {
                    IsSuccess = false,
                    Message = "Please check tool configureation...",
                    HttpStatusCode = HttpStatusCode.NotFound
                };
                return res;

            }
            res = _service.GetIncidentsByClientToolPKIds(_clientToolIncidentPKIds, conndtl).Result;

            return res;
        }

        private bool GetUserAdminFlag(int userId)
        {
            bool isAdinuser = false;

            var user = _securityBl.GetUserbyID(userId);

            if (user.Userdata != null)
            {
                if (user.Userdata.ClientAdminRole == 1 || user.Userdata.GlobalAdminRole == 1)
                {
                    isAdinuser = true;
                }
            }

            return isAdinuser;
        }

        public getUnattendedIncidentcountResponse GetUnAttendedIncidentsCount(GetUnAttendedIncidentCount request)
        {

            var statusId = _plattformBL.GetMasterDataByDataValue(Constants.IncidentStatusType, Constants.IncidentNewStatus);
            getUnattendedIncidentcountResponse response = new getUnattendedIncidentcountResponse();
            response.UnattendedIncidentCount = _repo.GetUnAttendedIncidentsCount(request, statusId).Result;
            response.IsSuccess = true;
            response.Message = "Success";
            return response;
        }

        
        public IncidentAssignOwnerResponse AssignOwner(IncidentAssignOwnerRequest request)
        {
            IncidentAssignOwnerResponse returnobj = new IncidentAssignOwnerResponse();

            var repoResponse = _repo.AssignOwner(request);

            //get alert data to pass to update the user actions
            var alertdata = _repo.GetInternalIncidentData(new GetInternalIncidentDataRequest() { IncidentID = request.IncidentID });
            if (alertdata.Result != null)
            {
                var _userActionRequest = _mapper.Map<Incident, UserActionRequest>(alertdata.Result);
                
                _useractionBL.AssignOwner(_userActionRequest);
            }

            //Adding history data 

            var alerts = _alertIncidentMappingBL.GetAlertsByIncidentId(new GetAlertsByIncidentIdRequest()
            { IncidentId = request.IncidentID });
            if (alerts != null && alerts.Count > 0)
            {
                AlertHistoryModel alertHistory = null;
                List<AlertHistoryModel> alertHistoryRequestList = new List<AlertHistoryModel    >();
               // Get for Alert wise 
                foreach (var alert in alerts)
                {
                    alertHistory = new AlertHistoryModel()
                    {
                        AlertId = alert.alertid,
                        HistoryDate = request.ModifiedDate,
                        CreatedUser = request.ModifiedUser,
                        OrgId = alert.orgid,
                        IncidentId = 0,
                        CreatedUserId = request.ModifiedUserId,
                        HistoryDescription = "Incident Owner assigned  , Incident number : " + request.IncidentID
                        + " Assined to : " + request.OwnerUserName
                        + " Assigned by : " + request.ModifiedUser
                       

                    };
                    alertHistoryRequestList.Add(alertHistory);
                }
                // Get for Incident wise
                alertHistory = new AlertHistoryModel()
                {
                    AlertId = 0,
                    HistoryDate = request.ModifiedDate,
                    CreatedUser = request.ModifiedUser,
                    OrgId = alertHistoryRequestList[0].OrgId,
                    IncidentId = request.IncidentID,
                    CreatedUserId = request.ModifiedUserId,
                    HistoryDescription = "Incident Owner assigned  , Incident number : " + request.IncidentID
                       + " Assined to : " + request.OwnerUserName
                       + " Assigned by : " + request.ModifiedUser


                };
                alertHistoryRequestList.Add(alertHistory);
                //
                _alertHistoryBL.AddRangealertHistory(alertHistoryRequestList);
            }
            //
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
            }

            return returnobj;
        }



        public SetIncidentStatusResponse SetIncidentStatus(SetIncidentStatusRequest request)
        {
            SetIncidentStatusResponse returnobj = new SetIncidentStatusResponse();

            var repoResponse = _repo.SetIncidentStatus(request);


            // updating user action data 

            SetUserActionStatusRequest statusRequst = new SetUserActionStatusRequest()
            {
                ActionType = Constants.User_Action_Incident_Type,
                Id = request.IncidentID,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = request.ModifiedUser,
                StatusId = request.StatusID,
                StatusName = request.StatusName
            };
            var useractionRes = _useractionBL.SetUserActionStatus(statusRequst);

            //Adding history data 

            var alerts = _alertIncidentMappingBL.GetAlertsByIncidentId(new GetAlertsByIncidentIdRequest()
            { IncidentId = request.IncidentID });
            if (alerts != null && alerts.Count > 0)
            {
                AlertHistoryModel alertHistory = null;
                List<AlertHistoryModel> alertHistoryRequestList = new List<AlertHistoryModel>();
                // Get for Alert wise 
                foreach (var alert in alerts)
                {
                    alertHistory = new AlertHistoryModel()
                    {
                        AlertId = alert.alertid,
                        HistoryDate = request.ModifiedDate,
                        CreatedUser = request.ModifiedUser,
                        OrgId = alert.orgid,
                        IncidentId = 0,
                        CreatedUserId = request.ModifiedUserId,
                        HistoryDescription = "Incident Status assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.StatusName
                        + " Assigned by : " + request.ModifiedUser


                    };
                    alertHistoryRequestList.Add(alertHistory);
                }
                // Get for Incident wise
                alertHistory = new AlertHistoryModel()
                {
                    AlertId = 0,
                    HistoryDate = request.ModifiedDate,
                    CreatedUser = request.ModifiedUser,
                    OrgId = alertHistoryRequestList[0].OrgId,
                    IncidentId = request.IncidentID,
                    CreatedUserId = request.ModifiedUserId,
                    HistoryDescription = "Incident Status assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.StatusName
                        + " Assigned by : " + request.ModifiedUser


                };
                alertHistoryRequestList.Add(alertHistory);
                //
                _alertHistoryBL.AddRangealertHistory(alertHistoryRequestList);
            }

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
            }

            return returnobj;
        }

        public SetIncidentPriorityResponse SetIncidentPriority(SetIncidentPriorityRequest request)
        {
            SetIncidentPriorityResponse returnobj = new SetIncidentPriorityResponse();

            var repoResponse = _repo.SetIncidentPriority(request);

            //Update the user actions if any 
            SetUserActionPriorityRequest priorityRequst = new SetUserActionPriorityRequest()
            {
                ActionType = Constants.User_Action_Incident_Type,
                Id = request.IncidentID,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = request.ModifiedUser,
                PriorityId = request.PriorityID,
                PriorityValue = request.PriorityValue
            };
            var useractionRes = _useractionBL.SetUserActiontPriority(priorityRequst);

            //Adding history data 

            var alerts = _alertIncidentMappingBL.GetAlertsByIncidentId(new GetAlertsByIncidentIdRequest()
            { IncidentId = request.IncidentID });
            if (alerts != null && alerts.Count > 0)
            {
                AlertHistoryModel alertHistory = null;
                List<AlertHistoryModel> alertHistoryRequestList = new List<AlertHistoryModel>();
                // Get for Alert wise 
                foreach (var alert in alerts)
                {
                    alertHistory = new AlertHistoryRequest()
                    {
                        AlertId = alert.alertid,
                        HistoryDate = request.ModifiedDate,
                        CreatedUser = request.ModifiedUser,
                        OrgId = alert.orgid,
                        IncidentId = 0,
                        CreatedUserId = request.ModifiedUserId,
                        HistoryDescription = "Incident Priority assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.PriorityValue
                        + " Assigned by : " + request.ModifiedUser


                    };
                    alertHistoryRequestList.Add(alertHistory);
                }
                // Get for Incident wise
                alertHistory = new AlertHistoryRequest()
                {
                    AlertId = 0,
                    HistoryDate = request.ModifiedDate,
                    CreatedUser = request.ModifiedUser,
                    OrgId = alertHistoryRequestList[0].OrgId,
                    IncidentId = request.IncidentID,
                    CreatedUserId = request.ModifiedUserId,
                    HistoryDescription = "Incident Priority assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.PriorityValue
                        + " Assigned by : " + request.ModifiedUser


                };
                alertHistoryRequestList.Add(alertHistory);
                //
                _alertHistoryBL.AddRangealertHistory(alertHistoryRequestList);
            }
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
            }

            return returnobj;
        }

        public SetIncidentSeviarityResponse SetIncidentSeviarity(SetIncidentSeviarityRequest request)
        {
            SetIncidentSeviarityResponse returnobj = new SetIncidentSeviarityResponse();

            var repoResponse = _repo.SetIncidentSeviarity(request);

            //Update the user actions if any 
            SetUserActionSeviarityRequest seviarityRequst = new SetUserActionSeviarityRequest()
            {
                ActionType = Constants.User_Action_Incident_Type,
                Id = request.IncidentID,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = request.ModifiedUser,
                Sevirity = request.Seviarity,
                SevirityId = request.SeviarityID
            };
            var useractionRes = _useractionBL.SetUserActiontSeviarity(seviarityRequst);

            //Adding history data 

            var alerts = _alertIncidentMappingBL.GetAlertsByIncidentId(new GetAlertsByIncidentIdRequest()
            { IncidentId = request.IncidentID });
            if (alerts != null && alerts.Count > 0)
            {
                AlertHistoryModel alertHistory = null;
                List<AlertHistoryModel> alertHistoryRequestList = new List<AlertHistoryModel>();
                // Get for Alert wise 
                foreach (var alert in alerts)
                {
                    alertHistory = new AlertHistoryModel()
                    {
                        AlertId = alert.alertid,
                        HistoryDate = request.ModifiedDate,
                        CreatedUser = request.ModifiedUser,
                        OrgId = alert.orgid,
                        IncidentId = 0,
                        CreatedUserId = request.ModifiedUserId,
                        HistoryDescription = "Incident Seviarity assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.Seviarity
                        + " Assigned by : " + request.ModifiedUser


                    };
                    alertHistoryRequestList.Add(alertHistory);
                }
                // Get for Incident wise
                alertHistory = new AlertHistoryRequest()
                {
                    AlertId = 0,
                    HistoryDate = request.ModifiedDate,
                    CreatedUser = request.ModifiedUser,
                    OrgId = alertHistoryRequestList[0].OrgId,
                    IncidentId = request.IncidentID,
                    CreatedUserId = request.ModifiedUserId,
                    HistoryDescription = "Incident Seviarity assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.Seviarity
                        + " Assigned by : " + request.ModifiedUser


                };
                alertHistoryRequestList.Add(alertHistory);
                //
                _alertHistoryBL.AddRangealertHistory(alertHistoryRequestList);
            }

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
            }

            return returnobj;
        }

        public SetIncidentTypeResponse SetIncidentType(SetIncidentTypeRequest request)
        {
            SetIncidentTypeResponse returnobj = new SetIncidentTypeResponse();

            var repoResponse = _repo.SetIncidentType(request);

            //Update the user actions if any 
            //SetUserActionTypeRequest typeRequest = new SetUserActionTypeRequest()
            //{
            //    ActionType = Constants.User_Action_Incident_Type,
            //    Id = request.IncidentID,
            //    ModifiedDate = request.ModifiedDate,
            //    ModifiedUser = request.ModifiedUser,
            //    Type = request.TypeName,
            //    TypeId = request.TypeId
            //};
            //var useractiontypeRes = _useractionBL.SetUserActiontSeviarity(typeRequest);

            //Adding history data 

            var alerts = _alertIncidentMappingBL.GetAlertsByIncidentId(new GetAlertsByIncidentIdRequest()
            { IncidentId = request.IncidentID });
            if (alerts != null && alerts.Count > 0)
            {
                AlertHistoryModel alertHistory = null;
                List<AlertHistoryModel> alertHistoryRequestList = new List<AlertHistoryModel>();
                // Get for Alert wise 
                foreach (var alert in alerts)
                {
                    alertHistory = new AlertHistoryModel()
                    {
                        AlertId = alert.alertid,
                        HistoryDate = request.ModifiedDate,
                        CreatedUser = request.ModifiedUser,
                        OrgId = alert.orgid,
                        IncidentId = 0,
                        CreatedUserId = request.ModifiedUserId,
                        HistoryDescription = "Incident Type assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.TypeName
                        + " Assigned by : " + request.ModifiedUser


                    };
                    alertHistoryRequestList.Add(alertHistory);
                }
                // Get for Incident wise
                alertHistory = new AlertHistoryModel()
                {
                    AlertId = 0,
                    HistoryDate = request.ModifiedDate,
                    CreatedUser = request.ModifiedUser,
                    OrgId = alertHistoryRequestList[0].OrgId,
                    IncidentId = request.IncidentID,
                    CreatedUserId = request.ModifiedUserId,
                    HistoryDescription = "Incident Type assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.TypeName
                        + " Assigned by : " + request.ModifiedUser


                };
                alertHistoryRequestList.Add(alertHistory);
                //
                _alertHistoryBL.AddRangealertHistory(alertHistoryRequestList);
            }

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
            }

            return returnobj;
        }

        public SetIncidentScoreResponse SetIncidentScore(SetIncidentScoreRequest request)
        {
            SetIncidentScoreResponse returnobj = new SetIncidentScoreResponse();

            var repoResponse = _repo.SetIncidentScore(request);

            AssignUserActionScoresRequest UserActionScoreRequest = new AssignUserActionScoresRequest()
            {
                ActionType = Constants.User_Action_Incident_Type,
                Id = request.IncidentID,
                ModifiedDate = request.ModifiedDate,
                ModifiedUser = request.ModifiedUser,
                Score = request.Score
            };
            var useractionRes = _useractionBL.AssignUserActionScore(UserActionScoreRequest);

            //
            //Adding history data 

            var alerts = _alertIncidentMappingBL.GetAlertsByIncidentId(new GetAlertsByIncidentIdRequest()
            { IncidentId = request.IncidentID });
            if (alerts != null && alerts.Count > 0)
            {
                AlertHistoryModel alertHistory = null;
                List<AlertHistoryModel> alertHistoryRequestList = new List<AlertHistoryModel>();
                // Get for Alert wise 
                foreach (var alert in alerts)
                {
                    alertHistory = new AlertHistoryModel()
                    {
                        AlertId = alert.alertid,
                        HistoryDate = request.ModifiedDate,
                        CreatedUser = request.ModifiedUser,
                        OrgId = alert.orgid,
                        IncidentId = 0,
                        CreatedUserId = request.ModifiedUserId,
                        HistoryDescription = "Incident Score assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.Score
                        + " Assigned by : " + request.ModifiedUser


                    };
                    alertHistoryRequestList.Add(alertHistory);
                }
                // Get for Incident wise
                alertHistory = new AlertHistoryModel()
                {
                    AlertId = 0,
                    HistoryDate = request.ModifiedDate,
                    CreatedUser = request.ModifiedUser,
                    OrgId = alertHistoryRequestList[0].OrgId,
                    IncidentId = request.IncidentID,
                    CreatedUserId = request.ModifiedUserId,
                    HistoryDescription = "Incident Score assigned/Changed  , Incident number : " + request.IncidentID
                        + " Assinged / Changed to : " + request.Score
                        + " Assigned by : " + request.ModifiedUser


                };
                alertHistoryRequestList.Add(alertHistory);
                //
                _alertHistoryBL.AddRangealertHistory(alertHistoryRequestList);
            }
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
            }

            return returnobj;
        }

        public Responses.GetIncidentsResponse GetMyInternalIncidents(GetMyInternalIncidentsRequest request)
        {
            Responses.GetIncidentsResponse response = new Responses.GetIncidentsResponse();
            var res = _repo.GetMyInternalIncidents(request);
            
            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Incidents data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                var _mappedIncidents = _mapper.Map<List<Incident>, List<GetIncidentModel>>(res.Result);
                response.IncidentList = _mappedIncidents;
                response.HttpStatusCode = HttpStatusCode.OK;
            }

            return response;
        }

        public GetIncidentCountByPriorityAndStatusResponse GetIncidentCountByPriorityAndStatus(GetIncidentCountByPriorityAndStatusRequest request)
        {
            GetIncidentCountByPriorityAndStatusResponse response = new GetIncidentCountByPriorityAndStatusResponse();
            var _count = _repo.GetIncidentCountByPriorityAndStatus(request).Result;

            response.IncidentCount =_count;
            if (_count > 0 )
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "No records found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }

        public SearchIncidentsResponse GetIncidentSearchResult(IncidentSeeachRequest request)
        {
            SearchIncidentsResponse response = new SearchIncidentsResponse();

            List<Alerts>? alertsdata = null;
            // Get the user role
            bool isAdinuser = GetUserAdminFlag(request.LoggedInUserId);


            List<Incident> res = null;
            int tooltypeID = 0;
            OrganizationToolModel conndtl = null;
            List<GetIncidentModel> _mappedIncidents = null;
            
            var searchSortOptions = _platformBL.GetMasterDataValueByDataId("IncidentSortOptions",request.SortOptionId);

            res = _repo.GetIncidentSearchResult(request, isAdinuser, searchSortOptions).Result;
            if (res.Count == 0)
            {
                response.Message = "Incidents data not found";
                response.TotalIncidentsCount = 0;
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            _mappedIncidents = _mapper.Map<List<Incident>, List<GetIncidentModel>>(res);
            //
            var IncidentIDs = _mappedIncidents.Select(itm => itm.IncidentID);
            //
            var alertIncidentMaps = _alertIncidentMappingBL.GetAlertIncidentMappingByIncidentIDs(IncidentIDs.ToList()).AlertIncidentMappingData;

            //
            // Add can see client incident details / Normal user cannot see as incidents cannot assigned to him
            //
            GetIncidentsResponse _clientToolIncidentData = null;
            response.IsSuccess = true;
            response.Message = "Success";
            response.HttpStatusCode = HttpStatusCode.OK;
            //
            if (alertIncidentMaps == null )
            {
                response.IncidentList = _mappedIncidents.ToList();
                response.TotalIncidentsCount = _repo.GetIncidentSearchResultCount(request, isAdinuser).Result;

                return response;
            }
            if (alertIncidentMaps != null )
            {
                foreach (var incident in _mappedIncidents)
                {
                    var alertIncidentMapForIncident = alertIncidentMaps.Where(m => m.incidentnumber == incident.IncidentID).FirstOrDefault();

                    if (alertIncidentMapForIncident != null)
                    {
                        incident.AlertIncidentMapping = new GetAlertIncidentMappingModel();
                        incident.AlertIncidentMapping = alertIncidentMapForIncident;
                        incident.SignificantIncident = alertIncidentMapForIncident.SignificantIncident;
                    }
                }
            }
            
            response.IncidentList = _mappedIncidents.ToList();
            response.TotalIncidentsCount = _repo.GetIncidentSearchResultCount(request, isAdinuser).Result;

            return response;

        }

        public UpdateIncidentResponse UpdateIncident_Old(UpdateIncidentRequest request)

        {
            UpdateIncidentResponse response = new UpdateIncidentResponse();

            //

            //Get the Alert details from DB
            var _incidentData = _repo.GetInternalIncidentData(new GetInternalIncidentDataRequest()
            { IncidentID = request.IncidentId }).Result;
            if (_incidentData == null)
            {
                response.IsSuccess = false;
                response.Message = "Incident data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            //
            var _userData = GetUserDetails(request.OwnerUserId, request.ModifiedUserId);
            var _masterData = GetMasterData(request.StatusId, request.PriorityId, request.SeverityId,request.TypeId);
            //Update if status changed 
            if (request.StatusId != _incidentData.Incident_Status)
            {
                var _setstatusres = this.SetIncidentStatus(
                        new SetIncidentStatusRequest()
                        {
                            IncidentID = request.IncidentId,
                            StatusID = request.StatusId,
                            ModifiedUserId = request.ModifiedUserId,
                            ModifiedDate = request.ModifiedDate,
                            ModifiedUser = _userData.UpdatedUseName,
                            StatusName = _masterData.StatusName
                             
                        }
                    );
            }

            //Update if priority changed 
            if (request.PriorityId != _incidentData.Priority)
            {
                var _setpriorityres = this.SetIncidentPriority(
                        new SetIncidentPriorityRequest()
                        {
                            IncidentID = request.IncidentId,
                            PriorityID = request.PriorityId,
                           // OrgID = request.OrgID,
                            ModifiedUserId = request.ModifiedUserId,
                            ModifiedDate = request.ModifiedDate,
                            ModifiedUser = _userData.UpdatedUseName,
                            PriorityValue = _masterData.PriorityName
                         }
                    );
            }

            //Update if severity changed 
            if (request.SeverityId != _incidentData.Severity)
            {
                var _setseverityres = this.SetIncidentSeviarity(
                        new SetIncidentSeviarityRequest()
                        {
                            IncidentID = request.IncidentId,
                            SeviarityID = request.SeverityId,
                           // OrgID = request.OrgID,
                            ModifiedUserId = request.ModifiedUserId,
                            ModifiedDate = request.ModifiedDate,
                            ModifiedUser = _userData.UpdatedUseName,
                            Seviarity = _masterData.SeverityName
                        }
                    );
            }
            //
            //Update if owner changed 
            if (request.OwnerUserId != _incidentData.Owner)
            {
                var _assignownerRes = this.AssignOwner(
                        new IncidentAssignOwnerRequest()
                        {
                            IncidentID = request.IncidentId,
                            OwnerUserID = request.OwnerUserId,
                           // OrgID = request.OrgID,
                            ModifiedUserId = request.ModifiedUserId,
                            ModifiedDate = request.ModifiedDate,
                            ModifiedUser = _userData.UpdatedUseName,
                            OwnerUserName = _userData.OwnerUserName
                        }
                    );
            }
            //
            // Update if tyoe changed
            if (request.TypeId != _incidentData.type_id)
            {
                var _settyperes = this.SetIncidentType(
                        new SetIncidentTypeRequest()
                        {
                            IncidentID = request.IncidentId,
                            TypeId = request.TypeId,
                          //  OrgID = request.OrgID,
                            ModifiedUserId = request.ModifiedUserId,
                            ModifiedDate = request.ModifiedDate,
                            ModifiedUser = _userData.UpdatedUseName,
                            TypeName = _masterData.TypeName
                        }
                    );
            }

            ////Update if score changed 
            if (request.Score != _incidentData.Score)
            {
                var _settagres = this.SetIncidentScore(
                        new SetIncidentScoreRequest()
                        {
                            IncidentID = request.IncidentId,
                            ModifiedUserId = request.ModifiedUserId,
                            ModifiedDate = request.ModifiedDate,
                            ModifiedUser = _userData.UpdatedUseName,
                            Score = request.Score
                        }
                    );
            }
            response.IsSuccess = true;
            response.Message = "Success";
            response.HttpStatusCode = HttpStatusCode.OK;
            return response;
        }

        public UpdateIncidentResponse UpdateIncident(UpdateIncidentRequest request)
        {
            UpdateIncidentResponse response = new UpdateIncidentResponse();

            //
            bool isUserActionInsert = false;
            List<AddActivityRequest> activityLogList = new List<AddActivityRequest>();
            //Get the Alert details from DB
            var _incidentData = _repo.GetInternalIncidentData(new GetInternalIncidentDataRequest()
            { IncidentID = request.IncidentId }).Result;
            
            if (_incidentData == null)
            {
                response.IsSuccess = false;
                response.Message = "Incident data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            //
            var _userData = GetUserDetails(request.OwnerUserId, request.ModifiedUserId);
            var _masterData = GetMasterData(request.StatusId, request.PriorityId, request.SeverityId, request.TypeId);
            //Update if status changed 
            List<AlertHistoryModel> historyRequestList = new List<AlertHistoryModel>();
            AlertHistoryModel historyRequest = null;
            //
            // Get the user action if any for alert 
            
            var _useractiondata = _useractionBL.GetUserActionsByActionTypeRefID(new GetUserActionByActionTypeRefIDRequest()
            {
                ActionType = Constants.User_Action_Incident_Type,
                ActionTypeRefId = request.IncidentId
            }).UserActionData;
            // if user assigned .. create the user action object if not found 
            //if (_useractiondata == null && request.OwnerUserId != _incidentData.Owner)
            if (_useractiondata == null )
            {
                isUserActionInsert = true;
                _useractiondata = new UserActionRequest()
                {
                    ActionType = Constants.User_Action_Incident_Type,
                    ActionTypeRefid = request.IncidentId,
                    ActionDate = request.ModifiedDate,
                    CreatedDate = request.ModifiedDate,
                    ActionStatus = _incidentData.Incident_Status,
                    ActionStatusName = _incidentData.Incident_Status_Name,
                    Description = _incidentData.Description,
                    Owner = _incidentData.Owner,
                    OwnerName = _incidentData.Owner_Name,
                    Priority = _incidentData.Priority,
                    PriorityName = _incidentData.Priority_Name,
                    Score = _incidentData.Score,
                    Severity = _incidentData.Severity_Name,
                    SeverityId = _incidentData.Severity,
                    CreatedUser = _userData.UpdatedUseName
                };

            };

            //Update if status changed 
            if (request.StatusId != _incidentData.Incident_Status)
            {

                _incidentData.Incident_Status = request.StatusId;
                _incidentData.Incident_Status_Name = _masterData.StatusName;

                if (_useractiondata != null)
                {
                    _useractiondata.ActionStatus = request.StatusId;
                    _useractiondata.ActionStatusName = _masterData.StatusName;

                }


                //string historyDescription = $"Status set to {_masterData.StatusName}";
                //AddIncidentHistoryData(request, historyDescription, _userData.UpdatedUseName, historyRequestList);


                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("StatusName", _masterData.StatusName);
                _templateData.Add("IncidentNumber", request.IncidentId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Incident_Assign_Status, _templateData, request.ModifiedUserId, request.ModifiedDate, _incidentData.org_id, true, request.IncidentId, 0));

            }

            //Update if priority changed 
            if (request.PriorityId != _incidentData.Priority)
            {

                _incidentData.Priority = request.PriorityId;
                _incidentData.Priority_Name = _masterData.PriorityName;

                if (_useractiondata != null)
                {
                    _useractiondata.Priority = request.PriorityId;
                    _useractiondata.PriorityName = _masterData.PriorityName;

                }
                //string historyDescription = $"Priority set to {_masterData.PriorityName}";
                //AddIncidentHistoryData(request, historyDescription, _userData.UpdatedUseName, historyRequestList);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("PriorityName", _masterData.PriorityName);
                _templateData.Add("IncidentNumber", request.IncidentId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Incident_Assign_Priority, _templateData, request.ModifiedUserId, request.ModifiedDate, _incidentData.org_id, true, request.IncidentId, 0));

            }
            if (request.TypeId != _incidentData.type_id)
            {

                _incidentData.type_id = request.TypeId;
                _incidentData.Type = _masterData.TypeName;

                // string historyDescription = $"Incident Type set to {_masterData.TypeName}";
                // AddIncidentHistoryData(request, historyDescription, _userData.UpdatedUseName, historyRequestList);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("TypeName", _masterData.TypeName);
                _templateData.Add("IncidentNumber", request.IncidentId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Incident_Assign_Type, _templateData, request.ModifiedUserId, request.ModifiedDate, _incidentData.org_id, true, request.IncidentId, 0));

            }
            //Update if severity changed 
            if (request.SeverityId != _incidentData.Severity)
            {

                _incidentData.Severity = request.SeverityId;
                _incidentData.Severity_Name = _masterData.SeverityName;

                if (_useractiondata != null)
                {
                    _useractiondata.SeverityId = request.SeverityId;
                    _useractiondata.Severity = _masterData.SeverityName;

                }


                //string historyDescription = $"Incident severity set to {_masterData.SeverityName}";
                //AddIncidentHistoryData(request, historyDescription, _userData.UpdatedUseName, historyRequestList);
                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("SeviarityName", _masterData.SeverityName);
                _templateData.Add("IncidentNumber", request.IncidentId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Incident_Assign_Severity, _templateData, request.ModifiedUserId, request.ModifiedDate, _incidentData.org_id, true, request.IncidentId, 0));


            }
            //
            //Update if owner changed 
            if (request.OwnerUserId != _incidentData.Owner)
            {

                _incidentData.Owner = request.OwnerUserId;
                _incidentData.Owner_Name = _userData.OwnerUserName;


                if (_useractiondata != null)
                {
                    _useractiondata.Owner = request.OwnerUserId;
                    _useractiondata.OwnerName = _userData.OwnerUserName;

                }

                // string historyDescription = $"Incident owner assigned to {_userData.OwnerUserName}";
                // AddIncidentHistoryData(request, historyDescription, _userData.UpdatedUseName, historyRequestList);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("AssignUser", _userData.OwnerUserName);
                _templateData.Add("IncidentNumber", request.IncidentId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Incident_Assign_Severity, _templateData, request.ModifiedUserId, request.ModifiedDate, _incidentData.org_id, true, request.IncidentId, 0));

            }

            //Update if score changed 
            if (request.Score != _incidentData.Score)
            {
                _incidentData.Score = request.Score;

                if (_useractiondata != null)
                {
                    _useractiondata.Score = request.Score;
                }



                //string historyDescription = $"Incident score assigned to {request.Score}";
                //AddIncidentHistoryData(request, historyDescription, _userData.UpdatedUseName, historyRequestList);

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("Score", request.Score);
                _templateData.Add("IncidentNumber", request.IncidentId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Incident_Assign_Score, _templateData, request.ModifiedUserId, request.ModifiedDate, _incidentData.org_id, true, request.IncidentId, 0));


            }
            if (historyRequestList.Count > 0)
            {
                //_alertHistoryBL.AddRangealertHistory(historyRequestList);
            }
            if (isUserActionInsert && _useractiondata != null)
            {
                _useractiondata.ActionDate = request.ModifiedDate;
                _useractiondata.ModifiedUser = _userData.UpdatedUseName;
                _useractiondata.ModifiedDate = request.ModifiedDate;
                _useractionBL.AddUserAction(_useractiondata);
            }
            if (!isUserActionInsert && _useractiondata != null)
            {
                _useractionBL.UpdateUserAction(_useractiondata);
            }
            // updating the alert
            _incidentData.Modified_User = _userData.UpdatedUseName;
            _incidentData.Modified_Date = request.ModifiedDate;


            var updateres = _repo.UpdateIncident(_incidentData);
            if (updateres.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to update incident";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("IncidentNumber", request.IncidentId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Update_Incident, _templateData, request.ModifiedUserId, request.ModifiedDate, _incidentData.org_id, false, request.IncidentId, 0));

            }
            else
            { 
                UpdateSignicantFlag(request.IncidentId, request.SignificantIncident);
                response.IsSuccess = true;
                response.Message = "success";
                response.HttpStatusCode = HttpStatusCode.OK;

                Dictionary<string, string> _templateData = new Dictionary<string, string>();
                _templateData.Add(Constants.Acvities_Template_username, _userData.UpdatedUseName);
                _templateData.Add("IncidentNumber", request.IncidentId.ToString());

                activityLogList.Add
                    (BuildActivityLogObj(Constants.Activity_Template_Update_Incident, _templateData, request.ModifiedUserId, request.ModifiedDate, _incidentData.org_id, true, request.IncidentId, 0));

                
            }

           var _activityLogRes = _commonBL.LogActivity(activityLogList);

           return response;
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

        public int UpdateSignicantFlag(int alertId, bool requestSignificantFlag)
        {
            var alertMapData = _alertIncidentMappingBL.GetAlertIncidentMappingByIncidentID(alertId);
            int intSinficantdata = 0;
            if (requestSignificantFlag) intSinficantdata = 1;

            if (intSinficantdata != alertMapData.AlertIncidentMappingData.SignificantIncident)
            {
                _alertIncidentMappingBL.UpdateSignificantFlag(alertId, intSinficantdata);
            }

            return 1;
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

        public (string StatusName, string PriorityName, string SeverityName,string TypeName) GetMasterData(int statusId, int priorityId, int severityId, int typeId)
        {
            List<string> masterdatatypes = new List<string>()
                                            {
                                            "incident_status","incident_priority","incident_severity","Incident_Type"
                                            };

            LDPMasterDataByMultipleTypesRequest masterdatarequest = new LDPMasterDataByMultipleTypesRequest();
            masterdatarequest.MaserDataTypes = masterdatatypes;
            var masterdata = _plattformBL.GetMasterDataByMultipleTypes(masterdatarequest);
            string _Priority_Name = string.Empty;
            string _Severity_Name = string.Empty;
            string _Status_Name = string.Empty;
            string _type_Name = string.Empty;

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

                if (typeId > 0)
                {
                    var mdata2 = masterdata.MasterData.Where(md => md.DataID == typeId).FirstOrDefault();
                    if (mdata2 != null)
                    {
                        _type_Name = mdata2.DataValue;
                    }
                }
   
            }

            return (StatusName: _Status_Name, PriorityName: _Priority_Name, SeverityName: _Severity_Name , TypeName:_type_Name);
        }

        public bool AddIncidentHistoryData(UpdateIncidentRequest request,string historyDescription,string modifiedUserName, List<AlertHistoryModel> alertHistoryRequestList)
        {
            bool res = true;           
            var alerts = _alertIncidentMappingBL.GetAlertsByIncidentId(new GetAlertsByIncidentIdRequest()
            { IncidentId = request.IncidentId });
            if (alerts != null && alerts.Count > 0)
            {
                AlertHistoryModel alertHistory = null;
                // Get for Alert wise 
                foreach (var alert in alerts)
                {
                    alertHistory = new AlertHistoryModel()
                    {
                        AlertId = alert.alertid,
                        HistoryDate = request.ModifiedDate,
                        CreatedUser = modifiedUserName,
                        OrgId = alert.orgid,
                        IncidentId = 0,
                        CreatedUserId = request.ModifiedUserId,
                        HistoryDescription = historyDescription
                

                    };
                    alertHistoryRequestList.Add(alertHistory);
                }
                // Get for Incident wise
                alertHistory = new AlertHistoryRequest()
                {
                    AlertId = 0,
                    HistoryDate = request.ModifiedDate,
                    CreatedUser = modifiedUserName,
                    OrgId = alertHistoryRequestList[0].OrgId,
                    IncidentId = request.IncidentId,
                    CreatedUserId = request.ModifiedUserId,
                    HistoryDescription = historyDescription

                };
                alertHistoryRequestList.Add(alertHistory);


                
            }
            return res;
        }
    }
}
