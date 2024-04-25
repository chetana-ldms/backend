using LDP_APIs.BL.Models;

namespace LDP_APIs.BL.APIRequests
{
    public class AddOrganizationRequest: OrganizationModel
    {
        public string? CreadtedByUserName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class UpdateOrganizationRequest : UpdateOrganizationModel
    {

    }

   
}
