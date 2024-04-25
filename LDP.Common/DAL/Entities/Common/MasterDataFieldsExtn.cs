﻿using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities.Common
{
    public class MasterDataExtnFields
    {
        [Key]
        public int data_extn_fields_id { get; set; }
        public string? data_type { get; set; }
        public int data_id { get; set; }
        public string? data_field_name { get; set; }
        public string? data_field_value_type { get; set; }
        public string? data_field_value { get; set; }
        public int active { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public string? Created_User { get; set; }
        public string? Modified_User { get; set; }
    }
}
