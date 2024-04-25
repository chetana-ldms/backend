using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.Interfaces;

namespace LDPRuleEngine.BL.Framework.Actions
{
    public class TicketManagementRuleActionExecuter : baseRuleActionExecuter
    {
        IIncidentsBL _bl;
        IAlertsBL _alertbl;
        public TicketManagementRuleActionExecuter(IIncidentsBL bl, IAlertsBL alertbl)
        {
            _bl = bl;
            _alertbl = alertbl;
        }
        public override BaseRuleActionResponse ExeCuteRuleAction(baseRuleActionRequest request)
        {
            //baseRuleActionRequest requestData = request as ExecuteRuleActionRequest;
            
            BaseRuleActionResponse response = new BaseRuleActionResponse();

            CreateIncidentRequest CreateIncidentrequst = new CreateIncidentRequest()
            {
                 
               //ToolID = request.ToolID,
                OrgId = request.OrgID
                //ToolTypeID = request.ToolTypeID
            };
            // CreateIncidentrequst.IncidentdtlsList = new List<Incidentdtls>();

            var alertData = _alertbl.GetAlertData(new LDP_APIs.BL.APIRequests.GetAlertRequest()
                            { alertID = request.alertiD }).AlertsList;
            //Incidentdtls incidentdtls = new Incidentdtls();
            //incidentdtls.description = "TEst description";
            //incidentdtls.short_description = "Short description";
            //incidentdtls.incidentTitle = "Title";

            //CreateIncidentResponse.AlertData = alertData.ToList();
            //
           var  res = _bl.CreateIncident(CreateIncidentrequst);
            //if (response.Message == "success")
            //{
            //    response.IsSuccess = true;
            //}
            //else
            //{
            //    response.IsSuccess = false;
            //}
             return response;

        }
    
}
}