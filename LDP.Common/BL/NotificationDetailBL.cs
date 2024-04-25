using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP.Common.Services.Notifications.Teams;

namespace LDP.Common.BL
{
    public class NotificationDetailBL : INotificationDetailBL
    {
        INotificationDetailsRepository? _repo;
        IMapper _mapper;
        ITeamsMessageBL _teamsMessageBL;
        public NotificationDetailBL(INotificationDetailsRepository repo, IMapper mapper, ITeamsMessageBL teamsMessageBL)
        {
            _repo = repo;
            _mapper = mapper;
            _teamsMessageBL = teamsMessageBL;
        }

        public NoficationDetailResponse AddNotificationDetails(NoficationDetailRequest request)
        {
            NoficationDetailResponse response = new NoficationDetailResponse();

            var _mappedRequest = _mapper.Map<NotificationDetailModel, NotificaionDetail>(request);

            var _repositoryres = _repo.AddNotificationDetails(_mappedRequest);
           
            if (_repositoryres.Result == 1)
            {
                response.IsSuccess = true;
                response.Message = "Notificatin details data added";
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Failed to add Notificatin details data";
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
            }

            return response;
        }
        public GetNoficationDetailResponse GetNotificationDetails(GetNoficationDetailRequest request)
        {
            GetNoficationDetailResponse response = new GetNoficationDetailResponse();

            var _repositoryres = _repo.GetNotificationDetails(request).Result;

            if (_repositoryres == null )
            {
                var _mappedModelData = _mapper.Map<List<NotificaionDetail>, List<NotificationDetailModel>>(_repositoryres);
                response.IsSuccess = true;
                response.Message = "Success";
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                response.Data = _mappedModelData;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "No records found";
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return response;
        }

        public SendTeamsMessageResponse SendTeamsMessage(SendTeamsMessageRequest request)
        {
           return _teamsMessageBL.SendTeamsMessage(request);
        }

        public NoficationDetailResponse UpdateNotificationDetails(NoficationDetailRequest request)
        {
            //
            NoficationDetailResponse response = new NoficationDetailResponse();
            var _mappedRequest = _mapper.Map<NotificationDetailModel, NotificaionDetail>(request);
            var _repositoryres = _repo.UpdateNotificationDetails(_mappedRequest);
            if (_repositoryres.Result == 1)
            {
                response.IsSuccess = true;
                response.Message = "Notificatin details data updated";
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to update notificatin details data";
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
            }
            return response;
            //
        }
    }
}
