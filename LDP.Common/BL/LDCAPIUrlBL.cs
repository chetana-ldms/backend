using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Responses;
using System.Net;

namespace LDP.Common.BL
{
    public class LDCAPIUrlBL : IAPIUrlBL
    {
        ILDCAPIUrlRepository _repo;
        public readonly IMapper _mapper;
        public LDCAPIUrlBL(ILDCAPIUrlRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public GetLDCAPIUrlResponse GetLDCUrls(int orgId)
        {
            GetLDCAPIUrlResponse returnobj = new GetLDCAPIUrlResponse();

            var alertsdata = _repo.GetLDCUrls(orgId).Result;
            if (alertsdata.Count > 0)
            {
                var _mappedResponse = _mapper.Map<List<LDCApiUrls>, List<LDCUrlModel>>(alertsdata);

                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.UrlList = _mappedResponse;
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "No data found";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return returnobj;
        }

        public GetLDCAPIUrlResponse GetLDCUrlsByGroup(string groupName)
        {
            GetLDCAPIUrlResponse returnobj = new GetLDCAPIUrlResponse();

            var alertsdata = _repo.GetLDCUrlsByGroup(groupName).Result;
            if (alertsdata.Count > 0)
            {
                var _mappedResponse = _mapper.Map<List<LDCApiUrls>, List<LDCUrlModel>>(alertsdata);

                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.UrlList = _mappedResponse;
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "No data found";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return returnobj;

        }

        public GetLDCAPIUrlResponse GetLDCUrlsByGroupAndAction(string groupName, string actionName)
        {
            GetLDCAPIUrlResponse returnobj = new GetLDCAPIUrlResponse();

            var alertsdata = _repo.GetLDCUrlsByGroupAndAction(groupName, actionName).Result;
            if (alertsdata.Count > 0)
            {
                var _mappedResponse = _mapper.Map<List<LDCApiUrls>, List<LDCUrlModel>>(alertsdata);

                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.UrlList = _mappedResponse;
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "No data found";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return returnobj;
        }

        public string GetUrl(List<LDCUrlModel> Urls, string actionname)
        {
            string Url = string.Empty;

            var response = Urls.Where(u => u.ActionName == actionname).FirstOrDefault();

            if (response != null)
            {
                Url = response.ActionUrl;
            }

            return Url;
        }

    }
}
