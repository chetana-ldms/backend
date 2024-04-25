using LDP.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDP.Common.Requests
{
    public class AlertHistoryRequest:AlertHistoryModel
    {
    }

    public class GetAlertHistoryRequest
    {
        public int OrgId { get; set; }
        public int AlertId { get; set; }

    }

    public class GetIncidentHistoryRequest
    {
        public int OrgId { get; set; }
        public int IncidentId { get; set; }

    }
}
