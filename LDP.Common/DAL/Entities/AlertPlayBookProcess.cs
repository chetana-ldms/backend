using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDP.Common.DAL.Entities
{
    public class AlertPlayBookProcess
    {
		[Key]
		public int alert_playbooks_process_id { get; set; }
		public int org_id { get; set; }
		public double alert_id { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }
	}
}
