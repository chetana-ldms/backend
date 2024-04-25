using LDP_APIs.Models;
using LDPRuleEngine.BL.Models;

namespace LDPRuleEngine.Controllers.Requests
{
    public class AddRuleRequest: AddLDPRuleModel
    {
       
    }

    public class UpdateRuleRequest : UPdateLDPRuleModel
    {
        public int ModifiedUserId { get; set; }
       
    }

    public class DeleteRuleRequest : DeleteLDPRuleModel
    {

    }
}
