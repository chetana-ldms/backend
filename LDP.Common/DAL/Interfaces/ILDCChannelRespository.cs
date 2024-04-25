using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDPRuleEngine.DAL.Interfaces
{
    public interface ILDCChannelRespository
    {
        Task<string> AddChannel(LDCChannel request);

        Task<string> UpdateChannel(LDCChannel request);

        Task<List<LDCChannel>> GetAllChannels(int orgId);

        Task<List<LDCChannel>> GetActiveChannels(int orgId);

        Task<List<LDCChannel>> GetPreActiveChannels(int orgId);

        Task<string> DeleteChannel(DeleteChannelRequest request, string deletedUseName);

        Task<LDCChannel> GetChannelDetails(int channelId);
        Task<LDCChannel> GetChannelDetailsByChannelName(string  channelName);

        Task<LDCChannel> GetChannelDetailsByUpdateChannelName(string channelName, int channelId);

        Task<string> AddChannelSubItem(ChannelSubItem request);

        Task<string> UpdateChannelSubItem(ChannelSubItem request);

        Task<List<ChannelSubItem>> GetChannelsubitemsByOrgChannel(GetChannelSubItemsRequest request);


        Task<ChannelSubItem> GetChannelSubItemDetails(int subItemId);

        Task<ChannelSubItem> GetChannelSubItemDetailsByName(string subItemName);

        Task<ChannelSubItem> GetChannelSubItemDetailsByUpdateName(string subItemName,int subItemId);

        Task<string> DeleteChannelSubItem(DeleteChannelSubItemRequest request, string deletedUseName);
        ///

        Task<string> AddChannelQA(ChannelQA request);

        Task<string> UpdateChannelQA(ChannelQA request);

        Task<List<GetChannelQACombinedModel>> GetChannelQuestions(GetChannelQuestionsRequest request);

        Task<GetChannelQACombinedModel> GetChannelQADetails(int channelQAId);

        Task<GetChannelAnswerDetailsModel> GetChannelAnswerDetails(int AnswerId);

        Task<string> DeleteChannelQuestion(DeleteChannelQuestionRequest request, string deletedUseName);
        Task<string> DeleteChannelAnswer(DeleteChannelAnswerRequest request, string deletedUseName);

        Task<List<MSTeam>> GetTeams(int ogrId);

        Task<MSTeam> GetTeamDetails(int teamId);

        Task<string> UpdateMsTeamsData(UpdateMsTeamsDataRequest request);

    }
}
