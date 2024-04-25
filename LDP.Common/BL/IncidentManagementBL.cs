//using LDP.Common;
//using LDP_APIs.BL.Factories;
//using LDP_APIs.BL.Interfaces;
//using LDP_APIs.BL.Models;
//using LDP_APIs.Interfaces;
//using LDP_APIs.Models;

//namespace LDP_APIs.BL
//{
//    public class IncidentManagementBL : IIncidentManagementBL
//    { 
//        IIncidentManagementService _service;
//        ILDPlattformBL _plattformBL;
//        public IncidentManagementBL(IIncidentManagementService service, ILDPlattformBL plattformBL)
//        {
//            _service = service;
//            _plattformBL = plattformBL;
//    }
//        public string CreateIncident(CreateIncidentRequest requestlist)
//        {
//            CreateIncidentDTO createIncidentDTO = new CreateIncidentDTO();
//            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_TicketManagement);
//            var conndtl = _plattformBL.GetToolConnectionDetails(requestlist.OrgID, tooltypeID);
//            if (conndtl==null)
//            {
//                return "Tool connection details not found";
//            }
//            createIncidentDTO.APIUrl = conndtl.ApiUrl;
//            createIncidentDTO.AuthKey = conndtl.AuthKey;
//            _service = TicketManagementFactory.GetInstance(requestlist.ToolID);
//            foreach (AlertModel alertData in requestlist.AlertData)
//            {
//                createIncidentDTO.AlertData = alertData;
//                try
//                {
                    
//                    _service.CreateIncident(createIncidentDTO);
//                }
//                catch (Exception ex)
//                {

//                }
//            }
//            return "success";
//        }

//        public GetIncidentsResponse GetIncidents()
//        {
//            //var res = _service.GetIncidents();
//            //return res;

//            return new GetIncidentsResponse();
//        }

//        Task<GetIncidentsResponse> IIncidentManagementBL.GetIncidents()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
