using LDPRuleEngine.BL.Framework.Actions;
using LDPRuleEngine.BL.Models;

namespace LDPRuleEngine.Controllers.Requests
{
    public class AddRuleActionRequest:AddRuleActionModel
    {
    }

    public class UpdateRuleActionRequest : UpdateRuleActionModel
    {
        public int Modifieduserid { get; set; }
    }

    public class ExecuteRuleActionRequest : baseRuleActionRequest
    {
       
    }

    public class DeleteRuleActionRequest : DeleteRuleActionModel
    {
    }

}
