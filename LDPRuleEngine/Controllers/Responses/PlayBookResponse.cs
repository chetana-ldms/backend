using LDP_APIs.Models;
using LDPRuleEngine.BL.Models;

namespace LDPRuleEngine.Controllers.Responses
{
    public class PlayBookResponse:baseResponse
    {
    }

    public class GetPlayBookResponse : baseResponse
    {
        public List<GetPlayBookModel>? Playbooks { get; set; }
    }
}
