namespace LDP.Common.Model
{
    public class OrgMasterDataModel
    {
        public int OrgMasterDataId { get; set; }
        public string? DataType { get; set; }
        public int DataId { get; set; }
        public int OrgId { get; set; }
        public int OrgDataId { get; set; }
        public string? OrgDataType { get; set; }
        public string? OrgDataName { get; set; }
        public string? OrgDataValue { get; set; }
        public int Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
    }

}
