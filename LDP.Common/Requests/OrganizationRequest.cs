using LDP_APIs.BL.Models;

namespace LDP_APIs.BL.APIRequests
{
    public class AddOrganizationRequest: OrganizationModel
    {
        public int CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class UpdateOrganizationRequest : UpdateOrganizationModel
    {
        public int UpdatedUserId { get; set; }
    }
    public class DeleteOrganizationRequest : DeleteOrganizationModel
    {

    }
    
}
