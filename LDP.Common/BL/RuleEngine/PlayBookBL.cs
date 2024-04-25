using AutoMapper;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Interfaces;
using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;
using LDPRuleEngine.DAL.Repositories;
using System.Net;

namespace LDPRuleEngine.BL
{
    public class PlayBookBL : IPlaybookBL
    {
        IMapper _mapper;
        IPlayBookRepository _Playbookrepo;
        IPlayBookDtlsRepository _PlaybookDtlsrepo;
        IRulesConfigurationBL _ruleBl;
        IRuleActionBL _ruleActionbL;
        IAlertsBL _alertbl;
        ILDPSecurityBL _securityBl;
        ILDPlattformBL _platformBL;
        public PlayBookBL(IPlayBookRepository repo, IMapper mapper, IPlayBookDtlsRepository PlaybookDtlsrepo,
            IRulesConfigurationBL ruleBl, IRuleActionBL ruleActionbL
            , IAlertsBL alertbl, ILDPSecurityBL securityBl, ILDPlattformBL platformBL)
        {
            _Playbookrepo = repo;
            _mapper = mapper;
            _PlaybookDtlsrepo = PlaybookDtlsrepo;
            _ruleActionbL = ruleActionbL;
            _ruleBl = ruleBl;
            _alertbl = alertbl;
            _securityBl = securityBl;
            _platformBL = platformBL;
        }
        public PlayBookResponse AddPlayBook(AddPlayBookRequest request)
        {
            PlayBookResponse response = new PlayBookResponse();
            var userdata = _securityBl.GetUserbyID(request.CreatedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var playbook = _Playbookrepo.GetPlayBookbyName(request.PlayBookName);
            if (playbook.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "Play book name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }
            var _playbookentity = _mapper.Map<AddPlayBookRequest, PlayBook>(request);
            _playbookentity.Created_User = userdata.Userdata.Name;
            var _playbookdtlsentity = _mapper.Map<List<AddPlayBookDtlsModel> , List<PlayBookDtl>>(request.PlaybookDtls);
            
 
            _playbookdtlsentity.ForEach(bookdtl =>
            {
                bookdtl.Created_Date = _playbookentity.Created_Date;
                bookdtl.Created_User = _playbookentity.Created_User;
            });

            var ret = _Playbookrepo.AddPlayBook(_playbookentity, _playbookdtlsentity);

            response.IsSuccess = true;
            response.Message = ret.Result;
            response.HttpStatusCode = (HttpStatusCode)200;
            return response;

        }

        public DeletePlayBookResponse DeletePlaybook(DeletePlayBookRequest request)
        {
            DeletePlayBookResponse response = new DeletePlayBookResponse();
            var userdata = _securityBl.GetUserbyID(request.DeletedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var res = _Playbookrepo.DeletePlayBook(request,userdata.Userdata.Name);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "Play book deleted";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = res.Result;
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;

        }

        public PlayBookResponse ExecutePlaybook(ExecutePlayBookRequest request)
        {
            PlayBookResponse response = new PlayBookResponse();
            ExecuteRuleActionRequest actionRequest = null;
            ExecuteRuleRequest rulerequest = null;
            bool ruleexcutionFlag = false;
            var PlaybookdtlsData = _PlaybookDtlsrepo.GetPlaybookDtlsByPlaybookID(request.PlaybookID).Result.OrderBy( pbdtl => pbdtl.Execution_Sequence_Number);
            GetAlertRequest alertrequest = new GetAlertRequest() { alertID = request.alertiD };
            var AlertData = _alertbl.GetAlertData(alertrequest).AlertsList.FirstOrDefault();
            if (AlertData == null)
            {
                response.IsSuccess=false;
                response.Message = "Alert data not found";
                return response;
            }
            foreach(var dtl in PlaybookdtlsData)
            {
                if (dtl.Play_Book_Item_Type == "rule")
                {
                    rulerequest = new ExecuteRuleRequest();
                    rulerequest.RuleID = dtl.Play_Book_Item_Type_RefID;
                    rulerequest.InputTextToRuleExecute = AlertData.Name;
                    var ruleexectinResponse = _ruleBl.ExecuteRule(rulerequest);
                    ruleexcutionFlag = ruleexectinResponse.IsSuccess;
                    response.Message = response.Message + ruleexectinResponse.RuleExecuterMessage;
                    response.IsSuccess = ruleexectinResponse.IsSuccess;
                }
                if (dtl.Play_Book_Item_Type == "action" && ruleexcutionFlag)
                {
                    actionRequest = new ExecuteRuleActionRequest();
                    actionRequest.ruleActionID = dtl.Play_Book_Item_Type_RefID;
                    actionRequest.alertiD = request.alertiD;
                    actionRequest.ToolTypeID = request.ToolTypeID;
                    actionRequest.OrgID = request.OrgID;
                    actionRequest.ToolID = request.ToolID;
                    var actionExecutionResponse = _ruleActionbL.ExecuteRuleAction(actionRequest);
                    //ruleexcutionFlag = ruleexectinResponse.IsSuccess;
                    response.Message = response.Message + actionExecutionResponse.Message;
                    response.IsSuccess = actionExecutionResponse.IsSuccess;
                }
            }

            return response;
        }

        public GetPlayBookResponse GetPlayBookbyPlaybookID(int playbookid)
        {
            GetPlayBookResponse response = new GetPlayBookResponse();
            var DBplaybooks = _Playbookrepo.GetPlayBookByPlayBookID(playbookid);

            if (DBplaybooks.Result == null )
            {
                response.IsSuccess = false;
                response.Message = "Play book not found";
                return response;
            }
            var DBplaybookDtls = _PlaybookDtlsrepo.GetPlaybookDtlsByPlaybookID(playbookid);
            var DBPlaybooksModel = _mapper.Map<PlayBook, GetPlayBookModel>(DBplaybooks.Result);
            

            if (DBplaybookDtls != null)
            {
                var DBPlaybooksDtlModel = _mapper.Map<List<PlayBookDtl>, List<GetPlayBookDtlsModel>>(DBplaybookDtls.Result);
                DBPlaybooksModel.PlaybookDtl = DBPlaybooksDtlModel;
                var org = _platformBL.GetOrganizationByID(DBplaybooks.Result.org_id);
                if (org.OrganizationData != null)
                {
                    DBPlaybooksModel.OrgName = org.OrganizationData.OrgName;
                }
                
            }
            response.IsSuccess = true;
            response.Message = "Success";
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Playbooks = new List<GetPlayBookModel>();

            response.Playbooks.Add(DBPlaybooksModel);

            return response;

        }

        public GetPlayBookResponse GetPlayBooks()
        {
            GetPlayBookResponse response = new GetPlayBookResponse();
            var DBplaybooks = _Playbookrepo.GetPlaybooks();
            
            if (DBplaybooks.Result.Count == 0 )
            {
                response.IsSuccess = false;
                response.Message = "Play books not found";
                return response;
            }
            var DBplaybookDtls = _PlaybookDtlsrepo.GetPlaybookDtls();
            var DBPlaybooksModel = _mapper.Map<List<PlayBook>, List<GetPlayBookModel>>(DBplaybooks.Result);

            if (DBplaybookDtls != null )
            {
                var DBPlaybooksDtlModel = _mapper.Map<List<PlayBookDtl>, List<GetPlayBookDtlsModel>>(DBplaybookDtls.Result);
                DBPlaybooksModel.ForEach(pb => pb.PlaybookDtl
                = DBPlaybooksDtlModel.Where(dtl => dtl.PlayBookID == pb.PlayBookID).ToList());

            }
            response.IsSuccess=true;
            response.Message = "Success";
            response.Playbooks = DBPlaybooksModel;


            return response;
        }

        public GetPlayBookResponse GetPlayBooks(int orgId)
        {
            GetPlayBookResponse response = new GetPlayBookResponse();
            var DBplaybooks = _Playbookrepo.GetPlaybooks(orgId);

            if (DBplaybooks.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Play books not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            var DBPlaybooksModel = _mapper.Map<List<PlayBook>, List<GetPlayBookModel>>(DBplaybooks.Result);
            var orgs = _platformBL.GetOrganizationList();
            if (orgs.OrganizationList.Count > 0)
            {
                foreach (var pb in DBPlaybooksModel)
                {
                    var org = orgs.OrganizationList.Where(o => o.OrgID == pb.OrgId).FirstOrDefault();
                    if (org != null )
                    {
                        pb.OrgName = org.OrgName;
                    }
                    
                }
            }

        var DBplaybookDtls = _PlaybookDtlsrepo.GetPlaybookDtls();
            
            
            if (DBplaybookDtls != null)
            {

                var DBPlaybooksDtlModel = _mapper.Map<List<PlayBookDtl>, List<GetPlayBookDtlsModel>>(DBplaybookDtls.Result);
                DBPlaybooksModel.ForEach(pb => pb.PlaybookDtl
                = DBPlaybooksDtlModel.Where(dtl => dtl.PlayBookID == pb.PlayBookID).ToList());


            }
            response.IsSuccess = true;
            response.Message = "Success";
            response.Playbooks = DBPlaybooksModel;


            return response;
        }
        public PlayBookResponse UpdatePlaybook(UpdatePlayBookRequest request)
        {
            PlayBookResponse response = new PlayBookResponse();
            var userdata = _securityBl.GetUserbyID(request.ModifiedUserId);
            if (userdata.Userdata == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid User";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var playbook = _Playbookrepo.GetPlayBookbyNameOnUpdate(request.PlayBookName,request.PlayBookID);
            if (playbook.Result != null)
            {
                response.IsSuccess = false;
                response.Message = "Validation Error";
                response.errors = new List<string>() { "Play book name already exists" };
                response.HttpStatusCode = HttpStatusCode.BadRequest;

                return response;
            }

            var _playbookentity = _mapper.Map<UpdatePlayBookRequest, PlayBook>(request);
            _playbookentity.Modified_User = userdata.Userdata.Name;
            var _playbookdtlsentity = _mapper.Map<List<UpdatePlayBookDtlsModel>, List<PlayBookDtl>>(request.PlaybookDtls);


            _playbookdtlsentity.ForEach(bookdtl =>
            {
                bookdtl.Modified_Date = _playbookentity.Modified_Date;
                bookdtl.Modified_User = _playbookentity.Modified_User;
            });

            var ret = _Playbookrepo.UpdatePlayBook(_playbookentity, _playbookdtlsentity);

            response.IsSuccess = true;
            response.Message = ret.Result;
            response.HttpStatusCode = (HttpStatusCode)200;
            return response;

        }
    }
}
