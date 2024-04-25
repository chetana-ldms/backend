using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class InternalIncidentsRepository : IInternalIncidentsRepository
    {
        private readonly AlertsDataContext? _context;
        public InternalIncidentsRepository(AlertsDataContext? context)
        {
            _context = context;
        }
        public async Task<CreateIncidentInternalResponse> CreateInternalIncident(Incident request)
        {
            CreateIncidentInternalResponse response = new CreateIncidentInternalResponse();
            _context.vm_Incidents.Add(request);
            var res = await _context.SaveChangesAsync();
            response.Message = "success";
            response.IsSuccess= true;
            response.IncidentID = request.Incident_ID;
            return response;
        }
        
        public async Task<List<Incident>> GetInternalIncidents(GetIncidentsRequest request, bool adminUser)
        {
            //List<Incident> incidents = null;
            //if (adminUser)
            //{
            //    incidents = await _context.vm_Incidents
            //        .Where(inc => inc.org_id == request.OrgID)
            //        .OrderByDescending(incnt => incnt.Incident_ID)
            //        .Skip(request.paging.RangeStart-1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)
            //        .AsNoTracking().ToListAsync();
            //} 
            //else
            //{
            //    incidents = await _context.vm_Incidents
            //        .Where(inc => inc.Owner == request.LoggedInUserId && inc.org_id == request.OrgID)
            //        .OrderByDescending(incnt => incnt.Incident_ID-1).Skip(request.paging.RangeStart-1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)
            //        .AsNoTracking().ToListAsync();
            //}
            IQueryable<Incident> query = null;
            IQueryable<Incident> dynamicquery = _context.vm_Incidents.Where(i => i.org_id == request.OrgID);

            if (!adminUser)
            {
                dynamicquery = dynamicquery.Where(i => i.Owner == request.LoggedInUserId);
            }
            dynamicquery = dynamicquery.OrderByDescending(incnt => incnt.Incident_ID);
            dynamicquery = dynamicquery.Skip(request.paging.RangeStart - 1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1);
        
            var res = await dynamicquery.AsNoTracking().ToListAsync();
            //if (!string.IsNullOrEmpty(request.SearchText))
            //{
            //    dynamicquery = dynamicquery.Where(i => EF.Functions.Like(i.Description, $"%{request.SearchText}%"));
            //}
            //if (request.StatusId > 0)
            //{
            //    dynamicquery = dynamicquery.Where(i => i.Incident_Status == request.StatusId);
            //}

            //if (sortOption == Constants.Incident_SortOption_Recent_Updated)
            //{
            //    dynamicquery = dynamicquery.OrderByDescending(incnt => incnt.Modified_Date);

            //}
            //else
            //{
            //    dynamicquery = dynamicquery.OrderByDescending(incnt => incnt.Created_Date);
            //}

            return res;
           // return incidents;
        }
        public async Task<string> UpdateIncident(Incident request)
        {


            _context.vm_Incidents.Update(request);
            var res = await _context.SaveChangesAsync();
            if (res == 1)
                return "success";
            else
                return null;
        }
        public async Task<Incident> GetInternalIncidentData(GetInternalIncidentDataRequest request)
        {
            var res = await _context.vm_Incidents.Where(incnt => incnt.Incident_ID == request.IncidentID
            )
                .AsNoTracking().FirstOrDefaultAsync();
            return res;
        }

        
        public async Task<double> GetInternalIncidentsCount(GetIncidentsRequest request, bool adminUser)
        {
           // double res = 0;
            //if (adminUser)
            //{
            //    res = await _context.vm_Incidents.Where(incMap =>
            //    incMap.org_id == request.OrgID)
            //        .CountAsync();
            //}
            //else
            //{
            //    res = await _context.vm_Incidents.Where(inc =>
            //    inc.org_id == request.OrgID && 
            //    inc.Owner == request.LoggedInUserId)
            //        .CountAsync();
            //}
           // IQueryable <double> query = null;
            IQueryable<Incident> dynamicquery = _context.vm_Incidents.Where(i => i.org_id == request.OrgID);

            if (!adminUser)
            {
                dynamicquery = dynamicquery.Where(i => i.Owner == request.LoggedInUserId);
            }
            var  res = dynamicquery.CountAsync().Result;
            return res;
        }

        public async Task<int> GetUnAttendedIncidentsCount(GetUnAttendedIncidentCount request, int masterdataID)
        {
            var res = await (from inc in _context.vm_Incidents
                             join map in _context.vm_alert_incident_mapping
                             on inc.Incident_ID equals map.incident_number
                             where map.org_id == request.OrgID
                             //&& inc.Owner == request.UserID
                             && inc.Created_Date >= DateTime.UtcNow.AddDays(-(double)request.NumberofDays)
                             && inc.Incident_Status == masterdataID 
                             select inc).CountAsync();
            return res;


        }

        public async Task<string> AssignOwner(IncidentAssignOwnerRequest request)
        {
            var incidentdata = await _context.vm_Incidents.Where(alrt => 
            alrt.Incident_ID == request.IncidentID).AsNoTracking().FirstOrDefaultAsync();
            if (incidentdata == null)
            {
                return "Incident data not found";
            }
            else
            {
                incidentdata.Owner = request.OwnerUserID;
                incidentdata.Owner_Name = request.OwnerUserName;
                incidentdata.Modified_Date = request.ModifiedDate;
                incidentdata.Modified_User = request.ModifiedUser;
            }

            _context.vm_Incidents.Update(incidentdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the user to incident ";
        }

        public async Task<string> SetIncidentStatus(SetIncidentStatusRequest request)
        {
            var incidentdata = await _context.vm_Incidents.Where(alrt =>
             alrt.Incident_ID == request.IncidentID).AsNoTracking().FirstOrDefaultAsync();
            if (incidentdata == null)
            {
                return "Incident data not found";
            }
            else
            {
                incidentdata.Incident_Status = request.StatusID;
                incidentdata.Incident_Status_Name = request.StatusName;
                incidentdata.Modified_Date = request.ModifiedDate;
                incidentdata.Modified_User = request.ModifiedUser;
            }

            _context.vm_Incidents.Update(incidentdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the status to incident ";
        }

        public async Task<string> SetIncidentPriority(SetIncidentPriorityRequest request)
        {
            var incidentdata = await _context.vm_Incidents.Where(alrt =>
             alrt.Incident_ID == request.IncidentID).AsNoTracking().FirstOrDefaultAsync();
            if (incidentdata == null)
            {
                return "Incident data not found";
            }
            else
            {
                incidentdata.Priority = request.PriorityID;
                incidentdata.Priority_Name = request.PriorityValue;
                incidentdata.Modified_Date = request.ModifiedDate;
                incidentdata.Modified_User = request.ModifiedUser;
            }

            _context.vm_Incidents.Update(incidentdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the priority to incident ";
        }

        public async Task<string> SetIncidentSeviarity(SetIncidentSeviarityRequest request)
        {
            var incidentdata = await _context.vm_Incidents.Where(alrt =>
            alrt.Incident_ID == request.IncidentID).AsNoTracking().FirstOrDefaultAsync();
            if (incidentdata == null)
            {
                return "Incident data not found";
            }
            else
            {
                incidentdata.Severity = request.SeviarityID;
                incidentdata.Severity_Name = request.Seviarity;
                incidentdata.Modified_Date = request.ModifiedDate;
                incidentdata.Modified_User = request.ModifiedUser;
            }

            _context.vm_Incidents.Update(incidentdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the seviarity to incident ";
        }
        public async Task<string> SetIncidentType(SetIncidentTypeRequest request)
        {
            var incidentdata = await _context.vm_Incidents.Where(alrt =>
            alrt.Incident_ID == request.IncidentID && alrt.org_id == request.OrgID).AsNoTracking().FirstOrDefaultAsync();
            if (incidentdata == null)
            {
                return "Incident data not found";
            }
            else
            {
                incidentdata.type_id = request.TypeId;
                incidentdata.Type = request.TypeName;
                incidentdata.Modified_Date = request.ModifiedDate;
                incidentdata.Modified_User = request.ModifiedUser;
            }

            _context.vm_Incidents.Update(incidentdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the type to incident ";
        }
        public async Task<string> SetIncidentScore(SetIncidentScoreRequest request)
        {
            var incidentdata = await _context.vm_Incidents.Where(alrt =>
             alrt.Incident_ID == request.IncidentID).AsNoTracking().FirstOrDefaultAsync();
            if (incidentdata == null)
            {
                return "Incident data not found";
            }
            else
            {
                incidentdata.Score = request.Score;
                incidentdata.Modified_Date = request.ModifiedDate;
                incidentdata.Modified_User = request.ModifiedUser;
            }

            _context.vm_Incidents.Update(incidentdata);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to assign the Score to incident ";
        }

        public async Task<List<Incident>> GetMyInternalIncidents(GetMyInternalIncidentsRequest request)
        {
            var query =  _context.vm_Incidents
                .Where(i => i.Owner == request.UserID
                 && 
                 (
                 i.Created_Date >= DateTime.UtcNow.AddHours(-(double)24)
                 || i.Modified_Date >= DateTime.UtcNow.AddHours(-(double)24)
                 )
                    )
                .Skip(0).Take(5)
                //.OrderByDescending(incnt => incnt.Modified_Date)
                .OrderByDescending(incnt => incnt.Created_Date)
               // Modified_Date
                .ThenByDescending(inc => inc.Modified_Date)
                .AsNoTracking();
            var result = await query.ToListAsync();

            return result;
        }

        public async Task<double> GetIncidentCountByPriorityAndStatus(GetIncidentCountByPriorityAndStatusRequest request)
        {
            var res = await _context.vm_Incidents.Where(inc =>
            inc.org_id == request.OrgID
           && inc.Incident_Status== request.StatusID
           && inc.Priority == request.PriorityID
           ).CountAsync();
            return res;
        }

        public async Task<List<Incident>> GetIncidentSearchResult(IncidentSeeachRequest request, bool adminUser , string sortOption)
        {
            IQueryable<Incident> query = null;
            IQueryable<Incident> dynamicquery  = _context.vm_Incidents.Where(i => i.org_id == request.OrgID);

            if (!adminUser)
            {
                dynamicquery = dynamicquery.Where(i => i.Owner == request.LoggedInUserId);
            }
            if (!string.IsNullOrEmpty(request.SearchText) )
            {
                dynamicquery = dynamicquery.Where(i => EF.Functions.Like(i.Description, $"%{request.SearchText}%"));
            }
            if (request.StatusId > 0)
            {
                dynamicquery = dynamicquery.Where(i => i.Incident_Status == request.StatusId);

            }
            if (sortOption == Constants.Incident_SortOption_Recent_Updated)
            {
                dynamicquery = dynamicquery.OrderByDescending(incnt => incnt.Modified_Date);

            }
            else
            {
                dynamicquery = dynamicquery.OrderByDescending(incnt => incnt.Created_Date);
            }
            dynamicquery = dynamicquery.Skip(request.paging.RangeStart - 1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1);

           
            var res =  await dynamicquery.AsNoTracking().ToListAsync();
            return res;

            ////
            //if (adminUser)
            //{
            //    if (sortOption == Constants.Incident_SortOption_Recent_Created)
            //    {
            //        query = _context.vm_Incidents
            //       .Where
            //       (i => i.Incident_Status == request.StatusId
            //        && i.org_id == request.OrgID
            //        && EF.Functions.Like(i.Description, $"%{request.SearchText}%")
            //       )
            //       //.Skip(request.paging.RangeStart).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)
            //       .Skip(request.paging.RangeStart - 1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)

            //       .OrderByDescending(incnt => incnt.Created_Date)
            //       .AsNoTracking();
            //    }
            //    else
            //    {
            //        query = _context.vm_Incidents
            //       .Where
            //       (i => i.Incident_Status == request.StatusId
            //        && i.org_id == request.OrgID
            //        && EF.Functions.Like(i.Description, $"%{request.SearchText}%")
            //       )
            //       //.Skip(request.paging.RangeStart).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)
            //       .Skip(request.paging.RangeStart - 1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)
            //       .OrderByDescending(incnt => incnt.Modified_Date)
            //       .AsNoTracking();
            //    }

            //}
            //else
            //{
            //    if (sortOption == Constants.Incident_SortOption_Recent_Created)
            //    {
            //        query = _context.vm_Incidents
            //       .Where
            //       (i => (i.Incident_Status == request.StatusId || request.StatusId == 0)
            //        && i.Owner == request.LoggedInUserId
            //        && (i.org_id == request.OrgID || request.OrgID == 0)
            //        && (EF.Functions.Like(i.Description, $"%{request.SearchText}%") || string.IsNullOrEmpty(request.SearchText))
            //       )
            //        //                   .Skip(request.paging.RangeStart).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)
            //        .Skip(request.paging.RangeStart - 1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)
            //       .OrderByDescending(incnt => incnt.Created_Date)
            //       .AsNoTracking();
            //    }
            //    else 
            //    {
            //        query = _context.vm_Incidents
            //      .Where
            //      (i => (i.Incident_Status == request.StatusId || request.StatusId == 0)
            //       && i.Owner == request.LoggedInUserId
            //       && (i.org_id == request.OrgID || request.OrgID == 0)
            //       && (EF.Functions.Like(i.Description, $"%{request.SearchText}%") || string.IsNullOrEmpty(request.SearchText)  )
            //      )
            //      //.Skip(request.paging.RangeStart).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)
            //      .Skip(request.paging.RangeStart - 1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1)
            //      .OrderByDescending(incnt => incnt.Modified_Date)
            //      .AsNoTracking();
            //    }
            //}

            //var result = await query.ToListAsync();
            ////
            //return result;
            //
        }

        public async Task<double> GetIncidentSearchResultCount(IncidentSeeachRequest request, bool adminUser)
        {
            //double res = 0;

            IQueryable<Incident> query = null;
            IQueryable<Incident> dynamicquery = _context.vm_Incidents.Where(i => i.org_id == request.OrgID);

            if (!adminUser)
            {
                dynamicquery = dynamicquery.Where(i => i.Owner == request.LoggedInUserId);
            }
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                dynamicquery = dynamicquery.Where(i => EF.Functions.Like(i.Description, $"%{request.SearchText}%"));
            }
            if (request.StatusId > 0)
            {
                dynamicquery = dynamicquery.Where(i => i.Incident_Status == request.StatusId);
            }
            //dynamicquery = dynamicquery.Skip(request.paging.RangeStart - 1).Take(request.paging.RangeEnd - request.paging.RangeStart + 1);

            
            return await dynamicquery.CountAsync();

            //if (adminUser)
            //{
            //    res = await _context.vm_Incidents.Where(inc =>
            //    ( inc.org_id == request.OrgID || request.OrgID == 0) 
            //    && (inc.Incident_Status == request.StatusId || request.StatusId == 0 )
            //     && (EF.Functions.Like(inc.Description, $"%{request.SearchText}%") || string.IsNullOrEmpty(request.SearchText)))
            //        .CountAsync();
            //}
            //else
            //{
            //   res = await _context.vm_Incidents.Where(inc =>
            //   (inc.org_id == request.OrgID || request.OrgID == 0)
            //   && inc.Owner == request.LoggedInUserId
            //   && (inc.Incident_Status == request.StatusId || request.StatusId == 0)
            //   && (EF.Functions.Like(inc.Description, $"%{request.SearchText}%") || string.IsNullOrEmpty(request.SearchText)))
            //  .CountAsync();
            //}
            //return res;
        }
    }
}
