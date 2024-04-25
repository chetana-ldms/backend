using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDP.Common.Model
{
    public class CommonAlertIncidentMappingDtlModel
    {
       // public int alert_incident_mapping_dtl_id { get; set; }
        public double alertid { get; set; }
        public double alertincidentmappingid { get; set; }
    }

    public class AddAlertIncidentMappingDtlModel: CommonAlertIncidentMappingDtlModel
    {
       
    }

    public class UpdateAlertIncidentMappingDtlModel : CommonAlertIncidentMappingDtlModel
    {
         public double alertincidentmappingdtlid { get; set; }
  
    }

    public class GetAlertIncidentMappingDtlModel : UpdateAlertIncidentMappingDtlModel
    {
  
    }

}
