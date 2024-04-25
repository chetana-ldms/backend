using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDP.Common.DAL.Repositories
{
    public class LdpMasterDataRepository : ILdpMasterDataRepository
    {
        readonly CommonDataContext _context;

        public LdpMasterDataRepository(CommonDataContext context)
        {
            _context = context;
        }

        

        public async Task<List<MasterDataExtnFields>> GetMasterDataExtnFields(string strDataType)
        {
            return await _context.vm_ldp_masterdata_extn_fields.Where(ext => ext.data_type == strDataType)
                .AsNoTracking().ToListAsync();
        }

        public async Task<List<OrgMasterData>> GetOrgMasterData(string strDataType)
        {
            return await _context.vm_ldp_org_masterdata.Where(ext => ext.data_type == strDataType)
               .AsNoTracking().ToListAsync();
        }
    }
}
