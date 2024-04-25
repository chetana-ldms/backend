using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LDP.Common.DAL.Repositories
{
    public class AlertIncidentMappingRepository : IAlertIncidentMappingRepository
    {
        private readonly AlertsDataContext? _context;
        public readonly IMapper _mapper;
        public AlertIncidentMappingRepository(AlertsDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AlertIncidentMappingResponse> AddAlertIncidentMapping(AddAlertIncidentMappingRequest request)
        {
            AlertIncidentMappingResponse response = new AlertIncidentMappingResponse();
            //
            AlertIncidentMapping _mappedRequest = null;
            List<AlertIncidentMappingDtl> _mappedDtlRequest  = null;

            using (IDbContextTransaction? transaction = _context?.Database.BeginTransaction())
            {
                try
                {
                    _mappedRequest = _mapper.Map<AddAlertIncidentMappingRequest, AlertIncidentMapping>(request);
                    _mappedDtlRequest = _mapper.Map<List<AddAlertIncidentMappingDtlModel>, List<AlertIncidentMappingDtl>>(request.AlertIncidentMappingDtl);
                    _context.vm_alert_incident_mapping.Add(_mappedRequest);
                    _context.SaveChangesAsync();
                    _mappedDtlRequest.ForEach(item => item.alert_incident_mapping_id = _mappedRequest.alert_incident_mapping_id);
                    _context.vm_alert_incident_mapping_dtl.AddRange(_mappedDtlRequest);

                    _context.SaveChangesAsync();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
                //
                response.IsSuccess = true;
                response.AlertIncidentMappingId = _mappedRequest.alert_incident_mapping_id;
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                response.Message = "Success";

                return response;
            }
        }
        public async Task<string> AddAlertIncidentMappingDtl(AlertIncidentMappingDtl request)
        {
            _context.vm_alert_incident_mapping_dtl.Add(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<string> AddRangeAlertIncidentMapping(List<AddAlertIncidentMappingRequest> request)
        {
           // _context.vm_alert_incident_mapping.AddRange(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<string> AddRangeAlertIncidentMappingDtl(List<AlertIncidentMappingDtl> request)
        {
            _context.vm_alert_incident_mapping_dtl.AddRange(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<List<AlertIncidentMapping>> GetAlertIncidentMappingByIncidentIDs(List<double> incidentIds)
        {
            var res = _context.vm_alert_incident_mapping.Where(map => 
                 incidentIds.Contains(map.incident_number))
                .AsNoTracking().ToListAsync();
            return res.Result;
        }


        public async Task<List<AlertIncidentMappingDtl>> GetAlertIncidentMappingDtlByMappingIDs(List<int> mappingIDs)
        {
            var res = _context.vm_alert_incident_mapping_dtl.Where(map =>
                mappingIDs.Contains(map.alert_incident_mapping_id))
               .AsNoTracking().ToListAsync();
            return res.Result;
        }

        public async Task<List<GetAlertsByIncidentIdModel>> GetAlertsByIncidentId(GetAlertsByIncidentIdRequest request)
        {
            var res = await (from map in _context.vm_alert_incident_mapping
                                     join mapdtl in _context.vm_alert_incident_mapping_dtl 
                                     on map.alert_incident_mapping_id equals mapdtl.alert_incident_mapping_id
                                     where map.incident_number == request.IncidentId
                                     select (new GetAlertsByIncidentIdModel
                                     {
                                        alertid = mapdtl.alert_id,
                                        orgid = map.org_id

                                     }))
                              .AsNoTracking()
                              .ToListAsync();
            return res;
        }

        public async Task<AlertIncidentMapping> GetAlertIncidentMappingByIncidentID(double incidentId)
        {
            var res = await _context.vm_alert_incident_mapping.Where(map =>
                 map.incident_number == incidentId)
                .AsNoTracking().FirstOrDefaultAsync();
            return res;

        }
        
        
        public async Task<List<AlertIncidentMappingDtl>> GetAlertIncidentMappingDtlByMappingID(double mappingID)
        {
            var res = await _context.vm_alert_incident_mapping_dtl.Where(map =>
                map.alert_incident_mapping_id == mappingID)
               .AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<string> UpdateSignificantFlag(int incidentId, int significantFlag)
        {
            var alertMap = await _context.vm_alert_incident_mapping.Where(m => m.incident_number == incidentId).AsNoTracking().FirstOrDefaultAsync(); ;

            if (alertMap != null) 
            {
                alertMap.significant_incident = significantFlag;
                _context.vm_alert_incident_mapping.Update(alertMap);
                _context.SaveChangesAsync();
            }
            return "success";

        }
    }
}
