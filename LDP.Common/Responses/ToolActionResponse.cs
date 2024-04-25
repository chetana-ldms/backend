using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class GetToolActionResponse: baseResponse 
    {
        public List<GetToolActionModel> ToolAcationsList { get; set; }

    }

    public class ToolActionResponse : baseResponse
    {

    }
    public class DeleteToolActionResponse : baseResponse
    {

    }

    public class GetToolActionSingleResponse : baseResponse
    {
        public GetToolActionModel? ToolAcation { get; set; }

    }
}
