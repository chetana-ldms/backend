using LDPRuleEngine.BL.Framework.Actions;
using LDPRuleEngine.BL.Models;

namespace LDPRuleEngine.Controllers.Requests
{
    public class AddPlayBookRequest:AddPlayBookModel
    {
        public List<AddPlayBookDtlsModel> PlaybookDtls { get; set; }
    }

    public class UpdatePlayBookRequest : UpdatePlayBookModel
    {
        public int ModifiedUserId { get; set; }
        public List<UpdatePlayBookDtlsModel> PlaybookDtls { get; set; }
    }

    public class ExecutePlayBookRequest : baseRuleActionRequest
    {
        public int PlaybookID { get; set; }
    }

    public class DeletePlayBookRequest : DeletePlayBookModel
    {
       
    }
}
