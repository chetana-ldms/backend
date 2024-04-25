using LDP.Common.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.DataContext
{
    public class ChannelsDataContext : DbContext 
    {
        public ChannelsDataContext(DbContextOptions<ChannelsDataContext> options) : base(options)
        {
        }
        public DbSet<LDCChannel>? vm_channels { get; set; }
        public DbSet<ChannelSubItem>? vm_channel_subitems { get; set; }

        public DbSet<ChannelQA>? vm_channel_qa { get; set; }

        public DbSet<ChannelUploadFile>? vm_channel_upload_files { get; set; }
        public DbSet<MSTeam>? vm_ms_teams { get; set; }

    }
}
