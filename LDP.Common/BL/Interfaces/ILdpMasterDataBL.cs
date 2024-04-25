using LDP.Common.DAL.Entities.Common;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface ILdpMasterDataBL
    {
        GetMasterDataExtnFieldsResponse GetMasterDataExtnFields(string strDataType);
        GetOrgMasterDataResponse GetOrgMasterData(string strDataType, int orgId);
    }
}
