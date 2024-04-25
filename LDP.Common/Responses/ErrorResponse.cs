using LDP_APIs.Models;

namespace LDP_APIs.BL.APIResponse
{
    public class ErrorResponse:baseResponse
    {
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorDetail { get; set; }
    }
}
