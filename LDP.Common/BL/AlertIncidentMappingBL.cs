using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL
{
    public class AlertIncidentMappingBL : IAlertIncidentMappingBL
    {
        IAlertIncidentMappingRepository _repo;
        public readonly IMapper _mapper;

        public AlertIncidentMappingBL(IAlertIncidentMappingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }
        public AlertIncidentMappingResponse AddAlertIncidentMapping(AddAlertIncidentMappingRequest request)
        {
            //AlertIncidentMappingResponse response = new AlertIncidentMappingResponse();
           return  _repo.AddAlertIncidentMapping(request).Result;
           // return response;
        }

        public AlertIncidentMappingResponse AddRangeAlertIncidentMapping(List<AddAlertIncidentMappingRequest> request)
        {
            AlertIncidentMappingResponse response = new AlertIncidentMappingResponse();
            _repo.AddRangeAlertIncidentMapping(request);
            return response;
        }

        public GetAlertIncidentMappingResponse GetAlertIncidentMapping(GetAlertIncidentMappingRequest request)
        {
            GetAlertIncidentMappingResponse response = new GetAlertIncidentMappingResponse();
            //_repo.get(request);
            return response;
        }

        public AlertIncidentMappingResponse UpdateAlertIncidentMapping(UpdateAlertIncidentMappingRequest request)
        {
            AlertIncidentMappingResponse response = new AlertIncidentMappingResponse();
            //_repo.up(request);
            return response;
        }

       

        public GetAlertIncidentMappingResponse GetAlertIncidentMappingByIncidentIDs(List<double> incidentIds)
        {
            GetAlertIncidentMappingResponse response = new GetAlertIncidentMappingResponse();

            var mappings = _repo.GetAlertIncidentMappingByIncidentIDs(incidentIds);

            if (mappings != null && mappings.Result.Count > 0)
            {
                var mappingModels = _mapper.Map< List<AlertIncidentMapping>, List<GetAlertIncidentMappingModel>>(mappings.Result);
               var mappingiDs = mappings.Result.Select(map => map.alert_incident_mapping_id);

                if (mappingiDs.Count() > 0)
                {
                   var mappingDtls = _repo.GetAlertIncidentMappingDtlByMappingIDs(mappingiDs.ToList());

                    if (mappingDtls.Result.Count > 0)
                    {
                        foreach(var map in mappingModels)
                        {
                            var mappingDtlsformappings = mappingDtls.Result.Where(m => m.alert_incident_mapping_id == map.alertincientmappingid).ToList();
                            if (mappingDtlsformappings.Count() > 0)
                            {
                                map.AlertIncidentMappingDtl = _mapper.Map<List<AlertIncidentMappingDtl>, List<GetAlertIncidentMappingDtlModel>>(mappingDtlsformappings.ToList());
                                
                            }
                        }
                    }
                }

                response.AlertIncidentMappingData = mappingModels;

            }
            return response;
        }

        public GetAlertIncidentMappingSingleResponse GetAlertIncidentMappingByIncidentID(int incidentId)
        {
            GetAlertIncidentMappingSingleResponse response = new GetAlertIncidentMappingSingleResponse();

            var mappings = _repo.GetAlertIncidentMappingByIncidentID(incidentId).Result;

            if (mappings != null )
            {
                var mappingModel = _mapper.Map<AlertIncidentMapping, GetAlertIncidentMappingModel>(mappings);

                var mappingdtls = _repo.GetAlertIncidentMappingDtlByMappingID(mappingModel.alertincientmappingid).Result;

                if (mappingdtls.Count > 0)
                {
                    mappingModel.AlertIncidentMappingDtl = _mapper.Map<List<AlertIncidentMappingDtl>, List<GetAlertIncidentMappingDtlModel>>(mappingdtls);
                  
                }
                response.AlertIncidentMappingData = mappingModel;
            }

            return response;
        }
        public List<GetAlertsByIncidentIdModel> GetAlertsByIncidentId(GetAlertsByIncidentIdRequest request)
        {
            return _repo.GetAlertsByIncidentId(request).Result;
        }

        public AlertIncidentMappingResponse UpdateSignificantFlag(int incidentId, int significantFlag)
        {
            AlertIncidentMappingResponse response = new AlertIncidentMappingResponse();
            _repo.UpdateSignificantFlag(incidentId, significantFlag);
            response.IsSuccess = true;
            return response;
        }
    }
}
