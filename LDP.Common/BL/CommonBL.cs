using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model.Common;
using LDP.Common.Requests.Common;
using LDP.Common.Responses;
using System.Net;

namespace LDP.Common.BL
{
    public class CommonBL : ICommonBL
    {
        ICommonRepository _repo;
        public readonly IMapper _mapper;
        public CommonBL(ICommonRepository repo, IMapper mapper
            )
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ActivityTyperesponse GetActivityTypeByName(string name)
        {
            ActivityTyperesponse response = new ActivityTyperesponse();
            var res = _repo.GetActivityTypeByTypeName(name);

            if (res.Result == null)
            {
                response.IsSuccess = false;
                response.Message = "Activity type not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<ActivityType, ActivityTypeModel>(res.Result);
                response.ActivityType = _mappedResponse;
                response.Message = "Success";
            }
            return response;
        }
        public ActivityTypeListresponse GetActivityTypeByTypeNameList(List<string> names)
        {
            ActivityTypeListresponse response = new ActivityTypeListresponse();
            var res = _repo.GetActivityTypeByTypeNames(names);

            if (res.Result.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Activity type list not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<ActivityType>, List<ActivityTypeModel>>(res.Result);
                response.ActivityTypeList = _mappedResponse;
                response.Message = "Success";
            }
            return response;
        }
        public ActivityResponse AddActivity(AddActivityRequest request, string userName)
        {
            ActivityResponse response = new ActivityResponse();
                     
            //
            var _mappedRequest = _mapper.Map<AddActivityRequest, LdcActivity>(request);
            if (request.CreatedUserId > 0 )
            {
                _mappedRequest.created_user = userName;
            }
            
          
            var res = _repo.AddActivity(_mappedRequest);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "New activity data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }

            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new activity data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;

        }
        public ActivityResponse AddRangeActivity(List<AddActivityRequest> request)
        {
            ActivityResponse response = new ActivityResponse();

            //
            var _mappedRequest = _mapper.Map<List<AddActivityRequest>, List<LdcActivity>>(request);


            _mappedRequest.ForEach(a => a.created_user = Constants.User_Background_User);
            
            var res = _repo.AddActivityRange(_mappedRequest);

            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "New activity data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }

            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add new activity data";
                response.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
            }
            return response;
        }
        public ActivityResponse LogActivity(AddActivityRequest request, Dictionary<string, string> templateData, string activityType, bool IsSuccess = true)
        {
            ActivityResponse response = new ActivityResponse();

            var _activityType = GetActivityTypeByName(activityType);
            if (_activityType.IsSuccess == false)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = "Invalid activity name";
                return response;
            }
            string _templateString = _activityType.ActivityType.Template;
            string _userName = string.Empty;
            templateData.TryGetValue(Constants.Acvities_Template_username, out _userName);
            foreach ( var key in templateData.Keys ) 
            {
                var _templatevalue = templateData[key];
                _templateString = _templateString.Replace("{"+key+"}" , _templatevalue);

            }

            if (!IsSuccess)
            {
                _templateString = "Failed : " + _templateString;
            }
            request.ActivityTypeId = _activityType.ActivityType.ActivityTypeId;
            request.PrimaryDescription = _templateString;
            request.CreatedDate ??= DateTime.Now;
            request.ActivityDate ??= DateTime.Now;

            AddActivity(request, _userName);
            return  response;

        }
        public ActivityResponse LogActivity(List<AddActivityRequest> request)
        {
            ActivityResponse response = new ActivityResponse();
            var names = request.Select(a => a.ActivityType).ToList<string>();
            var _activityTypeListRes = _repo.GetActivityTypeByTypeNames(names).Result;
           // var _activityType = GetActivityTypeByName(activityType);
            if (_activityTypeListRes.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Activity type list not found";
                return response;
            }
            List<LdcActivity> activityList = new List<LdcActivity>();

            foreach(var actRequest in  request) 
            {
                var _activityType = _activityTypeListRes.Where(t => t.type_name == actRequest.ActivityType).FirstOrDefault();
                string _templateString = _activityType.template;
                string _userName = string.Empty;
                actRequest.TemplateData.TryGetValue(Constants.Acvities_Template_username, out _userName);
                foreach (var key in actRequest.TemplateData.Keys)
                {
                    var _templatevalue = actRequest.TemplateData[key];
                    _templateString = _templateString.Replace("{" + key + "}", _templatevalue);

                }

                if (!actRequest.IsSuccess)
                {
                    _templateString = "Failed : " + _templateString;
                }

                if (!string.IsNullOrEmpty(actRequest.AdditionalText))
                {
                    _templateString = _templateString + actRequest.AdditionalText;
                }
                actRequest.ActivityTypeId = _activityType.activity_type_id;
                actRequest.PrimaryDescription = _templateString;
                actRequest.CreatedDate ??= DateTime.Now;
                actRequest.ActivityDate ??= DateTime.Now;

                // AddActivity(request, _userName);
                var _mappedRequest = _mapper.Map<AddActivityRequest, LdcActivity>(actRequest);
                if (actRequest.CreatedUserId > 0)
                {
                    _mappedRequest.created_user = _userName;
                }

                activityList.Add(_mappedRequest);
                //var res = _repo.AddActivity(_mappedRequest);
            }
            var res = _repo.AddActivityRange(activityList).Result;
            if (string.IsNullOrEmpty(res))
            {
                response.IsSuccess = true;
                response.Message = "Success";

            }
            else
            {
                response.IsSuccess = false;
                response.Message = res;
            }
            return response;

        }
        public ActivityTypesresponse GetActivityTypes()
        {
            ActivityTypesresponse response = new ActivityTypesresponse();
            var res = _repo.GetActivityTypes().Result;

            if (res.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Activitypes type not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<ActivityType>, List<ActivityTypeModel>>(res);
                response.ActivityTypes = _mappedResponse;
                response.Message = "Success";
            }
            return response;

        }

        public GetActivitiesResponse GetActiviites(GetActivitiesRequest request)
        {
            GetActivitiesResponse response = new GetActivitiesResponse();
           
            (List<LdcActivity>   activitiesData , double recordcount) = _repo.GetActivities(request);
            if (activitiesData.Count > 0)
            {
                var _mappedResponse = _mapper.Map<List<LdcActivity>, List<GetActivityModel>>(activitiesData);

                response.IsSuccess = true;
                response.Message = "Success";
                response.ActivitiesList = _mappedResponse;
                response.TotalActivitiesCount = recordcount;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "No data found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.TotalActivitiesCount = 0;
            }

            return response;
        }

    }

    
}
