using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class GetToolTypeActionResponse: baseResponse
    {
        public List<GetToolTypeActionModel> ToolTypeActionsList { get; set; }
    }

    public class ToolTypeActionResponse : baseResponse
    {

    }

    public class DeleteToolTypeActionResponse : baseResponse
    {

    }

    public class GetToolTypeActionSingleResponse : baseResponse
    {
        public GetToolTypeActionModel? ToolTypeAction { get; set; }
    }
}
