using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.DAL.Interfaces
{
    public interface IIncidentReportsRepository
    {
        public Task<List<Incident>> GetIncidents(IncidentReportsBase request);

        public AllCreatedIncidentsStatusReportResponse GetAllCreatedIncidentsStatusReport(AllCreatedIncidentsStatusReportRequest request);
        public ClosedIncidentsReportResponse GetClosedIncidentsReport(ClosedIncidentsReportRequest request);

        public OpenIncidentsReportResponse GetOpenIncidentsReport(OpenIncidentsReportRequest request);

        public Task<List<Incident>> GetSignificantIncidentsReport(SignificantIncidentsReportRequest request);

    }
}
