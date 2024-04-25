using LDPRuleEngine.BL.Interfaces;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using System.Text;

namespace LDPRuleEngine.BL.Framework
{
    public class TextContainsRuleTypeExecuter : IRuleConditionTypeExecuter
    {
        public ExecuteRuleConditionTypeResponse ExecuteRuleConditionType(ExecuteRuleConditionTypeRequest? request)
        {
            StringBuilder validationMessage = new StringBuilder();
            bool ValidationResult = true;
            ExecuteRuleConditionTypeResponse response = new ExecuteRuleConditionTypeResponse();
            string? InputData = (request.InputDataToValidate as string).ToLower();
           // List<string>? DataToValidate = request.DataToPerformValidationOnInputData as List<string>;
            validationMessage.AppendLine(string.Format("Rule condition exection started "));
            foreach (var data in request.DataToPerformValidationOnInputData)
            {
                if(data == null)
                {
                    ValidationResult = false;
                    validationMessage.AppendLine();
                    validationMessage.Append("Data missing to find the matches ");
                }
                else
                {
                   string? data1 = (data.RulesConditionValue as string).ToLower();
                    
                   if ( InputData.Contains(data1))
                    {
                        if (ValidationResult) ValidationResult = true;
                        validationMessage.AppendLine();
                        validationMessage.AppendFormat(string.Format("{0} keyword found in the input string ", data1));
                    }
                   else
                    {
                        ValidationResult = false;
                        validationMessage.AppendLine();
                        validationMessage.AppendFormat(string.Format("{0} keyword not found in the input string ", data1));
                    }
                }
            }
            validationMessage.AppendLine();
            validationMessage.AppendLine(string.Format("Rule condition exection completed "));
            response.IsSuccess = ValidationResult;
            response.RuleConditionExecutionMessages = validationMessage;
            return response;
        }
    }
}
