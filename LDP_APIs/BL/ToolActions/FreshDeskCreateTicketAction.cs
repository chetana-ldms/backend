using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;

namespace LDP_APIs.BL.ToolActions
{
    public class FreshDeskCreateTicketAction : ToolActionBase
    {
        IIncidentsBL _Incidentmgmt;
        public readonly IMapper _mapper;
        public FreshDeskCreateTicketAction(IIncidentsBL Incidentmgmt, IMapper mapper)
        {
            _Incidentmgmt=Incidentmgmt;
            _mapper=mapper;
        }
        public override ToolActionResponse ExecuteAction(ToolActionRequest request)
        {
            ToolActionResponse response = new ToolActionResponse();
            FreshDeskTicketingToolActionRequest? freshdeskRequest = new FreshDeskTicketingToolActionRequest();
            freshdeskRequest = request as FreshDeskTicketingToolActionRequest;
            var _mapperResponse = _mapper.Map<FreshDeskTicketingToolActionRequest, CreateIncidentRequest>(freshdeskRequest);
            var incidentresponse = _Incidentmgmt.CreateIncident(_mapperResponse);
            response.IsSuccess = true;
            response.Message = "Success";
            return response;    


        }
    }
}
