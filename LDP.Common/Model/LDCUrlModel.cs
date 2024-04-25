namespace LDP.Common.Model
{
    public class LDCUrlModel
    {
        public int UrlId { get; set; }
        public int OrgId { get; set; }
        public string? UrlGroup { get; set; }
        public string? ActionName { get; set; }
        public string? ActionUrl { get; set; }
        public string? AuthToken { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
    }
}
