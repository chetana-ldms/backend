using LDP_APIs.BL.Models;

namespace LDP_APIs.BL.APIRequests
{
    public class LDPToolRequest : LDPToolCommon
    {
        public string? CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
    }

   

    public class UpdateLDPToolRequest : LDPToolmodel
    {
       
    }
    
}
