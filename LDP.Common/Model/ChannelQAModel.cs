namespace LDP.Common.Model
{
 
    public class ChannelQABase
    {
        public int ChannelId { get; set; }
        public int OrgId { get; set; }
    }
    public class CommonChannelQAModel: ChannelQABase
    {
       
        public string? QAType { get; set; }
     //   public string? QADescription { get; set; }
        public int QAParentRefId { get; set; }
      
    }

    public class AddChannelQACommonModel: ChannelQABase
    {
       
        public DateTime? CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
    }

    public class UpdateChannelQACommonModel : ChannelQABase
    {

        public DateTime? ModifiedDate { get; set; }
        public int ModifiedUserId { get; set; }
    }
    public class AddChannelQAModel : AddChannelQACommonModel
    {

        // public DateTime? CreatedDate { get; set; }
        //public int CreatedUserId { get; set; }
        public string? QuestionDescription { get; set; }
        public string? AnswerDescription { get; set; }
    }

    public class AddChannelQuestionModel : AddChannelQACommonModel
    {

        // public DateTime? CreatedDate { get; set; }
        public string? QuestionDescription { get; set; }
    }
    public class UpdateChannelQuestionModel : UpdateChannelQACommonModel
    {

        public int QuestionId { get; set; }
        public string? QuestionDescription { get; set; }
    }

    public class UpdateChannelAnswerModel : UpdateChannelQACommonModel
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string? AnswerDescription { get; set; }
    }

    public class AddChannelAnswerModel : AddChannelQACommonModel
    {

        // public DateTime? CreatedDate { get; set; }
        public int ChannelQuestionId { get; set; }
        public string? AnswerDescription { get; set; }
    }
    public class UpdateChannelQAModel:CommonChannelQAModel
    {
        public int ChannelQAId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ModifiedUserId { get; set; }
    }
    public class GetChannelQAModel: CommonChannelQAModel
    {
        public int ChannelQAId { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
    }

    public class DeleteChannelQuestionModel
    {
        public int QuestionId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedUserId { get; set; }
    }

    public class DeleteChannelAnswerModel
    {
        public int AnswerId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedUserId { get; set; }
    }
    public class GetChannelQACombinedModel
    {
        public int QuestionId { get; set; }

        public string QuestionDescription { get; set; }

        public int? AnswerId { get; set; }

        public string? AnswerDescription { get; set; }

        public int ChannelId { get; set; }

        public int OrgId { get; set; }

    }

    public class GetChannelAnswerDetailsModel
    {
        public int? AnswerId { get; set; }


        public string AnswerDescription { get; set; }

        public int QuestionId { get; set; }


        public int ChannelId { get; set; }

        public int OrgId { get; set; }

    }
}
