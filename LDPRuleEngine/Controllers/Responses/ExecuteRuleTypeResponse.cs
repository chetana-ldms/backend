using LDP_APIs.Models;
using System.Text;

namespace LDPRuleEngine.Controllers.Responses
{
    public class ExecuteRuleConditionTypeResponse:baseResponse 
    {
        public StringBuilder?  RuleConditionExecutionMessages { get; set; }
    }
}
