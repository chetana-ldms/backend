using LDP.Common.DAL.Entities.Common;
using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class GetMasterDataExtnFieldsResponse:baseResponse
    {
        public List<MasterDataExtnFieldsModel> Data { get; set; }

    }

    public class GetOrgMasterDataResponse : baseResponse
    {
        public List<OrgMasterDataModel> Data { get; set; }

    }
}
