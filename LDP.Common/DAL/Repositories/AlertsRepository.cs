
using LDP.Common;
using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Services.SentinalOneIntegration.Sentinel;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Entities;
using LDP_APIs.DAL.Interfaces;
using LDP_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.Respository
{
    public class AlertsRepository: IAlertsRepository
    {
        private readonly AlertsDataContext? _context;
        public AlertsRepository(AlertsDataContext context)
        {
            _context = context;
        }

       public async Task<string> Addalerts(List<Alerts> request)
       {
            
                request.ForEach(x => x.Automation_Status = "pending");
                _context.vm_alerts.AddRange(request);
                await _context.SaveChangesAsync();
            
            return "success";
        }

        public async Task<string> UpdateAlert(Alerts request)
        {

          
            _context.vm_alerts.Update(request);
            var res = await _context.SaveChangesAsync();
            if (res == 1)
                return "success";
            else
                return null;
        }

        public List<Alerts> Getalerts(GetAlertsRequest request ,  bool isAdmin)
        {
            

            IQueryable<Alerts> dynamicquery = _context.vm_alerts.Where(i => i.org_id == request.OrgID);

            if (!isAdmin)
            {
                dynamicquery = dynamicquery.Where(i => i.owner_user_id == request.LoggedInUserId);
            }
            dynamicquery = dynamicquery.OrderByDescending(incnt => incnt.alert_id);
            dynamicquery = dynamicquery.Skip(request.paging.RangeStart - 1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1);
            
            var query = dynamicquery.AsNoTracking().ToList();
            return query;

        }
        public async Task<Double> GetAlertsDataCount(GetAlertsRequest request, bool isAdmin)
        {

            IQueryable<Alerts> dynamicquery = _context.vm_alerts.Where(i => i.org_id == request.OrgID);

            if (!isAdmin)
            {
                dynamicquery = dynamicquery.Where(i => i.owner_user_id == request.LoggedInUserId);
            }
            var count = dynamicquery.AsNoTracking().CountAsync();
            return count.Result;

        }
        public double GetAlertsDataCount()
        {
           return _context.vm_alerts.Count();
        }

        public async Task<string> AssignOwner(AssignOwnerRequest request, string modifiedUserName)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.AlertID )
                .AsNoTracking().FirstOrDefaultAsync();
            if (alertdata == null)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.owner_user_id = request.UserID;
                alertdata.owner_user_name = request.UserName;
                alertdata.Modified_Date = request.ModifiedDate;
                alertdata.Modified_User = modifiedUserName;
            }
            _context.Entry(alertdata).State = EntityState.Detached;
            _context.vm_alerts.Update(alertdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the user to alert ";
        }

        public async Task<List<Alerts>> GetAlertData(GetAlertRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.alertID)
                .AsNoTracking().ToListAsync();

            return res;
        }

        public async Task<List<Alerts>> GetAlertsByDevicePKIds(List<double> request,int orgId)
        {
            var res = await _context.vm_alerts.Where(alrt => request.Contains(alrt.alert_Device_PKID)
                        && alrt.org_id == orgId)
                .AsNoTracking().ToListAsync();

            return res;
        }

        public async Task<List<Alerts>> GetAlertsByAssignedUser(GetAlertByAssignedOwnerRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt => alrt.owner_user_id == request.UserID
                    && request.StatusIDs.Contains(alrt.status_ID))
                .OrderBy(alrt => alrt.alert_id).Skip(request.paging.RangeStart-1)
                .Take(request.paging.RangeEnd - request.paging.RangeStart + 1).ToListAsync();

           
            return res;

         }


        public async Task<double> GetAlertsCountByAssignedUser(GetAlertByAssignedOwnerRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt => alrt.owner_user_id == request.UserID
            && request.StatusIDs.Contains(alrt.status_ID)).CountAsync();

            return res;
        }

        public async Task<List<Alerts>> GetAlertDataByAutomationStatus(GetAlertByAutomationStatusRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt => request.AutomationStatusList.Contains(alrt.Automation_Status)
             && alrt.org_id == request.OrgID && alrt.tool_id == request.ToolID )
                .OrderBy(alrt => alrt.alert_id).Skip(request.paging.RangeStart-1)
                .Take(request.paging.RangeEnd - request.paging.RangeStart + 1).ToListAsync();
            return res;
 
        }
        public async Task<double> GetAlertsCountByAutomationStatus(GetAlertByAutomationStatusRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt => request.AutomationStatusList.Contains(alrt.Automation_Status)
            && alrt.org_id == request.OrgID && alrt.tool_id == request.ToolID).CountAsync();
            return res;
        }

        public async Task<string> UpdateAlertAutomationStatus(UpdateAutomationStatusRequest request)
        {

            var res = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.AlertID).FirstOrDefaultAsync();
            if (res == null)
                return "alert data not found to update";
            else
            {
                res.Automation_Status = request.Status;
                _context.SaveChangesAsync();

            }
            return "";
        }

        public async Task<double> GetUnAttendedAlertsCount(GetUnAttendedAlertCount request,int masterdataID)
        {
            var res = await _context.vm_alerts.Where(alrt => 
           // alrt.owner_user_id == request.UserID &&
             alrt.org_id == request.OrgID
            && alrt.status_ID == masterdataID
            && alrt.detected_time >= DateTime.UtcNow.AddDays(-(double)request.NumberofDays)).CountAsync();
            return res;
        }

        public async Task<string> SetAlertStatus(SetAlertStatusRequest request, string modifiedUserName)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.alertID).AsNoTracking().FirstOrDefaultAsync();
            if (alertdata == null)
            {
                return "Alert data not found";
            }
            else
            {
                BuildSetStatusObject(request, alertdata, modifiedUserName);


            }
            
            _context.vm_alerts.Update(alertdata);

             var status = await _context.SaveChangesAsync();
            if (status > 0)
                return "";
            else
                return "Failed to assign the status to alert ";
        }

        public async Task<string> SetMultipleAlertsStatus(SetMultipleAlertStatusRequest request, string modifiedUserName)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => request.alertIDs.Contains(alrt.alert_id)).AsNoTracking().ToListAsync();
            
            if (alertdata == null || alertdata.Count == 0 )
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.ForEach(alert => BuildSetStatusObject(request,alert,modifiedUserName));
            }
            _context.vm_alerts.UpdateRange(alertdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the status to alert ";
        }
        public async Task<string> AlertEscalateAction(AlertEscalateActionRequest request, string ownerUserName, string modifiedUserName)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => request.alertIDs.Contains(alrt.alert_id)).AsNoTracking().ToListAsync();

            if (alertdata == null || alertdata.Count == 0)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.ForEach(alert =>
                {
                    alert.Modified_Date = request.ModifiedDate;
                    alert.Modified_User = modifiedUserName;
                    alert.owner_user_id = request.OwnerUserId;
                    alert.owner_user_name = ownerUserName;
                }
                );
            }
            _context.vm_alerts.UpdateRange(alertdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to perform escalate action ";
        }

        public async Task<string> IgnoreORIrrelavantAction(AlertIgnoreORIrrelavantActionRequest request, int  analystVerdictId, string modifiedUserName)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => request.alertIDs.Contains(alrt.alert_id)).AsNoTracking().ToListAsync();

            if (alertdata == null || alertdata.Count == 0)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.ForEach(alert =>
                {
                    alert.Modified_Date = request.ModifiedDate;
                    alert.Modified_User = modifiedUserName;
                    //alert.owner_user_id = request.OwnerUserId;
                    //alert.owner_user_name = ownerUserName;
                    alert.false_positive = 1;
                    alert.positive_analysis_id = analystVerdictId;
                    alert.positive_analysis = Constants.LDC_Analyst_Verdict_FalsePositive;

                }
                );
            }
            _context.vm_alerts.UpdateRange(alertdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to perform escalate action ";
        }

        public async Task<string> UpdateAlertStatus(UpdateAlertStatusRequest request, string modifiedUserName, string statusName)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => request.AlertIds.Contains(alrt.alert_id)).AsNoTracking().ToListAsync();

            if (alertdata == null || alertdata.Count == 0)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.ForEach(alert => BuildSetStatusObject(new SetAlertStatusCommonRequest()
                {
                    OrgId = request.OrgID,
                    ModifiedUserId = request.ModifiedUserId,
                    ModifiedDate = request.ModifiedDate,
                    StatusID = request.StatusId,
                    StatusName = statusName
                }, 
                alert, modifiedUserName)
                );
            
            }
            _context.vm_alerts.UpdateRange(alertdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the status to alert ";
        }

        private static void BuildSetStatusObject(SetAlertStatusCommonRequest request, Alerts? alertdata, string modifiedUserName)
        {
            alertdata.status_ID = request.StatusID;
            alertdata.status = request.StatusName;
            alertdata.Modified_Date = request.ModifiedDate;
            alertdata.Modified_User = modifiedUserName;

            //Resolved status
            if (alertdata.resolved_time == null && (request.StatusName == Constants.Alert_Closed_Status || request.StatusName == Constants.Alert_Resolved_Status))

                {
                alertdata.resolved_time = request.ModifiedDate;
                TimeSpan timeSpan = (request.ModifiedDate.Value - alertdata.detected_time.Value);
                alertdata.resolved_time_Duration = timeSpan.TotalSeconds;
                }
            else
            {
                alertdata.resolved_time = null;
                alertdata.resolved_time_Duration = 0;
            }

            //Irrelavant / Ignore

            //if (request.StatusName == Constants.Alert_Irrelavant_Status)
            //{
            //    alertdata.false_positive = 1;
            //}
            //else
            //{
            //    alertdata.false_positive = 0;
            //}
            //setting assigne user changes
            if (!string.IsNullOrEmpty(request.OwnerName) && request.OwnerID > 0 )
            {
                if (alertdata.owner_user_id != request.OwnerID)
                {
                    alertdata.owner_user_id = request.OwnerID;
                    alertdata.owner_user_name = request.OwnerName;
                }
            }
            

        }

        public async Task<string> SetAlertPriority(SetAlertPriorityRequest request, string modifiedUserName)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.AlertID).AsNoTracking().FirstOrDefaultAsync();
            if (alertdata == null)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.priority_id = request.PriorityID;
                alertdata.priority_name = request.PriorityValue;
                alertdata.Modified_Date = request.ModifiedDate;
                alertdata.Modified_User =  modifiedUserName;
            }
            _context.vm_alerts.Update(alertdata);
            var status = await _context.SaveChangesAsync();
  
            if (status > 0)
                return "";
            else
                return "Failed to assign the user to alert ";
        }
        public async Task<string> SetAlertSevirity(SetAlertSevirityRequest request, string modifiedUserName)
        {
            
            var alertdata = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.AlertID).AsNoTracking().FirstOrDefaultAsync();
            if (alertdata == null)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.severity_id = request.SevirityId;
                alertdata.severity_name = request.Sevirity;
                alertdata.Modified_Date = request.ModifiedDate;
                alertdata.Modified_User = modifiedUserName;

                _context.vm_alerts.Update(alertdata);
                var status = await _context.SaveChangesAsync();

                if (status > 0)
                    return "";
                else
                    return "Failed to assign the user to alert ";
            }
        }
        public async Task<string> AssignAlertTag(AssignAlertTagsRequest request, string modifiedUserName)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.AlertID).AsNoTracking().FirstOrDefaultAsync();
            if (alertdata == null)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.observable_tag_ID = request.TagID;
                alertdata.observable_tag = request.TagText;
                alertdata.Modified_Date = request.ModifiedDate;
                alertdata.Modified_User = modifiedUserName;
            }
             _context.vm_alerts.Update(alertdata);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the tag to alert ";
        }

        public async Task<string> AssignAlertScores(AssignAlertScoresRequest request, string modifiedUserName)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.AlertID).AsNoTracking().FirstOrDefaultAsync();
            if (alertdata == null)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.score = request.Score;
                alertdata.Modified_Date = request.ModifiedDate;
                alertdata.Modified_User = modifiedUserName;
            }
            _context.vm_alerts.Update(alertdata);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the score to alert ";

        }

        public async Task<double> GetAlertsCountByPriorityAndStatus(GetAlertsCountByPriorityAndStatusRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt =>
             alrt.org_id == request.OrgID
            && alrt.status_ID == request.StatusID
            && alrt.priority_id == request.PriorityID
            && alrt.detected_time >= DateTime.UtcNow.AddDays(-(double)request.NumberofDays)
            ).CountAsync();
            return res;
        }

        public async Task<double> GetAlertsCountByPositiveAnalysis(GetAlertsCountByPositiveAnalysisRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt =>
             //alrt.owner_user_id == request.UserID &&
             alrt.org_id == request.OrgID
            && alrt.false_positive == 1
            && alrt.detected_time >= DateTime.UtcNow.AddDays(-(double)request.NumberofDays)
            ).CountAsync();
            return res;
        }

        public async Task<string> SetAlertPositiveAnalysis(SetAlertPositiveAnalysisRequest request)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => alrt.alert_id == request.AlertID).AsNoTracking().FirstOrDefaultAsync();
            if (alertdata == null)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.positive_analysis_id = request.PositiveAnalysisID;
                alertdata.positive_analysis = request.PositiveAnalysisValue;
                alertdata.Modified_Date = request.ModifiedDate;
                //alertdata.Modified_User = request.ModifiedUser;
            }

            _context.vm_alerts.Update(alertdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the tag to alert ";
        }
        public async Task<string> SetAnalystVerdict(SetAnalystVerdictDTO request)
        {
            var alertdata = await _context.vm_alerts.Where(alrt => request.AlertIds.Contains(alrt.alert_id)).AsNoTracking().ToListAsync();
            if (alertdata.Count == 0)
            {
                return "Alert data not found";
            }
            else
            {
                alertdata.ForEach( alert =>
                {
                    alert.positive_analysis_id = request.AnalystVerdictId;
                    alert.positive_analysis = request.AnalystVerdictValue;
                    if (request.AnalystVerdictValue == Constants.LDC_Analyst_Verdict_FalsePositive)
                    {
                        alert.false_positive = 1;
                    }
                    else
                    {
                        alert.false_positive = 0;
                    }
                    alert.Modified_Date = request.ModifiedDate;
                    alert.Modified_User = request.ModifiedUser;
                 }
                );
            }

            _context.vm_alerts.UpdateRange(alertdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the tag to alert ";
        }
        public async Task<double> GetAlertsResolvedMeanTime(GetAlertsResolvedMeanTimeRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt =>
            //alrt.owner_user_id == request.UserID &&
            alrt.org_id == request.OrgID
            && alrt.resolved_time_Duration > 0
            && alrt.detected_time >= DateTime.UtcNow.AddDays(-(double)request.NumberofDays)
           ).ToListAsync();
            if (res != null && res.Count > 0)
            {
                return res.Select(a => a.resolved_time_Duration).Sum()/ res.Count;
            }
            
            return 0;
        }

        public async Task<List<Alerts>> GetAlertsMostUsedTages(GetAlertsMostUsedTagsRequest request)
        {
            var res = await _context.vm_alerts.Where(alrt =>
            //alrt.owner_user_id == request.UserID &&
            alrt.org_id == request.OrgID
            && alrt.observable_tag_ID > 0
            && alrt.detected_time >= DateTime.UtcNow.AddDays(-(double)request.NumberofDays)
           ).ToListAsync();
            return res;
        }

        public async Task<List<Alerts>> GetAlertsTrendData(GetAlertsTrendDatasRequest request, int LastNumberofHours)
        {
            var res = await _context.vm_alerts.Where(alrt =>
            //alrt.owner_user_id == request.UserID &&
            alrt.org_id == request.OrgID
            && alrt.detected_time >= DateTime.UtcNow.AddHours(-(double)LastNumberofHours)
           )
            .OrderByDescending( a => a.alert_id)
                .ToListAsync();
            return res;
        }

        public async Task<string> AddAlertNote(alert_note request)
        {
            _context.vm_alerts_notes.Add(request);
           // await _context.SaveChangesAsync();
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the alert note";
           // return "success";
        }

        public async Task<string> AddRangeAlertNote(List<alert_note> request)
        {
            _context.vm_alerts_notes.AddRange(request);
            //await _context.SaveChangesAsync();
            //return "success";

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the alert notes";

        }

        public async Task<List<alert_note>> GetAlertNotesByAlertID(GetAlertNoteRequest request)
        {
           return  await _context.vm_alerts_notes.Where(note => note.alert_id == request.AlertID)
                .OrderByDescending(on => on.Created_date).AsNoTracking().ToListAsync();
        }

        public async Task<string> UpdateAlertIncidentMappingId(List<double> alertIds, int alertIncidentMapId)
        {
            var alertMapdatadata = await _context.vm_alert_incident_mapping_dtl.Where(map => 
                 alertIds.Contains(map.alert_id)).OrderByDescending(m => m.alert_incident_mapping_dtl_id)
                .AsNoTracking().ToListAsync();

            if (alertMapdatadata == null)
            {
                return "Alert mapping data not found";
            }
            else
            {
                var alertdatas = await _context.vm_alerts.Where(alrt =>  alertIds.Contains(alrt.alert_id)).AsNoTracking().ToListAsync();
                if (alertdatas == null)
                {
                    return "Alert data not found";
                }
                else
                {
                    foreach(var alertdata in alertdatas) 
                    {
                        alertdata.alert_incident_mapping_id = alertIncidentMapId;
                    }
                    _context.vm_alerts.UpdateRange(alertdatas);

                    var status = await _context.SaveChangesAsync();

                    if (status > 0)
                        return "";
                    else
                        return "Failed to assign the alert incident mapping id to alert ";

                }
            }

           
        }

        public List<Alerts> GetalertsByAlertIds(List<double> request)
        {
            
            var alerts = _context.vm_alerts
               .Where(al =>  request.Contains(al.alert_id))
               .AsNoTracking()
               .ToList();

          
            return alerts;
        }

       public async Task<string> AddAlertAccountStructureRange(List<AlertsAccountStructure> alertAccountStructureList)
        {
            _context.vm_alerts_account_structure.AddRange(alertAccountStructureList);

            var res = await _context.SaveChangesAsync();
            if (res == 1)
                return "success";
            else
                return null;


        }
    }


}
