using AutoMapper;
using LDP.Common.Services.SentinalOneIntegration.Applications.Agent;
using LDP.Common.Services.SentinalOneIntegration.Applications.CVS;
using LDP.Common.Services.SentinalOneIntegration.Applications.EndPoints;
using LDP.Common.Services.SentinalOneIntegration.Applications.Inventory;
using LDP.Common.Services.SentinalOneIntegration.Applications.ManagementSettings;
using LDP.Common.Services.SentinalOneIntegration.Applications.RiskApplicaionsEndPoint;
using LDP.Common.Services.SentinalOneIntegration.Applications.Risks;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;
using System.Net;

namespace LDP.Common.Services.SentinalOneIntegration.Applications
{
    public class SentinalOneApplicationsBL : ISentinalOneApplicationsBL
    {
        ISentinalOneApplicationsIntegrationService _service;
        public readonly IMapper _mapper;
        ILDPlattformBL _plattformBL;

        public SentinalOneApplicationsBL(
                   ILDPlattformBL plattformBL, 
                   ISentinalOneApplicationsIntegrationService service, 
                   IMapper mapper
                  )
        {
            _plattformBL = plattformBL;
            _service = service;
            _mapper = mapper;
         
        }

        public SentinalOneAggregatedApplicationData GetSentinalOneAggregateApplicationData(SentinalOneApplicationsRequest request)
        {
            SentinalOneAggregatedApplicationData methodResponse = new SentinalOneAggregatedApplicationData();

            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(request.OrgID, tooltypeID);
            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, Constants.Tool_Action_Get_Application_Data);

            if (conndtl == null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }

            //
            var res = _service.GetAggregatedApplications(conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                return methodResponse;
            }
            //
            methodResponse.ApplicationList = new List<Data>();
            methodResponse.ApplicationList.AddRange(res.data);
            //
            while (res != null && res.pagination != null && res.pagination.nextCursor != null)
            {
                res = _service.GetAggregatedApplications(conndtl, res.pagination.nextCursor).Result;
                if (res.data != null && res.data.Count > 0)
                {
                    methodResponse.ApplicationList.AddRange(res.data);
                }
            }
            
          //  _plattformBL.UpdateLastReadAlertDate(conndtl);
            methodResponse.IsSuccess = true;
            methodResponse.Message = "List of applications from sentinalOne tool";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;

            
        }

        public ApplicationsWithRisks GetApplicationsAndRisks(GetSentinalOneEndPointApplicationsRisksRequest request)
        {
            ApplicationsWithRisks methodResponse = new ApplicationsWithRisks();

             var conndtl = GetConnectionDetails(request.OrgID, methodResponse, Constants.Tool_Action_Get_Application_Risks_Data);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var res = _service.GetApplicationsAndRisks(request, conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.errors = res.errors;
                return methodResponse;
            }
            //
            methodResponse.Data = new List<Application>();
            methodResponse.Data.AddRange(res.Data);
            //
            while (res != null && res.Pagination != null && res.Pagination.nextCursor != null)
            {
                res = _service.GetApplicationsAndRisks(request ,conndtl, res.Pagination.nextCursor).Result;
                if (res.Data != null && res.Data.Count > 0)
                {
                    methodResponse.Data.AddRange(res.Data);
                }
            }

            //  _plattformBL.UpdateLastReadAlertDate(conndtl);
            methodResponse.IsSuccess = true;
            methodResponse.Message = "List of applications and risks from sentinalOne tool";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;


        }
        public RiskApplicationsEndPointsResponse GetRiskApplicationEndpoints(GetRiskApplicationEndpointRequest request)
        {
            RiskApplicationsEndPointsResponse methodResponse = new RiskApplicationsEndPointsResponse();

            var conndtl = GetConnectionDetails(request.OrgID, methodResponse, Constants.Tool_Action_Get_Risks_Application_Endpoints_Data);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var res = _service.GetRiskApplicationEndpoints(request, conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.errors = res.errors;
                methodResponse.HttpStatusCode = res.HttpStatusCode;

                return methodResponse;
            }
            //
            methodResponse.EndPoints = new List<RiskApplicationEndpointData>();
            var _mappedResponse = _mapper.Map<List<RiskApplicaionsEndPoint.Data>, List<RiskApplicationEndpointData>>(res.Data);
            methodResponse.EndPoints.AddRange(_mappedResponse);
            //
            while (res != null && res.Pagination != null && res.Pagination.nextCursor != null)
            {
                res = _service.GetRiskApplicationEndpoints(request, conndtl, res.Pagination.nextCursor).Result;
                if (res.Data != null && res.Data.Count > 0)
                {
                    _mappedResponse = _mapper.Map<List<RiskApplicaionsEndPoint.Data>, List<RiskApplicationEndpointData>>(res.Data);
                    methodResponse.EndPoints.AddRange(_mappedResponse);
                }
            }
            //
            methodResponse.IsSuccess = true;
            methodResponse.Message = "List of risk application end points from sentinalOne tool";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }
        public SentinalOneApplicationInventoryResponse GetSentinalOneApplicationInventory(SentinalOneApplicationsInventoryRequest request)
        {
            SentinalOneApplicationInventoryResponse methodResponse = new SentinalOneApplicationInventoryResponse();

            
            var conndtl = GetConnectionDetails(request.OrgID, methodResponse, Constants.Tool_Action_Get_Application_Inventory);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var res = _service.GetSentinalOneApplicationInventory(request , conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                methodResponse.errors = res.errors;

                return methodResponse;
            }
            //
            methodResponse.ApplicationList = new List<ApplicationInventory>();
            
            var _mappedResponse = _mapper.Map<List<Inventory.Data>, List<ApplicationInventory>>(res.data);
            methodResponse.ApplicationList.AddRange(_mappedResponse);
            while (res != null && res.pagination != null && res.pagination.nextCursor != null)
            {
                res = _service.GetSentinalOneApplicationInventory(request , conndtl, res.pagination.nextCursor).Result;
                if (res.data != null && res.data.Count > 0)
                {
                    _mappedResponse = null;
                    _mappedResponse = _mapper.Map<List<Inventory.Data>, List<ApplicationInventory>>(res.data);
                    methodResponse.ApplicationList.AddRange(_mappedResponse);
                }
            
            }
            methodResponse.IsSuccess = true;
            methodResponse.Message = "List of applications inventory from sentinalOne tool";
            methodResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return methodResponse;
        }
        public ApplicationEndPointsResponse GetSentinalOneApplicationEndPoints(SentinalOneApplicationsEndPointsRequest request)
        {
            ApplicationEndPointsResponse methodResponse = new ApplicationEndPointsResponse();


            var conndtl = GetConnectionDetails(request.OrgID, methodResponse, Constants.Tool_Action_Get_Application_EndPoints);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var res = _service.GetSentinalOneApplicationEndPoints(request,conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = res.Message;
                methodResponse.HttpStatusCode = res.HttpStatusCode;
                methodResponse.errors = res.errors;
                return methodResponse;
            }
            //
            methodResponse.EndPoints = new List<ApplicationEndPoint>();

            var _mappedResponse = _mapper.Map<List<EndPoints.Data>,List< EndPoints.ApplicationEndPoint>> (res.data);

            methodResponse.EndPoints.AddRange(_mappedResponse);


            while (res != null && res.pagination != null && res.pagination.nextCursor != null)
            {
                res = _service.GetSentinalOneApplicationEndPoints(request,conndtl, res.pagination.nextCursor).Result;
                if (res.data != null && res.data.Count > 0)
                {
                    _mappedResponse = null;
                     _mappedResponse = _mapper.Map<List<EndPoints.Data>, List<EndPoints.ApplicationEndPoint>>(res.data);

                    methodResponse.EndPoints.AddRange(_mappedResponse);
                }
            }

            
            BuildBaseResponseObject(methodResponse, true, "List of applications end points from sentinalOne tool", HttpStatusCode.OK);


            return methodResponse;

        }

        public ApplicationCVSResponse GetSentinalOneApplicationCVS(SentinalOneApplicationsCVSRequest request)
        {
            ApplicationCVSResponse methodResponse = new ApplicationCVSResponse();


            var conndtl = GetConnectionDetails(request.OrgID, methodResponse, Constants.Tool_Action_Get_Application_CVS);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var res = _service.GetSentinalOneApplicationCVS(request, conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "API call failed";
                methodResponse.errors = new List<string>() { res.Message };
                return methodResponse;
            }
            //
            methodResponse.CVSList = new List<ApplicatioCVSData>();

            var _mappedResponse = _mapper.Map<List<CVS.Data>, List<CVS.ApplicatioCVSData>>(res.data);

            methodResponse.CVSList.AddRange(_mappedResponse);


            while (res != null && res.pagination != null && res.pagination.nextCursor != null)
            {
                res = _service.GetSentinalOneApplicationCVS(request, conndtl, res.pagination.nextCursor).Result;
                if (res.data != null && res.data.Count > 0)
                {
                    _mappedResponse = null;
                    _mappedResponse = _mapper.Map<List<CVS.Data>, List<CVS.ApplicatioCVSData>>(res.data);

                    methodResponse.CVSList.AddRange(_mappedResponse);
                }
            }

            //
            BuildBaseResponseObject(methodResponse, true, "List of applications CVS from sentinalOne tool", HttpStatusCode.OK);
            //

            return methodResponse;
        }

        public EndPointAgentDetailsResponse GetSentinalOneApplicationEndPointDetails(SentinalOneApplicationsAgentRequest request)
        {
            EndPointAgentDetailsResponse methodResponse = new EndPointAgentDetailsResponse();

           

            var conndtl = GetConnectionDetails(request.OrgID, methodResponse, Constants.Tool_Action_Get_Application_EndPoint_Details);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var res = _service.GetSentinalOneApplicationAgentDetails(request, conndtl).Result;
            if (!res.IsSuccess)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "API call failed";
                methodResponse.errors = new List<string>() { res.Message };
                return methodResponse;
            }
            //
            methodResponse.EndPoints = new List<EndPointAgentDetails>();

            var _mappedResponse = _mapper.Map<List<Agent.Datum>, List<EndPointAgentDetails>>(res.data);

            methodResponse.EndPoints.AddRange(_mappedResponse);


            while (res != null && res.pagination != null && res.pagination.nextCursor != null)
            {
                res = _service.GetSentinalOneApplicationAgentDetails(request, conndtl, res.pagination.nextCursor).Result;
                if (res.data != null && res.data.Count > 0)
                {
                    _mappedResponse = null;
                    _mappedResponse = _mapper.Map<List<Agent.Datum>, List<EndPointAgentDetails>>(res.data);

                    methodResponse.EndPoints.AddRange(_mappedResponse);
                }
            }

            //
            BuildBaseResponseObject(methodResponse, true, "List of applications End point details from sentinalOne tool", HttpStatusCode.OK);
            //

            return methodResponse;
        }
        public EndPointApplications GetSentinalOneEndPointApplications(GetSentinalOneEndPointApplicationsRequest request)
        {
            EndPointApplications methodResponse = new EndPointApplications();

            var conndtl = GetConnectionDetails(request.OrgID, methodResponse, Constants.Tool_Action_Get_EndPoint_Applications);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            return _service.GetSentinalOneEndPointApplications(request, conndtl).Result;
          
        }
       public  ApplicationManagementSettingsResponse GetApplicationSettings(GetApplicationSettingsRequest request)
        {
            ApplicationManagementSettingsResponse methodResponse = new ApplicationManagementSettingsResponse();

            var conndtl = GetConnectionDetails(request.OrgID, methodResponse, Constants.Tool_Action_Get_Application_Management_Settings);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var _serviceResponse = _service.GetApplicationSettings(conndtl).Result;
            
            
            var _mappedResponse = _mapper.Map<AppManagementSettings, ApplicationManagementSettingsResponse>(_serviceResponse);

            var _mappedAppSettingsResponse = _mapper.Map<LDP.Common.Services.SentinalOneIntegration.Applications.ManagementSettings.Data, ApplicationManagementSettings>(_serviceResponse.data);

            _mappedResponse.AppSettings = _mappedAppSettingsResponse;

            return _mappedResponse;

        }

        public EndPointUpdatesResponse GetEndPointUpdates(GetEndPointUpdatesRequest request)
        {
            EndPointUpdatesResponse methodResponse = new EndPointUpdatesResponse();

            var conndtl = GetConnectionDetails(request.OrgID, methodResponse, Constants.Tool_Action_Get_EndPoint_Updates);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var _serviceResponse = _service.GetEndPointUpdates(request , conndtl).Result;

           
            if (!_serviceResponse.IsSuccess)
            {
                BuildBaseResponseObject(methodResponse, _serviceResponse.IsSuccess, _serviceResponse.Message, _serviceResponse.HttpStatusCode);

                methodResponse.errors = _serviceResponse.errors;
                return methodResponse;
            }
            methodResponse.EndPointUpdateList = new List<DataItem>();
            methodResponse.EndPointUpdateList.AddRange(_serviceResponse.Data);

            while (_serviceResponse != null && _serviceResponse.Pagination != null && _serviceResponse.Pagination.nextCursor != null)
            {
                _serviceResponse = _service.GetEndPointUpdates(request, conndtl, _serviceResponse.Pagination.nextCursor).Result;
                if (_serviceResponse.Data != null && _serviceResponse.Data.Count > 0)
                {
                   methodResponse.EndPointUpdateList.AddRange(_serviceResponse.Data);
                }
            }
            BuildBaseResponseObject(methodResponse, true, "List of Endpoint updates",HttpStatusCode.OK);

            return methodResponse;
        }
        private OrganizationToolModel GetConnectionDetails(int OrgId , baseResponse methodResponse , string actionName)
        {
            OrganizationToolModel conndtl = null;
            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_EDR);
            //
             conndtl = _plattformBL.GetToolConnectionDetails(OrgId, tooltypeID);
            if (conndtl == null)
            {
                BuildBaseResponseObject(methodResponse, false, "Tool connection details not found , please check the request or configuration", HttpStatusCode.NotFound);

                return conndtl;
            }
            conndtl = _plattformBL.FilterConnectionAction(conndtl, actionName);

            if (conndtl == null)
            {
                BuildBaseResponseObject(methodResponse,false, "Tool connection details not found , please check the request or configuration",HttpStatusCode.NotFound);
                return conndtl;
            }
            methodResponse.IsSuccess = true;
            return conndtl;
        }

        private void BuildBaseResponseObject(baseResponse methodResponse , bool successFlag , string message , HttpStatusCode? statuscode)
        {
            methodResponse.IsSuccess = successFlag;
            methodResponse.Message = message;
            methodResponse.HttpStatusCode = statuscode;
        }

        //public EndPointAgentDetailsResponse GetSentinelEndPoints(GetEndPointsRequest request)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
