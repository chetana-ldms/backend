using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Interfaces;

namespace LDP.Common.BL
{
    public class IncidentReportsBL : IIncidentReportsBL
    {
        IIncidentReportsRepository _repo;
        ILDPlattformBL _platformBL;

        public IncidentReportsBL(IIncidentReportsRepository repo, ILDPlattformBL platformBL)
        {
            _repo = repo;
            _platformBL = platformBL;
        }
        public ClosedIncidentsReportResponse GetClosedIncidentsReport(ClosedIncidentsReportRequest request)
        {
            ClosedIncidentsReportResponse res = new ClosedIncidentsReportResponse();


            var incidents = _repo.GetIncidents(request);
            double totalcount = 0;
            if (incidents.Result.Count == 0)
            {
                res.IsSuccess = false;
                res.Message = "No records found for requested criteria...";
                res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return res;
            }
            totalcount = incidents.Result.Count;
            res.TotalAlertsCount = totalcount;
            res.Data = new List<IncidenttStatusSummery>();
            //Get Incident Status 
            IncidenttStatusSummery summery = null;
            double openIncidentcount = 0;
            var statusList = _platformBL.GetMasterDataByDatType(new LDPMasterDataRequest() { MaserDataType = Constants.IncidentStatusType });
            double closedStatusPercentage = 0;
            foreach (var status in statusList.MasterData)
            {
               
                summery = new IncidenttStatusSummery();
                summery.StatusId = status.DataID;
                summery.StatusName = status.DataValue;
                double count = incidents.Result.Where(inc => inc.Incident_Status == status.DataID).Count();
                if (!Constants.IncidentClosedSstatusList.Contains(status.DataValue))
                {
                    openIncidentcount = openIncidentcount + count;
                    continue;
                }
                if (count > 0)
                {
                    summery.AlertCount = count;
                    double percentageValue = Math.Round((count / totalcount) * 100, 2);
                    summery.PercentageValue = percentageValue;
                    closedStatusPercentage = closedStatusPercentage + percentageValue;
                }
                else
                {
                    summery.AlertCount = 0;
                    summery.PercentageValue = 0;
                }

                
                res.Data.Add(summery);
            }

            summery = new IncidenttStatusSummery();
            summery.StatusId = 0;
            summery.StatusName = "Open";
            summery.AlertCount = openIncidentcount;
            summery.PercentageValue = Math.Round(100 - closedStatusPercentage,2);
            res.Data.Add(summery);

            res.Message = "Success";
            res.IsSuccess = true;
            res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return res;

        }

        public AllCreatedIncidentsStatusReportResponse GetAllCreatedIncidentsStatusReport(AllCreatedIncidentsStatusReportRequest request)
        {
            AllCreatedIncidentsStatusReportResponse res = new AllCreatedIncidentsStatusReportResponse ();

           
            var incidents = _repo.GetIncidents(request);
            double totalcount = 0;
            if (incidents.Result.Count == 0)
            {
                res.IsSuccess = false;
                res.Message = "No records found for requested criteria...";
                res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return res as AllCreatedIncidentsStatusReportResponse;
            }
            totalcount = incidents.Result.Count;
            res.TotalAlertsCount = totalcount;
            res.Data = new List<IncidenttStatusSummery>();
            //Get Incident Status 
            IncidenttStatusSummery summery = null;
            var statusList = _platformBL.GetMasterDataByDatType(new LDPMasterDataRequest() { MaserDataType = Constants.IncidentStatusType });
            double cumulativePercentSum = 0;
            double PreviousCumulativeRoundedValue = 0;
            foreach (var status in statusList.MasterData)
            {
                summery = new IncidenttStatusSummery();
                summery.StatusId = status.DataID;
                summery.StatusName = status.DataValue;
                double count = incidents.Result.Where(inc => inc.Incident_Status== status.DataID).Count();
                if (count > 0)
                {
                    //summery.AlertCount = count;
                    //summery.PercentageValue = (count / totalcount) * 100;
                    summery.AlertCount = count;
                    double percentValue = (count / totalcount) * 100;
                    cumulativePercentSum = cumulativePercentSum + percentValue;
                    var currentRoundedPercentagValue = Math.Round(cumulativePercentSum, 2);
                    //currentPercentagValue = 
                    summery.PercentageValue = Math.Round(currentRoundedPercentagValue - PreviousCumulativeRoundedValue, 2);
                    PreviousCumulativeRoundedValue = currentRoundedPercentagValue;
                }
                else
                {
                    summery.AlertCount = 0;
                    summery.PercentageValue = 0;
                }

                res.Message = "Success";
                res.IsSuccess = true;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                res.Data.Add(summery);
            }
            return res;
          
        }

        public SignificantIncidentsReportResponse GetSignificantIncidentsReport(SignificantIncidentsReportRequest request)
        {
            SignificantIncidentsReportResponse res = new SignificantIncidentsReportResponse();


            var incidents = _repo.GetSignificantIncidentsReport(request);
            double totalcount = 0;
            if (incidents.Result.Count == 0)
            {
                res.IsSuccess = false;
                res.Message = "No records found for requested criteria...";
                res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return res as SignificantIncidentsReportResponse;
            }
            totalcount = incidents.Result.Count;
            res.TotalAlertsCount = totalcount;
            res.Data = new List<IncidenttStatusSummery>();
            //Get Incident Status 
            IncidenttStatusSummery summery = null;
            var statusList = _platformBL.GetMasterDataByDatType(new LDPMasterDataRequest() { MaserDataType = Constants.IncidentStatusType });
            double cumulativePercentSum = 0;
            double PreviousCumulativeRoundedValue = 0;
            foreach (var status in statusList.MasterData)
            {
                summery = new IncidenttStatusSummery();
                summery.StatusId = status.DataID;
                summery.StatusName = status.DataValue;
                double count = incidents.Result.Where(inc => inc.Incident_Status == status.DataID).Count();
                if (count > 0)
                {
                    //summery.AlertCount = count;
                    //summery.PercentageValue = (count / totalcount) * 100;
                    summery.AlertCount = count;
                    double percentValue = (count / totalcount) * 100;
                    cumulativePercentSum = cumulativePercentSum + percentValue;
                    var currentRoundedPercentagValue = Math.Round(cumulativePercentSum, 2);
                    //currentPercentagValue = 
                    summery.PercentageValue = Math.Round(currentRoundedPercentagValue - PreviousCumulativeRoundedValue, 2);
                    PreviousCumulativeRoundedValue = currentRoundedPercentagValue;
                }
                else
                {
                    summery.AlertCount = 0;
                    summery.PercentageValue = 0;
                }

                res.Message = "Success";
                res.IsSuccess = true;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                res.Data.Add(summery);
            }
            return res;
        }

        public OpenIncidentsReportResponse GetOpenIncidentsReport(OpenIncidentsReportRequest request)
        {
            OpenIncidentsReportResponse res = new OpenIncidentsReportResponse();


            var incidents = _repo.GetIncidents(request);
            double totalcount = 0;
            if (incidents.Result.Count == 0)
            {
                res.IsSuccess = false;
                res.Message = "No records found for requested criteria...";
                res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return res;
            }
            totalcount = incidents.Result.Count;
            
            res.Data = new List<IncidenttStatusSummery>();
            //Get Incident Status 
            IncidenttStatusSummery summery = null;
            var statusList = _platformBL.GetMasterDataByDatType(new LDPMasterDataRequest() { MaserDataType = Constants.IncidentStatusType });
            double cumulativePercentSum = 0;
            double PreviousCumulativeRoundedValue = 0;
            // Remove the closed count from total 
            foreach (var status in statusList.MasterData)
            {
                if (Constants.IncidentClosedSstatusList.Contains(status.DataValue))
                {
                    double ClosedIncidentcount = incidents.Result.Where(inc => inc.Incident_Status == status.DataID).Count();
                    if (ClosedIncidentcount > 0)
                    {
                        totalcount = totalcount - ClosedIncidentcount;
                    }
                }

            }
            res.TotalAlertsCount = totalcount;
           
            foreach (var status in statusList.MasterData)
            {
                if (Constants.IncidentClosedSstatusList.Contains(status.DataValue)) continue;
                summery = new IncidenttStatusSummery();
                summery.StatusId = status.DataID;
                summery.StatusName = status.DataValue;
                double count = incidents.Result.Where(inc => inc.Incident_Status == status.DataID).Count();
                if (count > 0)
                {
                    //summery.AlertCount = count;
                    //summery.PercentageValue = (count / totalcount) * 100;
                    summery.AlertCount = count;
                    double percentValue = (count / totalcount) * 100;
                    cumulativePercentSum = cumulativePercentSum + percentValue;
                    var currentRoundedPercentagValue = Math.Round(cumulativePercentSum, 2);
                    //currentPercentagValue = 
                    summery.PercentageValue = Math.Round(currentRoundedPercentagValue - PreviousCumulativeRoundedValue, 2);
                    PreviousCumulativeRoundedValue = currentRoundedPercentagValue;
                }
                else
                {
                    summery.AlertCount = 0;
                    summery.PercentageValue = 0;
                }

                res.Message = "Success";
                res.IsSuccess = true;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                res.Data.Add(summery);
            }
            return res;
        }
    }
}
