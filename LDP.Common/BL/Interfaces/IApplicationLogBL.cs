using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IApplicationLogBL
    {
        ApplicationLogResponse AddLog(ApplicationLogRequest request);
        void AddLogInforation(string message, string logsource = null, int orgid = 0);
        void AddLogError(string message, string logsource = null, int orgid = 0);

    }
}
