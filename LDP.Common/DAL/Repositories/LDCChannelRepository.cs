using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP_APIs.DAL.DataContext;
using LDPRuleEngine.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LDPRuleEngine.DAL.Repositories
{
    public class LDCChannelRespository : ILDCChannelRespository
    {
        IPlayBookDtlsRepository _playbookdtlsRepo;

        private readonly ChannelsDataContext? _context;
        public LDCChannelRespository(ChannelsDataContext context)
        {
            _context = context;
        }
        public async Task<string> AddChannel(LDCChannel request)
        {
            _context.vm_channels.Add(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the channel";

        }

        public async Task<string> UpdateChannel(LDCChannel request)
        {
            var channel = await _context.vm_channels.Where(c => c.channel_id ==  request.channel_id    ).AsNoTracking().FirstOrDefaultAsync();
            if (channel == null)
            {
                return "Channel details not found ";
            }
            request.Created_date = channel.Created_date;
            request.Created_user = channel.Created_user;
            request.active = channel.active;
            request.deleted_user = channel.deleted_user;
            request.deleted_date = channel.deleted_date;

            _context.vm_channels.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the channel";
        }

        public async  Task<List<LDCChannel>> GetAllChannels(int orgId)
        {
            var res = await _context.vm_channels.Where(c => c.org_id == orgId && c.active == 1 ).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<List<LDCChannel>> GetActiveChannels(int orgId)
        {
            var res = await _context.vm_channels.Where(c =>  // c.org_id == orgId &&
            c.active == 1 ).AsNoTracking().ToListAsync();
            return res;
        }

        
        public async Task<List<LDCChannel>> GetPreActiveChannels(int orgId)
        {
            var res = await _context.vm_channels.Where(c => c.org_id == orgId && c.active == 0
            && c.deleted_user == null ).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<string> DeleteChannel(DeleteChannelRequest request, string deletedUseName)
        {
            var channel = await _context.vm_channels.Where(c => c.channel_id == request.ChannelId).AsNoTracking().FirstOrDefaultAsync();
            if (channel == null)
            {
                return "Channel details not found ";
            }
            channel.Modified_date = request.DeletedDate;
            channel.Modified_user = deletedUseName;
            channel.active = 0;
            channel.deleted_user = deletedUseName;
            channel.deleted_date = request.DeletedDate;

            _context.vm_channels.Update(channel);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the channel";
        }

        public async Task<LDCChannel> GetChannelDetails(int channelId)
        {
            var res = await _context.vm_channels.Where(c => c.channel_id == channelId).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        public async Task<LDCChannel> GetChannelDetailsByChannelName(string channelName)
        {
            var res = await _context.vm_channels.Where(c => c.channel_Name.ToLower() == channelName).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }

        public async Task<LDCChannel> GetChannelDetailsByUpdateChannelName(string channelName, int channelId)
        {
            var res = await _context.vm_channels.Where(c => c.channel_Name.ToLower() == channelName
            && c.channel_id != channelId).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        public async Task<string> AddChannelSubItem(ChannelSubItem request)
        {
            _context.vm_channel_subitems.Add(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the channel sub item";

        }

        public async Task<string> UpdateChannelSubItem(ChannelSubItem request)
        {
            var channelSubItem = await _context.vm_channel_subitems.Where(c => c.channel_subitem_id == request.channel_subitem_id).AsNoTracking().FirstOrDefaultAsync();
            if (channelSubItem == null)
            {
                return "Channel sub itme details not found ";
            }
            request.Created_date = channelSubItem.Created_date;
            request.Created_user = channelSubItem.Created_user;
            request.active = channelSubItem.active;
            request.deleted_user = channelSubItem.deleted_user;
            request.deleted_date = channelSubItem.deleted_date;

            _context.vm_channel_subitems.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the channel sub item";
        }

        
        public async Task<List<ChannelSubItem>> GetChannelsubitemsByOrgChannel(GetChannelSubItemsRequest request)
        {
            var res = await (from channel in _context.vm_channels
                            join subitem in _context.vm_channel_subitems
                            on channel.channel_id equals subitem.channel_id
                            where channel.active == 1
                            && subitem.active == 1
                            && channel.org_id == request.OrgId
                            && channel.channel_id == request.ChannelId
                            select subitem).AsNoTracking().ToListAsync();
               
            return res;
        }

        public async Task<ChannelSubItem> GetChannelSubItemDetails(int subItemId)
        {
            var res = await _context.vm_channel_subitems.Where(c => c.channel_subitem_id == subItemId).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        public async Task<ChannelSubItem> GetChannelSubItemDetailsByName(string  subItemName)
        {
            var res = await _context.vm_channel_subitems.Where(c => c.channel_subitem_Name.ToLower() == subItemName.ToLower()).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }

        public async Task<ChannelSubItem> GetChannelSubItemDetailsByUpdateName(string subItemName, int subItemId)
        {
            var res = await _context.vm_channel_subitems.Where(c => c.channel_subitem_Name.ToLower() == subItemName.ToLower()
                            && c.channel_subitem_id != subItemId).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }

        public async Task<string> DeleteChannelSubItem(DeleteChannelSubItemRequest request, string deletedUseName)
        {
            var channelSubItem = await _context.vm_channel_subitems.Where(c => c.channel_subitem_id == request.ChannelSubItemId).AsNoTracking().FirstOrDefaultAsync();
            if (channelSubItem == null)
            {
                return "Channel sub item details not found ";
            }
            channelSubItem.Modified_date = request.DeletedDate;
            channelSubItem.Modified_user = deletedUseName;
            channelSubItem.active = 0;
            channelSubItem.deleted_user = deletedUseName;
            channelSubItem.deleted_date = request.DeletedDate;

            _context.vm_channel_subitems.Update(channelSubItem);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the channel sub item";
        }

        // 
        // Channels Question and Answers 
        public async Task<string> AddChannelQA(ChannelQA request)
        {
            _context.vm_channel_qa.Add(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the channel QA";

        }

        public async Task<string> UpdateChannelQA(ChannelQA request)
        {
            var channelQA = await _context.vm_channel_qa.Where(c => c.channel_qa_id == request.channel_qa_id).AsNoTracking().FirstOrDefaultAsync();
            if (channelQA == null)
            {
                return "Channel QA  details not found ";
            }
            request.created_date = channelQA.created_date;
            request.created_user= channelQA.created_user;
            request.active = channelQA.active;
            request.deleted_user = channelQA.deleted_user;
            request.deleted_date = channelQA.deleted_date;

            _context.vm_channel_qa.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the channel QA";
        }

        public async Task<List<GetChannelQACombinedModel>> GetChannelQuestions(GetChannelQuestionsRequest request)
        {
            var results = await (
            from q in _context.vm_channel_qa
            join a in _context.vm_channel_qa on q.channel_qa_id equals a.qa_parent_refid into qaJoin
            from a in qaJoin.Where(v0 => v0.active == 1).DefaultIfEmpty()
            where q.qa_type == "Q" && (a == null || a.qa_type == "A")
            && q.channel_id == request.ChannelId
            && q.org_id == request.OrgId
            && q.active == 1 && (a == null || a.active == 1)
            orderby q.channel_qa_id
            select new GetChannelQACombinedModel
            {
                QuestionId = q.channel_qa_id,
                QuestionDescription = q.qa_description,
                AnswerId = a == null ? null : (int?)a.channel_qa_id,
                AnswerDescription = a == null ? null : a.qa_description,
                ChannelId = request.ChannelId,
                OrgId = request.OrgId
            }
            ).AsNoTracking().ToListAsync();




            return results;
        }

        
        public async Task<GetChannelQACombinedModel> GetChannelQADetails(int channelQAId)
        {

                var results = await (
                from q in _context.vm_channel_qa
                join a in _context.vm_channel_qa on q.channel_qa_id equals a.qa_parent_refid into qaJoin
                from a in qaJoin.Where(v0 => v0.active == 1).DefaultIfEmpty()
                where q.qa_type == "Q" && (a == null || a.qa_type == "A")
                && q.channel_qa_id == channelQAId
                && q.active == 1 && (a == null || a.active == 1)
                select new GetChannelQACombinedModel
                    {
                        QuestionId = q.channel_qa_id,
                        QuestionDescription = q.qa_description,
                        AnswerId = a == null ? null : (int?)a.channel_qa_id,
                        AnswerDescription = a == null ? null : a.qa_description,
                        ChannelId = q.channel_id,
                        OrgId = q.org_id
                    }
                ).AsNoTracking().FirstOrDefaultAsync();



            return results;
        }

        public async Task<GetChannelAnswerDetailsModel> GetChannelAnswerDetails(int AnswerId)
        {

            var results = await (
            from A in _context.vm_channel_qa
            where A.qa_type == "A" 
            && A.channel_qa_id == AnswerId
            && A.active == 1
            select new GetChannelAnswerDetailsModel
            {
                AnswerId = A.channel_qa_id,
                AnswerDescription = A.qa_description,
                QuestionId = A.qa_parent_refid,
                ChannelId = A.channel_id,
                OrgId = A.org_id
            }
            ).AsNoTracking().FirstOrDefaultAsync();



            return results;
        }

        public async Task<string> DeleteChannelQuestion(DeleteChannelQuestionRequest request, string deletedUseName)
        {
            var channelQ = await _context.vm_channel_qa.Where(c => c.channel_qa_id == request.QuestionId).AsNoTracking().FirstOrDefaultAsync();
            if (channelQ == null)
            {
                return "Channel sub item details not found ";
            }

            channelQ.modified_date = request.DeletedDate;
            channelQ.modified_user = deletedUseName;
            channelQ.active = 0;
            channelQ.deleted_user = deletedUseName;
            channelQ.deleted_date = request.DeletedDate;

            _context.vm_channel_qa.Update(channelQ);

            var channelA = await _context.vm_channel_qa.Where(c => c.qa_parent_refid == request.QuestionId
            && c.active == 1 ).AsNoTracking().FirstOrDefaultAsync();
            if (channelA != null)
            {
                channelA.modified_date = request.DeletedDate;
                channelA.modified_user = deletedUseName;
                channelA.active = 0;
                channelA.deleted_user = deletedUseName;
                channelA.deleted_date = request.DeletedDate;

                _context.vm_channel_qa.Update(channelA);
            }
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the channel QA";
        }

        public async Task<string> DeleteChannelAnswer(DeleteChannelAnswerRequest request, string deletedUseName)
        {
            var channelQ = await _context.vm_channel_qa.Where(c => c.channel_qa_id == request.AnswerId).AsNoTracking().FirstOrDefaultAsync();
            if (channelQ == null)
            {
                return "Channel answer not found ";
            }
            channelQ.modified_date = request.DeletedDate;
            channelQ.modified_user = deletedUseName;
            channelQ.active = 0;
            channelQ.deleted_user = deletedUseName;
            channelQ.deleted_date = request.DeletedDate;

            _context.vm_channel_qa.Update(channelQ);

                    var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the channel QA";
        }

        

        public async  Task<List<MSTeam>> GetTeams(int ogrId)
        {
            var res = await _context.vm_ms_teams.Where(t => t.org_id == ogrId && t.active == 1).AsNoTracking().ToListAsync();
            return res;
        }
        public async Task<MSTeam> GetTeamDetails(int teamId)
        {
            var res = await _context.vm_ms_teams.Where(t => t.team_id == teamId && t.active == 1).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        public async Task<string> UpdateMsTeamsData(UpdateMsTeamsDataRequest request)
        {
            var channel = await _context.vm_channels.Where(c => c.channel_id == request.ChannelId).AsNoTracking().FirstOrDefaultAsync();
            if (channel == null)
            {
                return "Channel  details not found ";
            }
            channel.msteams_channelid = request.MsTeamsChannelId;
            channel.msteams_teamsid = request.MsTeamsId;
            _context.vm_channels.Update(channel);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the channel data";
        }
    }

    
}
