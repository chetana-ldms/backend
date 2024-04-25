namespace LDP_APIs.Models
{
    public class GetOffenseDTO
    {
        public GetOffenseRequest? clientRequest { get; set; }
        public string? AuthKey { get; set; }

        public string? APIUrl { get; set; }
        public int ToolID { get; set; }

        public double LastReadID { get; set; }

        public int alertID { get; set; }

        public double alert_MaxPKID { get; set; }

        public int GetDataBatchSize { get; set; }
        public string? APIVersion { get; set; }
    }
}
