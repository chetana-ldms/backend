using LDP.Common.Model;

namespace LDP.Common.Requests
{
    public class AddChannelQARequest:AddChannelQAModel
    {
    }

    public class UpdateChannelQARequest : UpdateChannelQAModel
    {
    }

    public class DeleteChannelQuestionRequest : DeleteChannelQuestionModel
    {
    }

    public class DeleteChannelAnswerRequest : DeleteChannelAnswerModel
    {
    }

    public class AddChannelQuestionRequest : AddChannelQuestionModel
    {
    }
    public class AddChannelAnswerRequest : AddChannelAnswerModel
    {
    }

    public class UpdateChannelQuestionRequest : UpdateChannelQuestionModel
    {
    }
    public class UpdateChannelAnswerRequest : UpdateChannelAnswerModel
    {
    }

    public class GetChannelQuestionsRequest 
    {
        public int ChannelId { get; set; }
        public int OrgId { get; set; }
    }
}
