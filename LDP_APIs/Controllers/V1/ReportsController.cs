using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("Reports")]
    public class ReportsController : ControllerBase
    {
        IAlertReportsBL _alertReportBL;
        IIncidentReportsBL _incidentReportL;
        public ReportsController
            (
              IAlertReportsBL alertReportBL
            , IIncidentReportsBL incidentReportL
            ) 
        {
            _alertReportBL = alertReportBL;
            _incidentReportL = incidentReportL;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AlertsSummery")]
        public AlertSummeryReportResponse GetAlertSummeryReport(AlertSummeryReportRequest request)
        {
           
            return _alertReportBL.GetAlertSummery(request);

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AlertsRuleSummery")]
        public AlertRulesSummeryReportResponse GetAlertRulesSummery(AlertRulesSummeryReportRequest request)

        {

            return _alertReportBL.GetAlertRulesSummery(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SLAMeasurementSummery")]
        public AlertSLAMeasurementReportResponse GetAlertSLAMeasurementSummery(AlertSLAMeasurementReportRequest request)
        {

            return _alertReportBL.GetAlertSLAMeasurementSummery(request);

        }

        
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AllIncidentsSummery")]
        public AllCreatedIncidentsStatusReportResponse GetAllCreatedIncidentsStatusReport(AllCreatedIncidentsStatusReportRequest request)
        {
            return _incidentReportL.GetAllCreatedIncidentsStatusReport(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ClosedIncidentsSummery")]

        public ClosedIncidentsReportResponse GetClosedIncidentsReport(ClosedIncidentsReportRequest request)
        {
            return _incidentReportL.GetClosedIncidentsReport(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("OpenIncidentsSummery")]

        public OpenIncidentsReportResponse GetOpenIncidentsReport(OpenIncidentsReportRequest request)
        {
            return _incidentReportL.GetOpenIncidentsReport(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SignificantsIncidentsSummery")]
        public SignificantIncidentsReportResponse GetSignificantIncidentsReport(SignificantIncidentsReportRequest request)
        {
            return _incidentReportL.GetSignificantIncidentsReport(request);
        }


    }
}
