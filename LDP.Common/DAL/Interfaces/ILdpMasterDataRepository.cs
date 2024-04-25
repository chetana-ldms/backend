using LDP.Common.DAL.Entities.Common;

namespace LDP.Common.DAL.Interfaces
{
    public interface ILdpMasterDataRepository
    {
        Task<List<MasterDataExtnFields>> GetMasterDataExtnFields(string strDataType);
        Task<List<OrgMasterData>> GetOrgMasterData(string strDataType);
    }
}
