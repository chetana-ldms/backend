using LDP_APIs.BL.APIRequests;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.Respository
{
    public class ChatHistoryRepository : IChatHistoryRepository
    {
        private readonly GetChatHistoryDataConext? _context;
        public ChatHistoryRepository(GetChatHistoryDataConext context)
        {
            _context = context;
        }
        public async Task<List<ChatHistory>> GetChatHistory(GetChatHistoryRequest request)
        
        {
               return await _context.vm_ldc_chathistory.Where(c => c.org_id == request.OrgId
                                && c.chat_subject == request.Subject
                                && c.subject_refid == request.SubjectRefId).AsNoTracking().ToListAsync();
        }
        public async Task<string> AddChatMessages(List<ChatHistory> request)
        {
            _context.vm_ldc_chathistory.AddRange(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add chat messages";
        }

     
        public async Task<string> AddChatMessage(ChatHistory request)
        {
            _context.vm_ldc_chathistory.Add(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add chat message";
        }

        
        public async Task<ChatHistory> GetChatHistoryDetails(int chatId)

        {
            return await _context.vm_ldc_chathistory.Where(c => c.chat_id == chatId).AsNoTracking().FirstOrDefaultAsync();
                            
        }

    }
}
