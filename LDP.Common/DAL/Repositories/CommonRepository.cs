using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Requests.Common;
using LDP.Common.Responses;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

namespace LDP.Common.DAL.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly CommonDataContext? _context;

        public List<string> _taskOpenCriteria = new List<string> { "Closed", "Cancelled" };
        public CommonRepository(CommonDataContext? context)
        {
            _context = context;
        }

       

        public async Task<string> AddUserAction(useraction request)
        {
            _context.vm_user_actions.Add(request);

            var status = await _context.SaveChangesAsync();
                

            if (status > 0)
                return "";
            else
                return "Failed to add the user action";
        }

        public List<useraction> GetUserActions(int UserID)
        {
            throw new NotImplementedException();
        }


        #region Tasks

        public async Task<string> AddTask(Tasks request)
        {
            _context.vm_tasks.Add(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the task";

        }
        public async  Task<string> UpdateTask(Tasks request)
        {
            var _task = await _context.vm_tasks.Where(ts => ts.task_id == request.task_id
            && !_taskOpenCriteria.Contains(ts.status)).AsNoTracking().FirstOrDefaultAsync();
            if (_task == null)
            {
                return "No Task found ";
            }
            request.created_date = _task.created_date;
            request.created_user = _task.cancelled_user;
            request.status = _task.status;
            request.cancelled_user = _task.cancelled_user;
            request.cancelled_date = _task.cancelled_date;
            _context.vm_tasks.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the task data ";
        }
        public async Task<string> UpdatePasswordResetTaskStatus(UpdateTaskStatusRequest request , string modifiedUser )
        {
            var _tasks = await _context.vm_tasks.Where(ts => ts.task_for_user_id == request.UserId 
             && ts.task_type == "Password_Reset"
             && !_taskOpenCriteria.Contains(ts.status)).AsNoTracking().ToListAsync();
            if (_tasks.Count == 0)
            {
                return "No Task found ";
            }
            else
            {
                foreach (var _task in _tasks)
                {
                    _task.status = request.Status;
                    _task.modified_date = request.ModifiedDate.Value.ToUniversalTime();
                    _task.modified_user = modifiedUser;
                }
            }

            _context.vm_tasks.UpdateRange(_tasks);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the task status";
        }

        public async Task<string> CancelTask(TaskCancelRequest request, string cancelledUser)
        {
            var _task = await _context.vm_tasks.Where(ts => ts.task_id == request.TaskId
             && !_taskOpenCriteria.Contains(ts.status)).AsNoTracking().FirstOrDefaultAsync();
            if (_task == null)
            {
                return "No Task found ";
            }
            else
            {
                _task.status = "Cancelled";
                _task.cancelled_date = request.CancelledDate;
                _task.cancelled_user = cancelledUser;
            }

            _context.vm_tasks.Update(_task);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the task status";
        }
        public async Task<List<Tasks>> GetOpenTasksByOwner(int ownerUserId)
        {
            //try
            //{
                var _notInStatusList = new List<string> { "Closed", "Cancelled" };

                var res = await  _context.vm_tasks.Where(ts => ts.owner_Id == ownerUserId
                                    && !_taskOpenCriteria.Contains(ts.status)).OrderByDescending(t => t.task_id).AsNoTracking().ToListAsync(); ;
                                                                               // .AsNoTracking();
                                                                               // .ToListAsync();
               // var res1 = await res.ToListAsync();
                return res;
            //}
            //catch (Exception ex)
            //{
            //    var x = ex.InnerException.ToString();
            //}


           // return null;
        }

        
        public async Task<List<Tasks>> GetOpenTasksByTaskForUser(int taskForUserId)
        {
            var _notInStatusList = new List<string> { "Closed", "Cancelled" };

            var res = await _context.vm_tasks.Where(ts => ts.task_for_user_id == taskForUserId
                                && !_taskOpenCriteria.Contains(ts.status)).OrderByDescending(t => t.task_id)
                .AsNoTracking().ToListAsync();
            return res;
        }


        #endregion Tasks

        public async Task<ActivityType> GetActivityTypeByTypeName(string activityTypeName)
        {
            var res = await _context.vm_ldc_activity_types.Where(at => at.type_name.ToLower()==activityTypeName.ToLower())
                .AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
       public async  Task<List<ActivityType>> GetActivityTypeByTypeNames(List<string> activityTypeNames)
       {
            return await _context.vm_ldc_activity_types.Where(at => activityTypeNames.Contains(at.type_name))
                        .AsNoTracking().ToListAsync();
       }
        public async Task<string> AddActivity(LdcActivity request)
        {
            _context.vm_ldc_activities.Add(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the activity";

        }
       public async Task<string> AddActivityRange(List<LdcActivity> request)
        {
            _context.vm_ldc_activities.AddRange(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the activity list";
        }
        public async Task<List<ActivityType>> GetActivityTypes()
        {
            var res = await _context.vm_ldc_activity_types.AsNoTracking().ToListAsync();
            return res;

        }
        public  (List<LdcActivity>, double count) GetActivities(GetActivitiesRequest request)
        {
            if (request.paging == null )
            {
                request.paging = new LDP_APIs.Models.RequestPaging()
                {
                     RangeEnd = 10 ,
                      RangeStart = 1
                };
            }
            if (request.paging.RangeStart == 0) request.paging.RangeStart = 1;
            if (request.paging.RangeEnd == 0) request.paging.RangeEnd = 10;

            List<int?> orgIds = new List<int?>()
            {0, request.OrgId };

            IQueryable<LdcActivity> dynamicquery = _context.vm_ldc_activities.Where(a => orgIds.Contains(a.org_id));

            if (!string.IsNullOrEmpty(request.UserId.ToString()) && request.UserId > 0 )
            {
                dynamicquery = dynamicquery.Where(a => a.created_user_id == request.UserId);
            }

            if (request.ActivityTypeIds!=null && request.ActivityTypeIds.Count>0)
            {
                dynamicquery = dynamicquery.Where(a => request.ActivityTypeIds.Contains(a.activity_type_id));
            }
            if (request.AlertIds != null && request.AlertIds.Count > 0)
            {
                dynamicquery = dynamicquery.Where(a => request.AlertIds.Contains(a.alert_id));
            }
            if (request.IncidentIds != null && request.IncidentIds.Count > 0)
            {
                dynamicquery = dynamicquery.Where(a => request.IncidentIds.Contains(a.incident_id));
            }
            if (request.FromDateTime != null )
            {
                dynamicquery = dynamicquery.Where(a => request.FromDateTime.Value.ToUniversalTime() <= a.activity_date  );
            }
            if (request.ToDateTime != null)
            {
                dynamicquery = dynamicquery.Where(a => request.ToDateTime.Value.ToUniversalTime() >= a.activity_date);
            }
            var count = dynamicquery.Count();
            dynamicquery = dynamicquery.OrderByDescending(a => a.activity_id);
            dynamicquery = dynamicquery.Skip(request.paging.RangeStart - 1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1);
           
            var query = dynamicquery.AsNoTracking().ToList();
            return (query,count);
        }
    }
}

