using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Responses;

namespace LDP.Common.BL
{
    public class LDPMasterDataBL : ILdpMasterDataBL
    {
        ILdpMasterDataRepository _masterdatRepo;
        readonly IMapper _mapper;

        public LDPMasterDataBL(ILdpMasterDataRepository masterdatRepo, IMapper mapper)
        {
            _masterdatRepo = masterdatRepo;
            _mapper = mapper;
        }

        public GetMasterDataExtnFieldsResponse GetMasterDataExtnFields(string strDataType)
        {
            GetMasterDataExtnFieldsResponse response = new GetMasterDataExtnFieldsResponse();
            var res = _masterdatRepo.GetMasterDataExtnFields(strDataType);

            if (res.Result.Count == 0 ) 
            {
                response.IsSuccess =false;
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "Data not found";
                return response;
            }
            response.IsSuccess = true;
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            response.Message = "Success";
            var _mapData = _mapper.Map< List< MasterDataExtnFields >, List<MasterDataExtnFieldsModel>> (res.Result);
            response.Data = _mapData;
            return response;

        }

        public GetOrgMasterDataResponse GetOrgMasterData(string strDataType, int orgId)
        {
            GetOrgMasterDataResponse response = new GetOrgMasterDataResponse();
            var res = _masterdatRepo.GetOrgMasterData(strDataType);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "Data not found";
                return response;
            }
            response.IsSuccess = true;
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            response.Message = "Success";
            var _mapData = _mapper.Map<List<OrgMasterData>, List<OrgMasterDataModel>>(res.Result);
            response.Data = _mapData;
            return response;

        }
    }
}
