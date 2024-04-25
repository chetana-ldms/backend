using LDP_APIs.BL.Interfaces;
using LDP_APIs.Interfaces;
using LDP_APIs.Models;
using LDPRuleEngine.Controllers.Requests;

namespace LDPRuleEngine.BL.Framework.Actions
{
    public class TicketManagementRuleActionExecuter : baseRuleActionExecuter
    {
        IIncidentManagementBL _bl;
        IAlertsBL _alertbl;
        public TicketManagementRuleActionExecuter(IIncidentManagementBL bl, IAlertsBL alertbl)
        {
            _bl = bl;
            _alertbl = alertbl;
        }
        public override BaseRuleActionResponse ExeCuteRuleAction(baseRuleActionRequest request)
        {
            //baseRuleActionRequest requestData = request as ExecuteRuleActionRequest;
            
            BaseRuleActionResponse response = new BaseRuleActionResponse();
            CreateIncidentRequest CreateIncidentrequst = new CreateIncidentRequest();
            CreateIncidentrequst.ToolID = request.ToolID;
            CreateIncidentrequst.OrgID = request.OrgID;
            CreateIncidentrequst.ToolTypeID = request.ToolTypeID;
           // CreateIncidentrequst.IncidentdtlsList = new List<Incidentdtls>();

            var alertData = _alertbl.GetAlertData(new LDP_APIs.BL.APIRequests.GetAlertRequest()
                            { alertID = request.alertiD }).AlertsList;
            //Incidentdtls incidentdtls = new Incidentdtls();
            //incidentdtls.description = "TEst description";
            //incidentdtls.short_description = "Short description";
            //incidentdtls.incidentTitle = "Title";

            CreateIncidentrequst.AlertData = alertData.ToList();
            //
            response.Message = _bl.CreateIncident(CreateIncidentrequst);
            if (response.Message == "success")
            {
                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }
             return response;

        }
    
}
}