namespace LDP.Common.Requests
{
    public class SendTeamsMessageRequest
    {
        public string  Message { get; set; }
        public string Title { get; set; } = "LSP";
        public string ThemeColor { get; set; } = "0072C6";

    }

    public class SendTeamsMessageConfiguration
    {
        public string HookUrl { get; set; }
        public string Title { get; set; } = "LSP";
        public string ThemeColor { get; set; } = "0072C6";

    }
}
