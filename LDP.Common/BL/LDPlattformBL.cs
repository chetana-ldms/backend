using AutoMapper;
using LDP.Common;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;
//using static LDP.Common.Requests.DeleteToolActionRequest;

namespace LDP_APIs.BL
{
    public class LDPlattformBL : ILDPlattformBL
    {
        ILDPlattformRepository _repo;
        public readonly IMapper _mapper;
        private readonly ILDPSecurityBL _securityBl;
        ICommonBL _commonBL;
        public LDPlattformBL(ILDPlattformRepository repo, IMapper mapper, ILDPSecurityBL securityBl, ICommonBL commonBL)
        {
            _repo = repo;
            _mapper = mapper;
            _securityBl = securityBl;
            _commonBL = commonBL;
        }

        #region LDP


        public GetConfiguredLDPToolsResponse GetConfiguredLDPTools()
        {
            GetConfiguredLDPToolsResponse response = new GetConfiguredLDPToolsResponse();
            var res = _repo.GetConfiguredLDPTools();
            
            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "LDP Tools not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
           else
            {
       
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<LDP_APIs.DAL.Entities.LDPTool>, List<GetLDPTool>>(res.Result);
                response.LDPToolsList = _mappedResponse.ToList();
                response.Message = "Success";
                
            }
            return response;
        }

        public GetConfiguredLDPToolsResponse GetConfiguredLDPTools(GetLDPToolRequest request)
        {
            GetConfiguredLDPToolsResponse response = new GetConfiguredLDPToolsResponse();
            var res = _repo.GetConfiguredLDPTools(request);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "LDP Tools not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<LDP_APIs.DAL.Entities.LDPTool>, List<GetLDPTool>>(res.Result);
                response.LDPToolsList = _mappedResponse.ToList();
                response.Message = "Success";

            }
            return response;
        }
        public LDPToolResponse NewLDPTool(LDPToolRequest request)
        {
            LDPToolResponse response = new LDPToolResponse();
            var _mappedRequest = _mapper.Map<LDPToolRequest, LDP_APIs.DAL.Entities.LDPTool>(request);
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.Created_user = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            // Get ToolTypeName
           
            var _toolTypeName = this.GetMasterDataValueByDataId(LDP.Common.Constants.Tool_Type, request.ToolTypeId);
            _mappedRequest.Tool_Type = _toolTypeName;
            _mappedRequest.active = 1;
            var res = _repo.NewLDPTool(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "LDP Tool added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = "Failed to add new LDP tool";
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }

            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Created_user);
            _templateData.Add("NewTool", request.ToolName);


            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.CreatedUserId,
                 OrgId = userdata.Userdata.OrgId,
                CreatedDate = request.CreatedDate,
                ActivityDate = request.CreatedDate
            }, _templateData, Constants.Activity_Template_Tool_Add, response.IsSuccess
            );
            return response;
        }

        public LDPToolResponse UpdateLDPTool(UpdateLDPToolRequest request)
        {
            LDPToolResponse response = new LDPToolResponse();
            var _mappedRequest = _mapper.Map<UpdateLDPToolRequest, LDP_APIs.DAL.Entities.LDPTool>(request);
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.Modified_user = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _toolTypeName = this.GetMasterDataValueByDataId(LDP.Common.Constants.Tool_Type, request.ToolTypeId);
            _mappedRequest.Tool_Type = _toolTypeName;

            var res = _repo.UpdateLDPTool(_mappedRequest);
           
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "LDP Tool data updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update LDP tool";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;

            }

            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, userdata.Userdata.Name);
            _templateData.Add("UpdateTool", request.ToolName);


            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.ModifiedUserId,
                OrgId = userdata.Userdata.OrgId,
                CreatedDate = request.UpdatedDate,
                ActivityDate = request.UpdatedDate
            }, _templateData, Constants.Activity_Template_Tool_Update, response.IsSuccess
            );

            return response;
        }

        public DeleteLDPToolResponse DeleteLDPTool(DeleteLDPToolRequest request)
        {
            DeleteLDPToolResponse response = new DeleteLDPToolResponse();
            string _deletedUserName = string.Empty;
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata != null)
            {
                _deletedUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var _toolData = _repo.GetLDPToolByID(request.ToolId).Result;

            if (_toolData == null) 
            {
                response.IsSuccess = false;
                response.Message = "Invalid Tool";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            
            var res = _repo.DeleteLDPTool(request, _deletedUserName);

            if (res.Result == "") 
            {
                response.IsSuccess = true;
                response.Message = "Tool deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, userdata.Userdata.Name);
            _templateData.Add("DeleteTool", _toolData.Tool_Name);


            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.DeletedUserId,
                OrgId = userdata.Userdata.OrgId,
                CreatedDate = request.DeletedDate,
                ActivityDate = request.DeletedDate
            }, _templateData, Constants.Activity_Template_Tool_Delete, response.IsSuccess
            );


            return response;
        }
        #endregion

        #region Organizations
        public GetOrganizationsResponse GetOrganizationList()
        {
            GetOrganizationsResponse response = new GetOrganizationsResponse();
            var res = _repo.GetOrganizationList();
            
            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Organization data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                var _mappedResponse = _mapper.Map<List<Organization>, List<GettOrganizationsModel>>(res.Result);
                response.OrganizationList = _mappedResponse.ToList();
                response.HttpStatusCode = HttpStatusCode.OK;
            }
                    
            return response;
        }

        public OrganizationResponse AddOrganization(AddOrganizationRequest request)
        {
            OrganizationResponse response = new OrganizationResponse();
            var _mappedRequest = _mapper.Map<AddOrganizationRequest, Organization>(request);
            _mappedRequest.active = 1;

            var userdata =  _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata != null )
            {
                _mappedRequest.Created_User = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.AddOrganization(_mappedRequest);
            
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "New Organization data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
                
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new Organization data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Created_User);
            _templateData.Add("NewOrg", request.OrgName );


            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.CreatedUserId,
               // OrgId = request.OrgId,
                CreatedDate = request.CreatedDate,
                ActivityDate = request.CreatedDate

            }, _templateData, Constants.Activity_Template_Organization_Add, response.IsSuccess
            );
            return response;
        }

        public OrganizationResponse UpdateOrganization(UpdateOrganizationRequest request)
        {
            OrganizationResponse response = new OrganizationResponse();
            var _mappedRequest = _mapper.Map<UpdateOrganizationModel, Organization>(request);
            var userdata = _securityBl.GetUserbyID(request.UpdatedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.Modified_User = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var _OrgData = _repo.GetOrganizationByID(request.OrgID).Result;
            if (_OrgData == null )
            {
                response.IsSuccess = false;
                response.Message = "Invalid Org Id";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.UpdateOrganization(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Organization data updated";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update the Organization data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Modified_User);
            _templateData.Add("UpdateOrg", _OrgData.Org_Name);


            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.UpdatedUserId,
                OrgId = request.OrgID,
                CreatedDate = request.UpdatedDate,
                ActivityDate = request.UpdatedDate

            }, _templateData, Constants.Activity_Template_Organization_Update, response.IsSuccess
              );
            return response;
        }


        public DeleteOrganizationsResponse DeleteOrganization(DeleteOrganizationRequest request)
        {
            DeleteOrganizationsResponse response = new DeleteOrganizationsResponse();
            string _deleteUserName = string.Empty;
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata != null)
            {
                _deleteUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid user";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _OrgData = _repo.GetOrganizationByID(request.OrgID).Result;
            if (_OrgData == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid Org Id";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.DeleteOrganization(request, _deleteUserName);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Organization deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }

            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, userdata.Userdata.Name);
            _templateData.Add("DeleteOrg", _OrgData.Org_Name);


            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.DeletedUserId,
                OrgId = request.OrgID,
                CreatedDate = request.DeletedDate,
                ActivityDate = request.DeletedDate

            }, _templateData, Constants.Activity_Template_Organization_Delete, response.IsSuccess
              );

            return response;
        }

        #endregion

        #region OrganizationTool

        public GetOrganizationToolsResponse GetOrganizationToolsList()
        {
            GetOrganizationToolsResponse response = new GetOrganizationToolsResponse();
            var res = _repo.GetOrganizationToolList();
            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Organization Tools data not found";
            }
            else
            {
                var orgtools = res.Result;
                orgtools.ForEach(ot =>
                ot.ToolActions = _repo.GetOrganizationToolActions(ot.OrgToolID).Result
                ) ;
                response.Message = "Success";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.OrganizationToolList = orgtools;
            }
            return response;
        }

        public GetOrganizationToolResponse GetOrganizationToolByID(int id)
        {
            GetOrganizationToolResponse response = new GetOrganizationToolResponse();
            var res = _repo.GetOrganizationToolByID(id);
            
            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Organization Tool data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                var orgtool = res.Result;
                orgtool.ToolActions = _repo.GetOrganizationToolActions(orgtool.OrgToolID).Result;
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                response.OrganizationToolData = orgtool;
            }
            return response;
        }
        public GetOrganizationToolsResponse GetOrganizationToolsByToolType(string toolType)
        {
            GetOrganizationToolsResponse response = new GetOrganizationToolsResponse();
            var res = _repo.GetOrganizationToolsByToolType(toolType);

            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Organization Tool data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                var orgtools = res.Result;
                orgtools.ForEach(ot =>
                ot.ToolActions = _repo.GetOrganizationToolActions(ot.OrgToolID).Result
                );
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                response.OrganizationToolList = orgtools;
            }
            return response;
        }

        public OrganizationToolsResponse AddOrganizationTool(AddOrganizationToolsRequest request)
        {
            OrganizationToolsResponse response = new OrganizationToolsResponse();
            var _mappedRequest = _mapper.Map<AddOrganizationToolsRequest, OganizationTool>(request);
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.Created_User = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            _mappedRequest.active = 1;
            var res = _repo.AddOrganizationTool(_mappedRequest);
           
            if (res.Result == "")
            {
                var _mappedOrgToolActions = _mapper.Map<List<AddOrganizationToolActionModel>, List<OrganizationToolAction>>(request.ToolActions);

                _mappedOrgToolActions.ForEach(ta =>
                 {
                     ta.org_id = request.OrgID;
                     ta.tool_id = request.ToolID;
                     ta.org_tool_id = _mappedRequest.Org_Tool_ID;
                  }

                 ) ;
                var res1 = _repo.AddOrganizationToolAction(_mappedOrgToolActions);
                response.IsSuccess = true;
                response.Message = "New Organization Tool data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new Organization Tool data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            //       Activity log creation
            var _tooldata = _repo.GetLDPToolByID(request.ToolID).Result;
            var _orgdata = _repo.GetOrganizationByID(request.OrgID).Result;
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Created_User);
            _templateData.Add("Tool", _tooldata.Tool_Name);
            _templateData.Add("OrgName", _orgdata.Org_Name);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.CreatedUserId,
                OrgId = request.OrgID,
                CreatedDate = request.CreatedDate,
                ActivityDate = request.CreatedDate
            }, _templateData, Constants.Activity_Template_OrgTool_Add, response.IsSuccess
            );
            return response;
        }

        public OrganizationToolsResponse UpdateOrganizationTool(UpdateOrganizationToolsRequest request)
        {
            OrganizationToolsResponse response = new OrganizationToolsResponse();
            var _mappedRequest = _mapper.Map<UpdateOrganizationToolsRequest, OganizationTool>(request);
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.Modified_User = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.UpdateOrganizationTool(_mappedRequest);
           
            if (res.Result == "")
            {
                var _mappedOrgToolActions = _mapper.Map<List<UpdateOrganizationToolActionModel>, List<OrganizationToolAction>>(request.ToolActions);

                _mappedOrgToolActions.ForEach(ta =>
                {
                    ta.org_id = request.OrgID;
                    ta.tool_id = request.ToolID;
                    ta.org_tool_id = _mappedRequest.Org_Tool_ID;
                }

                 );
                var res1 = _repo.UpdateOrganizationToolActions(_mappedOrgToolActions);
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Organization Tool data upated";
            }
            else
            {
                response.IsSuccess = false ;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                response.Message = "Failed to update the Organization Tool data";
            }

            //       Activity log creation
            var _tooldata = _repo.GetLDPToolByID(request.ToolID).Result;
            var _orgdata = _repo.GetOrganizationByID(request.OrgID).Result;
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Modified_User);
            _templateData.Add("Tool", _tooldata.Tool_Name);
            _templateData.Add("OrgName", _orgdata.Org_Name);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.ModifiedUserId,
                OrgId = request.OrgID,
                CreatedDate = request.ModifiedDate,
                ActivityDate = request.ModifiedDate
            }, _templateData, Constants.Activity_Template_OrgTool_Add, response.IsSuccess
            );
            return response;
        }

        public OrganizationToolsResponse DeleteOrganizationTool(DeleteOrganizationToolsRequest request)
        {
            OrganizationToolsResponse response = new OrganizationToolsResponse();
            string _deleteUserName = string.Empty;
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata != null)
            {
                _deleteUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid user";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.DeleteOrganizationTool(request, _deleteUserName);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Organization Tool deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }

            //       Activity log creation
            var _orgtooldata = _repo.GetOrganizationToolByID(request.OrgToolID).Result;
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username,userdata.Userdata.Name);
            if (_orgtooldata != null)
            {
                _templateData.Add("Tool", _orgtooldata.ToolName);
                _templateData.Add("OrgName", _orgtooldata.OrgName);

            }

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.DeletedUserId,
                OrgId = _orgtooldata.OrgID,
                CreatedDate = request.DeletedDate,
                ActivityDate = request.DeletedDate
            }, _templateData, Constants.Activity_Template_OrgTool_Add, response.IsSuccess
            );

            return response;
        }

        public OrganizationToolModel GetToolConnectionDetails(int OrgID, int tooltypeID)
        {
            

            var repoResponse = _repo.GetToolConnectionDetails(OrgID, tooltypeID);
            if (repoResponse.Result == null)
            {
                  return null;
            }
            OrganizationToolModel _mappedResponse = new OrganizationToolModel();
            _mappedResponse.AuthKey = repoResponse.Result.Auth_Key;
            _mappedResponse.ToolID = repoResponse.Result.Tool_ID;
            _mappedResponse.OrgToolID = repoResponse.Result.Org_Tool_ID;
            _mappedResponse.OrgID = repoResponse.Result.Org_ID;
            _mappedResponse.ToolID = repoResponse.Result.Tool_ID;
            var orgToolActions = _repo.GetOrganizationToolActions(_mappedResponse.OrgToolID).Result;
            if (orgToolActions != null && orgToolActions.Count > 0) 
            {
                _mappedResponse.ToolActions = new List<OrganizationToolActionModel>();
                _mappedResponse.ToolActions.AddRange(orgToolActions);
            }
            else
            {
                return null;
            }

            return _mappedResponse;
        }

        public OrganizationToolModel FilterConnectionAction(OrganizationToolModel orgtool, string actionname)
        {
            OrganizationToolModel filteredToolmode = orgtool;

            var toolactions = orgtool.ToolActions;

            if  (toolactions.Count >0)
            {
                var res = toolactions.Where(ta => ta.ToolActionName == actionname).FirstOrDefault();
                if (res != null ) 
                {
                    filteredToolmode.ToolActions = new List<OrganizationToolActionModel>();
                    filteredToolmode.ToolActions.Add(res);
                    return filteredToolmode;
                }
            }

            return null ;
        }

        public Task<string> UpdateLastReadPKID(GetOffenseDTO request)
        {
            return _repo.UpdateLastReadPKID(request);
        }

        public string UpdateLastReadAlertDate(OrganizationToolModel request)
        {
            return _repo.UpdateLastReadAlertDate(request).Result;
        }



        public Task<GetOffenseDTO> GetLastReadPKID(GetOffenseDTO request)
        {
            return _repo.GetLastReadPKID(request);
        }


        #endregion

        #region Master data types
        public LDPMasterDataResponse GetMasterDataByDatType(LDPMasterDataRequest request)
        {
            LDPMasterDataResponse response = new LDPMasterDataResponse();
            var res = _repo.GetMasterData(request.MaserDataType);
            response.IsSuccess = true;
            if (res == null)
                response.Message = "requesting master data not found";
            else
            {
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<List<LDPMasterData>, List<LDPMasterDataModel>>(res.Result);
                response.MasterData = _mappedResponse;
            }
            return response;
        }

        public LDPMasterDataResponse GetMasterDataByMultipleTypes(LDPMasterDataByMultipleTypesRequest request)
        {
            LDPMasterDataResponse response = new LDPMasterDataResponse();
            var res = _repo.GetMasterDataByMultipleTypes(request.MaserDataTypes);
            response.IsSuccess = true;
            if (res == null)
                response.Message = "requesting master data not found";
            else
            {
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<List<LDPMasterData>, List<LDPMasterDataModel>>(res.Result);
                response.MasterData = _mappedResponse;
            }
            return response;
        }

        public int GetMasterDataByDataValue(string MasterDataType, string MasterDataValue)
        {
            var res = _repo.GetMasterDataByDataValue(MasterDataType, MasterDataValue);

            if (res.Result == null )
            {
                return 0;
            }
            return res.Result.data_id;

        }

        public LDPMasterDataModel GetMasterDataobjectByDataValue(string MasterDataType, string MasterDataValue)
        {
            var res = _repo.GetMasterDataByDataValue(MasterDataType, MasterDataValue);

            if (res.Result == null)
            {
                return null ;
            }
            var _mappedResponse = _mapper.Map<LDPMasterData, LDPMasterDataModel>(res.Result);

            return _mappedResponse;

        }
        public string GetMasterDataValueByDataId(string MasterDataType, int MasterDataId)
        {
            var res = _repo.GetMasterDataValueByDataId(MasterDataType, MasterDataId);

            if (res.Result == null)
            {
                return null;
            }

            return res.Result.data_value;

        }
        #endregion

        #region Tool type actions

        public GetToolTypeActionResponse GetToolTypeActions()
        {
            GetToolTypeActionResponse response = new GetToolTypeActionResponse();
            var res = _repo.GetToolTypeActions();
            response.IsSuccess = true;
            if (res == null)
                response.Message = "Tool type actins data not found";
            else
            {
                response.Message = "Success";

                response.ToolTypeActionsList = res.Result;
            }
            return response;
        }

        public GetToolTypeActionResponse GetToolTypeActionsByToolType(GetToolTypeActinByToolTypeRequest requuest)
        {
            GetToolTypeActionResponse response = new GetToolTypeActionResponse();
            var res = _repo.GetToolTypeActionsByToolType(requuest);
            response.IsSuccess = true;
            if (res == null)
                response.Message = "Tool type actins data not found";
            else
            {
                response.Message = "Success";

                response.ToolTypeActionsList = res.Result;
            }
            return response;
        }
        public ToolTypeActionResponse AddToolTypeAction(AddToolTypeActionRequest request)
        {
            ToolTypeActionResponse response = new ToolTypeActionResponse();
            var _mappedRequest = _mapper.Map<AddToolTypeActionRequest, ToolTypeAction>(request);

            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.Created_user = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            string tooltype = string.Empty;
            _mappedRequest.active = 1;
            var _toolTypeData = _repo.GetLDPMasterDataByID(request.ToolTypeID).Result;
            if (_toolTypeData != null) 
            {
                tooltype = _toolTypeData.data_value;

            };


            var res = _repo.AddToolTypeAction(_mappedRequest);
            
            if (res.Result == "")
            {
                response.Message = "New Tool type action data added";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new Tool type action data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }

            //       Activity log creation
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Created_user);
            _templateData.Add("NewAction", request.ToolAction);
            _templateData.Add("ToolType", tooltype);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.CreatedUserId,
                OrgId = 0,
                CreatedDate = request.CreatedDate,
                ActivityDate = request.CreatedDate
            }, _templateData, Constants.Activity_Template_ToolType_Action_Add, response.IsSuccess
            );
            return response;
        }

        public ToolTypeActionResponse UpdateToolTypeAction(UpdateToolTypeActionRequest request)
        {
            ToolTypeActionResponse response = new ToolTypeActionResponse();
            var _mappedRequest = _mapper.Map<UpdateToolTypeActionRequest, ToolTypeAction>(request);
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.Modified_user = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.UpdateToolTypeAction(_mappedRequest);
           
            if (res.Result == "")
            {
                response.Message = "Tool type action data upated ";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = "Failed to update Tool type action data ";
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }

            //       Activity log creation
            string tooltype = string.Empty;
            var _toolTypeData = _repo.GetLDPMasterDataByID(request.ToolTypeID).Result;
            if (_toolTypeData != null)
            {
                tooltype = _toolTypeData.data_value;

            };
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Modified_user);
            _templateData.Add("TooltypeAction", request.ToolAction);
            _templateData.Add("ToolType", tooltype);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.ModifiedUserId,
                OrgId = 0,
                CreatedDate = request.ModifiedDate,
                ActivityDate = request.ModifiedDate
            }, _templateData, Constants.Activity_Template_ToolType_Action_Update, response.IsSuccess
            );

            return response;
        }


        public DeleteToolTypeActionResponse DeleteToolTypeAction(DeleteToolTypeActionRequest request)
        {
            DeleteToolTypeActionResponse response = new DeleteToolTypeActionResponse();
            string _deleteUserName = string.Empty;
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata != null)
            {
                _deleteUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid user";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.DeleteToolTypeAction(request, _deleteUserName);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Organization Tool deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }

            //       Activity log creation
            var _ToolTypeAction = _repo.GetToolTypeActionByID(request.ToolTypeActionID).Result;
            int tooltypeid = 0;
            string tooltypeaction = string.Empty;
            LDPMasterData _toolTypeData = null;
            if (_ToolTypeAction != null) 
            {
                tooltypeid = _ToolTypeAction.Tool_Type_ID;
                tooltypeaction = _ToolTypeAction.Tool_Action;
                 _toolTypeData = _repo.GetLDPMasterDataByID(tooltypeid).Result;
            }
            string tooltype = string.Empty;
            
            if (_toolTypeData != null)
            {
                tooltype = _toolTypeData.data_value;

            };
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _deleteUserName);
            _templateData.Add("TooltypeAction", tooltypeaction);
            _templateData.Add("ToolType", tooltype);

            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.DeletedUserId,
                OrgId = 0,
                CreatedDate = request.DeletedDate,
                ActivityDate = request.DeletedDate
            }, _templateData, Constants.Activity_Template_ToolType_Action_Delete, response.IsSuccess
            );
            return response;
        }

        public GetToolTypeActionSingleResponse GetTooltypeActionByToolActionID(int id)
        {
            GetToolTypeActionSingleResponse response = new GetToolTypeActionSingleResponse();
            var res = _repo.GetTooltypeActionByToolActionID(id);

            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Tool type action data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                response.ToolTypeAction = res.Result;
            }
            return response;
        }

        #endregion

        #region Tool actions 
        public GetToolActionResponse GetToolActionList()
        {
            GetToolActionResponse response = new GetToolActionResponse();
            var res = _repo.GetToolActions();
            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Tool actions data not found";
            }
            else
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Success";
                response.ToolAcationsList = res.Result;
            }
            return response;
        }

        public GetToolActionResponse GetToolActionsByTool(GetActionRequest request)
        {
            GetToolActionResponse response = new GetToolActionResponse();
            var res = _repo.GetToolActionsByTool(request);
            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Tool actions data not found";
            }
            else
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Success";
                response.ToolAcationsList = res.Result;
            }
            return response;
        }

        public GetToolActionSingleResponse GetToolActionByID(int id)
        {
            GetToolActionSingleResponse response = new GetToolActionSingleResponse();
            var res = _repo.GetToolActionByID(id);
            
            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Tool action data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                response.ToolAcation = res.Result;
            }
            return response;
        }
        public ToolActionResponse AddToolAction(AddToolActionRequest request)
        {
            ToolActionResponse response = new ToolActionResponse();
            var _mappedRequest = _mapper.Map<AddToolActionRequest, LDPToolActions>(request);
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.Created_user = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            _mappedRequest.active = 1;
            var res = _repo.AddTooolAction(_mappedRequest);
            
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "New Tool action data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
           
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new Tool action data";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                
            }
            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Created_user);
            string _tooltypeAction = string.Empty;
            var _tooltypeactionData = _repo.GetToolTypeActionByID(request.ToolTypeActionID).Result;
            if (_tooltypeactionData != null )
            {
                _tooltypeAction = _tooltypeactionData.Tool_Action;
            }
            _templateData.Add("ToolTypeAction", _tooltypeAction);
            //
            string _tool = string.Empty;
             var _toolData = _repo.GetLDPToolByID(request.ToolID).Result;
            if (_toolData != null )
            {
                _tool = _toolData.Tool_Name;
            }
            _templateData.Add("Tool", _tool);
            //
            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.CreatedUserId,
                OrgId = 0,
                CreatedDate = request.CreatedDate,
                ActivityDate = request.CreatedDate
            }, _templateData, Constants.Activity_Template_Tool_Action_Add, response.IsSuccess
            );
            return response;
        }

        public ToolActionResponse UpdateToolAction(UpdateToolActionRequest request)
        {
            ToolActionResponse response = new ToolActionResponse();
            var _mappedRequest = _mapper.Map<UpdateToolActionRequest, LDPToolActions>(request);

            //var res = _repo.UpdateToolAction(_mappedRequest);
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata != null)
            {
                _mappedRequest.Modified_user = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.UpdateToolAction(_mappedRequest);
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "New Tool action data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }

            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new Tool action data";
                response.HttpStatusCode = HttpStatusCode.BadRequest;

            }

            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _mappedRequest.Modified_user);
            string _tooltypeAction = string.Empty;
            var _tooltypeactionData = _repo.GetToolTypeActionByID(request.ToolTypeActionID).Result;
            if (_tooltypeactionData != null)
            {
                _tooltypeAction = _tooltypeactionData.Tool_Action;
            }
            _templateData.Add("ToolTypeAction", _tooltypeAction);
            //
            string _tool = string.Empty;
            var _toolData = _repo.GetLDPToolByID(request.ToolID).Result;
            if (_toolData != null)
            {
                _tool = _toolData.Tool_Name;
            }
            _templateData.Add("Tool", _tool);
            //
            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.ModifiedUserId,
                OrgId = 0,
                CreatedDate = request.ModifiedDate,
                ActivityDate = request.ModifiedDate
            }, _templateData, Constants.Activity_Template_Tool_Action_Update, response.IsSuccess
            );
            return response;
        }

        public DeleteToolActionResponse DeleteToolAction(DeleteToolActionRequest request)
        {
            DeleteToolActionResponse response = new DeleteToolActionResponse();
            string? _deleteUserName = string.Empty;
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata != null)
            {
                _deleteUserName = userdata.Userdata.Name;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid user";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.DeleteToolAction(request, _deleteUserName);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Organization Tool deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }

            Dictionary<string, string> _templateData = new Dictionary<string, string>();
            _templateData.Add(Constants.Acvities_Template_username, _deleteUserName);
            string _tooltypeAction = string.Empty;
            string _tool = string.Empty;
            var _toolactionData = _repo.GetToolActionByID(request.ToolActionId).Result;
            if (_toolactionData != null)
            {
                //var _tooltypeactionData = _repo.GetToolTypeActionByID(_toolactionData.ToolTypeActionID).Result;
                //if (_tooltypeactionData != null)
                //{
                //    _tooltypeAction = _tooltypeactionData.Tool_Action;
                //}
                _tooltypeAction = _toolactionData.ToolTypeActionName;
                _tool = _toolactionData.ToolTypeActionName;

                // var _toolData = _repo.GetLDPToolByID(_toolactionData.ToolID).Result;
                //if (_toolData != null)
                //{
                //    _tool = _toolData.Tool_Name;
                //}
            }
            
            _templateData.Add("ToolTypeAction", _tooltypeAction);
            //
            _templateData.Add("Tool", _tool);
            //
            _commonBL.LogActivity(new LDP.Common.Requests.Common.AddActivityRequest()
            {
                CreatedUserId = request.DeletedUserId,
                OrgId = 0,
                CreatedDate = request.DeletedDate,
                ActivityDate = request.DeletedDate
            }, _templateData, Constants.Activity_Template_Tool_Action_Delete, response.IsSuccess
            );
            return response;
        }
        public GetConfiguredLDPToolResponse GetLDPToolByID(int tooid)
        {

            GetConfiguredLDPToolResponse response = new GetConfiguredLDPToolResponse();
                var res = _repo.GetLDPToolByID(tooid);
                
                if (res.Result == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Tool data not found";
                    response.HttpStatusCode = HttpStatusCode.NotFound;

                }
                else
                {
                    response.IsSuccess = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = "Success";
                    var _mappedResponse = _mapper.Map<LDP_APIs.DAL.Entities.LDPTool, GetLDPTool>(res.Result);
                    response.LDPTool = _mappedResponse;
                }
                return response;
         
        }

        public GetOrganizationResponse GetOrganizationByID(int id)
        {
            GetOrganizationResponse response = new GetOrganizationResponse();
            var res = _repo.GetOrganizationByID(id);
            
            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Organization data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<Organization, GettOrganizationsModel>(res.Result);
                response.OrganizationData = _mappedResponse;
            }
            return response;
        }
        public LDPMasterDataByIDResponse GetLDPMasterDataByID(int id)
        {
            LDPMasterDataByIDResponse response = new LDPMasterDataByIDResponse();
            var res = _repo.GetLDPMasterDataByID(id);
           
            if (res == null)
            {
                response.Message = "Master data not found";
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.Message = "Success";
                response.IsSuccess = true;
                var _mappedResponse = _mapper.Map<LDPMasterData, LDPMasterDataModel>(res.Result);
                response.MasterData = _mappedResponse;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            return response;
        }

        public GetToolTypeActionSingleResponse GetToolTypeActionByID(int id)
        {
            GetToolTypeActionSingleResponse response = new GetToolTypeActionSingleResponse();
            var res = _repo.GetToolTypeActionByID(id);
           
            if (res == null)
            {
                response.IsSuccess = false;
                response.Message = "Tool type action data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Success";
                response.IsSuccess = true;
                var _mappedResponse = _mapper.Map<ToolTypeAction, GetToolTypeActionModel>(res.Result);
                response.ToolTypeAction = _mappedResponse;
            }
            return response;
        }

        #endregion

        }
}
