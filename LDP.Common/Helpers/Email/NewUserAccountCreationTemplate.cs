using LDP.Common.Helpers.Interfaces;
using LDP.Common.Responses;

namespace LDP.Common.Helpers.Email
{
    public class NewAccountCreationTemplate : IEmailTemplate
    {
        public string BuildEmailTemplate(Dictionary<string, string> data ,  ConfigurationDataResponse configData)
        {

            string templateFileName = "NewUserAccountTemplate.html";
            string templateDirectory = "EmailTemplates";
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templateDirectory, templateFileName);

            var template = RenderTemplateAsync(templatePath, data , configData).Result;

            return template;
        }

        public async Task<string> RenderTemplateAsync(string templatePath, Dictionary<string, string> data , ConfigurationDataResponse configData)
        {
            string templateContent = await File.ReadAllTextAsync(templatePath);

            string _name = string.Empty;

            data.TryGetValue("Name", out _name);

            string _newPassword = string.Empty;

            data.TryGetValue("NewPassword", out _name);

           // string _singatureCompanyName = string.Empty;

            var _singatureCompanyName = configData.Data.Where(c => c.DataName == "PasswordResetSignatureName").FirstOrDefault().DataValue;


            // Replace placeholders with actual values
            templateContent = templateContent.Replace("[Name]", _name)
                                             .Replace("[NewPassword]", _newPassword)
                                             .Replace("[CompanyName]", _singatureCompanyName);

            return templateContent;
        }

    }
}
