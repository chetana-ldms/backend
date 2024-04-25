using LDP.Common.Model;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IAPIUrlBL
    {
        GetLDCAPIUrlResponse GetLDCUrls(int orgId);

        GetLDCAPIUrlResponse GetLDCUrlsByGroup(string groupName);

        GetLDCAPIUrlResponse GetLDCUrlsByGroupAndAction(string groupName, string actionName);

        string GetUrl(List<LDCUrlModel> Urls, string actionname);

    }
}
