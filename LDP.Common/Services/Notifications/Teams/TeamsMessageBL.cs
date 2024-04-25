using LDP.Common.BL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.Services.Notifications.Teams
{
    public class TeamsMessageBL : ITeamsMessageBL
    {
        ITeamsMessageService _teamsService;
        IConfigurationDataBL _configBL;

        public TeamsMessageBL(ITeamsMessageService teamsService, IConfigurationDataBL configBL)
        {
            _teamsService = teamsService;
            _configBL = configBL;
        }

        public SendTeamsMessageResponse SendTeamsMessage(SendTeamsMessageRequest request)
        {
            var _configDataFromDB = _configBL.GetConfigurationData("SendTeamsMessageToChannel").Data;

            string strUrl1 = _configDataFromDB.Where(c => c.DataName == "WebHookURL1").FirstOrDefault().DataValue;
            string strUrl2 = _configDataFromDB.Where(c => c.DataName == "WebHookURL2").FirstOrDefault().DataValue;
            string strUrl3 = _configDataFromDB.Where(c => c.DataName == "WebHookURL3").FirstOrDefault().DataValue;
            string strTitle = _configDataFromDB.Where(c => c.DataName == "Title").FirstOrDefault().DataValue;
            string strTheme = _configDataFromDB.Where(c => c.DataName == "Themes").FirstOrDefault().DataValue;

            SendTeamsMessageConfiguration _configData = new SendTeamsMessageConfiguration();
            _configData.HookUrl = strUrl1 + strUrl2 + strUrl3;
            _configData.Title = strTitle;
            _configData.ThemeColor = strTheme;
            return _teamsService.SendTeamsMessage(request, _configData).Result;
        }
    }
}
