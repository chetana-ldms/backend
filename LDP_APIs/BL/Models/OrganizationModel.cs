namespace LDP_APIs.BL.Models
{
    public class OrganizationModel
    {
        public string? OrgName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
       
    }

    public class UpdateOrganizationModel: OrganizationModel
    {
        public int OrgID { get; set; }
        public string? UpdatedByUserName { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }

    public class GettOrganizationsModel : UpdateOrganizationModel
    {
        public string? CreadtedByUserName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
   

}
