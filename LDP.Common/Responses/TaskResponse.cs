using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class TaskResponse : baseResponse
    {
    }

    public class GetTasksResponse : baseResponse
    {
        public List<GetTasksModel>? TaskList { get; set; }
    }
}
