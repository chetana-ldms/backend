using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;

namespace LDP.Common.DAL.Repositories
{
    public class ApplicationlogsRepository : IApplicationlogsRepository
    {
        private readonly CommonDataContext? _context;

        public ApplicationlogsRepository(CommonDataContext? context)
        {
            _context = context;
        }

        public async Task<string> AddApplicatinLog(ApplicationLog request)
        {

            _context.vm_application_logs.Add(request);

            var status = await _context.SaveChangesAsync();


            if (status > 0)
                return "";
            else
                return "Failed to add the application log";
        }

        
       
    }
}

