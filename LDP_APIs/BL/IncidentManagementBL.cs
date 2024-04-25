using LDP_APIs.BL.Factories;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.Interfaces;
using LDP_APIs.Models;

namespace LDP_APIs.BL
{
    public class IncidentManagementBL : IIncidentManagementBL
    { 
        IIncidentManagementService _service;
        ILDPlattformBL _plattformBL;
        public IncidentManagementBL(IIncidentManagementService service, ILDPlattformBL plattformBL)
        {
            _service = service;
            _plattformBL = plattformBL;
    }
        public string CreateIncident(CreateIncidentRequest requestlist)
        {
            //IncidentsSystemAuthDtls authdata = new IncidentsSystemAuthDtls()
            // { UserName = "username", IncidentSystemUrl = "url", Password = "pwd" };
            CreateIncidentDTO createIncidentDTO = new CreateIncidentDTO();
            var conndtl = _plattformBL.GetToolConnectionDetails(requestlist.OrgID, requestlist.ToolID);
            createIncidentDTO.APIUrl = conndtl.ApiUrl;
            createIncidentDTO.AuthKey = conndtl.AuthKey;
            _service = TicketManagementFactory.GetInstance(requestlist.ToolID);
            foreach (AlertModel alertData in requestlist.AlertData)
            {
                
                createIncidentDTO.AlertData = alertData;
               // createIncidentDTO.AuthData = authdata;
                try
                {
                    
                    _service.CreateIncident(createIncidentDTO);
                }
                catch (Exception ex)
                {

                }
            }
            return "success";
        }

        public Task<GetIncidentsResponse> GetIncidents()
        {
            var res = _service.GetIncidents();
            return res;
        }
    }
}
