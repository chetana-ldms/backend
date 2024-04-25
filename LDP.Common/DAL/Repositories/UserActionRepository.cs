using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class UserActionRepository : IUserActionsRepository
    {
        private readonly CommonDataContext? _context;
        public UserActionRepository(CommonDataContext? context)
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

        public async Task<string> UpdateUserAction(useraction request)
        {
            _context.vm_user_actions.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the user action";
        }
        public async Task<string> AssignUserActionScore(AssignUserActionScoresRequest request)
        {
            var useraction = await _context.vm_user_actions.Where(ua => ua.action_type_refid == request.Id 
                                    && ua.action_type == request.ActionType)
                .AsNoTracking().FirstOrDefaultAsync();
            if (useraction == null)
            {
                return "User action data not found";
            }
            else
            {
                useraction.score = request.Score;
                useraction.Modified_Date = request.ModifiedDate;
                useraction.Modified_User = request.ModifiedUser;
            }

            _context.vm_user_actions.Update(useraction);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the score to user action ";
        }

        public async Task<string> AssignOwner(AssignUserActionOwnerRequest request)
        {
            var useraction = await _context.vm_user_actions.Where(ua => ua.action_type_refid == request.Id
                                   && ua.action_type == request.ActionType)
               .AsNoTracking().FirstOrDefaultAsync();
            if (useraction == null)
            {
                return "User action data not found";
            }
            else
            {
                useraction.Owner = request.UserId;
                useraction.Owner_Name = request.UserName;
                useraction.Modified_Date = request.ModifiedDate;
                useraction.Modified_User = request.ModifiedUser;
            }

            _context.vm_user_actions.Update(useraction);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the user to user action ";
        }

       

        public async Task<List<useraction>> GetUserActionsByUser(GetUserActionRequest request)
        {
            var res = await _context.vm_user_actions.Where(ua => ua.Owner == request.UserId
             && ua.action_Date >= DateTime.UtcNow.AddHours(-(double)24)
             )
                .Skip(0).Take(5)
                .OrderByDescending(a => a.action_Date)
                .AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<useraction> GetUserActionsByActionTypeRefID(GetUserActionByActionTypeRefIDRequest request)
        {
            var res = await _context.vm_user_actions.Where(ua => ua.action_type == request.ActionType
            && ua.action_type_refid == request.ActionTypeRefId).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }

        public async Task<List<useraction>> GetUserActionsByMultipleActionTypeRefID(GetUserActionByMultipleActionTypeRefIDRequest request)
        { 
            var res = await _context.vm_user_actions.Where(ua => ua.action_type == request.ActionType
            && request.ActionTypeRefIds.Contains(ua.action_type_refid)).AsNoTracking().ToListAsync();
            return res;
        }
        

        public async Task<string> SetUserActiontPriority(SetUserActionPriorityRequest request)
        {
            var useraction = await _context.vm_user_actions.Where(ua => ua.action_type_refid == request.Id
                                   && ua.action_type == request.ActionType)
               .AsNoTracking().FirstOrDefaultAsync();
            if (useraction == null)
            {
                return "User action data not found";
            }
            else
            {
                useraction.Priority = request.PriorityId;
                useraction.Priority_Name = request.PriorityValue;
                useraction.Modified_Date = request.ModifiedDate;
                useraction.Modified_User = request.ModifiedUser;
            }

            _context.vm_user_actions.Update(useraction);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the priority to user action ";
        }


        public async Task<string> SetUserActiontSeviarity(SetUserActionSeviarityRequest request)
        {
            var useraction = await _context.vm_user_actions.Where(ua => ua.action_type_refid == request.Id
                                   && ua.action_type == request.ActionType)
               .AsNoTracking().FirstOrDefaultAsync();
            if (useraction == null)
            {
                return "User action data not found";
            }
            else
            {
                useraction.Severity = request.Sevirity;
                useraction.severity_id = request.SevirityId;
                useraction.Modified_Date = request.ModifiedDate;
                useraction.Modified_User = request.ModifiedUser;
            }

            _context.vm_user_actions.Update(useraction);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the priority to user action ";
        }

        public async Task<string> SetUserActionStatus(SetUserActionStatusRequest request)
        {
            var useraction = await _context.vm_user_actions.Where(ua => ua.action_type_refid == request.Id
                                  && ua.action_type == request.ActionType)
              .AsNoTracking().FirstOrDefaultAsync();
            if (useraction == null)
            {
                return "User action data not found";
            }
            else
            {
                useraction.action_Status = request.StatusId;
                useraction.Action_Status_Name = request.StatusName;
                useraction.Modified_Date = request.ModifiedDate;
                useraction.Modified_User = request.ModifiedUser;
            }

            _context.vm_user_actions.Update(useraction);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the status to user action ";
        }

        public async Task<string> SetMultipleUserActionStatus(SetMultipleUserActionStatusRequest request)
        {
            var useractions = await _context.vm_user_actions.Where(ua => request.Ids.Contains(ua.action_type_refid)  
                                  && ua.action_type == request.ActionType)
              .AsNoTracking().ToListAsync();
            if (useractions == null && useractions.Count ==0 )
            {
                return "User action data not found";
            }
            else
            {
                
                foreach (var useraction in useractions)
                {
                    useraction.action_Status = request.StatusId;
                    useraction.Action_Status_Name = request.StatusName;
                    useraction.Modified_Date = request.ModifiedDate;
                    useraction.Modified_User = request.ModifiedUser;
                    if(request.OwnerId > 0 && !string.IsNullOrEmpty(request.OwnerName)) 
                    {
                        useraction.Owner = request.OwnerId;
                        useraction.Owner_Name = request.OwnerName;
                    }
                }
            }

            _context.vm_user_actions.UpdateRange(useractions);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the status to user action ";
        }

    }
}
