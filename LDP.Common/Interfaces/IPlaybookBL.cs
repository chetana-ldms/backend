using LDPRuleEngine.Controllers.Requests;
using LDPRuleEngine.Controllers.Responses;

namespace LDPRuleEngine.BL.Interfaces
{
    public interface IPlaybookBL
    {
        GetPlayBookResponse GetPlayBooks();
        GetPlayBookResponse GetPlayBooks(int orgId);
        GetPlayBookResponse GetPlayBookbyPlaybookID(int playbookid);
        PlayBookResponse AddPlayBook(AddPlayBookRequest request);
        PlayBookResponse UpdatePlaybook(UpdatePlayBookRequest request);

        DeletePlayBookResponse DeletePlaybook(DeletePlayBookRequest request);
        PlayBookResponse ExecutePlaybook(ExecutePlayBookRequest request);

    }
}
