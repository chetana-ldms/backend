using LDP.Common.Requests.Common;
using LDP.Common.Responses;
using System.Collections.Generic;

namespace LDP.Common.BL.Interfaces
{
    public interface ICommonBL
    {
        ActivityTyperesponse GetActivityTypeByName(string name);

        ActivityTypeListresponse GetActivityTypeByTypeNameList(List<string> names);
        ActivityTypesresponse GetActivityTypes();

        ActivityResponse AddActivity(AddActivityRequest request , string userName);

        ActivityResponse AddRangeActivity(List<AddActivityRequest> request);

        ActivityResponse LogActivity(AddActivityRequest request, Dictionary<string , string> templateData, string activityType , bool IsSuccess = true);

        ActivityResponse LogActivity(List<AddActivityRequest> request);

        GetActivitiesResponse GetActiviites(GetActivitiesRequest request);

       



    }
}
