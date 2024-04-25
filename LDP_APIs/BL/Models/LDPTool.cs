using LDP_APIs.BL.APIRequests;

namespace LDP_APIs.BL.Models
{
    public class LDPToolCommon
    {
        public string? ToolName { get; set; }
        public string? ToolType { get; set; }
    }
    public class LDPToolmodel : LDPToolCommon
    {
        public int ToolID { get; set; }
        public string? UpdatedByUser { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
    public class GetLDPTool: LDPToolmodel
    {
        public string? CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
