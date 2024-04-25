using LDP_APIs.Models;

namespace LDP.Common.Services.SentinalOneIntegration.Activities
{
    public class ActivityTypes:baseResponse
    {
        public List<ActivityType>? data { get; set; }
    }

    public class ActivityType
    {
        public string? Action { get; set; }
        public string? DescriptionTemplate { get; set; }
        public int Id { get; set; }

    }
}
