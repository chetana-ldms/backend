using AutoMapper;
using LDP.Common.BL.Interfaces;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using LDP.Common.Responses;
using System.Net;

namespace LDP.Common.BL
{

    public class ApplicationLogBL : IApplicationLogBL
    {
        IApplicationlogsRepository _repo;
        IMapper _mapper;
        public ApplicationLogBL(IApplicationlogsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ApplicationLogResponse AddLog(ApplicationLogRequest request)
        {
            ApplicationLogResponse response = new ApplicationLogResponse();

            var _mappedRequest = _mapper.Map<ApplicationLogRequest, ApplicationLog>(request);
            _mappedRequest.org_id = request.OrgId;
            _mappedRequest.log_date = DateTime.UtcNow;
            var res = _repo.AddApplicatinLog(_mappedRequest);
            //
            if (res.Result == "")
            {
                response.IsSuccess = true;
                response.Message = "application log data added";
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Failed to add application log data";
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            };
            return response;
        }
        public void AddLogInforation(string message, string logsource = null, int orgid = 0)
        {

            ApplicationLogRequest request = new ApplicationLogRequest()
            {
                LogSource = logsource,
                Message = message,
                Severity = Constants.Logger_severity_Information,
                OrgId = orgid
            };
            this.AddLog(request);

            
        }

       
        public void AddLogError(string message, string logsource, int orgid)
        {

            ApplicationLogRequest request = new ApplicationLogRequest()
            {
                LogSource = logsource,
                Message = message,
                Severity = Constants.Logger_severity_Error,
                OrgId = orgid
            };
            this.AddLog(request);

        }
    }
}
