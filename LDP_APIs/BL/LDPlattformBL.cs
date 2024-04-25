using AutoMapper;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Models;

namespace LDP_APIs.BL
{
    public class LDPlattformBL : ILDPlattformBL
    {
        ILDPlattformRepository _repo;
        public readonly IMapper _mapper;

        public LDPlattformBL(ILDPlattformRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        #region LDP


        public GetConfiguredLDPToolsResponse GetConfiguredLDPTools()
        {
            GetConfiguredLDPToolsResponse response = new GetConfiguredLDPToolsResponse();
            var res = _repo.GetConfiguredLDPTools();
            response.IsSuccess = true;
            if (res == null)
                response.Message = "LDP Tools not found";
            else
            {
                {
                    var _mappedResponse = _mapper.Map<List<LDP_APIs.DAL.Entities.LDPTool>, List<GetLDPTool>>(res.Result);
                    response.LDPToolsList = _mappedResponse.ToList();
                    response.Message = "Success";
                }

            }
            return response;
        }

        public LDPToolResponse NewLDPTool(LDPToolRequest request)
        {
            LDPToolResponse response = new LDPToolResponse();
            var _mappedRequest = _mapper.Map<LDPToolRequest, LDP_APIs.DAL.Entities.LDPTool>(request);

            var res = _repo.NewLDPTool(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
                response.Message = "LDP Tool added";
            else
            {
                response.Message = "Failed to add new LDP tool";
            }
            return response;
        }

        public LDPToolResponse UpdateLDPTool(UpdateLDPToolRequest request)
        {
            LDPToolResponse response = new LDPToolResponse();
            var _mappedRequest = _mapper.Map<UpdateLDPToolRequest, LDP_APIs.DAL.Entities.LDPTool>(request);

            var res = _repo.UpdateLDPTool(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
                response.Message = "SIEM Tool updated";
            else
            {
                response.Message = "Failed to update siem tool";

            }
            return response;
        }

        #endregion

        #region Organizations
        public GetOrganizationsResponse GetOrganizationList()
        {
            GetOrganizationsResponse response = new GetOrganizationsResponse();
            var res = _repo.GetOrganizationList();
            response.IsSuccess = true;
            if (res == null)
                response.Message = "Organization data not found";
            else
            {
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<List<Organization>, List<GettOrganizationsModel>>(res.Result);
                response.OrganizationList = _mappedResponse.ToList();
            }
            return response;
        }

        public OrganizationResponse AddOrganization(AddOrganizationRequest request)
        {
            OrganizationResponse response = new OrganizationResponse();
            var _mappedRequest = _mapper.Map<AddOrganizationRequest, Organization>(request);

            var res = _repo.AddOrganization(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
                response.Message = "New Organization data added";
            else
            {
                response.Message = "Failed to add new Organization data";
            }
            return response;
        }

        public OrganizationResponse UpdateOrganization(UpdateOrganizationRequest request)
        {
            OrganizationResponse response = new OrganizationResponse();
            var _mappedRequest = _mapper.Map<UpdateOrganizationModel, Organization>(request);

            var res = _repo.UpdateOrganization(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
                response.Message = "New Organization data added";
            else
            {
                response.Message = "Failed to add new Organization data";
            }
            return response;
        }
        #endregion

        #region OrganizationTool

        public GetOrganizationToolsResponse GetOrganizationToolsList()
        {
            GetOrganizationToolsResponse response = new GetOrganizationToolsResponse();
            var res = _repo.GetOrganizationToolList();
            response.IsSuccess = true;
            if (res == null)
                response.Message = "Organization Tools data not found";
            else
            {
                response.Message = "Success";

                var _mappedResponse = _mapper.Map<List<OganizationTool>, List<OrganizationToolModel>>(res.Result);
                response.OrganizationToolList = _mappedResponse;
            }
            return response;
        }

        public OrganizationToolsResponse AddOrganizationTools(AddOrganizationToolsRequest request)
        {
            OrganizationToolsResponse response = new OrganizationToolsResponse();
            var _mappedRequest = _mapper.Map<AddOrganizationToolsRequest, OganizationTool>(request);

            var res = _repo.AddOrganizationTool(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
                response.Message = "New Organization Tool data added";
            else
            {
                response.Message = "Failed to add new Organization Tool data";
            }
            return response;
        }

        public OrganizationToolsResponse UpdateOrganizationTools(UpdateOrganizationToolsRequest request)
        {
            OrganizationToolsResponse response = new OrganizationToolsResponse();
            var _mappedRequest = _mapper.Map<UpdateOrganizationToolsRequest, OganizationTool>(request);

            var res = _repo.UpdateOrganizationTool(_mappedRequest);
            response.IsSuccess = true;
            if (res.Result == "")
                response.Message = "Organization Tool data upated";
            else
            {
                response.Message = "Failed to update the Organization Tool data";
            }
            return response;
        }

        public OrganizationToolModel GetToolConnectionDetails(int OrgID , int toolID)
        {
            OrganizationToolModel _mappedResponse = new OrganizationToolModel();
            var repoResponse = _repo.GetToolConnectionDetails(OrgID, toolID);
            // var _mappedResponse = _mapper.Map<OganizationTool, OrganizationToolModel>(repoResponse.Result);
            _mappedResponse.ApiUrl = repoResponse.Result.Api_Url;
            _mappedResponse.AuthKey = repoResponse.Result.Auth_Key;
            return _mappedResponse;
        }

        public Task<string> UpdateLastReadPKID(GetOffenseDTO request)
        {
            return _repo.UpdateLastReadPKID(request);
        }

        public Task<GetOffenseDTO> GetLastReadPKID(GetOffenseDTO request)
        {
            return _repo.GetLastReadPKID(request);
        }


        #endregion
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


    }
}
