using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IIncidentReportsBL
    {
        public AllCreatedIncidentsStatusReportResponse GetAllCreatedIncidentsStatusReport(AllCreatedIncidentsStatusReportRequest request);
        public ClosedIncidentsReportResponse GetClosedIncidentsReport(ClosedIncidentsReportRequest request);

        public OpenIncidentsReportResponse GetOpenIncidentsReport(OpenIncidentsReportRequest request);

        public SignificantIncidentsReportResponse GetSignificantIncidentsReport(SignificantIncidentsReportRequest request);

    }
}
