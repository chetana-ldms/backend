using AutoMapper;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;
using LDP_APIs.DAL.Interfaces;
using LDP_APIs.Models;
using System.Net;

namespace LDP_APIs.BL
{
    public class AlertsBL : IAlertsBL
    {
        IAlertsRepository  _repo;
        public readonly IMapper _mapper;

        public AlertsBL(IAlertsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }
        public getAlertsResponse GetAlerts(GetOffenseRequest request)
        {
            try
            {
                var alertsdata = _repo.Getalerts(request);
                var _mappedResponse = _mapper.Map<List<Alerts>, List<AlertModel>>(alertsdata);
                getAlertsResponse returnobj = new getAlertsResponse();
                returnobj.IsSuccess = true;
                returnobj.Message = "Success";
                returnobj.AlertsList = _mappedResponse;
                returnobj.TotalOffenseCount = _repo.GetAlertsDataCount();
                return returnobj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public baseResponse AssignOwner(AssignOwnerRequest request)
        {
            baseResponse returnobj = new baseResponse();

            var repoResponse = _repo.AssignOwner(request);

            if (string.IsNullOrEmpty(repoResponse.Result))
            {
                returnobj.IsSuccess =true;  
                returnobj.Message = "Success";
                returnobj.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnobj.IsSuccess = false;
                returnobj.Message = "Failed";
                returnobj.HttpStatusCode = HttpStatusCode.NotFound;
                //returnobj.ErrorMessage = repoResponse.Result;
            }

            return returnobj;
        }

        public getAlertResponse GetAlertData(GetAlertRequest request)
        {
            var alertsdata = _repo.GetAlertData(request);
            var _mappedResponse = _mapper.Map<List<Alerts>, List<AlertModel>>(alertsdata.Result.ToList());
            getAlertResponse returnobj = new getAlertResponse();
            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.AlertsList = _mappedResponse;
            //returnobj.TotalOffenseCount = _repo.GetAlertsDataCount();
            return returnobj;

        }

        public getAlertsResponse GetAlertsByAssignedUser(GetAlertByAssignedOwnerRequest request)
        {
            var alertsdata = _repo.GetAlertsByAssignedUser(request);
            var _mappedResponse = _mapper.Map<IEnumerable<Alerts>, List<AlertModel>>(alertsdata.Result);
            getAlertsResponse returnobj = new getAlertsResponse();
            returnobj.IsSuccess = true;
            returnobj.Message = "Success";
            returnobj.AlertsList = _mappedResponse;
            returnobj.TotalOffenseCount = _repo.GetAlertsCountByAssignedUser(request).Result;
            return returnobj;
        }
    }
}
