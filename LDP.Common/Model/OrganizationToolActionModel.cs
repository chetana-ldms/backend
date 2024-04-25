namespace LDP.Common.Model
{
    public class OrganizationToolActionModel
    {
        public int OrgToolActionId { get; set; }
        public int OrgToolId { get; set; }
        public int ToolActionId { get; set; }
        public int ToolId { get; set; }
        public int OrgId { get; set; }
        public string? AuthKey { get; set; }
        public string? ApiUrl { get; set; }
        public int? Active { get; set; }
        public string? ApiVerson { get; set; }
        public int? GetDataBatchSize { get; set; }
        public string? LastReadAlertDate { get; set; }

        public string? LastReadPKID { get; set; }
        public string? ToolActionName { get; set; }
    }
    public class AddOrganizationToolActionModel
    {
        //public int OrgToolActionId { get; set; }
       // public int OrgToolId { get; set; }
        public int ToolActionId { get; set; }
        //public int ToolId { get; set; }
        //public int OrgId { get; set; }
       // public string? AuthKey { get; set; }
        public string? ApiUrl { get; set; }
       // public int? Active { get; set; }
        public string? ApiVerson { get; set; }
        public int? GetDataBatchSize { get; set; }
        //public string? LastReadAlertDate { get; set; }

       // public string? LastReadPKID { get; set; }
        //public string? ToolActionName { get; set; }
    }

    public class UpdateOrganizationToolActionModel
    {
        public int OrgToolActionId { get; set; }
        // public int OrgToolId { get; set; }
        public int ToolActionId { get; set; }
        //public int ToolId { get; set; }
        //public int OrgId { get; set; }
        // public string? AuthKey { get; set; }
        public string? ApiUrl { get; set; }
        // public int? Active { get; set; }
        public string? ApiVerson { get; set; }
        public int? GetDataBatchSize { get; set; }
        //public string? LastReadAlertDate { get; set; }

        // public string? LastReadPKID { get; set; }
        //public string? ToolActionName { get; set; }
    }


}
