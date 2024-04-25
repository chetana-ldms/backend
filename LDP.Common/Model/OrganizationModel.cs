namespace LDP_APIs.BL.Models
{
    public class OrganizationModel
    {
        public string? OrgName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }

        //public int ManageInternalIncidents { get; set;}
       
    }

    public class UpdateOrganizationModel: OrganizationModel
    {
        public int OrgID { get; set; }
       
        public DateTime? UpdatedDate { get; set; }

    }

    public class GettOrganizationsModel : UpdateOrganizationModel
    {
        public string? CreadtedByUserName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
        public string? DeletedUser { get; set; }
               

         public string? UpdatedByUserName { get; set; }
    }

    public class DeleteOrganizationModel 
    {
        public int OrgID { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedUserId { get; set; }

    }
}
