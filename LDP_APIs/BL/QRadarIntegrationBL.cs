using AutoMapper;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;
using LDP_APIs.DAL.Interfaces;
using LDP_APIs.Interfaces;
using LDP_APIs.Models;

namespace LDP_APIs.BL
{
    public class QRadarIntegrationBL : IQRadarIntegrationBL
    {
        IQRadarIntegrationservice _service;
        IAlertsRepository _repo;
        public readonly IMapper _mapper;
        ILDPlattformBL _plattformBL;
        public QRadarIntegrationBL(IQRadarIntegrationservice service,IAlertsRepository repo
            , IMapper mapper
             , ILDPlattformBL plattformBL )
        {
            _service = service;
            _repo = repo;   
            _mapper = mapper;
            _plattformBL = plattformBL;
        }
        public Task<getOffenseResponse> Getoffenses(GetOffenseRequest request)
        {
            GetOffenseDTO dto = new GetOffenseDTO();
            dto.clientRequest = request;
            dto.ToolID = 1;
            dto.clientRequest.OrgID = 1;
            var conndtl = _plattformBL.GetToolConnectionDetails(dto.clientRequest.OrgID,dto.ToolID);
            dto.APIUrl = conndtl.ApiUrl;
            dto.AuthKey = conndtl.AuthKey;
            dto.alert_MaxPKID = conndtl.LastReadPKID;
            var res = _service.Getoffenses(dto);
            var _mappedResponse = _mapper.Map<IEnumerable<QRadaroffense>, List<Alerts>> (res.Result.offensesList);
            _mappedResponse.ForEach(obj => { obj.tool_id = 1; obj.org_id = 1; });
               
            _repo.Addalerts(_mappedResponse);
            dto.alert_MaxPKID = _mappedResponse.Max(obj => obj.alert_Device_PKID);
            _plattformBL.UpdateLastReadPKID(dto);
            res.Result.offensesList = null;   
            return res; 

        }

      
    }
}
