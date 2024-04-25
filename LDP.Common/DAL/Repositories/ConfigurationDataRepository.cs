using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class ConfigurationDataRepository : IConfigurationDataRepository
    {
        private readonly CommonDataContext? _context;
        public ConfigurationDataRepository(CommonDataContext? context)
        {
            _context = context;
        }

        public async Task<List<ConfigurationData>> GetConfigurationData(ConfigurationDataRequest request)
        {
            var res = await _context.vm_ldp_configurationdata
                .Where(cd => cd.active == 1 && cd.data_type == request.DataType)
                .AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<ConfigurationData> GetConfigurationData(string ConfigurationType, string ConfigurationName)
        {
            var res = await _context.vm_ldp_configurationdata
                .Where(cd => cd.active == 1 && cd.data_type == ConfigurationType
                && cd.data_name == ConfigurationName)
                .AsNoTracking().FirstOrDefaultAsync();
            return res;
        }

        public async Task<List<ConfigurationData>> GetConfigurationData(string ConfigurationType)
        {
            var res = await _context.vm_ldp_configurationdata
                 .Where(cd => cd.active == 1 && cd.data_type == ConfigurationType)
                 .AsNoTracking().ToListAsync();
            return res;
        }
    }
}
