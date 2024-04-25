namespace LDP.Common.Services.DrataIntegration
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class Drata_GetControlsResponse
    {
        public List<Datum>? data { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public object nextPage { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public object slug { get; set; }
        public object archivedAt { get; set; }
        public List<int> topics { get; set; }
        public bool isMonitored { get; set; }
        public bool hasEvidence { get; set; }
        public bool hasOwner { get; set; }
        public bool isReady { get; set; }
        public bool hasTicket { get; set; }
        public List<object> frameworkTags { get; set; }
    }

   


}
