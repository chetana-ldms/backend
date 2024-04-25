using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.Respository
{
    public class LDPlattformRepository : ILDPlattformRepository
    {
        private readonly LDPlattformDataContext? _context;
        public LDPlattformRepository(LDPlattformDataContext context)
        {
            _context = context;
        }

        #region LDPTool

        public async Task<List<LDPTool>> GetConfiguredLDPTools()
        {
          var res = await _context.vm_ldp_tools.AsNoTracking().ToListAsync();
          return res;
        }

        public async Task<string> NewLDPTool(LDPTool request)
        {
            _context.vm_ldp_tools.Add(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";

        }
        public async Task<string> UpdateLDPTool(LDPTool request)
        {
            _context.vm_ldp_tools.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }

    
        #endregion

        #region Organization


        public async Task<List<Organization>> GetOrganizationList()
        {
            return await _context.vm_organizations.AsNoTracking().ToListAsync();
        }

        public async Task<string> AddOrganization(Organization request)
        {
            
            _context.vm_organizations.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }

        public async Task<string> UpdateOrganization(Organization request)
        {
            _context.vm_organizations.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }
        #endregion

        #region organizationTools

        public async Task<List<OganizationTool>> GetOrganizationToolList()
        {
            return await _context.vm_organization_tools.AsNoTracking().ToListAsync();
        }

        public async Task<string> AddOrganizationTool(OganizationTool request)
        {
            _context.vm_organization_tools.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }
        
        public async Task<string> UpdateOrganizationTool(OganizationTool request)
        {
            _context.vm_organization_tools.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";

        }

        public async Task<OganizationTool> GetToolConnectionDetails(int OrgID , int ToolID )
        {
            
                var res = await _context.vm_organization_tools.Where(tool => tool.Org_ID == OrgID
                                                                  && tool.Tool_ID == ToolID ).FirstOrDefaultAsync();
                return res;
        }

        public async Task<string> UpdateLastReadPKID(GetOffenseDTO request)
        {
           
            var alertObj =  await _context.vm_organization_tools
                .Where( tool => tool.Org_ID == request.clientRequest.OrgID  && tool.Tool_ID == request.ToolID)
                .FirstOrDefaultAsync();
            if (alertObj != null)
            {
                alertObj.Last_Read_PKID = request.alert_MaxPKID;
                _context.vm_organization_tools.Update(alertObj);
                await _context.SaveChangesAsync();
            }
            return "";

        }

        public async Task<GetOffenseDTO> GetLastReadPKID(GetOffenseDTO request)
        {

            var alertObj = await _context.vm_organization_tools
                .Where(tool => tool.Org_ID == request.clientRequest.OrgID && tool.Tool_ID == request.ToolID)
                .FirstOrDefaultAsync();
            if (alertObj != null)
            {
                request.alert_MaxPKID = alertObj.Last_Read_PKID;
               // _context.vm_organization_tools.Update(alertObj);
               // await _context.SaveChangesAsync();
            }
            return request;

        }

        #endregion

        #region MasterData
        public async Task<List<LDPMasterData>> GetMasterData(string MasterDataType)
        {
            try
            {
                return await _context.vm_ldp_masterdata.Where(mdata => mdata.data_type == MasterDataType)
                     .AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            { return new List<LDPMasterData>(); }
            
        }
        #endregion
    }
}
