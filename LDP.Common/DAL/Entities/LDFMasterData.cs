﻿using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{
    public class LDPMasterData
    {
		[Key]
		public int data_id { get; set; }
		public string? data_type { get; set; }
		public string? data_name { get; set; }
		//public int active { get; set; }
		public string? data_value { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public string? Created_User { get; set; }
        public string? Modified_User { get; set; }
        //public int Processed { get; set; }
    }
}
