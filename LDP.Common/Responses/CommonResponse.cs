using LDP.Common.Model.Common;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class ActivityTyperesponse:baseResponse
    {
        public  ActivityTypeModel? ActivityType { get; set; }
    }
    public class ActivityTypeListresponse : baseResponse
    {
        public List<ActivityTypeModel>? ActivityTypeList { get; set; }
    }
    public class ActivityTypesresponse : baseResponse
    {
        public List<ActivityTypeModel>? ActivityTypes { get; set; }
    }

    public class ActivityResponse : baseResponse
    {
       
    }

    public class GetActivitiesResponse : baseResponse
    {
        public double TotalActivitiesCount { get; set; }
        public List<GetActivityModel>? ActivitiesList { get; set; }
    }
}
