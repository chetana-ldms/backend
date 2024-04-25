using AutoMapper;
using LDPRuleEngine.BL.Models;
using LDPRuleEngine.DAL.Entities;

namespace LDPRuleEngine.BL.Mappers
{
    public class AddPlayBookDtlsMapper : Profile
    {
        public AddPlayBookDtlsMapper()
        {
            CreateMap<AddPlayBookDtlsModel, PlayBookDtl>()
            .ForMember(dest => dest.Play_Book_Item_Type, opt => opt.MapFrom(src => src.PlayBookItemType))
            .ForMember(dest => dest.Play_Book_Item_Type_RefID, opt => opt.MapFrom(src => src.PlayBookItemTypeRefID))
            .ForMember(dest => dest.Execution_Sequence_Number, opt => opt.MapFrom(src => src.ExecutionSequenceNumber))
             .ForMember(dest => dest.active, opt => opt.MapFrom(src =>1))
            // .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
            .ReverseMap();
        }
    }

    public class UpdatePlayBookDtlsMapper : Profile
    {
        public UpdatePlayBookDtlsMapper()
        {
            CreateMap<UpdatePlayBookDtlsModel, PlayBookDtl>()
            .ForMember(dest => dest.Play_Book_dtls_ID, opt => opt.MapFrom(src => src.PlayBookDtlsID))
            .ForMember(dest => dest.Play_Book_ID, opt => opt.MapFrom(src => src.PlayBookID))
            .ForMember(dest => dest.Play_Book_Item_Type, opt => opt.MapFrom(src => src.PlayBookItemType))
            .ForMember(dest => dest.Play_Book_Item_Type_RefID, opt => opt.MapFrom(src => src.PlayBookItemTypeRefID))
            .ForMember(dest => dest.Execution_Sequence_Number, opt => opt.MapFrom(src => src.ExecutionSequenceNumber))
            //.ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
            //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
            .ReverseMap();
        }
    }

    public class GetPlayBookDtlsMapper : Profile
    {
        public GetPlayBookDtlsMapper()
        {
            CreateMap<GetPlayBookDtlsModel, PlayBookDtl>()
             .ForMember(dest => dest.Play_Book_dtls_ID, opt => opt.MapFrom(src => src.PlayBookDtlsID))
            .ForMember(dest => dest.Play_Book_ID, opt => opt.MapFrom(src => src.PlayBookID))
            .ForMember(dest => dest.Play_Book_Item_Type, opt => opt.MapFrom(src => src.PlayBookItemType))
            .ForMember(dest => dest.Play_Book_Item_Type_RefID, opt => opt.MapFrom(src => src.PlayBookItemTypeRefID))
            .ForMember(dest => dest.Execution_Sequence_Number, opt => opt.MapFrom(src => src.ExecutionSequenceNumber))
            // .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.active))
            // .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => src.CreatedDate))
            //.ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => src.CreatedUser))
            //.ForMember(dest => dest.Modified_Date, opt => opt.MapFrom(src => src.ModifiedDate))
            //.ForMember(dest => dest.Modified_User, opt => opt.MapFrom(src => src.ModifiedUser))
            //.ForMember(dest => dest.Processed, opt => opt.MapFrom(src => src.Processed))
            .ReverseMap();

        }
    }
}
