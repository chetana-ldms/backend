using System.Net;

namespace LDP_APIs.Models
{
    public class baseResponse
    {
        public bool IsSuccess { get; set;}
        public string? Message { get; set;}

        public HttpStatusCode? HttpStatusCode { get; set;}
        

    }
}
