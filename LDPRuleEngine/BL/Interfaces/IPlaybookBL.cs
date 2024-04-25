using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;

namespace LDPRuleEngine.BL.Interfaces
{
    public interface IPlaybookBL
    {
        GetPlayBookResponse GetPlayBooks();

        GetPlayBookResponse GetPlayBookbyPlaybookID(int playbookid);
        PlayBookResponse AddPlayBook(AddPlayBookRequest request);
        PlayBookResponse UpdatePlaybook(UpdatePlayBookRequest request);
        PlayBookResponse ExecutePlaybook(ExecutePlayBookRequest request);

    }
}
