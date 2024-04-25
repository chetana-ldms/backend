using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL;
using LDP_APIs.BL.Interfaces;
using LDPRuleEngine.BL.Framework.Actions;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.DAL.Entities;
using LDPRuleEngine.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LDP.Common.BL
{
    public class AlertHistoryBL : IAlertHistoryBL
    {
        IAlertHistoryRepository _repo;
        IMapper _mapper;

        public AlertHistoryBL(IAlertHistoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public AlertHistoryResponse AddalertHistory(AlertHistoryRequest request)
        {
            AlertHistoryResponse response = new AlertHistoryResponse();
            
            var _mappedRequest = _mapper.Map<AlertHistoryRequest, AlertHistory>(request);
            var res = _repo.AddalertHistory(_mappedRequest);
            
            if (res.Result != "")
            {
                response.IsSuccess = true;
                response.Message = "Alert history data added";
                response.HttpStatusCode=HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add alert history data";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        public AlertHistoryResponse AddRangealertHistory(List<AlertHistoryModel> request)
        {
            AlertHistoryResponse response = new AlertHistoryResponse();

            var _mappedRequest = _mapper.Map<List<AlertHistoryModel>, List<AlertHistory> >(request);
            var res = _repo.AddRangealertHistory(_mappedRequest);

            if (res.Result != "")
            {
                response.IsSuccess = true;
                response.Message = "Alert history data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add alert history data";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }
        public GetAlertHistoryResponse GetalertHistory(GetAlertHistoryRequest request)
        {
            GetAlertHistoryResponse response = new GetAlertHistoryResponse();
            var res = _repo.GetalertHistory(request);
            
            if (res == null)
            {
                response.IsSuccess = false;
                response.Message = "Alert history data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<AlertHistory>, List<AlertHistoryModel>>(res.Result);

                response.AlertHistoryData = _mappedResponse;
            }
                
            
            return response;
        }

        public GetAlertHistoryResponse GetIncidentHistory(GetIncidentHistoryRequest request)
        {
            GetAlertHistoryResponse response = new GetAlertHistoryResponse();
            var res = _repo.GetIncidentHistory(request);

            if (res == null)
            {
                response.IsSuccess = false;
                response.Message = "Incident history data not found";
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = HttpStatusCode.OK;
                var _mappedResponse = _mapper.Map<List<AlertHistory>, List<AlertHistoryModel>>(res.Result);

                response.AlertHistoryData = _mappedResponse;
            }
            return response;
        }
    }
}
