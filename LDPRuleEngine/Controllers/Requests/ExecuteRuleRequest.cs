namespace LDPRuleEngine.Controllers.Requests
{
    public class ExecuteRuleRequest
    {
        public int RuleID { get; set; }
        public string? InputTextToRuleExecute { get; set; } 
    }
}
