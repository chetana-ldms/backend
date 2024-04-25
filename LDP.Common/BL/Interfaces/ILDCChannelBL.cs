using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface ILDCChannelBL
    {
            ChannelResponse AddChannel(AddChannelRequest request);
            ChannelResponse UpdateChannel(UpdateChannelRequest request);
            GetChannelsResponse GetAllChannels(int orgId);
           GetChannelsResponse GetActiveChannels(int orgId);

            GetChannelsResponse GetPreActiveChannels(int orgId);

            ChannelResponse DeleteChannel(DeleteChannelRequest request);

            GetChannelResponse GetChannelDetails(int channelId);

            ChannelSubItemsResponse AddChannelSubItem(AddChannelSubItemRequest request);

            ChannelSubItemsResponse UpdateChannelSubItem(UpdateChannelSubItemRequest request);

            GetChannelSubItemsResponse GetChannelsubitemsByOrgChannel(GetChannelSubItemsRequest request);

            //GetChannelSubItemsResponse GetChannelsubitemsByOrgId(int orgId);

            GetChannelSubItemResponse GetChannelSubitemDetails(int subItemId);

            ChannelSubItemsResponse DeleteChannelSubItem(DeleteChannelSubItemRequest request);

        //Channel Questions and Answers

            ChannelQAResponse AddChannelQuestion(AddChannelQuestionRequest request);

             ChannelQAResponse  AddChannelAnswer(AddChannelAnswerRequest request);

            ChannelQAResponse UpdateChannelQuestion(UpdateChannelQuestionRequest request);

            ChannelQAResponse UpdateChannelAnswer(UpdateChannelAnswerRequest request);
            GetChannelsQAResponse GetChannelQuestions(GetChannelQuestionsRequest request);

            //GetChannelsQAResponse GetChannelQAByOrgId(int orgId);

            GetChannelQAResponse GetChannelQuestionDetails(int QuestionId);

        GetChannelAnswerResponse GetChannelAnswerDetails(int AnswerId);

        //GetChannelQAResponse GetChannelAnswerDetails(int AnswerId);

        ChannelQAResponse DeleteChannelQuestion(DeleteChannelQuestionRequest request);
        ChannelQAResponse DeleteChannelAnswer(DeleteChannelAnswerRequest request);

        GetTeamsResponse GetTeamList(int orgId);

        GetTeamResponse GetTeamDetails(int teamsId);

        TeamResponse UpdateMsTeamsData(UpdateMsTeamsDataRequest request);


    }
}
