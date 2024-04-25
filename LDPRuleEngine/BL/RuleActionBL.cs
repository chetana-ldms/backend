using AutoMapper;
using LDPRuleEngine.BL.Framework;
using LDPRuleEngine.BL.Framework.Actions;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;

namespace LDPRuleEngine.BL
{
    public class RuleActionBL : IRuleActionBL
    {

        IRuleActionRespository _repo;
        IMapper _mapper;
        RuleActonExecuterFactory _ruleactionExecFactory;
        public RuleActionBL(IRuleActionRespository repo,IMapper mapper, RuleActonExecuterFactory ruleactionExecFactory)
        {
            _repo = repo;
            _mapper = mapper;
            _ruleactionExecFactory = ruleactionExecFactory;
        }
        public RuleActionResponse AddRuleAction(AddRuleActionRequest request)
        {
            RuleActionResponse response = new RuleActionResponse();
            var _mappedRequest = _mapper.Map<AddRuleActionRequest, RuleAction>(request);
            var res = _repo.AddRuleAction(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result != "")
                response.Message = "New rule action data added";
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new rule action data";
            }
            return response;
        }

       

        public GetRuleActionResponse GetRuleActions()
        {
            GetRuleActionResponse response = new GetRuleActionResponse();
            var res = _repo.GetRuleActions();
            response.IsSuccess = true;
            if (res == null)
                response.Message = "Rule action data not found";
            else
            {
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<List<RuleAction>, List<GetRuleActionModel>>(res.Result);
                response.RuleActionList = _mappedResponse;
            }
            return response;
        }

        public RuleActionResponse UpdateRuleAction(UpdateRuleActionRequest request)
        {
            RuleActionResponse response = new RuleActionResponse();
            var _mappedRequest = _mapper.Map<UpdateRuleActionModel, RuleAction>(request);

            var res = _repo.UpdateRuleAction(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
                response.Message = " Rule action data updated";
            else
            {
                response.Message = "Failed to update rule action data";
            }
            return response;
        }

        public RuleActionResponse ExecuteRuleAction(ExecuteRuleActionRequest request)
        {
            RuleActionResponse response = new RuleActionResponse();
            //1. Get the Rule action data 
            var res = _repo.GetRuleActionByRuleActionID(request.ruleActionID);
            if (res == null)
            {
                response.IsSuccess = false;
                response.Message = "Please check !!...Rule action data not found to execute";
                return response;
            }

            //2. Identify the Tool Type and Action 

            //3. Create the RuleActionExecuter object

            var RuleActionExecuterObj = _ruleactionExecFactory.GetInstance(res.Result[0].Tool_Type_ID);
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

        public GetRuleActionResponse GetRuleActionByRuleActionID(int RuleActionID)
        {
            GetRuleActionResponse response = new GetRuleActionResponse();
            var res = _repo.GetRuleActionByRuleActionID(RuleActionID);
            response.IsSuccess = true;
            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Rule action data not found";
            }
            else
            {
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<List<RuleAction>, List<GetRuleActionModel>>(res.Result);
                response.RuleActionList = _mappedResponse;
            }
            return response;
        }
    }
}
