using LDP.Common.Model;
using LDP_APIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDP.Common.Responses
{
    public class UserActionResponse:baseResponse
    {
    }

    public class GetUserActionResponse : baseResponse
    {
        public UserActionModel? UserActionData { get; set; }
    }


    public class GetUserActionsResponse : baseResponse
    {
        public List<UserActionModel>? UserActionsData { get; set; }
    }

}
