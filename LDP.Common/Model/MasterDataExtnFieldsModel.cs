using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDP.Common.Model
{
    public class MasterDataExtnFieldsModel
    {
        public int DataExtnFieldsId { get; set; }
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
