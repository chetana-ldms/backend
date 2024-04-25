using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using AutoMapper;
using LDP.Common.Requests;

namespace LDP.Common.Mappers
{
    public class AddTaskMappers:Profile
    {
        
        public AddTaskMappers()
        {
            CreateMap<AddTaskRequest, Tasks>()
            .ForMember(dest => dest.task_type, opt => opt.MapFrom(src => src.TaskType))
            .ForMember(dest => dest.task_title, opt => opt.MapFrom(src => src.TaskTitle))
            .ForMember(dest => dest.owner_Id, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.status, opt => opt.MapFrom(src => "New"))
            .ForMember(dest => dest.task_description, opt => opt.MapFrom(src => src.TaskDescription))
            .ForMember(dest => dest.task_for_user_id, opt => opt.MapFrom(src => src.TaskForUserId))
            //.ForMember(dest => dest.StartedDate, opt => opt.MapFrom(src => src.started_date))
            //.ForMember(dest => dest.ClosedDate, opt => opt.MapFrom(src => src.closed_date))
            //.ForMember(dest => dest.CreatedUserId, opt => opt.MapFrom(src => src.created_user))
            .ForMember(dest => dest.created_date, opt => opt.MapFrom(src => src.CreatedDate.Value.ToUniversalTime()))
            .ForMember(dest => dest.org_id, opt => opt.MapFrom(src => src.OrgId))
            .ReverseMap();
        }
    }
    public class UpdateTaskMapper : Profile
    {

        public UpdateTaskMapper()
        {
            CreateMap<Tasks, UpdateTasksModel>()
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.task_id))
            .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => src.task_type))
            .ForMember(dest => dest.TaskTitle, opt => opt.MapFrom(src => src.task_title))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.owner_Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
            .ForMember(dest => dest.TaskDescription, opt => opt.MapFrom(src => src.task_description))
           //  .ForMember(dest => dest.StartedDate, opt => opt.MapFrom(src => src.started_date))
           // .ForMember(dest => dest.ClosedDate, opt => opt.MapFrom(src => src.closed_date))
           // .ForMember(dest => dest.mo, opt => opt.MapFrom(src => src.modified_user))
           .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.modified_date))
            .ReverseMap();
        }
    }
    public class GetTaskMapper : Profile
    {

        public GetTaskMapper()
        {
            CreateMap<Tasks, GetTasksModel>()
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.task_id))
            .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => src.task_type))
            .ForMember(dest => dest.TaskTitle, opt => opt.MapFrom(src => src.task_title))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.owner_Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
            .ForMember(dest => dest.TaskDescription, opt => opt.MapFrom(src => src.task_description))
            .ForMember(dest => dest.StartedDate, opt => opt.MapFrom(src => src.started_date))
            .ForMember(dest => dest.ClosedDate, opt => opt.MapFrom(src => src.closed_date))
            .ForMember(dest => dest.CreadatedUser, opt => opt.MapFrom(src => src.created_user))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.created_date))
            .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.org_id))
            // .ForMember(dest => dest.mo, opt => opt.MapFrom(src => src.modified_user))
            .ForMember(dest => dest.TaskForUserId, opt => opt.MapFrom(src => src.task_for_user_id))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.modified_date))
            .ReverseMap();
        }
    }
}
