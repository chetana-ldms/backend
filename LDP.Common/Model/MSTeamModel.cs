namespace LDP.Common.Model
{
    public class MsTeamModel
    {
        public int TeamId { get; set; }
        public int OrgId { get; set; }
        public string? TeamName { get; set; }
        public string? MsTeamId { get; set; }
        public int DefaultTeam { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
    }

}
