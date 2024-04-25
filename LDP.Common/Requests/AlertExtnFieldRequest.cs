using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class AlertExtnFieldRequest: AlertExtnFieldModel
    {
    }
    public class GetAlertExtnFieldRequest
    {
        public int OrgId { get; set; }
    }
}
