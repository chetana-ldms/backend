namespace LDP.Common.Model
{
    public class AlertExtnFieldModel
    {
        public int AlertExtenFieldId { get; set; }
        public int AlertId { get; set; }
        public string? DataType { get; set; }
        public int DataId { get; set; }
        public string? DataFieldName { get; set; }
        public string? DataFieldValueType { get; set; }
        public string? DataFieldValue { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
    }
}
