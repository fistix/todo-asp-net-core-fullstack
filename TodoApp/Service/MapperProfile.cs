using AutoMapper;
using Fistix.Training.Domain.Commands;
using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.Domain.DataModels;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Service
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Todo, TodoDto>()
              .ForMember(x => x.Id, v => v.MapFrom(z => z.Id.ToString()));
            CreateMap<TodoDto, Todo>()
              .ForMember(x => x.Id, v => v.MapFrom(z => Guid.Parse(z.Id)));

            CreateMap<AddTodoCommand, Todo>();
            CreateMap<Todo, AddTodoCommand>();

            CreateMap<UpdateTodoCommand, Todo>();
            CreateMap<Todo, UpdateTodoCommand>();

            CreateMap<UpdateTodoCommand, TodoDto>();
            CreateMap<TodoDto, UpdateTodoCommand>();

            CreateMap<AddTodoCommand, TodoDto>();
            CreateMap<TodoDto, AddTodoCommand>();

            CreateMap<CreateTaskCommand, Task>();
            CreateMap<Task, CreateTaskCommand>();

            CreateMap<Domain.DataModels.Task, Domain.Dtos.TaskDto>()
            .ForMember(x => x.Id, v => v.MapFrom(z => z.Id.ToString()));
            CreateMap<Domain.Dtos.TaskDto, Domain.DataModels.Task>()
                .ForMember(x => x.Id, v => v.MapFrom(z => Guid.Parse(z.Id)));



            CreateMap<Domain.DataModels.Task, CreateTaskCommandResult>();
            //.ForMember(x => x.Payload.Id, v => v.MapFrom(z => z.Id.ToString()));
            CreateMap<CreateTaskCommandResult, Domain.DataModels.Task>();
            //.ForMember(x => x.Id, v => v.MapFrom(z => Guid.Parse(z.Payload.Id)));

            CreateMap<CreateTaskCommand, CreateTaskCommandResult>();
            //.ForMember(x => x.Payload.Id, v => v.MapFrom(z => z.Id.ToString()));
            CreateMap<CreateTaskCommandResult, CreateTaskCommand>();
            //.ForMember(x => x.Id, v => v.MapFrom(z => Guid.Parse(z.Payload.Id)));


            CreateMap<CreateTaskCommand, TaskDto>();
            CreateMap<TaskDto, CreateTaskCommand>();

            CreateMap<TaskDto, GetAllTasksQueryResult>();
            CreateMap<GetAllTasksQueryResult, TaskDto>();

            
        }
    }
}
