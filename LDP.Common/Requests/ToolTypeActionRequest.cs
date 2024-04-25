using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class AddToolTypeActionRequest:AddToolTypeActionModel
    {
    }
    public class UpdateToolTypeActionRequest : UpdateToolTypeActionModel
    {
    }

    public class DeleteToolTypeActionRequest : DeleteToolTypeActionModel
    {
    }

    public class GetToolTypeActinByToolTypeRequest
    {
        public int ToolTypeId { get; set;}
    }
}
