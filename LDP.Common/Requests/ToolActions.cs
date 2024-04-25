using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class AddToolActionRequest: AddToolActionModel
    {
    }

    public class UpdateToolActionRequest : UpdateToolActionModel
    {
        public int ModifiedUserId { get; set; }
    }

    public class DeleteToolActionRequest : DeleteToolActionModel
    {

    }

    public class GetActionRequest 
    {
            public int ToolId { get; set; }
    }
}
