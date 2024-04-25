using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL
{
    public class AlertPlayBookProcessActionBL : IAlertPlayBookProcessActionBL
    {
        IAlertPlayBookProcessActionRepository _actionRepo;
        public readonly IMapper _mapper;

        public AlertPlayBookProcessActionBL(IAlertPlayBookProcessActionRepository actionRepo
            , IMapper mapper)
        {
            _actionRepo = actionRepo;
            _mapper = mapper;   

        }
        public AlertPlaybookProcessActionResponse AddAlertPlayBookProcessAction(DAL.Entities.AlertPlayBookProcessAction request)
        {
            throw new NotImplementedException();
        }

        public AlertPlaybookProcessActionResponse AddRangeAlertPlayBookProcessActions(List<DAL.Entities.AlertPlayBookProcessAction> request)
        {
            throw new NotImplementedException();
        }

        public GetAlertPlayBookProcessactionResponse GetAlertPlayBookProcessActionsByStatus(GetAlertPlayBookProcessActionsByStatusRequest request)
        {
            GetAlertPlayBookProcessactionResponse res = new GetAlertPlayBookProcessactionResponse();
            res.IsSuccess = true;
            res.Message = "success";
            var reporesponse = _actionRepo.GetAlertPlayBookProcessActionsByStatus(request);
            var _mappedResponse = _mapper.Map<List<AlertPlayBookProcessAction>, List<GetAlertPlayBookProcessActionModel>>(reporesponse.Result);
            res.AlertPlayBookProcessActionData = _mappedResponse;
            
            return res;
        }

        public double GetCountAlertPlayBookProcessActionsByStatus(GetAlertPlayBookProcessActionsByStatusRequest request)
        {
          double count = 0;
          var  reporesponse = _actionRepo.GetCountAlertPlayBookProcessActionsByStatus(request);
          count = reporesponse.Result;
          return count;
        }

        public string UpdateActionStatus(UpdateActionStatusRequest request)
        {
            var res = _actionRepo.UpdateActionStatus(request);
            return res.Result;
        }
    }
}
