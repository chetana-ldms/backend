namespace LDPRuleEngine.Controllers.Requests
{
    public class ExecuteRuleConditionTypeRequest
    {
        public dynamic? InputDataToValidate { get; set; }
        public dynamic? DataToPerformValidationOnInputData { get; set; }
    }
}
