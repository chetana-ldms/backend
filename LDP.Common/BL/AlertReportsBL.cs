using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Interfaces;

namespace LDP.Common.BL
{
    public class AlertReportsBL : IAlertReportsBL
    {
        IAlertReportsRepository _repo;
        ILDPlattformBL _platformBL;
        ILdpMasterDataBL _masterDataBL;
        public AlertReportsBL(IAlertReportsRepository repo
            , ILDPlattformBL platformB
            , ILdpMasterDataBL masterDataBL
            )
        {
            _repo = repo;
            _platformBL = platformB;
            _masterDataBL = masterDataBL;
        }
        public AlertRulesSummeryReportResponse GetAlertRulesSummery(AlertRulesSummeryReportRequest request)
        {
            AlertRulesSummeryReportResponse res = new AlertRulesSummeryReportResponse();

             var alertRules = _repo.GetAlertRulesSummery(request);

            if (alertRules.Result.Count > 0) 
            {
                var ruleSummary = alertRules.Result
                            .GroupBy(rule => rule.data_field_value)
                            .Select(group => new {
                                GroupName = group.Key,
                                Count = group.Count()
                            });

                AlertRuleSummery summery = null;
                List<AlertRuleSummery> summerys = new List<AlertRuleSummery>();
                foreach (var groupSummary in ruleSummary)
                {
                    summery = new AlertRuleSummery()
                    {
                        AlertRule = groupSummary.GroupName,
                        AlertCount = groupSummary.Count
                    };
                    summerys.Add(summery);
                }
                res.Data = summerys;
                res.IsSuccess = true;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                res.IsSuccess = true;
                res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                res.Message = "No records found";
            }

            return res;
        }

        public AlertSLAMeasurementReportResponse GetAlertSLAMeasurementSummery(AlertSLAMeasurementReportRequest request)
        {
            AlertSLAMeasurementReportResponse res = new AlertSLAMeasurementReportResponse();
            var alerts = _repo.GetAlertSummery(request);
            double totalcount = 0;
            if (alerts.Result.Count == 0)
            {
                res.IsSuccess = false;
                res.Message = "No records found for requested criteria...";
                res.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return res;
            }
            totalcount = alerts.Result.Count;
            res.TotalAlertsCount = totalcount;
            res.Data = new List<AlertSLASummery>();
            //Get severity list 
            AlertSLASummery summery = null;
            var sevirityList = _platformBL.GetMasterDataByDatType(new LDPMasterDataRequest() { MaserDataType = Constants.AlertSevirityType });
            double specificDaysAlertCount = 0;
            var _seviritySlaData = _masterDataBL.GetMasterDataExtnFields(Constants.AlertSevirityType).Data;

            foreach (int days in Constants.GetAlertSLAMeasurementSummeryDays)
            {
               var  specificDaysAlerts = alerts.Result.Where(alert => alert.detected_time >= request.AlertToDate.AddDays(-days));
                double cumulativePercentSum = 0;
                double PreviousCumulativeRoundedValue = 0;

                foreach (var sevirity in sevirityList.MasterData)
                {

                    summery = new AlertSLASummery();
                    summery.SummeryPeriod = days.ToString();
                    summery.SummeryPeriodType = "Days";
                    summery.SummeryPeriodAlertCount = specificDaysAlerts.Count();
                    if (summery.SummeryPeriodAlertCount > 0 )
                    {
                        summery.SummeryPeriodPercentageValue = Math.Round((summery.SummeryPeriodAlertCount / totalcount) * 100);
                    }
                    else
                    {
                        summery.SummeryPeriodPercentageValue = 0;
                    }
                    summery.SevirityId = sevirity.DataID;
                    summery.SevirityName = sevirity.DataValue;
                    double count = specificDaysAlerts.Where(al => al.severity_id == sevirity.DataID).Count();
                    summery.SevirityWiseAlertCount = count;
                    if (count > 0)
                    {

                        // summery.SevirityWisePercentageValue = (summery.SevirityWiseAlertCount / summery.SummeryPeriodAlertCount) * 100;
                        double percentValue = (summery.SevirityWiseAlertCount / summery.SummeryPeriodAlertCount) * 100;
                        cumulativePercentSum = cumulativePercentSum + percentValue;
                        var currentRoundedPercentagValue = Math.Round(cumulativePercentSum, 2);
                        //currentPercentagValue = 
                        
                        summery.SevirityWisePercentageValue = Math.Round(currentRoundedPercentagValue - PreviousCumulativeRoundedValue, 2);
                        PreviousCumulativeRoundedValue = currentRoundedPercentagValue;

                        var _definedSLA = _seviritySlaData.Where(sla => sla.DataFieldName == Constants.AlertSevirityFieldSLAHours
                        && sla.DataId == sevirity.DataID).FirstOrDefault().DataFieldValue;
                       
                        int _intSLA = 0;
                        int.TryParse( _definedSLA, out _intSLA);
                        if (_intSLA > 0)
                        {
                 
                            double slaAlertscount = specificDaysAlerts.Where(al => al.severity_id == sevirity.DataID 
                            && al.resolved_time != null
                            && (al.resolved_time.Value - al.detected_time).Value.TotalHours >= _intSLA).Count();
                           

                            if (slaAlertscount > 0) 
                            {
                                summery.SLAMetPercentageValue = Math.Round((slaAlertscount / summery.SevirityWiseAlertCount) * 100);
                            }
                            else
                            {
                                summery.SLAMetPercentageValue =0;
                            }
                            
                        }

                    }
                    else
                    {
                        summery.SevirityWisePercentageValue = 0;
                    }

                    res.Data.Add(summery);
                }
            }
           
            res.Message = "Success";
            res.IsSuccess = true;
            res.HttpStatusCode = System.Net.HttpStatusCode.OK;

            return res;
        }

        public AlertSummeryReportResponse GetAlertSummery(AlertSummeryReportRequest request)
        {
            AlertSummeryReportResponse res = new AlertSummeryReportResponse();
            var alerts= _repo.GetAlertSummery(request);
            double totalcount = 0;
            if (alerts.Result.Count==0)
            {
                res.IsSuccess = false;
                res.Message = "No records found for requested criteria...";
                res.HttpStatusCode = System.Net.HttpStatusCode.NotFound; 
                return res;
            }
            totalcount = alerts.Result.Count;
            res.TotalAlertsCount = totalcount;
            res.Data = new List<AlertStatusSummery>();
            //Get Alert Status 
            AlertStatusSummery summery = null;
            var statusList = _platformBL.GetMasterDataByDatType(new LDPMasterDataRequest() { MaserDataType = Constants.AlertStatusType });
            double cumulativePercentSum = 0;
            double PreviousCumulativeRoundedValue = 0;
            foreach( var status in statusList.MasterData) 
            {
                summery = new AlertStatusSummery();
                summery.StatusId = status.DataID;
                summery.StatusName = status.DataValue;
                double count = alerts.Result.Where(al => al.status_ID == status.DataID).Count();
                if (count > 0) 
                {
                    // Logic Referece - https://stackoverflow.com/questions/13483430/how-to-make-rounded-percentages-add-up-to-100/13483486#13483486
                    //int percentagevaalue = count / totalcount * 100;
                    summery.AlertCount = count;
                    double percentValue = (count / totalcount) * 100;
                    cumulativePercentSum = cumulativePercentSum + percentValue;
                    var currentRoundedPercentagValue = Math.Round(cumulativePercentSum,2);
                    //currentPercentagValue = 
                    summery.PercentageValue = Math.Round(currentRoundedPercentagValue - PreviousCumulativeRoundedValue , 2 );
                    PreviousCumulativeRoundedValue = currentRoundedPercentagValue;
                }
                else
                {
                    summery.AlertCount = 0;
                    summery.PercentageValue = 0;
                }
                res.Data.Add(summery);
            }
            res.Message = "Success";
            res.IsSuccess = true;
            res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return res;
        }
    }
}
