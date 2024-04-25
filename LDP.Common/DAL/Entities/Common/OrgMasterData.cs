using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDP.Common.DAL.Entities.Common
{
    public class OrgMasterData
    {
            [Key]
            public int org_masterdata_id { get; set; }
            public string? data_type { get; set; }
            public int data_id { get; set; }
            public int org_id { get; set; }
            public int org_data_id { get; set; }
            public string? org_data_type { get; set; }
            public string? org_data_name { get; set; }
            public string? org_data_value { get; set; }
            public int active { get; set; }
            public DateTime? Created_Date { get; set; }
            public DateTime? Modified_Date { get; set; }
            public string? Created_User { get; set; }
            public string? Modified_User { get; set; }
        }
}
