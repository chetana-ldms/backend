using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class IncidentReportsRepository : IIncidentReportsRepository
    {

        private readonly AlertsDataContext? _context;
        public IncidentReportsRepository(AlertsDataContext context)
        {
            _context = context;
        }

        public ClosedIncidentsReportResponse GetClosedIncidentsReport(ClosedIncidentsReportRequest request)
        {
            throw new NotImplementedException();
        }

        public AllCreatedIncidentsStatusReportResponse GetAllCreatedIncidentsStatusReport(AllCreatedIncidentsStatusReportRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Incident>> GetSignificantIncidentsReport(SignificantIncidentsReportRequest request)
        {

           var  res = await _context.vm_Incidents.Where(
            inc =>
           (_context.vm_alert_incident_mapping.Where(map => map.org_id == request.OrgId && map.significant_incident == 1))
           .Select(m => m.incident_number).Contains(inc.Incident_ID )
            && inc.Created_Date >= request.IncidentFromDate
            && inc.Created_Date <= request.IncidentToDate
            ).AsNoTracking().ToListAsync();

            return res;
        }

        public OpenIncidentsReportResponse GetOpenIncidentsReport(OpenIncidentsReportRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Incident>> GetIncidents(IncidentReportsBase request)
        {
            List<Incident> res = null;

            // res = await _context.vm_Incidents.Where(
            // inc =>
            //(_context.vm_alert_incident_mapping.Where(map => map.org_id == request.OrgId))
            //.Select(m => m.incident_number).Contains(inc.Incident_ID)
            // && inc.Created_Date >= request.IncidentFromDate.ToUniversalTime()
            // && inc.Created_Date <= request.IncidentToDate.ToUniversalTime()
            // ).AsNoTracking().ToListAsync();

            res = await _context.vm_Incidents.Where(
          inc => inc.org_id == request.OrgId && 
         inc.Created_Date >= request.IncidentFromDate
          && inc.Created_Date <= request.IncidentToDate).AsNoTracking().ToListAsync();

            return res;
        }
    }
}
