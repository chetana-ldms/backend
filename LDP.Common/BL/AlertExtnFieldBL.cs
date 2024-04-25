using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;
using System.Net;

namespace LDP.Common.BL
{
    public class AlertExtnFieldBL : IAlertExtnFieldBL
    {
        IAlertExtnFieldRepository _repo;
        IMapper _mapper;

        public AlertExtnFieldBL(IAlertExtnFieldRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public AddAlertExtnFieldResponse AddAlertExtnFields(AlertExtnFieldModel request)
        {
            AddAlertExtnFieldResponse response = new AddAlertExtnFieldResponse();
            var _mappedData = _mapper.Map< AlertExtnFieldModel, AlertExtnField>(request);

            var res = _repo.AddAlertExtnFields(_mappedData);
            if (res.Result == 0)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Failed to add Alert data";
            }
            else
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Success";
               
            }
            return response;
        }

        public AddAlertExtnFieldResponse AddRangeAlertExtnFields(List<AlertExtnFieldModel> request)
        {
            AddAlertExtnFieldResponse response = new AddAlertExtnFieldResponse();

            var _mappedData = _mapper.Map< List<AlertExtnFieldModel>, List<AlertExtnField>>(request);
            var res = _repo.AddRangeAlertExtnFields(_mappedData);
            if (res.Result == 0)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Failed to add Alert data";
            }
            else
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Success";

            }
            return response;
        }

        public GetAlertExtnFieldResponse GetAlertExtnFields(GetAlertExtnFieldRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
