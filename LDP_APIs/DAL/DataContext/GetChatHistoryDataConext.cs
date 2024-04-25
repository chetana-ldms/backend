using LDP_APIs.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.DataContext
{
    public class GetChatHistoryDataConext:DbContext 
    {
        public GetChatHistoryDataConext(DbContextOptions<GetChatHistoryDataConext> options) : base(options)
        {
        }
        public DbSet<ChatHistory>? vm_ldc_chathistory { get; set; }
    }
}
