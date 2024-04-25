namespace LDP_APIs.BL.Models
{
    public class CommonModel
    {
        public string? CreatedByUserName { get; set; }

        public DateTime CreatedDete { get; set; }
    }

    public class SelectCommon
    {
        public string? UpdatedByUserName { get; set; }

        public DateTime UpdatedDete { get; set; }
    }
}
