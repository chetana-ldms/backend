using LDPRuleEngine.BL.Models;
using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.BL.Interfaces
{
    public interface IPlaybookdtlsBL
    {
        GetPlayBookResponse GetPlayBookdtlsByID(int ruleID);
        PlayBookResponse AddPlayBookdtl(AddPlayBookDtlsModel request);

        PlayBookResponse AddRangePlayBookdtl(List<AddPlayBookDtlsModel> request);
        PlayBookResponse UpdatePlaybookdtl(AddPlayBookDtlsModel request);
      

    }
}
