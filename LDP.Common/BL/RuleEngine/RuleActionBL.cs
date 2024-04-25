using AutoMapper;
using LDP.Common;
using LDP_APIs.BL.Interfaces;
using LDPRuleEngine.BL.Framework.Actions;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;
using System.Net;

namespace LDPRuleEngine.BL
{
    public class RuleActionBL : IRuleActionBL
    {

        IRuleActionRespository _repo;
        IMapper _mapper;
        RuleActonExecuterFactory _ruleactionExecFactory;

        ILDPlattformBL _plattformBL;
        ILDPSecurityBL _securityBl;
        public RuleActionBL(IRuleActionRespository repo, IMapper mapper
            , RuleActonExecuterFactory ruleactionExecFactory, ILDPlattformBL plattformBL, ILDPSecurityBL securityBL)
        {
            _repo = repo;
            _mapper = mapper;
            _ruleactionExecFactory = ruleactionExecFactory;
            _plattformBL = plattformBL;
            _securityBl = securityBL;
        }
        public RuleActionResponse AddRuleAction(AddRuleActionRequest request)
        {
            RuleActionResponse response = new RuleActionResponse();

            var ruleaction = _repo.GetRuleActionByName(request.RuleActionName);
            if (ruleaction.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "Rule action name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }

            var userdata = _securityBl.GetUserbyID(request.CreateduserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var _mappedRequest = _mapper.Map<AddRuleActionRequest, RuleAction>(request);
            _mappedRequest.Created_user = userdata.Userdata.Name;
            var res = _repo.AddRuleAction(_mappedRequest);
            
            if (res.Result != "")
            {
                response.IsSuccess = true;
                response.Message = "New rule action data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new rule action data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }

       

        public GetRuleActionResponse GetRuleActions()
        {
            GetRuleActionResponse response = new GetRuleActionResponse();
            var res = _repo.GetRuleActions();
            
            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Rule action data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<RuleAction>, List<GetRuleActionModel>>(res.Result);

                foreach (var item in _mappedResponse.ToList())
                {
                    var tool = _plattformBL.GetLDPToolByID(item.ToolID);
                    if (tool.IsSuccess)
                    {
                        item.ToolName = tool.LDPTool.ToolName;
                    }

                    var tooltype = _plattformBL.GetLDPMasterDataByID(item.ToolTypeID);
                    if (tooltype.IsSuccess)
                    {
                        item.ToolTypeName = tooltype.MasterData.DataValue;
                    }

                    var toolaction = _plattformBL.GetTooltypeActionByToolActionID(item.ToolActionID);
                    if (toolaction.IsSuccess)
                    {
                        item.ToolActionName = toolaction.ToolTypeAction.ToolAction;
                    }
                }
                response.RuleActionList = _mappedResponse;
            }
            return response;
        }
        public GetRuleActionResponse GetRuleActions(int orgId)
        {
            GetRuleActionResponse response = new GetRuleActionResponse();
            var res = _repo.GetRuleActions(orgId);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Rule action data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<RuleAction>, List<GetRuleActionModel>>(res.Result);
                var tools = _plattformBL.GetConfiguredLDPTools();

                var tooltypes = _plattformBL.GetMasterDataByDatType(
                    new LDP_APIs.BL.APIRequests.LDPMasterDataRequest()
                    {
                        MaserDataType = Constants.Tool_Type
                    }
                    );

                var toolactions = _plattformBL.GetToolActionList();

                var orgs = _plattformBL.GetOrganizationList();

                foreach (var item in _mappedResponse)
                {
                   // v//ar tool = _plattformBL.GetLDPToolByID(item.ToolID);
                    if (tools.IsSuccess)
                    {
                        var toolIdFilter = tools.LDPToolsList.Where(t => t.ToolId == item.ToolID).ToList();
                        if (toolIdFilter.Count > 0 )
                        {
                            item.ToolName = toolIdFilter.FirstOrDefault().ToolName;
                        }
                        else
                        {
                            //_mappedResponse.Remove(item);
                            //continue;
                        }
                            
                    }

                    //var tooltype = _plattformBL.GetLDPMasterDataByID(item.ToolTypeID);
                    if (tooltypes.IsSuccess)
                    {
                        var filteredData = tooltypes.MasterData.Where(tt => tt.DataID == item.ToolTypeID).ToList();
                        if (filteredData.Count > 0)
                        {
                            item.ToolTypeName = filteredData.FirstOrDefault().DataName;
                        }
                        else
                        {
                            //_mappedResponse.Remove(item);
                            //continue;
                        }
                        //item.ToolTypeName = tooltypes.MasterData.Where(tt => tt.DataID == item.ToolTypeID).FirstOrDefault().DataValue;
                    }

                    //var toolaction = _plattformBL.GetTooltypeActionByToolActionID(item.ToolActionID);
                    if (toolactions.IsSuccess)
                    {
                        var filteredData = toolactions.ToolAcationsList.Where(ta => ta.ToolActionID == item.ToolActionID).ToList();
                        if (filteredData.Count > 0)
                        {
                            item.ToolActionName = filteredData.FirstOrDefault().ToolTypeActionName;
                        }
                        else
                        {
                            //_mappedResponse.Remove(item);
                            //continue;
                        }
                        //item.ToolActionName = toolactions.ToolAcationsList.Where(ta => ta.ToolActionID == item.ToolActionID).FirstOrDefault().ToolTypeActionName;
                    }

                    if (orgs.OrganizationList != null )
                    {
                        var filteredData = orgs.OrganizationList.Where(o => o.OrgID == item.OrgId).ToList();
                        if (filteredData.Count > 0)
                        {
                            item.OrgName = filteredData.FirstOrDefault().OrgName;
                        }
                        else
                        {
                            //_mappedResponse.Remove(item);
                            //continue;
                        }
                        //item.OrgName = orgs.OrganizationList.Where(o => o.OrgID == item.OrgId).FirstOrDefault().OrgName;
                    }
                }
                response.RuleActionList = _mappedResponse;
            }
            return response;
        }
        public RuleActionResponse UpdateRuleAction(UpdateRuleActionRequest request)
        {
            RuleActionResponse response = new RuleActionResponse();

            var userdata = _securityBl.GetUserbyID(request.Modifieduserid);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var ruleaction = _repo.GetRuleActionByNameOnUpdate(request.RuleActionName,request.RuleActionID);
            if (ruleaction.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "Rule action name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }
            var _mappedRequest = _mapper.Map<UpdateRuleActionModel, RuleAction>(request);
            _mappedRequest.Modified_user = userdata.Userdata.Name;
            var res = _repo.UpdateRuleAction(_mappedRequest);
            
            if (res.Result == "")
            {
                response.Message = " Rule action data updated";
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Message = "Failed to update rule action data";
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }

        public RuleActionResponse ExecuteRuleAction(ExecuteRuleActionRequest request)
        {
            RuleActionResponse response = new RuleActionResponse();
            //1. Get the Rule action data 
            var res = _repo.GetRuleActionByRuleActionID(request.ruleActionID);
            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Please check !!...Rule action data not found to execute";
                return response;
            }

            //2. Identify the Tool Type and Action 

            //3. Create the RuleActionExecuter object

            var RuleActionExecuterObj = _ruleactionExecFactory.GetInstance(res.Result.Tool_Type_ID);
           var ruleactionexecuteResponse= RuleActionExecuterObj.ExeCuteRuleAction(request);
            //4. Pass the data and execute the RuleActionExecuter
            if (ruleactionexecuteResponse.IsSuccess)
            {
                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }
            response.Message = ruleactionexecuteResponse.Message;
            return response;

        }

        public GetRuleActionSingleResponse GetRuleActionByRuleActionID(int RuleActionID)
        {
            GetRuleActionSingleResponse response = new GetRuleActionSingleResponse();
            var res = _repo.GetRuleActionByRuleActionID(RuleActionID);
            
            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Rule action data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<RuleAction, GetRuleActionModel>(res.Result);
                var tool = _plattformBL.GetLDPToolByID(_mappedResponse.ToolID);
                if (tool != null)
                {
                    _mappedResponse.ToolName = tool.LDPTool.ToolName;
                }

                var tooltype = _plattformBL.GetLDPMasterDataByID(_mappedResponse.ToolTypeID);
                if (tooltype.MasterData != null)
                {
                    _mappedResponse.ToolTypeName = tooltype.MasterData.DataValue;
                }

                var toolaction = _plattformBL.GetTooltypeActionByToolActionID(_mappedResponse.ToolActionID);
                if (toolaction.ToolTypeAction != null)
                {
                    _mappedResponse.ToolActionName = toolaction.ToolTypeAction.ToolAction;
                }
                var org = _plattformBL.GetOrganizationByID(_mappedResponse.OrgId);

                if (org.OrganizationData != null) 
                {
                    _mappedResponse.OrgName = org.OrganizationData.OrgName;
                }
                response.RuleActionData = _mappedResponse;
            }
            return response;
        }

        public RuleActionResponse DeleteRuleAction(DeleteRuleActionRequest request)
        {
            RuleActionResponse response = new RuleActionResponse();
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _repo.DeleteRuleAction(request,userdata.Userdata.Name);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Rule action deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }
    }
}
