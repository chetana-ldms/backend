namespace LDP.Common.Helpers.Email
{
    public class EmailPrepRequest
    {
        public int UseId { get; set; }
        public Constants.Email_types EmailType { get; set; }

        public string? EmailSubject { get; set; }
        public Dictionary<string, string>?  Data { get; set; }
    }
}
