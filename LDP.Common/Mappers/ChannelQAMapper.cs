using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.Requests;

namespace LDP.Common.Mappers
{
    public class AddChannelQuestionMapper:Profile
    {
        public AddChannelQuestionMapper()
        {
            CreateMap<AddChannelQuestionRequest, ChannelQA>()
                .ForMember(dest => dest.qa_type, opt => opt.MapFrom(src => "Q"))
                .ForMember(dest => dest.qa_description, opt => opt.MapFrom(src => src.QuestionDescription))
                .ForMember(dest => dest.qa_parent_refid, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate))
                //.ForMember(dest => dest.modified_date, opt => opt.MapFrom(src => src.ModifiedDate))
                //.ForMember(dest => dest.created_user, opt => opt.MapFrom(src => src.CreatedUser))
                //.ForMember(dest => dest.modified_user, opt => opt.MapFrom(src => src.ModifiedUser))
                .ReverseMap();
        }
    }

    public class AddChannelAnswerMapper : Profile
    {
        public AddChannelAnswerMapper()
        {
            CreateMap<AddChannelAnswerRequest, ChannelQA>()
                .ForMember(dest => dest.qa_type, opt => opt.MapFrom(src => "A"))
                .ForMember(dest => dest.qa_description, opt => opt.MapFrom(src => src.AnswerDescription))
                .ForMember(dest => dest.qa_parent_refid, opt => opt.MapFrom(src => src.ChannelQuestionId))
                .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                .ForMember(dest => dest.active, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate))
                //.ForMember(dest => dest.modified_date, opt => opt.MapFrom(src => src.ModifiedDate))
                //.ForMember(dest => dest.created_user, opt => opt.MapFrom(src => src.CreatedUser))
                //.ForMember(dest => dest.modified_user, opt => opt.MapFrom(src => src.ModifiedUser))
                .ReverseMap();
        }
    }

    public class UpdateChannelQuestionMapper : Profile
    {
        public UpdateChannelQuestionMapper()
        {
            CreateMap<UpdateChannelQuestionRequest, ChannelQA>()
                .ForMember(dest => dest.channel_qa_id, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.qa_type, opt => opt.MapFrom(src => "Q"))
                .ForMember(dest => dest.qa_description, opt => opt.MapFrom(src => src.QuestionDescription))
                .ForMember(dest => dest.qa_parent_refid, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
               // .ForMember(dest => dest.active, opt => opt.MapFrom(src => 0))
                //.ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.modified_date, opt => opt.MapFrom(src => src.ModifiedDate))
                //.ForMember(dest => dest.created_user, opt => opt.MapFrom(src => src.CreatedUser))
                //.ForMember(dest => dest.modified_user, opt => opt.MapFrom(src => src.ModifiedUser))
                .ReverseMap();
        }
    }

    public class UpdateChannelAnswerMapper : Profile
    {
        public UpdateChannelAnswerMapper()
        {
            CreateMap<UpdateChannelAnswerRequest, ChannelQA>()
                .ForMember(dest => dest.channel_qa_id, opt => opt.MapFrom(src => src.AnswerId))
                .ForMember(dest => dest.qa_type, opt => opt.MapFrom(src => "A"))
                .ForMember(dest => dest.qa_description, opt => opt.MapFrom(src => src.AnswerDescription))
                .ForMember(dest => dest.qa_parent_refid, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.channel_id, opt => opt.MapFrom(src => src.ChannelId))
                .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
                //.ForMember(dest => dest.active, opt => opt.MapFrom(src => 0))
                //.ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.modified_date, opt => opt.MapFrom(src => src.ModifiedDate))
                //.ForMember(dest => dest.created_user, opt => opt.MapFrom(src => src.CreatedUser))
                //.ForMember(dest => dest.modified_user, opt => opt.MapFrom(src => src.ModifiedUser))
                .ReverseMap();
        }
    }
}
