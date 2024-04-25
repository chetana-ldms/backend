using LDP_APIs.Models;
using System.Text;

namespace LDPRuleEngine.Controllers.Responses
{
    public class ExecuteRuleResponse: baseResponse
    {
        public string?  RuleExecuterMessage { get; set; }

    }
}
