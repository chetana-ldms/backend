using AutoMapper;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;
using System.Net;

namespace LDP.Common.Services.SentinalOneIntegration.Sentinel
{
    public class SentinelBL : ISentinalBL
    {

        ISentinelService _service;
        public readonly IMapper _mapper;
        ILDPlattformBL _plattformBL;
       
        public SentinelBL( ILDPlattformBL plattformBL,ISentinelService service,IMapper mapper  )
        {
            _plattformBL = plattformBL;
            _service = service;
            _mapper = mapper;
        }

        public ExclustionsResponse GetExclusions(ExclusionRequest request)
        {
            ExclustionsResponse methodResponse = new ExclustionsResponse();

            var conndtl = GetConnectionDetails(request.OrgId, methodResponse, Constants.Tool_Action_Get_Exclusions);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var _serviceResponse = _service.GetExclusions(request, conndtl).Result;


            if (!_serviceResponse.IsSuccess)
            {
                BuildBaseResponseObject(methodResponse, _serviceResponse.IsSuccess, _serviceResponse.Message, _serviceResponse.HttpStatusCode);

                methodResponse.errors = _serviceResponse.errors;
                return methodResponse;
            }
            methodResponse.ExclusionList = new List<ExclusionData>();
            methodResponse.ExclusionList.AddRange(_serviceResponse.Data);

            while (_serviceResponse != null && _serviceResponse.Pagination != null && _serviceResponse.Pagination.nextCursor != null)
            {
                _serviceResponse = _service.GetExclusions(request, conndtl, _serviceResponse.Pagination.nextCursor).Result;
                if (_serviceResponse.Data != null && _serviceResponse.Data.Count > 0)
                {
                    methodResponse.ExclusionList.AddRange(_serviceResponse.Data);
                }
            }
            BuildBaseResponseObject(methodResponse, true, "List of exclusions", HttpStatusCode.OK);

            return methodResponse;

        }

        public BlockListResponse GetBlockList(BlockListRequest request)
        {
            BlockListResponse methodResponse = new BlockListResponse();

            var conndtl = GetConnectionDetails(request.OrgId, methodResponse, Constants.Tool_Action_Get_Blocked_List);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var _serviceResponse = _service.GetBlockList(request, conndtl).Result;


            if (!_serviceResponse.IsSuccess)
            {
                BuildBaseResponseObject(methodResponse, _serviceResponse.IsSuccess, _serviceResponse.Message, _serviceResponse.HttpStatusCode);

                methodResponse.errors = _serviceResponse.errors;
                return methodResponse;
            }
            methodResponse.BlockedItemList = new List<BlockListData>();
            methodResponse.BlockedItemList.AddRange(_serviceResponse.Data);

            while (_serviceResponse != null && _serviceResponse.Pagination != null && _serviceResponse.Pagination.nextCursor != null)
            {
                _serviceResponse = _service.GetBlockList(request, conndtl, _serviceResponse.Pagination.nextCursor).Result;
                if (_serviceResponse.Data != null && _serviceResponse.Data.Count > 0)
                {
                    methodResponse.BlockedItemList.AddRange(_serviceResponse.Data);
                }
            }
            BuildBaseResponseObject(methodResponse, true, "List of blocked item list", HttpStatusCode.OK);

            return methodResponse ;

        }

        public AccountResponse GetAccounts(GetAccountsRequest request)
        {
            AccountResponse methodResponse = new AccountResponse();

            var conndtl = GetConnectionDetails(request.OrgId, methodResponse, Constants.Tool_Action_Get_Account_Details);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var _serviceResponse = _service.GetAccounts(request, conndtl).Result;


            if (!_serviceResponse.IsSuccess)
            {
                BuildBaseResponseObject(methodResponse, _serviceResponse.IsSuccess, _serviceResponse.Message, _serviceResponse.HttpStatusCode);

                methodResponse.errors = _serviceResponse.errors;
                return methodResponse;
            }
            methodResponse.Accounts = new List<AccountData>();
            methodResponse.Accounts.AddRange(_serviceResponse.data);

            while (_serviceResponse != null && _serviceResponse.pagination != null && _serviceResponse.pagination.nextCursor != null)
            {
                _serviceResponse = _service.GetAccounts(request, conndtl, _serviceResponse.pagination.nextCursor).Result;
                if (_serviceResponse.data != null && _serviceResponse.data.Count > 0)
                {
                    methodResponse.Accounts.AddRange(_serviceResponse.data);
                }
            }
            BuildBaseResponseObject(methodResponse, true, "Account details", HttpStatusCode.OK);

            return methodResponse;

        }
        public AccountPolicy GetAccountPolicy(GetAccountPolicyRequest request)
        {
            AccountPolicy methodResponse = new AccountPolicy();

            string actionName = string.Empty;
            if (request.TenantPolicyScope) actionName = Constants.Tool_Action_Get_Tenant_Policy_Details;
            if (request.AccountPolicyScope) actionName = Constants.Tool_Action_Get_Account_Policy_Details;
            if (request.SitePolicyScope) actionName = Constants.Tool_Action_Get_Site_Policy_Details;
            if (request.GroupPolicyScope) actionName = Constants.Tool_Action_Get_Group_Policy_Details;
            
            var conndtl = GetConnectionDetails(request.OrgId, methodResponse, actionName);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            return  _service.GetAccountPolicy(request, conndtl).Result;

        }

        public SentinelOneSiteDataResponse GetSites(GetSitesRequest request)
        {
            SentinelOneSiteDataResponse methodResponse = new SentinelOneSiteDataResponse();

            var conndtl = GetConnectionDetails(request.OrgId, methodResponse, Constants.Tool_Action_Get_Sites);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var _serviceResponse = _service.GetSites(request,conndtl).Result;


            if (!_serviceResponse.IsSuccess)
            {
                BuildBaseResponseObject(methodResponse, _serviceResponse.IsSuccess, _serviceResponse.Message, _serviceResponse.HttpStatusCode);

                methodResponse.errors = _serviceResponse.errors;
                return methodResponse;
            }
            methodResponse.data = new SiteData();
            methodResponse.data.allSites = _serviceResponse.data.allSites;
            methodResponse.data.sites = new List<Site>();
            methodResponse.data.sites.AddRange(_serviceResponse.data.sites);

            while (_serviceResponse != null && _serviceResponse.pagination != null && _serviceResponse.pagination.nextCursor != null)
            {
                _serviceResponse = _service.GetSites(request,conndtl, _serviceResponse.pagination.nextCursor).Result;
                if (_serviceResponse.data != null && _serviceResponse.data.sites.Count > 0)
                {
                    methodResponse.data.sites.AddRange(_serviceResponse.data.sites);
                }
            }
            BuildBaseResponseObject(methodResponse, true, "Site list", HttpStatusCode.OK);

            return methodResponse;
        }
        public SentinelOneGroupResponse GetGroups(GetGroupsRequest request)
        {
            SentinelOneGroupResponse methodResponse = new SentinelOneGroupResponse();

            var conndtl = GetConnectionDetails(request.OrgId, methodResponse, Constants.Tool_Action_Get_Groups);
            if (!methodResponse.IsSuccess)
            {
                return methodResponse;
            }
            //
            var _serviceResponse = _service.GetGroups(request, conndtl).Result;


            if (!_serviceResponse.IsSuccess)
            {
                BuildBaseResponseObject(methodResponse, _serviceResponse.IsSuccess, _serviceResponse.Message, _serviceResponse.HttpStatusCode);

                methodResponse.errors = _serviceResponse.errors;
                return methodResponse;
            }
            methodResponse.data = new List<GroupData>();
           
            methodResponse.data.AddRange(_serviceResponse.data);

            while (_serviceResponse != null && _serviceResponse.pagination != null && _serviceResponse.pagination.nextCursor != null)
            {
                _serviceResponse = _service.GetGroups(request, conndtl, _serviceResponse.pagination.nextCursor).Result;
                if (_serviceResponse.data != null && _serviceResponse.data.Count > 0)
                {
                    methodResponse.data.AddRange(_serviceResponse.data);
                }
            }
            BuildBaseResponseObject(methodResponse, true, "Group list", HttpStatusCode.OK);

            return methodResponse;
        }
        private OrganizationToolModel GetConnectionDetails(int OrgId, baseResponse methodResponse, string actionName)
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
                BuildBaseResponseObject(methodResponse, false, "Tool connection details not found , please check the request or configuration", HttpStatusCode.NotFound);
                return conndtl;
            }
            methodResponse.IsSuccess = true;
            return conndtl;
        }

        public AccountStructureResponse GetAccountStructure(GetAccountStructureRequest request)
        { 
            AccountStructureResponse response = new AccountStructureResponse();

            var _accountData = this.GetAccounts(new GetAccountsRequest() { OrgId = request.OrgId });

            if (!_accountData.IsSuccess)
            {
                response.IsSuccess = _accountData.IsSuccess;
                response.HttpStatusCode = _accountData.HttpStatusCode;
                response.Message = _accountData.Message;
                response.errors = _accountData.errors;
                return response;

            }
            response.Accounts = new List<AccountStructure>();
            SiteStructure _site = null;
            AccountStructure _account = null;
            GroupStructure _group = null;
            
            foreach (var account in _accountData.Accounts)
            {
                _account = new AccountStructure();
                _account.AccountId = account.id;
                _account.Name = account.name;
                _account.TotalSites = account.numberOfSites;
                _account.TotalEndpoints = account.activeAgents;

                var _siteData = this.GetSites(new GetSitesRequest()
                { OrgId = request.OrgId, AccountId = account.id });

                if (_siteData.IsSuccess)
                {
                    _account.Sites = new List<SiteStructure>();
                  
                    foreach (var site in _siteData.data.sites)
                    {
                        _site = new SiteStructure();
                        _site.SiteId = site.id;
                        _site.Name = site.name;
                        _site.IsDefault = site.isDefault;
                        _site.ActiveLicenses = site.activeLicenses;
                        var _groupData = GetGroups(new GetGroupsRequest()
                        {
                            OrgId = request.OrgId,
                            SiteId = site.id
                        });
                        if (_groupData.IsSuccess)
                        {
                            _site.Groups = new List<GroupStructure>();
                            _site.TotalGroups = _groupData.data.Count;
                            foreach(var grp in _groupData.data)
                            {
                                _group = new GroupStructure();
                                _group.GroupId = grp.id;
                                _group.Name = grp.name;
                                _group.TotalAgents = grp.totalAgents;
                                _site.Groups.Add(_group);
                            }
                        }
                        _account.Sites.Add(_site);
                    }
                }
                response.Accounts.Add(_account);    
            }

            return response;
        }

        private void BuildBaseResponseObject(baseResponse methodResponse, bool successFlag, string message, HttpStatusCode? statuscode)
        {
            methodResponse.IsSuccess = successFlag;
            methodResponse.Message = message;
            methodResponse.HttpStatusCode = statuscode;
        }
    }
}
