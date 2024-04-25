using LDP_APIs.BL.Models;

namespace LDP_APIs.BL.APIRequests
{
    public class LDPToolRequest : LDPToolCommon
    {
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }

   

    public class UpdateLDPToolRequest : LDPToolmodel
    {
        public int ModifiedUserId { get; set; }
    }
    public class DeleteLDPToolRequest : DeleteLDPToolmodel
    {

    }

    public class GetLDPToolRequest 
    {
          public int ToolTypeId { get; set; }
    }
}
