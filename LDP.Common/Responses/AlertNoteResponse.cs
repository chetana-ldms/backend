using LDP.Common.Model;
using LDP_APIs.Models;

namespace LDP.Common.Responses
{
    public class GetAlertNoteResponse:baseResponse
    {
        public List<AlertNoteModel>? AlertNotesList { get; set; }    
    }
}
