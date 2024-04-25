using LDP_APIs.Models;

namespace LDP_APIs.BL.APIRequests
{
    public class AssignOwnerRequest
    {
        public int alertID { get; set; }
        public int UserID { get; set; }
        public string? UpdatedByUserName { get; set; }
        public DateTime UpdatedDete { get; set; }
    }

    public class GetAlertRequest
    {
        public int alertID { get; set; }
  
    }

    public class GetAlertByAssignedOwnerRequest: GetOffenseRequest
    {
        public int UserID { get; set; }
        public List<int>? StatusIDs { get; set; }

   }
}
