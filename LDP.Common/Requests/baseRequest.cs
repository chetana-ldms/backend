namespace LDP_APIs.Models
{
    public class baseRequest
    {
        public int OrgID { get; set; }
 
        public int ToolID { get; set; } 

        public int ToolTypeID { get; set; } 

        public List<AccountStructureLevel>? OrgAccountStructureLevel { get; set; }


    }
    public class AccountStructureLevel
    {
        public string? LevelName { get; set; } // Account , Site , Group
        public string? LevelValue { get; set; } // AccountId , SiteId , GroupId
    }
}
